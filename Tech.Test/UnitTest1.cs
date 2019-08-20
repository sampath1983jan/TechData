using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tech.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var parent = Task.Factory.StartNew(() => {
                Console.WriteLine("Parent task beginning.");
                for (int ctr = 0; ctr < 10; ctr++)
                {
                    int taskNo = ctr;
                    Task.Factory.StartNew((x) => {
                        Thread.SpinWait(5000000);
                        Console.WriteLine("Attached child #{0} completed.",
                                          x);
                    },
                                          taskNo, TaskCreationOptions.AttachedToParent);
                }
            });

            parent.Wait();
            Console.WriteLine("Parent task completed.");
        }
    }
}
