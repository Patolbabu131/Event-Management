using System;
using Events.DomainObjects;

namespace Events.Repository
{
	public interface IEventsRepository
	{
        List<EventsResponse> GetEventsList(Dictionary<string, string> Parameters);

    }
}

