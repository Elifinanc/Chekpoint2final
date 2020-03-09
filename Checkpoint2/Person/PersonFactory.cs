using System;
using System.Collections.Generic;
using System.Text;

namespace Checkpoint2
{
    public abstract class PersonFactory
    {
        public static AbstractPerson CreatePerson (string name, string type, string cursus, IEnumerable<Event> agenda, int leadId, IEnumerable<AbstractPerson> subordinates)
        {
            if (type == "Student")
            {
                return new Student(name, type, cursus, agenda, leadId, subordinates);
            }
            else if (type == "Former")
            {
                return new Former(name, type, cursus, agenda, leadId, subordinates);
            }
            else if (type == "LeadFormer")
            {
                return new LeadFormer(name, type, cursus, agenda, leadId, subordinates);
            }
            else
            {
                throw new ArgumentException("There is no person for this type. Please enter a valid type of person!");
            }
        }
    }
}
