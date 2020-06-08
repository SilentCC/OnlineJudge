using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.Operations;
using OnlineJudgeServer.Models;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace OnlineJudgeServer.Services
{
    public class ExecuteCplusProgram : IExecuteProgram
    {
        public string Complie(Submit submit)
        {
            var file = $"{submit.UserId}to{submit.ProblemId}.{submit.CodeSuffix}";
            var executeObj = $"{submit.UserId}to{submit.ProblemId}";

            using (var writer = new StreamWriter(file))
            {
                writer.WriteLine(submit.SourceCode);
                writer.Flush();
            }

            try
            {
                var compileStr = $"g++ {file} -o {executeObj} -std=c++11";

                var str = compileStr.Bash();

                if (!File.Exists(executeObj))
                {
                    return JudgeStatus.CompileError.ToString();
                }

                File.Delete(file);

                if (str.Contains("error",StringComparison.OrdinalIgnoreCase))
                {
                    return JudgeStatus.CompileError.ToString();
                }

                return executeObj;
            }
            catch (Exception e)
            {
                return JudgeStatus.CompileError.ToString();
            }
            return executeObj;
        }

        public ProcessStartInfo GetProcessStartInfo(string file)
        {
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = $"./{file}";
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardInput = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;

            return processInfo;
        }
    }
}