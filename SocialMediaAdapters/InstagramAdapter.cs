using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAdapters
{
    using InstaSharp;
    using InstaSharp.Models;
    using InstaSharp.Models.Responses;

    public static class InstagramAdapter
    {
        public static InstagramConfig InstagramConfig;

        public static string GetLoginLink()
        {
            var scopes = new List<OAuth.Scope>();
            scopes.Add(InstaSharp.OAuth.Scope.Public_Content);

            return InstaSharp.OAuth.AuthLink(InstagramConfig.OAuthUri + "authorize", InstagramConfig.ClientId, InstagramConfig.RedirectUri, scopes, InstaSharp.OAuth.ResponseType.Code);
        }

        public static OAuthResponse GetOAuthResponse(string code)
        {
            var auth = new InstagramOAuth(InstagramConfig);
            var oauthResponse = auth.RequestToken(code);

            return oauthResponse;
        }

        public static async Task<string> SearchAsync(double latitude, double longitude, OAuthResponse oAuthResponse)
        {
            var media = new InstaSharp.Endpoints.Media(InstagramConfig, oAuthResponse);
            var searchResults = await media.Search(latitude, longitude, 5000);

            return SerializeResults(searchResults.Data);
        }

        public static async Task<string> TagsAsync(string query, OAuthResponse oAuthResponse)
        {
            var tags = new InstaSharp.Endpoints.Tags(InstagramConfig, oAuthResponse);
            var searchResults = await tags.Recent(query);

            return SerializeResults(searchResults.Data);
        }

        private static string SerializeResults(List<Media> mediaList)
        {
            var serializedResults = new StringBuilder();

            foreach (var resultItem in mediaList)
            {
                serializedResults.AppendFormat(
                        "<blockquote class=\"twitter-tweet\"><p>{0}</p><p><a href=\"{1}\"><img src=\"{2}\"/>{3}</a> - {4}</p></blockquote>",
                        resultItem.Caption.Text,
                        resultItem.Images.StandardResolution.Url,
                        resultItem.Images.Thumbnail.Url,
                        resultItem.User.Username,
                        resultItem.CreatedTime.Date);
            }

            return serializedResults.ToString();
        }
    }
}
