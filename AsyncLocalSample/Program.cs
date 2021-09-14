using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLocalSample
{
    /// <summary>
    /// AsyncLocal har bir patok uchun alohida value "opyuradi"
    /// </summary>
    class Program
    {
        private static AsyncLocal<int> AsyncLocalVar = new AsyncLocal<int>();
        private static int SimpleVar;
        static void Main(string[] args)
        {
            Console.WriteLine("Testing AsyncLocal, Genesis inovations, 2021, September");
            AsyncLocalVar.Value = 125;
            var testingTasks = new List<Task>();
            for (var i = 1; i <= 10; i++)
            {
                var tmp = i;
                var task = new Task(() => ChangeCheckValue(tmp));
                testingTasks.Add(task);
            }

            testingTasks.ForEach(t => t.Start());

            Task.WaitAll(testingTasks.ToArray());

            Console.WriteLine("All tests finished)");
            Console.WriteLine($"AsyncLocalVar:{AsyncLocalVar.Value}");
            Console.WriteLine($"SimpleVar:{SimpleVar}");
            Console.ReadLine();
        }
        private static void ChangeCheckValue(int v)
        {
            AsyncLocalVar.Value = v;
            SimpleVar = v;
            Console.WriteLine($"Task{v} started.... AsyncLocalValue:{AsyncLocalVar.Value}, SimpleVar:{v}");

            Thread.Sleep(1000 - 10 * v);
        }

    }
}
