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

        public bool ViewShedule(DateTime date) {
            Console.WriteLine(ViewStringCreator(date));
            return true;
        }


        public string ViewStringCreator(DateTime date) {
            List<Meeting> periodMeeting = meetings.Where(x => x.Start.Date == date).ToList();
            string sout = $"Meeting Count = {periodMeeting.Count}";
            for (int i = 0; i < periodMeeting.Count; i++)
            {
                sout += $"\n{i}. {periodMeeting[i].ToString()}";
            }
            return sout;
        }

        public bool PrintAllMeetings() {
            for (int i = 0; i < meetings.Count; i++) {
                Console.WriteLine($"{i}. {meetings[i].ToString()}");
            }
            return true;
        }

        public bool DeleteMeeting(int index) {
            meetings.RemoveAt(index);
            return true;
        }

        public bool ChangeMeeting(int index, Meeting newMeeting) {
            meetings[index] = newMeeting;
            return true;
        }
    }
}
