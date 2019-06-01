using OnlineJudgeApi.Services;

namespace OnlineJudgeApi.Entity
{
    public class Submit
    {
        public string SourceCode;
        public int ProblemId;
        public int UserId;
        public Compile Translater;
    }

    public enum JudgeStatus
    {
        Pending,
        WrongAnswer,
        Accept,
        RuntimeError,
        CompileError,
        PresentationError
    }

    public enum Compile
    {
        c,
        cpp
    }
}