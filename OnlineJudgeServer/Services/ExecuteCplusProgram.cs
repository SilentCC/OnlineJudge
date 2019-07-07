using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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
                var s = "";
                var str = "";
                while ((s = reader.ReadLine()) != null)
                {
                    if (s.Equals("康"))
                    {
                        result.Add(str);
                        str = "";
                        continue;
                    }

                    str += s + "\n";
                }
            }

            return result;
        }

        public JudgeDataMode GetMode(string file)
        {
            var mode = new JudgeDataMode();
            using (var reader = new StreamReader(file, System.Text.Encoding.Default))
            {
                var s = "";
                var i = 0;
                while ((s = reader.ReadLine()) != null)
                {
                    if (i == 0)
                        mode.FixOrderIndex = int.Parse(s);
                    else if (i == 1)
                        mode.RandomOrderIndex = int.Parse(s);
                    else if (i == 2)
                        mode.RandomRandomOrder = bool.Parse(s);
                    else
                    {
                        mode.RandomFixOrder = bool.Parse(s);
                    }

                    i++;
                }
            }

            return mode;
        }

        public JudgeStatus Execute(Submit submit, double memoryLimit, int timeLimit)
        {
            var inputData = GetData($"{submit.ProblemId}.input");
            var outputData = GetData($"{submit.ProblemId}.output");

            var judgeMode = GetMode($"{submit.ProblemId}.mode");

            var file = $"{submit.UserId}to{submit.ProblemId}.{submit.CodeSuffix}";
            var executeObj = $"{submit.UserId}to{submit.ProblemId}";

            using (var writer = new StreamWriter(file))
            {
                writer.WriteLine(submit.SourceCode);
                writer.Flush();
            }

            try
            {
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
            }
            catch (Exception e)
            {
                return JudgeStatus.CompileError;
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
                    output = "康";
                    input = "";

                    var task = Task.Run(async () =>
                    {
                        await streamWriter.WriteAsync(inputData[i]);
                        output = await streamReader.ReadToEndAsync();
                    });

                    var isCompletedSuccessfully = task.Wait(timeLimit);

                    if (!isCompletedSuccessfully || (i != 0 && output == ""))
                    {
                        status = JudgeStatus.TimeLimitExceed;
                        break;
                    }

                    if (!JudgeData(output, outputData[i], judgeMode))
                    {
                        status = JudgeStatus.WrongAnswer;
                        break;
                    }

                    /*var memory = process.VirtualMemorySize64;

                    if (memory >= memoryLimit * 1024 * 1024)
                    {
                        status = JudgeStatus.MemoryLimitExceed;
                        break;
                    }*/

                    process.Close();
                }
            }

            File.Delete(file);
            File.Delete(executeObj);

            return status;
        }

        public bool JudgeData(string a, string b, JudgeDataMode mode)
        {
            var ainfo = a.Split("\n");
            var binfo = b.Split("\n");

            if (mode.FixOrderIndex == -1)
            {
                return JudgeFixOrder(ainfo, binfo);
            }

            var result = true;
            if (mode.RandomOrderIndex == -1)
            {
                result = JudgeFixOrder(ainfo.Take(mode.FixOrderIndex + 1).ToArray(),
                    binfo.Take(mode.FixOrderIndex + 1).ToArray());

                if (!result)
                    return result;

                result = JudgeRandomOrder(ainfo.TakeLast(ainfo.Length - mode.FixOrderIndex - 1).ToArray(),
                    binfo.TakeLast(binfo.Length - mode.FixOrderIndex - 1).ToArray(), mode.RandomRandomOrder);
            }

            return result;
        }

        public bool JudgeFixOrder(string[] a, string[] b)
        {
            if (a.Length != b.Length)
                return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i]))
                    return false;
            }

            return true;
        }

        public bool JudgeRandomOrder(string[] a, string[] b, bool random)
        {
            if (a.Length != b.Length)
                return false;

            var dic1 = new Dictionary<string, int>();
            var dic2 = new Dictionary<string, int>();
            if (random)
            {
                dic1 = RandomDictionary(b);
                dic2 = RandomDictionary(b);
            }
            else
            {
                dic1 = FixDictionary(b);
                dic2 = FixDictionary(b);
            }

            foreach (var i in dic1)
            {
                if (!dic2.ContainsKey(i.Key))
                    return false;
                if (dic2[i.Key] == 0)
                    return false;
                dic2[i.Key]--;
            }

            return true;
        }

        public Dictionary<string, int> FixDictionary(string[] b)
        {
            var dic = new Dictionary<string, int>();
            for (int i = 0; i < b.Length; i++)
            {
                dic.Add(b[i], 1);
            }

            return dic;
        }

        public Dictionary<string, int> RandomDictionary(string[] b)
        {
            var dic = new Dictionary<string, int>();
            for (int i = 0; i < b.Length; i++)
            {
                var binfo = b[i].Split(" ");
                binfo.ToList().Sort();

                var str = "";
                foreach (var s in binfo)
                {
                    str += s;
                }

                if (!dic.ContainsKey(str))
                    dic.Add(str, 1);
                else
                    dic[str]++;
            }

            return dic;
        }
    }

    public class JudgeDataMode
    {
        //从第0个到第FixOrderIndex 都是固定顺序
        public int FixOrderIndex;

        //从第FixOrderIndex+1 到第RandomOrderIndex 都是随机顺序,RandomOrderIndex =-1 表示从全部
        public int RandomOrderIndex;

        //FixOrderIndex+1 到 RandomOrderIndex是否可以随机？
        public bool RandomRandomOrder;

        //0到FixOrderIndex 是否可以随机？
        public bool RandomFixOrder;
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