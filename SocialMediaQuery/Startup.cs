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
            var consumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"];
            var consumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"];

            if (string.IsNullOrEmpty(consumerKey) || string.IsNullOrEmpty(consumerSecret))
            {
                throw new NullReferenceException("Please check if TwitterConsumerKey and TwitterConsumerSecret values exist in the appSettings section of your web.config!");
            }

            System.Web.HttpContext.Current.Application["TwitterConsumerKey"] = consumerKey;
            System.Web.HttpContext.Current.Application["TwitterConsumerSecret"] = consumerSecret;
        }
    }
}
