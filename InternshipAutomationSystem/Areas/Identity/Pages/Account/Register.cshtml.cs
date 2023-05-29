// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Internship.DataAccess.Data;
using Internship.DataAccess.Repository.IRepository;
using Internship.Models;
using Internship.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InternshipAutomationSystem.Areas.Identity.Pages.Account
{
    
    
    [Authorize(Roles = SD.Role_Admin)]

    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [BindProperty]
            [Range(1, 3, ErrorMessage = "Please select a valid user type.")]
            public int UserType { get; set; }

            public string? Role { get; set; }
            public string? Coordinator { get; set; }
            public string Name { get; set; }

            public string PhoneNumber { get; set; }
            public string TC { get; set; }
            public string Faculty { get; set; }
            public string Department { get; set; }
            public string Class { get; set; }
            public bool IsApproved { get; set; }
            public bool HasTakenFirstInternship { get; set; }
            public bool HasTakenSecondInternship { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> RoleList { get; set; }

            [ValidateNever]
            public IEnumerable<SelectListItem> CoordinatorList { get; set; }

         
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Career_Center)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Coordinator)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Student)).GetAwaiter().GetResult();
            }
         
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            Input = new InputModel()
            {
                RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                }),
                
                CoordinatorList = _unitOfWork.Coordinators.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
              

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);
                var userRole = Input.Role ?? SD.Role_User_Student;
               
                switch (userRole)
                {
                    case SD.Role_User_Student:
                        var student = new Student_User
                        {
                            UserId = user.Id,
                            Email = Input.Email,
                            Name = Input.Name,
                            Department = Input.Department,
                            PhoneNumber = Input.PhoneNumber,
                            TC = Input.TC,
                            Faculty = Input.Faculty,
                            Class = Input.Class,
                            IsApproved = Input.IsApproved,
                            Status = "N/A",
                            CoordinatorId = 3,
                            HasTakenFirstInternship = Input.HasTakenFirstInternship,
                            HasTakenSecondInternship = Input.HasTakenSecondInternship,
                         
                        };

                        user.Status = "notyet";
                        _dbContext.Students.Add(student);
                        await _dbContext.SaveChangesAsync();
                        break;

                    case SD.Role_User_Coordinator:
                        var coordinator = new InternshipCoordinator_User
                        {
                            UserId = user.Id,
                            Email = Input.Email,
                            Name = Input.Name,
                            PhoneNumber = Input.PhoneNumber,
                        };

                        _dbContext.Coordinators.Add(coordinator);
                        await _dbContext.SaveChangesAsync();
                        break;

                    case SD.Role_Career_Center:
                        var careerCenter = new CareerCenter_User
                        {
                            UserId = user.Id,
                            Email = Input.Email,
                            Name = Input.Name,
                            PhoneNumber = Input.PhoneNumber,
                        };
                        _dbContext.CareerCenters.Add(careerCenter);
                        await _dbContext.SaveChangesAsync();
                        break;

                    case SD.Role_Admin:
                        var Admin = new Admin_User
                        {
                            UserId = user.Id,
                            Email = Input.Email,
                            Name = Input.Name,
                            PhoneNumber = Input.PhoneNumber,
                        };
                        _dbContext.Admins.Add(Admin);
                        await _dbContext.SaveChangesAsync();
                        break;

                    default:
                        // Handle other roles if needed
                        break;
                }
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (Input.Role == null)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_User_Student);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, Input.Role);
                    }

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
            
                        if (User.IsInRole(SD.Role_Admin))
                        {
                            TempData["success"] = "New User Created Successfully";
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);

                        }

                        //Step 5: Create the user - specific record in the respective user type table
                     
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private string GetUserTypeName(int userType)
        {
            switch (userType)
            {
                case 1: return "Student";
                case 2: return "Coordinator";
                case 3: return "CareerCenter";
                default: return string.Empty;
            }
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
