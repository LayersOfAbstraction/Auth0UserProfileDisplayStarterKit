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
using Auth0.ManagementApi.Paging;
using Example.Auth0.AuthenticationApi.Services;
using System.Threading;
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Auth0UserProfileDisplayStarterKit.Data;

namespace SampleMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TeamContext _context;
        public IPagedList<Auth0.ManagementApi.Models.User> Users { get; private set; }
        private readonly IUserService _userService;

        public HomeController(TeamContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

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

        /// <summary>
        /// Display Auth0 users from list.
        /// </summary>
        /// <param name="cancellationToken">Notify application any action should be canceled</param>
        /// <returns></returns>
        public async Task <IActionResult> GetAllAuth0Users(CancellationToken cancellationToken)
        {            
            //Get new token using Auth0ManagementApi with IUserService to interface with Identity Model library.
            var allUsers = await _userService.GetUsersAsync(new GetUsersRequest(), new PaginationInfo(), cancellationToken);
            //Get all auth0 user's first name, last name and email
            var renderedUsers = allUsers.Select(u => new Auth0UserProfileDisplayStarterKit.ViewModels.User
            {                
                UserFirstName = u.FullName.Contains(' ') ? u.FullName.Split(' ')[0] : "no space",
                UserLastName = u.FullName.Contains(' ') ? u.FullName.Split(' ')[1] : "no space",
                UserContactEmail = u.Email
            }).ToList();
            return Json(renderedUsers);
        }

        public async Task OnGet(CancellationToken cancellationToken)
        {
            Users = await _userService.GetUsersAsync(new GetUsersRequest(), new PaginationInfo(), cancellationToken);
        }

        // POST: User/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("UserLastName,UserFirstName,UserIsLeader,UserContactEmail,UserPhoneNumber,UserAddress,UserPostCode,UserCountry,UserMobileNumber,UserState,UserLogInName,UserPassword")] Auth0UserProfileDisplayStarterKit.ViewModels.User user)
        {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            return View(user);
        }
    }
}
