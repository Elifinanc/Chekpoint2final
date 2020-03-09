using System;
using System.Collections.Generic;
using System.Text;

namespace Checkpoint2
{
    class Student : AbstractPerson
    {
        public override string Type
        {
            get { return "Student"; }
            set { }
        }
        public override string Cursus { get; set; }

        public override IEnumerable<AbstractPerson> Subordinates
        {
            get { return null; }
            set { }
        }

        public Student(string name, string type, string cursus, IEnumerable<Event> agenda, int leadId, IEnumerable<AbstractPerson> subordinates) : base(name, type, cursus, agenda, leadId, subordinates)
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
