using Microsoft.Owin;

[assembly: OwinStartup(typeof(SocialMediaQuery.Startup))]

namespace SocialMediaQuery
{
    using System;
    using System.Configuration;

    using Owin;

    using SocialMediaAdapters;

    /// <summary>
    /// Class for Start up
    /// </summary>   
    public class Startup
    {
        /// <summary>
        /// Method to start configuration
        /// </summary>        
        /// <param name="app">The app.</param>
        public void Configuration(IAppBuilder app)
        {
            var twitterConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
            var twitterConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];
            var instagramConsumerKey = ConfigurationManager.AppSettings["InstagramClientKey"];
            var instagramConsumerSecret = ConfigurationManager.AppSettings["InstagramClientSecret"];
            var googleApiKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

            if (string.IsNullOrEmpty(twitterConsumerKey) || string.IsNullOrEmpty(twitterConsumerSecret))
            {
                throw new NullReferenceException("Please check if TwitterConsumerKey and TwitterConsumerSecret values exist in the appSettings section of your web.config!");
            }

            if (string.IsNullOrEmpty(instagramConsumerKey) || string.IsNullOrEmpty(instagramConsumerSecret))
            {
                throw new NullReferenceException("Please check if InstagramClientKey and InstagramClientSecret values exist in the appSettings section of your web.config!");
            }

            if (string.IsNullOrEmpty(googleApiKey))
            {
                throw new NullReferenceException("Please check if GoogleApiKey exists in the appSettings section of your web.config!");
            }

            System.Web.HttpContext.Current.Application["TwitterConsumerKey"] = twitterConsumerKey;
            System.Web.HttpContext.Current.Application["TwitterConsumerSecret"] = twitterConsumerSecret;
            System.Web.HttpContext.Current.Application["InstagramClientKey"] = instagramConsumerKey;
            System.Web.HttpContext.Current.Application["InstagramClientSecret"] = instagramConsumerSecret;
            System.Web.HttpContext.Current.Application["GoogleAPIKey"] = googleApiKey;
        }
    }
}
