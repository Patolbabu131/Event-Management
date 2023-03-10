
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Events.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Web.Controllers
{
    public class BaseController : Controller
    {
        
        #region Methods

        /// <summary> Set parameters like sorting, paging, filtering & other additional fields for
        /// datatable </summary> <returns>return a dictionary collection of parameters</returns>
        protected Dictionary<string, string> GetParameters(List<string> columns)
        {
            var queryStrings = Request.Query;
            var sortingColumn = columns[Convert.ToInt32(queryStrings["iSortCol_0"])];
            var sortDir = queryStrings["sSortDir_0"];
            var pageStart = Convert.ToInt32(queryStrings["iDisplayStart"]);
            var pageSize = Convert.ToInt32(queryStrings["iDisplayLength"]) == 0 ? 10 : Convert.ToInt32(queryStrings["iDisplayLength"]);
            var pageIndex = pageStart == 0 ? 1 : (pageStart / pageSize) + 1;
            var searchBy = System.Net.WebUtility.UrlDecode(queryStrings["srchBy"]);
            var searchTxt = System.Net.WebUtility.UrlDecode(queryStrings["srchTxt"]);
            //var carrierVal = queryStrings["CarrierGuid"];
            var parameters = new Dictionary<string, string>
                {
                    {"sort_by", sortingColumn},
                    {"sort_order", sortDir},
                    {"results_per_page", pageSize.ToString()},
                    {"page_number", pageIndex.ToString()},
                    {"PageStart", pageStart.ToString()},
                };
            if (!string.IsNullOrWhiteSpace(searchBy))
            {
                //searchTxt = CheckIfDate(searchTxt);
                parameters.Add("Key", searchBy);
                parameters.Add("Value", string.IsNullOrEmpty(searchTxt) ? string.Empty : searchTxt.ToString().ToLower());
            }
            return parameters;
        }

        #endregion

    }
}

