using AutoMapper;
using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositrys;
using Demo.DAL.Entites;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        /*private IDepartmentRepositry _departmentRepo;*/ // null
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork,
                  IMapper mapper
            /*IDepartmentRepositry department*/)
        {
            //_departmentRepo = department;
           _unitOfWork = unitOfWork;
           _mapper = mapper;
        }


        //Master Page DepartmentController
        public async Task<IActionResult> Index(string Name)
        {
            var departments =Enumerable.Empty<Department>();
            if (string.IsNullOrEmpty(Name))
            {
                departments =await _unitOfWork.DepartmentRepositry.GetAllAsync();

            }
            else 
            {
                departments = _unitOfWork.DepartmentRepositry.GetByName(Name.ToLower());
            }

            var DeptVM = _mapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(departments);
            return View(DeptVM);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var deptModel = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

              _unitOfWork.DepartmentRepositry.Create(deptModel);

                var row = await _unitOfWork.CompleteAsync();
                if (row >  0) 
                { 
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(departmentVM);
        }
        // Base Url /Department/Details/10

        // Base Url /Department/Details
        [HttpGet]
        public async Task<IActionResult> Details (int?Id, string ViewName = "Details") 
        {
            if (!Id.HasValue)
                return BadRequest();

            var department = await _unitOfWork.DepartmentRepositry.GetByIdAsync(Id.Value);

            var DeptVM = _mapper.Map<Department, DepartmentViewModel>(department);

            if(department is null)
                return NotFound();

            return View(ViewName, DeptVM);
        }




        [HttpGet]
        
        public async Task<IActionResult> Update(int? Id)
        {
           ///if (!Id.HasValue)
           ///    return BadRequest();
           ///var department = _departmentRepo.GetById(Id.Value);
           ///if (department is null)
           ///    return NotFound();
           ///return View(department);

            return await Details(Id, "Update");
        }





        [HttpPost]

        public async Task<IActionResult> Update([FromRoute]int Id ,DepartmentViewModel departmentVM,string ViewName = "Update")
        {
            if(Id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var deptModel = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DepartmentRepositry.Update(deptModel);
                var RowEffect =await _unitOfWork.CompleteAsync();
                if (RowEffect  > 0)
                    return RedirectToAction(nameof(Index));   
            }

            return View(ViewName,departmentVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete (int? Id) 
        {
            return await Details(Id,"Delete");
        }


        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int Id,DepartmentViewModel departmentVM)
        {
            if (Id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                var DeptModel = _mapper.Map<DepartmentViewModel,Department>(departmentVM);  
                 _unitOfWork.DepartmentRepositry.Delete(DeptModel);
                var RowEffect = await _unitOfWork.CompleteAsync();
                if (RowEffect > 0)
                    return RedirectToAction(nameof(Index));
            }

            return View(departmentVM);
        }

    }
}
