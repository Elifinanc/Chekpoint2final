using System;
using System.Collections.Generic;
using System.Text;

namespace Checkpoint2
{
    public class Cursus
    {
        public int CursusId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IEnumerable<AbstractPerson> PersonList { get; set; }
        public IEnumerable<Expedition> ExpeditionList { get; set; }
    }
}
