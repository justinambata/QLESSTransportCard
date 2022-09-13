using System;
using System.Threading;

namespace QLESS_Transport_Card
{
    public class DeadlockApp
    {
        public void Run()
        {
            Console.WriteLine("***DeadlockApp start...");
            var thread1 = new Thread(PerformTaskA);
            var thread2 = new Thread(PerformTaskB);

            Console.WriteLine("thread1.Start()");
            thread1.Start();
            Console.WriteLine("thread2.Start()");
            thread2.Start();
        }

        /*
         * https://stackoverflow.com/questions/15361810/how-to-find-out-deadlock-and-prevent-it-in-c-sharp#:~:text=The%20simplest%20way%20to%20avoid,timeout%20during%20acquiring%20a%20lock.
         * A deadlock occurs when each thread (minimum of two) tries to acquire a lock on a resource already locked by another.
         * Thread 1 locked on Resources 1 tries to acquire a lock on Resource 2.
         * At the same time, Thread 2 has a lock on Resource 2 and it tries to acquire a lock on Resource 1.
         * Two threads never give up their locks, hence the deadlock occurs.
         *
         * The simplest way to avoid deadlock is to use a timeout value.
         * The Monitor class (system.Threading.Monitor) can set a timeout during acquiring a lock
         */

        private static readonly object ObjA = new object();
        private static readonly object ObjB = new object();

        private static void PerformTaskA()
        {
            //-----------------some code------------//
            Console.WriteLine("PerformTaskA()");

            //Try to access ObjB//
            Console.WriteLine("PerformTaskA() > lock(ObjB): try");
            lock (ObjB)
            {
                Console.WriteLine("PerformTaskA() > lock(ObjB): locked!");
                Thread.Sleep(1000);
                
                Console.WriteLine("PerformTaskA() > lock(ObjB) > lock(ObjA): try");
                lock (ObjA)
                {
                    //-----------------some code------------//
                    Console.WriteLine("PerformTaskA() > lock(ObjB) > lock(ObjA): locked!");
                }

            }
        }

        private static void PerformTaskB()
        {
            //-----------------some code------------//
            Console.WriteLine("PerformTaskB()");

            Console.WriteLine("PerformTaskB() > lock(ObjA): try");
            lock (ObjA)
            {
                Console.WriteLine("PerformTaskB() > lock(ObjA): locked!");
                Console.WriteLine("PerformTaskB() > lock(ObjA) > lock(ObjB): try");
                lock (ObjB)
                {
                    //-----------------some code------------//
                    Console.WriteLine("PerformTaskB() > lock(ObjA) > lock(ObjB): locked!");
                }

            }
        }
    }
}
