using System;

<<<<<<< Updated upstream:Models/AccessTokenCache.cs
namespace Auth0UserProfileDisplayStarterKit.Models
{ 
=======
namespace Auth0UserProfileDisplayStarterKit.ViewModels
{
>>>>>>> Stashed changes:ViewModels/AccessTokenCache.cs
    internal partial class AccessTokenCache
    {
        public string Id { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset ExpiresAtTime { get; set; }
        public long? SlidingExpirationInSeconds { get; set; }
        public DateTimeOffset? AbsoluteExpiration { get; set; }
    }
}

