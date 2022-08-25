using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Meeting
    {
        string Name;
        public DateTime Start { get; private set; }
        DateTime End;
        DateTime Notify;
        meetStatus status;

        public Meeting(string name,  DateTime start, DateTime end)
        {
            Name = name;
            Start = start;
            End = end;


            if (Start > DateTime.Now)
                status = meetStatus.WAIT;
            else if (DateTime.Now < End)
                status = meetStatus.ONGO;
            else status = meetStatus.ENDED;


        }

        public Meeting(string name, DateTime start, DateTime end, DateTime notify) {
            Name = name;
            Start = start;
            End = end;
            Notify = notify;
        }

        public bool SetNotify(DateTime time) {
            Notify = time;

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
            return $"meet {Name} ({Start.ToString()}-{End.ToString()}) {((Notify==null)?"":Notify.ToString())}";
        }

        public void checkMe(DateTime dateTime, int delta= 1) {
            if (status != meetStatus.ENDED) {
                if (status == meetStatus.SOON && Math.Abs(dateTime.Subtract(Notify).TotalSeconds) <= delta) {
                    DoNotify();
                }
                if (status == meetStatus.WAIT && Math.Abs(dateTime.Subtract(Start).TotalSeconds) <= delta) {
                    DoMeet();
                }
                if (status == meetStatus.ONGO && Math.Abs(dateTime.Subtract(End).TotalSeconds) <= delta)
                {
                    EndMeet();
                }
            }
        }

        bool DoNotify() {
            Console.WriteLine($"Meet soon start. {this.ToString()}");
            status = meetStatus.WAIT;
            return true;
        }

        bool DoMeet()
        {
            Console.WriteLine($"Meet start. {this.ToString()}");
            status = meetStatus.ONGO;
            return true;
        }

        bool EndMeet() {
            Console.WriteLine($"Meet End. {this.ToString()}");
            status = meetStatus.ENDED;
            return true;
        }
    }

    enum meetStatus { 
        SOON,
        WAIT,
        ONGO,
        ENDED,
    }
}
