using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// Класс-встреча
    /// </summary>
    class Meeting
    {
        string Name;//название
        public DateTime Start { get; private set; }//начало встречи 
        DateTime End;//конец встречи
        DateTime Notify;//время уведомления
        meetStatus status;//текущее состояние

        ///конструктор
        public Meeting(string name,  DateTime start, DateTime end)
        {
            Name = name;
            Start = start;
            End = end;

            //определяем состояние встречи при создании
            if (Start > DateTime.Now)
                status = meetStatus.WAIT;
            else if (DateTime.Now < End)
                status = meetStatus.ONGO;
            else status = meetStatus.ENDED;


        }

        /// <summary>
        /// установка времени уведомления
        /// </summary>
        /// <param name="time">время уведосления</param>
        /// <returns></returns>
        public bool SetNotify(DateTime time) {
            
            Notify = time;
            //при задании времени проверяем состояние
            if (Notify > DateTime.Now)
                status = meetStatus.SOON;
            else if (Start > DateTime.Now)
                status = meetStatus.WAIT;
            else if (DateTime.Now < End)
                status = meetStatus.ONGO;
            else status = meetStatus.ENDED;

            return true;
        }

        public override string ToString()
        {
            return $"meet {Name} ({Start.ToString()}-{End.ToString()}) {((Notify==DateTime.MinValue)?"":Notify.ToString())}";
        }

        /// <summary>
        /// Проверка состояния встречи и изменение при наступлении события
        /// </summary>
        /// <param name="dateTime">время проверки</param>
        /// <param name="delta">допустимая погрешность во времни</param>
        public void checkMe(DateTime dateTime, int delta= 1) {
            //рассматриваем только активные встречи
            if (status != meetStatus.ENDED) {
                //если встреча имела состояние "скоро", и пришло время уведомлять, то уведомляем
                if (status == meetStatus.SOON && Math.Abs(dateTime.Subtract(Notify).TotalSeconds) <= delta) {
                    DoNotify();
                }
                //если встреча имела статус "ожидаем", и пришло время начала, то начинаем встречу
                if (status == meetStatus.WAIT && Math.Abs(dateTime.Subtract(Start).TotalSeconds) <= delta) {
                    DoMeet();
                }
                //если встреча имела статус "в процессе", и пришло время завершения, то завершаем встречу
                if (status == meetStatus.ONGO && Math.Abs(dateTime.Subtract(End).TotalSeconds) <= delta)
                {
                    EndMeet();
                }
            }
        }

        /// <summary>
        /// уведомляем о встрече
        /// </summary>
        /// <returns></returns>
        bool DoNotify() {
            Console.WriteLine($"Meet soon start. {this.ToString()}");
            status = meetStatus.WAIT;
            return true;
        }

        /// <summary>
        /// начинаем встречу
        /// </summary>
        /// <returns></returns>
        bool DoMeet()
        {
            Console.WriteLine($"Meet start. {this.ToString()}");
            status = meetStatus.ONGO;
            return true;
        }

        /// <summary>
        /// завершаем встречу
        /// </summary>
        /// <returns></returns>
        bool EndMeet() {
            Console.WriteLine($"Meet End. {this.ToString()}");
            status = meetStatus.ENDED;
            return true;
        }
    }


    /// <summary>
    /// состояние встречи
    /// </summary>
    enum meetStatus { 
        SOON, // скоро
        WAIT, //ожидаем
        ONGO, //в процессе
        ENDED //завершена
    }
}
