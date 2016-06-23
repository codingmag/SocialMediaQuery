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
            model.DataSources = new List<DataSource>()
                                    {
                                        new DataSource() { Id = 1, Name = "Twitter" }, 
                                        new DataSource() { Id = 2, Name = "Instagram" },
                                        new DataSource() { Id = 3, Name = "Google+" },
                                        new DataSource() { Id = 4, Name = "YouTube" }
                                    };
            return this.View(model);
        }

        public ActionResult RouteToController(string query, int dataSourceId)
        {
            switch (dataSourceId)
            {
                case 1:
                    return this.RedirectToAction("TwitterSearch", "Results", new { query = query });
                case 2:
                    return this.RedirectToAction("InstagramAuth", "Results", new { query = query });
                case 3:
                    return this.RedirectToAction("GooglePlusSearch", "Results", new { query = query });
                case 4:
                    return this.RedirectToAction("YouTubeSearch", "Results", new { query = query });
            }

            return this.RedirectToAction("Index");
        }
    }
}