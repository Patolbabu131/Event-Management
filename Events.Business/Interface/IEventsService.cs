using System;
using Events.Common;
using Events.DomainObjects;

namespace Events.Business
{
	public interface IEventsService
	{
        List<EventsResponse> GetEventsList(Dictionary<string, string> Parameters);
    }
}