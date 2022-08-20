using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        List<Meeting> meetingList = new List<Meeting>();

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
                        case (1): { AddNewMeeting(); break; }
                        case (2): { ChangeMeeting(); break; }
                        case (3): { ViewMeeting(); break; }
                        case (4): { ExportMeeting(); break; }
                        case (0):return;
                    }
                }
            }
        }

        static void AddNewMeeting() { 
            
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
