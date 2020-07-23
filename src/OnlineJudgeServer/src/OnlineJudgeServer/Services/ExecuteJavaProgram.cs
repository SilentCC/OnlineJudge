using System;
using System.Diagnostics;
using System.IO;
using OnlineJudgeServer.Models;
using OnlineJudgeServer.Settings;
using Microsoft.Extensions.Options;

namespace OnlineJudgeServer.Services
{
    public class ExecuteJavaProgram : IExecuteProgram
    {
        private readonly string _javaConsoleProgramPath ;
        private readonly string _javaConsoleTargePath;

        public ExecuteJavaProgram(IOptions<OnlineJudgeServerSettings> options)
        {
            this._javaConsoleProgramPath = options.Value.JavaConsoleProgramPath;
            this._javaConsoleTargePath = options.Value.JavaConsoleTargetPath;
            
        }
        public string Complie(Submit submit)
        {
            using (var writer = new StreamWriter(this._javaConsoleProgramPath))
            {
                writer.WriteLine(submit.SourceCode);
                writer.Flush();
            }

            try
            {
                var compileStr = $"javac {this._javaConsoleProgramPath}";

                var str = compileStr.Bash();

                if (str.Contains("error", StringComparison.OrdinalIgnoreCase))
                {
                    return JudgeStatus.CompileError.ToString();
                }

                if (!File.Exists($"{this._javaConsoleTargePath}.class"))
                {
                    return JudgeStatus.CompileError.ToString();
                }

                if (str=="")
                {
                    if (File.Exists($"{this._javaConsoleTargePath}.class"))
                    {
                        return this._javaConsoleTargePath;
                    }
                }

                return JudgeStatus.RuntimeError.ToString();
            }
            catch (Exception e)
            {
                return JudgeStatus.CompileError.ToString();
            }
        }

        public ProcessStartInfo GetProcessStartInfo(string file)
        {
            var processInfo = new ProcessStartInfo();
            processInfo.FileName = $"java";
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardInput = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            
            processInfo.Arguments = $"{file}";

            return processInfo;
        }

    }
}