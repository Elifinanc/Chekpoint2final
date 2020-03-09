using System;
using System.Collections.Generic;
using System.Text;

namespace Checkpoint2
{
    class Agenda
    {
        public int AgendaId { get; set; }
        public string AgendaName { get; set; }
        public List<Event> EventList { get; set; }

        public Agenda()
        {

        }
    }
}
