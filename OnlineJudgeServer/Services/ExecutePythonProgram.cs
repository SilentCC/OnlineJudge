using System;
using System.Diagnostics;
using System.IO;
using OnlineJudgeServer.Models;
using Remotion.Linq.Utilities;

namespace OnlineJudgeServer.Services
{
    public class ExecutePythonProgram : IExecuteProgram
    {
        public string Complie(Submit submit)
        {
            var file = $"{submit.UserId}to{submit.ProblemId}.{submit.CodeSuffix}";
            using (var writer = new StreamWriter(file))
            {
                writer.WriteLine(submit.SourceCode);
                writer.Flush();
            }

            var compileStr = $"python3 -m py_compile {file}";

            var str = compileStr.Bash();

            if (!File.Exists($"{submit.UserId}to{submit.ProblemId}.pyc"))
            {
                return JudgeStatus.CompileError.ToString();
            }
            
            File.Delete($"{submit.UserId}to{submit.ProblemId}.pyc");

            if (str == null)
                return file;

            if (str.Contains("SyntaxError"))
            {
                return JudgeStatus.CompileError.ToString();
            }
            return file;
        }

        public ProcessStartInfo GetProcessStartInfo(string file)
        {
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = $"python3";
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardInput = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;

            processInfo.Arguments = $"{file}";

            return processInfo;
        }
    }
}