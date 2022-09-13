using System;
using System.Threading;

namespace QLESS_Transport_Card
{
    public class DeadlockApp
    {
        public void Run()
        {
            Console.WriteLine("***DeadlockApp start...");
            Thread thread1 = new Thread(PerformTaskA);
            Thread thread2 = new Thread(PerformTaskB);

            Console.WriteLine("thread1.Start()");
            thread1.Start();
            Console.WriteLine("thread2.Start()");
            thread2.Start();
        }

        private static object ObjA = new object();
        private static object ObjB = new object();

        private static void PerformTaskA()
        {
            //-----------------some code------------//

            //Try to access ObjB//
            lock (ObjB)
            {
                Thread.Sleep(1000);
                lock (ObjA)
                {
                    //-----------------some code------------//
                }

            }
        }

        private static void PerformTaskB()
        {
            //-----------------some code------------//

            lock (ObjA)
            {
                lock (ObjB)
                {
                    //-----------------some code------------//
                }

            }
        }
    }
}
