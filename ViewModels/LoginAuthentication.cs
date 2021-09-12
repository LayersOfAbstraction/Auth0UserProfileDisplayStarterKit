using System.IO;
using Auth0.AuthenticationApi;
using Microsoft.Extensions.Configuration;
namespace Auth0UserProfileDisplayStarterKit.ViewModels
{
    public class LoginAuthentication
    {
      public static IConfiguration AppSetting { get; }
        static LoginAuthentication()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
        public static Auth0Token Login(string ClientID, string ClientSecret, string domain, string Audience)
        {
            var authenticationApiClient = new AuthenticationApiClient(domain);
            var token =  authenticationApiClient.GetTokenAsync(new Auth0.AuthenticationApi.Models.ClientCredentialsTokenRequest
            {                
                ClientId = ClientID,
                ClientSecret = ClientSecret,
                Audience = LoginAuthentication.AppSetting["AccessTokenManagement:Audience"],
            }).Result;
            return new Auth0Token {strAuth0Token = token.AccessToken};   
        }
    }
}