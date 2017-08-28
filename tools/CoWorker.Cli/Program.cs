using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;
using System;
using CommandLine;
namespace CoWorker.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new Test();
            Parser.Default.ParseArguments(args, typeof(Test))
                .MapResult(x => new Test(),x => default);
                
        }
    }

    public class Test
    {
        [Option()]
        public string TestA { get; set; }
    }
}
