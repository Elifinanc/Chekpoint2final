using System;
using System.Collections.Generic;
using System.Text;

namespace Checkpoint2
{
    public class Former : AbstractPerson
    {
        public override string Type
        {
            get { return "Former"; }
            set { }
        }
        public override string Cursus
        {
            get { return null; }
            set { }
        }
        public override IEnumerable<AbstractPerson> Subordinates
        {
            get => new List<Student>();
            set { }
        }

        public Former(string name, string type, string cursus, IEnumerable<Event> agenda, int leadId, IEnumerable<AbstractPerson> subordinates) : base (name, type,  cursus, agenda, leadId,subordinates)
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
