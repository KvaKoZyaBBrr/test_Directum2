using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    
    /// <summary>
    /// класс-рассписание
    /// </summary>
    class Sheduler
    {
        //сам список встреч
        static List<Meeting> meetings = new List<Meeting>();
        //поток - обработчик
        Thread monitor;
        //реализуем синглтон, чтоб не было дерущихся потоков
        static Sheduler instance;
        
        //в приватном конструкторе один раз создаем поток-обработки
        private Sheduler(Thread thread) {
            monitor = thread;
        }

        public static Sheduler GetInstance() {
            if (instance == null) {
                instance = new Sheduler(new Thread(MonitorAction));
            }
            return instance;
        }

        /// <summary>
        /// блокиратор потока
        /// </summary>
        static object locker = new object();

        /// <summary>
        /// добавление новой встречи
        /// </summary>
        /// <param name="newMeet">новая встреча</param>
        /// <returns></returns>
        public bool AddNewMeet(Meeting newMeet)
        {
            //блокируем доступ к потокозависимым данным
            lock (locker)
            {
                meetings.Add(newMeet);
            }
            return true;
        }

        /// <summary>
        /// старт монитора расписания
        /// </summary>
        public void startMonitor() {
            
            monitor.Start();
        }

        /// <summary>
        /// остановка монитора через флаг
        /// </summary>
        public void StopMonitor()
        {
            isStart = false;
        }
        //флаг работы монитора
        public static bool isStart = true;

        /// <summary>
        /// раота монитора
        /// </summary>
        private static void MonitorAction()
        {
            //пока запущен флаг
            while (isStart)
            {
                //блокируем объект
                lock (locker)
                {
                    //произвиодим проверку
                    foreach (Meeting meet in meetings)
                    {
                        meet.checkMe(DateTime.Now);
                    }
                }
                //делаем задержку
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Просмотр рассписания на день
        /// </summary>
        public bool ViewShedule(DateTime date) {
            Console.WriteLine(ViewStringCreator(date));
            return true;
        }

        /// <summary>
        /// составление строки на просмотр и экспорт
        /// </summary>
        public string ViewStringCreator(DateTime date) {
            List<Meeting> periodMeeting = meetings.Where(x => x.Start.Date == date).ToList();
            string sout = $"Meeting Count = {periodMeeting.Count}";
            for (int i = 0; i < periodMeeting.Count; i++)
            {
                sout += $"\n{i}. {periodMeeting[i].ToString()}";
            }
            return sout;
        }

        /// <summary>
        /// вывод всех встреч
        /// </summary>
        /// <returns></returns>
        public bool PrintAllMeetings() {
            for (int i = 0; i < meetings.Count; i++) {
                Console.WriteLine($"{i}. {meetings[i].ToString()}");
            }
            return true;
        }

        /// <summary>
        /// удалить встречу по индексу
        /// </summary>
        /// <param name="index">индекс встречи в списке</param>
        /// <returns></returns>
        public bool DeleteMeeting(int index) {
            lock (locker)
            {
                meetings.RemoveAt(index);
            }
            return true;
        }

        /// <summary>
        /// изменить встречу
        /// </summary>
        /// <param name="index">индекс изменяемой встречи</param>
        /// <param name="newMeeting">измененная встреча</param>
        /// <returns></returns>
        public bool ChangeMeeting(int index, Meeting newMeeting) {
            lock (locker)
            {
                meetings[index] = newMeeting;
            }
            return true;
        }
    }
}
