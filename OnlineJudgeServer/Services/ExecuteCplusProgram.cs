using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Operations;
using OnlineJudgeServer.Models;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace OnlineJudgeServer.Services
{
    public class ExecuteCplusProgram
    {
        public List<string> GetData(string file)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(file, System.Text.Encoding.Default))
            {
                string s = "";
                while ((s = reader.ReadLine()) != null)
                {
                    if (s.Equals("康")) continue;
                    result.Add(s + "\n");
                }
            }

            return result;
        }

        public JudgeStatus Execute(Submit submit, double memoryLimit, int timeLimit)
        {
            var inputData = GetData($"{submit.ProblemId}.input");
            var outputData = GetData($"{submit.ProblemId}.output");

            var file = $"{submit.UserId}to{submit.ProblemId}.{submit.CodeSuffix}";
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

            
            var status = JudgeStatus.Accept;
            using (var process = new Process())
            {
                process.StartInfo.FileName = $"./{executeObj}";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                
                var output = "";
                var input = "";
                
                var stopWatch = Stopwatch.StartNew();
                for (int i = 0; i < inputData.Count; i++) 
                {         
                    process.Start();
                    var streamWriter = process.StandardInput;
                    var streamReader = process.StandardOutput;
                    output = "";
                    input = "";

                    var task = Task.Run(() =>
                    {
                        streamWriter.Write(inputData[i]);
                        output = streamReader.ReadToEnd();
                    });

                    var isCompletedSuccessfully = task.Wait(timeLimit);

                    if (!isCompletedSuccessfully)
                    {
                        status = JudgeStatus.TimeLimitExceed;
                        break;
                    }
                   
                    if (!output.Equals(outputData[i]))
                    {
                        status = JudgeStatus.WrongAnswer;
                        break;
                    }

                    var memory = process.PrivateMemorySize64;

                    if (memory >= memoryLimit)
                    {
                        status = JudgeStatus.MemoryLimitExceed;
                        break;
                    }
                           
                    process.Close();
                }
                
            }

            File.Delete(file);
            File.Delete(executeObj);

            return status;
        }
    }

    public enum JudgeStatus
    {
        Pending = 0,
        WrongAnswer = 1,
        Accept = 2,
        RuntimeError = 3,
        CompileError = 4,
        PresentationError = 5,
        MemoryLimitExceed = 6,
        TimeLimitExceed = 7
    }
}