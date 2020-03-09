using NUnit.Framework;
using System;
using Checkpoint2;
using System.Linq;
using System.Collections.Generic;

namespace Checkpoint2Test
{
	[TestFixture]
	public class TestEvent
	{
		[Test]
		public void TestEventPostponed()
		{
			Event newEvent = new Event("TestPresent");
			newEvent.StartTime = DateTime.Now - TimeSpan.FromMinutes(1);
			newEvent.EndTime = newEvent.StartTime + TimeSpan.FromHours(1);
			DateTime startDateBeforePostpone = newEvent.StartTime;
			DateTime endDateBeforePostpone = newEvent.EndTime;

			newEvent.Postpone(TimeSpan.FromDays(1));

			Assert.AreEqual(startDateBeforePostpone, newEvent.StartTime - TimeSpan.FromDays(1));
			Assert.AreEqual(endDateBeforePostpone, newEvent.EndTime - TimeSpan.FromDays(1));
		}

		[Test]
		public void TestEventList()
		{
			List<Event> Agenda = new List<Event>();
			Event event1 = new Event(201, "event201", Convert.ToDateTime(02-02-2020), Convert.ToDateTime(03-03-2020), "Lorem ipsum");
			Event event2 = new Event(202, "event202", Convert.ToDateTime(03-03-2020), Convert.ToDateTime(04-04-2020), "Lorem ipsum");
			Event event3 = new Event(203, "event203", Convert.ToDateTime(04-04-2020), Convert.ToDateTime(05-05-2020), "Lorem ipsum");
			Event event4 = new Event(204, "event204", Convert.ToDateTime(05-05-2020), Convert.ToDateTime(06-06-2020), "Lorem ipsum");
			Event event5 = new Event(205, "event205", Convert.ToDateTime(06-06-2020), Convert.ToDateTime(06-06-2020), "Lorem ipsum");
			Agenda.Add(event1);
			Agenda.Add(event2);
			Agenda.Add(event3);
			Agenda.Add(event4);
			Agenda.Add(event5);

			IEnumerable<AbstractPerson> Subordinates = new List<AbstractPerson>();

			AbstractPerson newStudent = PersonFactory.CreatePerson("Student200", "Student", "3", Agenda, 5, Subordinates);

			List<Event> Student200Agenda = Database.GetEventsList("Student200", Convert.ToDateTime(03-03-2020), Convert.ToDateTime(05-05-2020));

			List<Event> ResultAgenda = new List<Event>();
			ResultAgenda.Add(event2);
			ResultAgenda.Add(event3);
			ResultAgenda.Add(event4);

			Assert.AreEqual(ResultAgenda, Student200Agenda);
		}
	}
}
