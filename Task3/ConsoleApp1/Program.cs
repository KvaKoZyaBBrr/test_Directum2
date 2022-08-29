using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Sheduler.GetInstance().startMonitor();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose operation");
                Console.WriteLine("1. Add new meeting");
                Console.WriteLine("2. Delete meeting");
                Console.WriteLine("3. Change  meeting");
                Console.WriteLine("4. View meetings");
                Console.WriteLine("5. Export meetings");
                Console.WriteLine("0. Exit");

                int command;
                string rawCommand = Console.ReadLine();
                bool parserResult = Int32.TryParse(rawCommand, out command);
                if (parserResult) {
                    switch (command) {
                        case (1): { AddNewMeeting(); Console.ReadLine(); break; }
                        case (2): { DeleteMeeting(); break; }
                        case (3): { ChangeMeeting(); break; }
                        case (4): { ViewMeeting(); Console.ReadLine(); break; }
                        case (5): { ExportMeeting(); break; }
                        case (0): { Sheduler.GetInstance().isStart = false; return; }
                    }
                }
            }
        }

        static bool AddNewMeeting() {
            Meeting meeting;
            if (MeetingCreator(out meeting))
            {
                Console.WriteLine($"New {meeting.ToString()} was created");
                Sheduler.GetInstance().AddNewMeet(meeting);
                return true;
            }
            return false;

        }


        static private bool MeetingCreator(out Meeting newMeeting) {
            newMeeting = null;
            Console.WriteLine("Enter name of meeting");
            string name = Console.ReadLine();
            DateTime startTime;
            if (!getValue<DateTime>("start time of meeting", "fail to write start time", out startTime))
                return false;

            if (startTime <= DateTime.Now)
            {
                Console.WriteLine("wrong time for meeting");
                return false;
            }

            DateTime endTime;
            if (!getValue<DateTime>("end time of meeting", "fail to write end time", out endTime))
                return false;

            if (endTime < startTime)
            {
                Console.WriteLine("fail to write time range");
                return false;
            }
            newMeeting = new Meeting(name, startTime, endTime);

            bool isNotify;
            if (!getValue<Boolean>("flag to notify", "cannot convert to bool", out isNotify))
                return false;

            if (isNotify)
            {
                DateTime notifyTime;
                if (!getValue<DateTime>("time for notify", "fail to write notify time", out notifyTime))
                    return false;
                newMeeting.SetNotify(notifyTime);
            }
            return true;
        }


        static bool getValue<T>(string nameOfData, string strOfErr, out T data) 
        {
            Console.WriteLine($"Enter {nameOfData}");
            string input = Console.ReadLine();
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                try
                {
                    data = (T)converter.ConvertFromString(input);
                }
                catch (Exception ex){
                    Console.WriteLine(ex.Message);
                    data = default(T);
                    return false;
                }
                return true;
            }
            Console.WriteLine(strOfErr);
            data = default(T);
            return false;
        }
        static void DeleteMeeting()
        {
            Sheduler.GetInstance().PrintAllMeetings();
            string indexStr = Console.ReadLine();
            int index;
            if (Int32.TryParse(indexStr, out index)) {
                Sheduler.GetInstance().DeleteMeeting(index);
            }
            
        }
        
        static void ChangeMeeting()
        {
            Sheduler.GetInstance().PrintAllMeetings();
            string indexStr = Console.ReadLine();
            int index;
            if (Int32.TryParse(indexStr, out index))
            {
                Meeting meeting;
                if (MeetingCreator(out meeting)) {
                    Sheduler.GetInstance().ChangeMeeting(index, meeting);
                }
            }
        }

        static void ViewMeeting()
        {
            DateTime choosenDay;
            if (getValue<DateTime>("day for view", "err date", out choosenDay))
            {
                Sheduler.GetInstance().ViewShedule(choosenDay.Date);
            }
        }
        static void ExportMeeting()
        {
            DateTime choosenDay; 
            if (getValue<DateTime>("day for export", "err date", out choosenDay))
            {
                string exportString = Sheduler.GetInstance().ViewStringCreator(choosenDay.Date);
                string path = "./export.txt";
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.Write(exportString);
                }
            }
            
            
        }
    }
}
