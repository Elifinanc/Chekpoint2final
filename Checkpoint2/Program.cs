using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using System.Linq;

namespace Checkpoint2
{
    class Program
    {
        public static AbstractPerson currentPerson;
        public static bool Verbose { set; get; }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<DisplayEvents>(args)
                .WithParsed<DisplayEvents>(RunEventCommand);

            /*Event newEvent = new Event("Important meeting");
            newEvent.StartTime = DateTime.Now;
            newEvent.EndTime = DateTime.Now + TimeSpan.FromHours(1);
            newEvent.Postpone(TimeSpan.FromHours(1));
            Console.WriteLine("Another meeting is postponed");*/
        }

        static void RunEventCommand(DisplayEvents options)
        {
            List<Event> EventsList = Database.GetEventsList(options.Name, DateTime.Parse(options.StartTime), DateTime.Parse(options.EndTime));
            IO.DisplayEventsList(options.Name, EventsList);
        }
    }
}

