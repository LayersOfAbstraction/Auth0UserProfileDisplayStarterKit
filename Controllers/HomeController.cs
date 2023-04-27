using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using Auth0UserProfileDisplayStarterKit.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Auth0UserProfileDisplayStarterKit.Controllers
{
    public class HomeController : Controller
    {
        public IPagedList<Auth0.ManagementApi.Models.User> Users { get; private set; }
        private readonly IUserService _userService;

        private readonly TeamContext _context;

        //Add the context to the constructor. Don't make another constructor. Use the one
        //that is already there.
        public HomeController(TeamContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IActionResult Index()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "UserFullname", null);
            return View();
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
