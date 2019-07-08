using System.Diagnostics;
using OnlineJudgeServer.Models;

namespace OnlineJudgeServer.Services
{
    public interface IExecuteProgram
    {
        string Complie(Submit submit);
        ProcessStartInfo GetProcessStartInfo(string file);
    }
}