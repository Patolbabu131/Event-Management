using Events.Database;
using Events.DomainObjects;

namespace Events.Repository
{
    public class EventsRepository : IEventsRepository
    {
        #region Variables

        //private readonly eventdbContext _context;

        #endregion

        #region Constructor

        //public EventsRepository(eventdbContext context)

        //{
        //    _context = context;
        //}

        #endregion

        #region Methods


        public List<EventsResponse> GetEventsList(Dictionary<string, string> Parameters)
        {
            throw new NotImplementedException();

            //finallist = (from exp in _context.ExpenseDetails
            //             join exptarget in _context.ExpenseTargetDetails on exp.Id equals exptarget.ExpenseDetailId
            //             where exp.StoreId == user.StoreId
            //              && exptarget.BranchId.HasValue
            //             && exptarget.BranchId == branchid
            //             select new ExpenseDetailsViewModel
            //             {
            //                 ExpenseId = exp.Id,
            //                 MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(exp.Month),
            //                 Year = exp.Year,
            //                 Month = exp.Month

            //             });

            //#region Filters

            //var queryStrings = Request.Query;

            //if (parameters.ContainsKey("Value") && !string.IsNullOrWhiteSpace(parameters["Value"]))
            //{
            //    var acode = parameters["Value"].ToString();

            //    int intyearmonth = 0;
            //    int.TryParse(acode, out intyearmonth);

            //    finallist = finallist.Where(p => p.MonthName.ToLower().Contains(acode) || (p.Year == intyearmonth) || (p.Month == intyearmonth));
            //}

            //#endregion

            //int TotalCount = finallist.Count();

            //#region Sorting

            //string SortColumn = parameters["sort_by"].ToString();
            //string SortDir = parameters["sort_order"].ToString();

            //switch (SortColumn)
            //{
            //    case "Year":
            //        if (SortDir == "desc")
            //        {
            //            finallist = finallist.OrderByDescending(x => x.Year);
            //        }
            //        else
            //        {
            //            finallist = finallist.OrderBy(x => x.Year);
            //        }
            //        break;
            //    case "Month":
            //        if (SortDir == "desc")
            //        {
            //            finallist = finallist.OrderByDescending(x => x.Month);
            //        }
            //        else
            //        {
            //            finallist = finallist.OrderBy(x => x.Month);
            //        }
            //        break;
            //}

            //#endregion

            //#region Pagination and OutPut


            //var pageNo = Convert.ToInt32(parameters["page_number"]);
            //var pageSize = Convert.ToInt32(parameters["results_per_page"]);
            //var list = finallist.Skip((pageNo - 1) * (pageSize)).Take(pageSize).ToList();

            //#endregion


        }

        #endregion
    }
}

