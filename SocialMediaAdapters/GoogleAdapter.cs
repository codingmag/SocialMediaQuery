using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAdapters
{
    using System.ServiceModel.Discovery;
    using System.Web.Mvc;
    using System.Web.UI.WebControls;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Auth.OAuth2.Mvc;
    using Google.Apis.Blogger.v3;
    using Google.Apis.Plus.v1;
    using Google.Apis.Services;
    using Google.Apis.YouTube.v3;

    public static class GoogleAdapter
    {
        public static string ApiKey;

        public static string SearchGooglePlus(string query)
        {
            var service = new PlusService(new Google.Apis.Services.BaseClientService.Initializer() { ApiKey = ApiKey });
            var activities = service.Activities.Search(query).Execute().Items;

            var serializedResults = new StringBuilder();

            foreach (var resultItem in activities)
            {
                serializedResults.AppendFormat(
                        "<blockquote class=\"twitter-tweet\"><p>{0}</p><p><a href=\"{1}\"><img src=\"{2}\"/>{3}</a> - {4}</p></blockquote>",
                        resultItem.Title,
                        resultItem.Url,
                        resultItem.Actor.Image.Url,
                        resultItem.Actor.DisplayName,
                        resultItem.Published);
            }

            return serializedResults.ToString();
        }

        public static string SearchYouTube(string query)
        {
            var service = new YouTubeService(new Google.Apis.Services.BaseClientService.Initializer() { ApiKey = ApiKey });
            var videoRequest = service.Search.List("snippet");
            videoRequest.Q = query;
            videoRequest.MaxResults = 50;

            var videos = videoRequest.Execute().Items;

            var serializedResults = new StringBuilder();

            foreach (var resultItem in videos)
            {
                serializedResults.AppendFormat(
                        "<blockquote class=\"twitter-tweet\"><p>{0}</p><p><a href=\"https://www.youtube.com/watch?v={1}\"><img src=\"{2}\"/>{3}</a> - {4}</p></blockquote>",
                        resultItem.Snippet.Title,
                        resultItem.Id.VideoId,
                        resultItem.Snippet.Thumbnails.Medium.Url,
                        resultItem.Snippet.ChannelTitle,
                        resultItem.Snippet.PublishedAt);
            }

            return serializedResults.ToString();
        }
    }
}
