using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Meeting
    {
        string Name;
        DateTime Start;
        DateTime End;
        DateTime Notify;

        public Meeting(string name,  DateTime start, DateTime end)
        {
            Name = name;
            Start = start;
            End = end;
        }

        public Meeting(string name, DateTime start, DateTime end, DateTime notify) {
            Name = name;
            Start = start;
            End = end;
            Notify = notify;
        }

        public bool SetNotify(DateTime time) {
            Notify = time;
            return true;
        }
    }
}
