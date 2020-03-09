using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace Checkpoint2
{
    class IO
    {
        public static void DisplayEventsList(string name, List<Event> EventList)
        {
            string array = String.Format("The event list for " + name + "is");
            string lignTemplate = "|{0, 10} | {1,20} | {2,20} | {3, 20}| {4, 20:dd/mm/yyyy} | {5, 20::dd/mm/yyyy}|\n";
            string header = String.Format(lignTemplate, "PersonId", "PersonName", "EventId", "EventName", "StartTime", "EndTime");
            string separationLine = new String('-', header.Length) + "\n";
            array += separationLine + header + separationLine;
            foreach (Event currentEvent in EventList)
            {
                array += String.Format(lignTemplate, Program.currentPerson.PersonId, name, currentEvent.EventId, currentEvent.EventName, currentEvent.StartTime, currentEvent.EndTime);
            }
            array += separationLine;
            Console.WriteLine($"\n{array}");
        }
    }
}

