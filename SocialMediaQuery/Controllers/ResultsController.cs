using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMediaQuery.Controllers
{
    using System.Globalization;
    using System.Threading.Tasks;

    using InstaSharp;

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

        [ActionName("InstagramAuth")]
        public async Task<ActionResult> InstagramAuthAsync(string query)
        {
            var consumerKey = System.Web.HttpContext.Current.Application["InstagramClientKey"].ToString();
            var consumerSecret = System.Web.HttpContext.Current.Application["InstagramClientSecret"].ToString();
            var redirecturl = this.Request.Url.AbsoluteUri.Replace("InstagramAuth", "InstagramSearch");
            redirecturl = redirecturl.Replace(query, "?query=" + query);
            InstagramAdapter.InstagramConfig = new InstagramConfig(
                consumerKey,
                consumerSecret,
                redirecturl,
                string.Empty);

            return this.Redirect(InstagramAdapter.GetLoginLink());
        }
        
        [ActionName("InstagramSearch")]
        public async Task<ActionResult> InstagramSearchAsync(string query, string code)
        {
            var model = new InstagramSearch() { Query = query, ResultsXml = string.Empty };

            if (string.IsNullOrEmpty(query))
            {
                return this.View(model);
            }

            query = query.Replace(" ", string.Empty);
            
            var oauthResponse = InstagramAdapter.GetOAuthResponse(code);
            
            if (oauthResponse == null || oauthResponse.User == null || oauthResponse.AccessToken == null)
            {
                return this.View(model);
            }

            if (query.StartsWith("lat:", StringComparison.InvariantCulture))
            {
                if (!query.Contains(',') || !query.ToLower().Contains("lat:") || !query.ToLower().Contains("lon:"))
                {
                    return this.View(model);
                }

                var coordinates = query.Split(',');
                var lat = Convert.ToDouble(coordinates[0].Replace("lat:", string.Empty), CultureInfo.InvariantCulture);
                var lon = Convert.ToDouble(coordinates[1].Replace("lon:", string.Empty), CultureInfo.InvariantCulture);
                model.ResultsXml = await InstagramAdapter.SearchAsync(lat, lon, oauthResponse);
            }
            else
            {
                model.ResultsXml = await InstagramAdapter.TagsAsync(query, oauthResponse);
            }

            return this.View(model);
        }
    }
}