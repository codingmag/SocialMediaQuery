namespace SocialMediaAdapters
{
    using System.Net.Http;

    using InstaSharp;
   
    using InstaSharp.Models.Responses;

    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Threading.Tasks;

    using InstaSharp.Models;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class InstagramOAuth : OAuth
    {
        private readonly InstagramConfig config;
        
        public InstagramOAuth(InstagramConfig config) : base(config)
        {
            this.config = config;
        }

        public OAuthResponse RequestToken(string code)
        {
            NameValueCollection parameters = new NameValueCollection
                                                 {
                                                     { "client_id", this.config.ClientId },
                                                     { "client_secret", this.config.ClientSecret },
                                                     { "grant_type", "authorization_code" },
                                                     {
                                                         "redirect_uri",
                                                         this.config.RedirectUri
                                                     },
                                                     { "code", code }
                                                 };

            var client = new WebClient();
            var result = client.UploadValues("https://api.instagram.com/oauth/access_token", "POST", parameters);
            var response = System.Text.Encoding.Default.GetString(result);
            var responseObject = (JObject)JsonConvert.DeserializeObject(response);

            return new OAuthResponse()
                       {
                           AccessToken = (string)responseObject["access_token"],
                           User = new UserInfo()
                                    {
                                        Id = Convert.ToInt64(responseObject["user"]["id"]),
                                        Username = responseObject["user"]["username"].ToString(),
                                        FullName = responseObject["user"]["full_name"].ToString(),
                                        ProfilePicture = responseObject["user"]["profile_picture"].ToString()
                                    }
                       };
        }
    }
}
