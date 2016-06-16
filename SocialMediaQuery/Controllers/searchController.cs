using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaQuery.Controllers
{
    using System.Web.Routing;

    using SocialMediaQuery.Models;
    using SocialMediaQuery.Models.Search;

    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            var model = new Index();
            model.DataSources = new List<DataSource>() { new DataSource() { Id = 1, Name = "Twitter" } };
            return this.View(model);
        }

        public ActionResult RouteToController(string query, int dataSourceId)
        {
            if (dataSourceId == 1)
            {
                return this.RedirectToAction("TwitterSearch", "Results", new { query = query });
            }

            return this.RedirectToAction("Index");
        }
    }
}