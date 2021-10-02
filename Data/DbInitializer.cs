using System.Linq;
using Auth0UserProfileDisplayStarterKit.ViewModels;

namespace Auth0UserProfileDisplayStarterKit.Data
{
public static class DbInitializer
    {
        public static void Initialize(TeamContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{UserFirstName="Carson",UserLastName="Alexander",UserContactEmail="jnash486+am15@gmail.com"},
            new User{UserFirstName="Arturo",UserLastName="Anand",UserContactEmail="jnash486+am15@gmail.com"},
            new User{UserFirstName="Carson",UserLastName="Alexander",}
            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();
        }
    }
}