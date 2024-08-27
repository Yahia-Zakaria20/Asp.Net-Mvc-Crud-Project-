using AutoMapper;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositrys;
using Demo.DAL.Entites;
using Demo.PL.Helpers;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IEmployeeRepositry _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork
            /*IEmployeeRepositry employee*/
            ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_employeeRepo = employee;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchByName)
        {
            var Employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(SearchByName)) 
            {
                Employees = await _unitOfWork.EmployeeRepositry.GetAllAsync();
                
            }
            else 
            {
                 Employees = _unitOfWork.EmployeeRepositry.GetByEmployeeByName(SearchByName.ToLower());
               
            }
            var Employeesvm = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
            return View(Employeesvm);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            //ViewData["GetAllDept"] = _departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeevm)
        {
            if (ModelState.IsValid) 
            {
                employeevm.ImageName = await DocumentSetting.UplodeFile(employeevm.Image,"Images");
               var Model = _mapper.Map<EmployeeViewModel, Employee>(employeevm);
               _unitOfWork.EmployeeRepositry.Create(Model);
                var roweffect = await _unitOfWork.CompleteAsync();

                //update Department
                //Delete Project 

                if (roweffect > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(employeevm);
        }
        // /Employee/Details/10
        // /Employee/Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id,string ViewName = "Details") 
        {
                if (!id.HasValue)
                    return BadRequest();
                var emp = await _unitOfWork.EmployeeRepositry.GetByIdAsync(id.Value);
            var EmployeeVm = _mapper.Map<Employee, EmployeeViewModel>(emp);
                if (emp is null)
                    return NotFound();
                return View(ViewName, EmployeeVm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id) 
        {
           ///if (!id.HasValue)
           ///    return BadRequest();
           ///var emp = _employeeRepo.GetById(id.Value);
           ///if (emp is null)
           ///    return NotFound();
           ///return View(emp);
           ///


            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel employeevm)
        {
            if (ModelState.IsValid)
            {
                var currentName = employeevm.ImageName;

                if (employeevm.Image is not null)
                  employeevm.ImageName =await DocumentSetting.UplodeFile(employeevm.Image, "Images");

                if (currentName is not null && currentName != employeevm.ImageName)
                    DocumentSetting.DeleteFile(currentName, "Images");

                var Model = _mapper.Map<EmployeeViewModel, Employee>(employeevm);
                _unitOfWork.EmployeeRepositry.Update(Model);
                var roweffect =await _unitOfWork.CompleteAsync();
                if (roweffect > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(employeevm);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ///if (!id.HasValue)
            ///    return BadRequest();
            ///var emp = _employeeRepo.GetById(id.Value);
            ///if (emp is null)
            ///    return NotFound();
            ///return View(emp);
            ///

            return await Details(id,"Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeevm)
        {
            var roweffect = 0;
           if (employeevm.ImageName is not null)
            DocumentSetting.DeleteFile(employeevm.ImageName, "Images");
            var Model = _mapper.Map<EmployeeViewModel, Employee>(employeevm);
            try
            {
                _unitOfWork.EmployeeRepositry.Delete(Model);
                 roweffect = await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {

               ModelState.AddModelError(string.Empty,ex.Message);
                return View(employeevm);
            }
          

            return roweffect > 0 ? RedirectToAction(nameof(Index)) /*true*/: /*Else*/ RedirectToAction(nameof(Index));
        }

    }
}
