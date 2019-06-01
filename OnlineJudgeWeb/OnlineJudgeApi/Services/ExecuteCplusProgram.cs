using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using OnlineJudgeApi.Entity;

namespace OnlineJudgeApi.Services
{
    public class ExecuteCplusProgram
    {
        public JudgeStatus Execute(Submit submit,List<string> inputData,List<string> outputData)
        {
            var file = $"{submit.UserId}to{submit.ProblemId}.{submit.Translater}";
            var executeObj = $"{submit.UserId}to{submit.ProblemId}";
            
            using (var writer = new StreamWriter(file))
            {
                writer.WriteLine(submit.SourceCode);
                writer.Flush();
            }

            var compileStr = $"g++ {file} -o {executeObj}";

            compileStr.Bash();

            if (!File.Exists(executeObj))
            {
                return JudgeStatus.CompileError;
            }
            else
            {
                Console.WriteLine("编译成功");
            }

            using (var process = new Process())
            {
                process.StartInfo.FileName = $"./{executeObj}";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();
                
 

                StreamWriter streamWriter = process.StandardInput;
                StreamReader streamReader = process.StandardOutput;

                string output = "";
                int i = 0;
                string input = "";
                foreach (var data in inputData)
                {
                    //input += data;
                    //input += "\n";
                    streamWriter.WriteLine(data);
                    streamWriter.Flush();
                    output = streamReader.ReadLine();
                   
                }

                
                
              
                
                
                if (!output.Equals(outputData[i]))
                {
                    return JudgeStatus.WrongAnswer;
                }
            }

            return JudgeStatus.Accept;
        }
    }
}