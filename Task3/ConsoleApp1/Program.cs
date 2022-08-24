using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ConsoleApp1
{
    class Program
    {
        static List<Meeting> meetingList = new List<Meeting>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose operation");
                Console.WriteLine("1. Add new meeting");
                Console.WriteLine("2. Change  meeting");
                Console.WriteLine("3. View meetings");
                Console.WriteLine("4. Export meetings");
                Console.WriteLine("0. Exit");

                int command;
                string rawCommand = Console.ReadLine();
                bool parserResult = Int32.TryParse(rawCommand, out command);
                if (parserResult) {
                    switch (command) {
                        case (1): { AddNewMeeting(); Console.ReadLine(); break; }
                        case (2): { ChangeMeeting(); break; }
                        case (3): { ViewMeeting(); break; }
                        case (4): { ExportMeeting(); break; }
                        case (0):return;
                    }
                }
            }
        }

        static bool AddNewMeeting() {
            Meeting meeting;
            Console.WriteLine("Enter name of meeting");
            string name = Console.ReadLine();
            DateTime startTime;
            if (!getValue<DateTime>("start time of meeting", "fail to write start time", out startTime))
                return false;


            DateTime endTime;
            if (!getValue<DateTime>("end time of meeting", "fail to write end time", out endTime))
                return false;

            if (endTime > startTime)
            {
                Console.WriteLine("fail to write time range");
                return false;
            }
            meeting = new Meeting(name, startTime, endTime);

            bool isNotify;
            if (!getValue<Boolean>("flag to notify", "cannot convert to bool", out isNotify))
                return false ;

            if (isNotify)
            {
                DateTime notifyTime;
                if (!getValue<DateTime>("time for notify", "fail to write notify time", out notifyTime))
                    return false;
                meeting.SetNotify(notifyTime);
            }
            Console.WriteLine($"New {meeting.ToString()} was created");
            meetingList.Add(meeting);
            return true;

        }

        static bool getValue<T>(string nameOfData, string strOfErr, out T data) 
        {
            Console.WriteLine($"Enter {nameOfData}");
            string input = Console.ReadLine();
            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null)
            {
                // Cast ConvertFromString(string text) : object to (T)
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

        static void ChangeMeeting()
        {

        }
        static void ViewMeeting()
        {

        }
        static void ExportMeeting()
        {

        }
    }
}
