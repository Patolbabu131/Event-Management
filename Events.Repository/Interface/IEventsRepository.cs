using System;
using Events.DomainObjects;

namespace Events.Repository.Interface
{
	public interface IEventsRepository
	{
        List<EventsResponse> GetEventsList(Dictionary<string, string> Parameters);

    }
}

