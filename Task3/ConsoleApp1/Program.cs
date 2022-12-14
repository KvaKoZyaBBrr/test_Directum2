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
            //пуск монитора
            Sheduler.GetInstance().startMonitor();
            //вывод меню
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
                        case (2): { DeleteMeeting(); Console.ReadLine(); break; }
                        case (3): { ChangeMeeting(); Console.ReadLine(); break; }
                        case (4): { ViewMeeting(); Console.ReadLine(); break; }
                        case (5): { ExportMeeting(); Console.ReadLine(); break; }
                        case (0): { Sheduler.GetInstance().StopMonitor(); return; }
                    }
                }
            }
        }

        /// <summary>
        /// создание новой встречи
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// создатель встреч. вынесен отдельно для испаользоания при изменении
        /// </summary>
        /// <param name="newMeeting">выходнйо параметр встречи</param>
        /// <returns></returns>
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

        /// <summary>
        /// получание данных из ввода
        /// </summary>
        /// <typeparam name="T">тип данных в который надо конвертнуть</typeparam>
        /// <param name="nameOfData">название параметра</param>
        /// <param name="strOfErr">строка в случае ошибки</param>
        /// <param name="data">выходной параметр данных</param>
        /// <returns></returns>
        static bool getValue<T>(string nameOfData, string strOfErr, out T data) 
        {
            Console.WriteLine($"Enter {nameOfData}");
            string input = Console.ReadLine();
            //просто использование T.TryParse не удалось, как планировал, поэтому так
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

        /// <summary>
        /// удаление встречи по индексу
        /// </summary>
        static bool DeleteMeeting()
        {
            Sheduler.GetInstance().PrintAllMeetings();
            string indexStr = Console.ReadLine();
            int index;
            if (Int32.TryParse(indexStr, out index)) {
                if(Sheduler.GetInstance().DeleteMeeting(index))
                    Console.WriteLine($"Meeting #{index} was deleted");
            }
            return true;
        }
        
        /// <summary>
        /// изменение встречи по индексу
        /// </summary>
        static void ChangeMeeting()
        {
            Sheduler.GetInstance().PrintAllMeetings();
            string indexStr = Console.ReadLine();
            int index;
            if (Int32.TryParse(indexStr, out index))
            {
                Meeting meeting;
                if (MeetingCreator(out meeting)) {
                    if(Sheduler.GetInstance().ChangeMeeting(index, meeting))
                        Console.WriteLine($"Meeting #{index} was changed");
                }
            }
        }

        /// <summary>
        /// просмотр встречи за день
        /// </summary>
        static void ViewMeeting()
        {
            DateTime choosenDay;
            if (getValue<DateTime>("day for view", "err date", out choosenDay))
            {
                Sheduler.GetInstance().ViewShedule(choosenDay.Date);
            }
        }

        /// <summary>
        /// экспорт встреч за день
        /// </summary>
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
                    Console.WriteLine($"Export success");
                }
            }
            
            
        }
    }
}
