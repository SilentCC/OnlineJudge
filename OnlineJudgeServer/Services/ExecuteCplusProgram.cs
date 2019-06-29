using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public JudgeStatus Execute(Submit submit)
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

            int i = 0;
            foreach (var x in inputData)
            {
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

                    string input = "";

                    streamWriter.Write(x);
                    output = streamReader.ReadToEnd();
                    if (!output.Equals(outputData[i]))
                    {
                        return JudgeStatus.WrongAnswer;
                    }
                }

                i++;
            }

            File.Delete(file);
            File.Delete(executeObj);

            return JudgeStatus.Accept;
        }
    }

    public enum JudgeStatus
    {
        Pending = 0,
        WrongAnswer = 1,
        Accept = 2,
        RuntimeError = 3,
        CompileError = 4,
        PresentationError = 5
    }
}