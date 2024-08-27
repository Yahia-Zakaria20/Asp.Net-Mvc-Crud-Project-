using Demo.DAL.Entites;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] string? name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var role = await _roleManager.FindByNameAsync(name); //retuen AplicationUser
                var Role = new RoleViewModel()
                {

                    Id = role.Id,
                    RoleName = role.Name,
                };
                if (Role is null)
                    return Unauthorized();
                return View(new List<RoleViewModel>() { Role });
            }
            else
            {
                var rols = await _roleManager.Roles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    RoleName = r.Name,

                }).ToListAsync(); //return Iquarabel<ApplicationUser>
                return View(rols);
            }
        }


        public async Task<IActionResult> Details(string? Id, string ViewName = "Details")
        {
            if (string.IsNullOrEmpty(Id))
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
                return NotFound();

            //var UserVM = new RoleViewModel()
            //{

            //    Id = user.Id,
            //    RoleName= user.Name,
            //};

            var RoleVM = _mapper.Map<IdentityRole, RoleViewModel>(role);

            return View(ViewName, RoleVM);
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
        public async Task<IActionResult> Update([FromRoute] string? id, RoleViewModel RoleVM)
        {
            if (id != RoleVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {

                var role = await _roleManager.FindByIdAsync(RoleVM.Id);

                role.Name = RoleVM.RoleName;


                var resutlt = await _roleManager.UpdateAsync(role);
                if (!resutlt.Succeeded)
                    ModelState.AddModelError(string.Empty, "The Data not Updeted");
                else
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(RoleVM);
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
        public async Task<IActionResult> Delete(RoleViewModel RoleVM)
        {
            var result = new IdentityResult();

            try
            {
                // var model = _mapper.Map<RoleViewModel, IdentityRole>(RoleVM);

                var model  = await _roleManager.FindByIdAsync(RoleVM.Id);

                result = await  _roleManager.DeleteAsync(model);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(RoleVM);
            }


            return result.Succeeded == true ? RedirectToAction(nameof(Index)) /*true*/: /*Else*/ RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel RoleVM)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<RoleViewModel, IdentityRole>(RoleVM);
                 await   _roleManager.CreateAsync(model);
                    return RedirectToAction(nameof(Index)); 
            }
            return View(RoleVM);
        }
    }

}

