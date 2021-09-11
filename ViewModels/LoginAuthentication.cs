using Auth0.AuthenticationApi;

namespace Auth0UserProfileDisplayStarterKit.ViewModels
{
    public class LoginAuthentication
    {
        public static Auth0Token Login(string ClientID, string ClientSecret, string domain)
        {
            var authenticationApiClient = new AuthenticationApiClient(domain);
            var token =  authenticationApiClient.GetTokenAsync(new Auth0.AuthenticationApi.Models.ClientCredentialsTokenRequest
            {                
                ClientId = ClientID,
                ClientSecret = ClientSecret,
                Audience = "https://dev-dgdfgfdgf324.au.auth0.com/api/v2/"
            }).Result;
            return new Auth0Token {strAuth0Token = token.AccessToken};   
        }
    }
}