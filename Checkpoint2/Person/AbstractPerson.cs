using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Checkpoint2
{
    public abstract class AbstractPerson
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public abstract string Type { get; set; }
        public abstract string Cursus { get; set; }
        public IEnumerable<Event> Agenda { get; set; }
        public int LeadId { get; set; }
        public abstract IEnumerable<AbstractPerson> Subordinates { get; set; }

        public AbstractPerson(string name, string type, string cursus, IEnumerable<Event> agenda, int leadId, IEnumerable<AbstractPerson> subordinates)
        {
            Name = name;
            Type = type;
            Cursus = cursus;
            Agenda = agenda;
            LeadId = leadId;
            Subordinates = subordinates;
        }
    }
}
