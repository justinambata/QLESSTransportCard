using System;
using System.Threading;

namespace QLESS_Transport_Card
{
    public class DeadlockApp
    {
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

        private static readonly object ObjA = new object();
        private static readonly object ObjB = new object();

        private static void PerformTaskA()
        {
            //-----------------some code------------//
            Console.WriteLine("PerformTaskA()");

            //Try to access ObjB
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

    public class NotADeadlockApp
    {
        /*
         * https://stackoverflow.com/questions/444191/how-to-prevent-deadlocks-in-the-following-c-sharp-code
         */

        public void Run()
        {
            Console.WriteLine("***NotADeadlockApp start...");
            var thread1 = new Thread(PerformTaskA);
            var thread2 = new Thread(PerformTaskB);

            Console.WriteLine("thread1.Start()");
            thread1.Start();
            Console.WriteLine("thread2.Start()");
            thread2.Start();
        }

        private static readonly object ObjA = new object();
        private static readonly object ObjB = new object();

        private static void PerformTaskA()
        {
            //-----------------some code------------//
            Console.WriteLine("PerformTaskA()");
            
            Console.WriteLine("PerformTaskA() > Monitor(ObjB): try");
            if (Monitor.TryEnter(ObjB, 1000)) // 1 second timeout
            {
                try
                {
                    Console.WriteLine("PerformTaskA() > Monitor(ObjB): success!");
                    Thread.Sleep(1000);
                    Console.WriteLine("PerformTaskA() > Monitor(ObjB) > Monitor(ObjA): try");
                    if (Monitor.TryEnter(ObjA, 1000)) // 1 second timeout
                    {
                        try
                        {
                            Console.WriteLine("PerformTaskA() > Monitor(ObjB) > Monitor(ObjA): success!");
                        }
                        finally
                        {
                            Monitor.Exit(ObjA);
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(ObjB);
                }
            }
        }

        private static void PerformTaskB()
        {
            //-----------------some code------------//
            Console.WriteLine("PerformTaskB()");

            Console.WriteLine("PerformTaskB() > Monitor(ObjA): try");
            if (Monitor.TryEnter(ObjA, 1000)) // 1 second timeout
            {
                try
                {
                    Console.WriteLine("PerformTaskB() > Monitor(ObjA): success!");
                    Console.WriteLine("PerformTaskB() > Monitor(ObjA) > Monitor(ObjB): try");
                    if (Monitor.TryEnter(ObjB, 1000)) // 1 second timeout
                    {
                        try
                        {
                            Console.WriteLine("PerformTaskB() > Monitor(ObjA) > Monitor(ObjB): success!");
                        }
                        finally
                        {
                            Monitor.Exit(ObjB);
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(ObjA);
                }
            }
        }
    }
}
