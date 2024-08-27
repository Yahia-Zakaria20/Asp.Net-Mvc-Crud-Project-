using Demo.DAL.Entites;
using Demo.PL.Helpers;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public UserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}




		public async Task<IActionResult> Index([FromQuery]string? email) 
		{
			if (!string.IsNullOrEmpty(email))
			{
				var user = await _userManager.FindByEmailAsync(email); //retuen AplicationUser
				var UserVm = new UserViewModel()
				{
					Email = user.Email,
					UserName = user.UserName,
					FName = user.Fname,
					LName = user.Lname,
					PhoneNumber = user.PhoneNumber,
					Id = user.Id,
					Roles = _userManager.GetRolesAsync(user).Result
				};
				if(UserVm is null)
					return Unauthorized();
				return View(new List<UserViewModel>() {UserVm});
			}
			else 
			{
				var Users =await _userManager.Users.Select( u => new UserViewModel() {

					Email = u.Email,
					UserName = u.UserName,
					FName = u.Fname,
					LName = u.Lname,
					Id = u.Id,
                    PhoneNumber = u.PhoneNumber,
                    Roles =  _userManager.GetRolesAsync(u).Result


				}).ToListAsync(); //return Iquarabel<ApplicationUser>
				return View(Users);
			}
		}


		public async Task<IActionResult> Details(string? Id, string ViewName = "Details") 
		{
			if (string.IsNullOrEmpty(Id))
				return BadRequest();
		  	var user =await _userManager.FindByIdAsync(Id);
			if (user == null)
				return NotFound();

			var UserVM = new UserViewModel() 
			{
                Email = user.Email,
                UserName = user.UserName,
                FName = user.Fname,
                LName = user.Lname,
                Id = user.Id,
				PhoneNumber = user.PhoneNumber,
                Roles = _userManager.GetRolesAsync(user).Result
            };

			return View(ViewName, UserVM);
		}

	
		public async Task<IActionResult> Update(string? Id)
		{
			///if (!id.HasValue)
			///    return BadRequest();
			///var emp = _employeeRepo.GetById(id.Value);
			///if (emp is null)
			///    return NotFound();
			///return View(emp);
			///


			return await Details(Id, "Update");
		}

		[HttpPost]
		public async Task<IActionResult> Update([FromRoute]string? id,UserViewModel employeevm)
		{
			if (id != employeevm.Id)
                return BadRequest();

                if (ModelState.IsValid)
				{

					var User = await _userManager.FindByIdAsync(employeevm.Id);

					User.Fname = employeevm.FName;
					User.Lname = employeevm.LName;
					User.PhoneNumber = employeevm.PhoneNumber;


					var resutlt = await _userManager.UpdateAsync(User);
					if (!resutlt.Succeeded)
						ModelState.AddModelError(string.Empty, "The Data not Updeted");
					else
					{
						return RedirectToAction(nameof(Index));
					}

				}
				return View(employeevm);	
		}
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            ///if (!id.HasValue)
            ///    return BadRequest();
            ///var emp = _employeeRepo.GetById(id.Value);
            ///if (emp is null)
            ///    return NotFound();
            ///return View(emp);
            ///

            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel employeevm)
        {
			var result = new IdentityResult();
           
            try
            {
				var user = await _userManager.FindByIdAsync(employeevm.Id);

				result =  await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeevm);
            }


            return result.Succeeded ==  true ? RedirectToAction(nameof(Index)) /*true*/: /*Else*/ RedirectToAction(nameof(Index));
        }
    }
}
