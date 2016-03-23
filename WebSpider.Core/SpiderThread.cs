using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSpider.Core
{
    public class SpiderThread
    {
        private sealed class ThreadValuePair
        {
            public Thread Thread { get; set; }
            public Object Parameter { get; set; }
        }

        //private Queue<Thread> QueuedThreads { get; set; }
        private static Queue<ThreadValuePair> QueuedThreads { get; set; }
        private static List<Thread> ActiveThreads { get; set; }
        public static int MaxActiveThreads { get; set; }
        private static Timer Timer;
        //private const int stackSize = 1024 * 1024 * 64;
        public static int ThreadCount
        { 
            get { 
                return (ActiveThreads.Count() + QueuedThreads.Count()); 
            } 
        }
        public static int ActiveThreadCount
        {
            get
            {
                return ActiveThreads.Count();
            }
        }
        public static int QueuedThreadCount
        {
            get
            {
                return QueuedThreads.Count();
            }
        }

        static SpiderThread()
        {
            //QueuedThreads = new Queue<Thread>();
            QueuedThreads = new Queue<ThreadValuePair>();
            ActiveThreads = new List<Thread>();
            MaxActiveThreads = Settings.GetValue("ConcurrentThreads");
            Timer = new Timer(timer_callback, null, 0, 1000);
        }


        private static bool timerCallbackExecuting;
        private static void timer_callback(object state)
        {
            if (!timerCallbackExecuting)
            {
                timerCallbackExecuting = true;
                //Monitor.Enter(ActiveThreads);
                //Monitor.Enter(QueuedThreads);
                //foreach (Thread thread in ActiveThreads)
                for (int index = ActiveThreads.Count() - 1; index >= 0; index--)
                {
                    //Thread thread = ActiveThreads[index];
                    if (!ActiveThreads[index].IsAlive)
                        ActiveThreads.Remove(ActiveThreads[index]);
                    //else if (ActiveThreads[index].ThreadState == ThreadState.Aborted || ActiveThreads[index].ThreadState == ThreadState.Stopped)
                    //    ActiveThreads.Remove(ActiveThreads[index]);
                }
                while (ActiveThreads.Count() < MaxActiveThreads && QueuedThreads.Count() > 0)
                {
                    ThreadValuePair threadDataPair = QueuedThreads.Dequeue();
                    Thread thread = threadDataPair.Thread;
                    ActiveThreads.Add(thread);
                    if (ReferenceEquals(threadDataPair.Parameter, null))
                        thread.Start();
                    else
                        thread.Start(threadDataPair.Parameter);
                }
                //Monitor.Exit(ActiveThreads);
                //Monitor.Exit(QueuedThreads);
                timerCallbackExecuting = false;
            }
        }

        public static void Add(ThreadStart threadStart)
        {
            ThreadValuePair tvPair = new ThreadValuePair();
            tvPair.Thread = new Thread(threadStart);
            tvPair.Parameter = null;
            if (ActiveThreads.Count() < MaxActiveThreads)
            {
                ActiveThreads.Add(tvPair.Thread);
                tvPair.Thread.Start();
            }
            else
                QueuedThreads.Enqueue(tvPair);
        }

        public static void Add(ParameterizedThreadStart parameterizedThreadStart, Object Parameter)
        {
            ThreadValuePair tvPair = new ThreadValuePair();
            tvPair.Thread = new Thread(parameterizedThreadStart);
            tvPair.Parameter = Parameter;
            if (ActiveThreads.Count() < MaxActiveThreads)
            {
                ActiveThreads.Add(tvPair.Thread);
                tvPair.Thread.Start(tvPair.Parameter);
            }
            else
                QueuedThreads.Enqueue(tvPair);
        }



        public static void PauseAll()
        {
            for (int index = 0; index < ActiveThreads.Count ; index++)
            {
                Thread thread = ActiveThreads[index];
                if (thread.ThreadState != ThreadState.Suspended && thread.ThreadState != ThreadState.SuspendRequested)
                    thread.Suspend();
            }
        }

        public static void ResumeAll()
        {
            for (int index = 0; index < ActiveThreads.Count; index++)
            {
                Thread thread = ActiveThreads[index];
                if (thread.ThreadState == ThreadState.Suspended || thread.ThreadState == ThreadState.SuspendRequested)
                    thread.Resume();
            }
        }

        public static void CancelAll()
        {
            for (int index = 0; index < ActiveThreads.Count; index++)
            {
                Thread thread = ActiveThreads[index];
                if (thread.ThreadState == ThreadState.Suspended || thread.ThreadState == ThreadState.SuspendRequested)
                    thread.Resume();
                thread.Abort();
            }
            ActiveThreads = new List<Thread>();
            QueuedThreads = new Queue<ThreadValuePair>();
        }
    }
}
