using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    
    class Sheduler
    {
        int delta = 1;
        List<Meeting> meetings = new List<Meeting>();

        static Sheduler instance;

        private Sheduler() { 
            
        }

        public static Sheduler GetInstance() {
            if (instance == null) {
                instance = new Sheduler();
            }
            return instance;
        }

        object locker = new object();
        public bool AddNewMeet(Meeting newMeet)
        {
            lock (locker)
            {
                meetings.Add(newMeet);
            }
            return true;
        }

        public void startMonitor() {
            Thread monitor = new Thread(MonitorAction);
            monitor.Start();
        }

        public bool isStart = true;
        private void MonitorAction()
        {
            while (isStart)
            {
                lock (locker)
                {
                    foreach (Meeting meet in meetings)
                    {
                        meet.checkMe(DateTime.Now, delta);
                    }
                }
                Thread.Sleep(500);
            }
        }

        public bool viewShedule(DateTime date) {
            Console.WriteLine(viewStringCreator(date));
            return true;
        }


        string viewStringCreator(DateTime date) {
            List<Meeting> periodMeeting = meetings.Where(x => x.Start.Date == date).ToList();
            string sout = $"Meeting Count = {periodMeeting.Count}";
            for (int i = 0; i < periodMeeting.Count; i++)
            {
                sout += $"\n{i + 1}. {periodMeeting[i].ToString()}";
            }
            return sout;
        }
    }
}
