

namespace CoWorker.Logging.Tests
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
        }
    }

    public class TaskBuilder
    {
        public TaskBuilder()
        {

        }

        async Task<T> Next<T>(Func<T> func)
        {
            return await Task.Run(func);
        }
    }
}
