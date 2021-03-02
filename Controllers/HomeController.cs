using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SampleMvcApp.ViewModels;
using Auth0.ManagementApi;
using Auth0UserProfileDisplayStarterKit.ViewModels;

namespace SampleMvcApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task <IActionResult> GetAllAuth0Users()
		{
			//Get token
			var apiClient = new ManagementApiClient(Auth0UserProfileDisplayStarterKit.ViewModels.ConstantStrings.strToken, new Uri ("https://dev-dgdfgfdgf324.au.auth0.com/api/v2/"));
			//Get all auth0 users that have this application
            var allUsers = await apiClient.Users.GetAllAsync(new Auth0.ManagementApi.Models.GetUsersRequest(), new Auth0.ManagementApi.Paging.PaginationInfo());
			//Read each Auth0 user by field
            var renderedUsers = allUsers.Select(u => new User
			{                
                //Split their full name into first and last name if there is a space
				UserFirstName = u.FullName.Contains(' ') ? u.FullName.Split(' ')[0] : "no space",
				UserLastName = u.FullName.Contains(' ') ? u.FullName.Split(' ')[1] : "no space",
				//Get user profile email
                UserContactEmail = u.Email
			}).ToList();

			return Json(renderedUsers);
		}
    }
}
