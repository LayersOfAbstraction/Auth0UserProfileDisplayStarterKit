using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Auth0UserProfileDisplayStarterKit.Controllers
{
    public class HomeController : Controller
    {
        public IPagedList<Auth0.ManagementApi.Models.User> Users { get; private set; }
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> GetAllAuth0Users(CancellationToken cancellationToken)
        {
            //Get new token using Auth0ManagementApi with IUserService to interface with Identity Model library.
            var allUsers = await _userService.GetUsersAsync(new GetUsersRequest(), new PaginationInfo(), cancellationToken);
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
    }
}
