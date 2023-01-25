using Events.DomainObjects;
using Events.Repository;

namespace Events.Business
{
    public class EventsService : IEventsService
    {

        #region Variables

        protected readonly IEventsRepository _eventsRepository;

        #endregion

        #region Contructor

        public EventsService(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        #endregion


        #region Methods

        public List<EventsResponse> GetEventsList(Dictionary<string, string> Parameters)
        {

            var responseList = _eventsRepository.GetEventsList(Parameters);

            return responseList;

        }

        #endregion
    }
}

