using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

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
        [SetUp]
        public void Init() {
            
        }

        [TestCase(-1,5)]
        [TestCase(0,3)]
        [TestCase(1,2)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value,int val2)
        {
               var result = IsPrime(value);
           // throw new Exception("");
          NUnit.Framework.Assert.IsTrue(result, $"{value} should not be prime");
        
        }

        public bool IsPrime(int candidate)
        {
            if (candidate >= 0)
            {
                return true;
            }
            return false;
        }

    }
}
