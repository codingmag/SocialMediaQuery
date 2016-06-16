using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaQuery.Controllers
{
    using System.Threading.Tasks;

    using SocialMediaAdapters;

    using SocialMediaQuery.Models.Results;

    public class ResultsController : AsyncController
    {
        [ActionName("TwitterSearch")]
        public async Task<ActionResult> TwitterSearchAsync(string query)
        {
            TwitterAdapter.ConsumerKey = System.Web.HttpContext.Current.Application["TwitterConsumerKey"].ToString();
            TwitterAdapter.ConsumerSecret = System.Web.HttpContext.Current.Application["TwitterConsumerSecret"].ToString();
            var results = await TwitterAdapter.SearchAsync(query);

            var model = new TwitterSearch() { Query = query, ResultsXml = results };

            return this.View(model);
        }
    }
}