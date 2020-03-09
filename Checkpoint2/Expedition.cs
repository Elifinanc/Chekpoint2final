using System;
using System.Collections.Generic;
using System.Text;

namespace Checkpoint2
{
    public class Expedition
    {
        public int ExpeditionId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IEnumerable<Quests> QuestsList { get; set; }
    }
}
