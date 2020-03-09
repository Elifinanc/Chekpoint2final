using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace Checkpoint2
{
    class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Activate Verbose Mode")]
        public bool Verbose
        {
            set
            {
                Program.Verbose = value;
            }
        }
    }


    [Verb("event", HelpText = "Display events.")]
    class DisplayEvents : Options
    {
        [Option('p', "person", Required = true, HelpText = "The person's name.")]
        public string Name { get; set; }

        [Option('b', "begins", Required = false, HelpText = "Enter the start time.", MetaValue = "yyyy-mm-dd")]
        public string StartTime { get; set; } 

        [Option('e', "ends", Required = false, HelpText = "Enter the end time.", MetaValue = "yyyy-mm-dd")]
        public string EndTime { get; set; }
    }

    [Verb("cursus", HelpText = "Display cursus.")]
    class DisplayCursus : Options
    {
        [Option('s', "students", Required = true, HelpText = "Display students for a cursus.")]
        public string StudentCursus { get; set; }

        [Option('n', "name", Required = false, HelpText = "The cursus' name.")]
        public string CursusName { get; set; }

        [Option('q', "quests", Required = false, HelpText = "Display quests for a cursus.")]
        public string QuestCursus { get; set; }
    }
}

