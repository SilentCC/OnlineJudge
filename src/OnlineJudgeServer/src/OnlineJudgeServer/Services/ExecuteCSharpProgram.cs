using System;
using System.Diagnostics;
using System.IO;
using OnlineJudgeServer.Models;
using OnlineJudgeServer.Settings;
using Microsoft.Extensions.Options;

namespace OnlineJudgeServer.Services
{
    public class ExecuteCSharpProgram : IExecuteProgram
    {
        private readonly string _dotNetConsoleProgramPath ;
        private readonly string _dotNetConsoleCsprojPath;
        private readonly string _dotNetConsoleDLLPath;

        public ExecuteCSharpProgram(IOptions<OnlineJudgeServerSettings> options)
        {
            _dotNetConsoleProgramPath = options.Value.DotNetConsoleProgramPath;
            _dotNetConsoleCsprojPath = options.Value.DotNetConsoleCsprojPath;
            _dotNetConsoleDLLPath = options.Value.DotNetConsoleDLLPath;
        }
        public string Complie(Submit submit)
        {
            using (var writer = new StreamWriter(_dotNetConsoleProgramPath))
            {
                writer.WriteLine(submit.SourceCode);
                writer.Flush();
            }

            try
            {
                var compileStr = $"dotnet build {_dotNetConsoleCsprojPath}";

                var str = compileStr.Bash();

                if (str.Contains("error", StringComparison.OrdinalIgnoreCase))
                {
                    return JudgeStatus.CompileError.ToString();
                }

                if (!File.Exists(_dotNetConsoleDLLPath))
                {
                    return JudgeStatus.CompileError.ToString();
                }

                if (str=="")
                {
                    if (File.Exists(_dotNetConsoleDLLPath))
                    {
                        return _dotNetConsoleDLLPath;
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
            processInfo.FileName = $"dotnet";
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardInput = true;
            processInfo.RedirectStandardOutput = true;
            processInfo.RedirectStandardError = true;
            
            processInfo.Arguments = $"{file}";

            return processInfo;
        }

    }
}