using Demo.BLL.Interfaces;
using Demo.DAL.Data.Context;
using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositrys
{
    public class DepartmentRepositry : GenericRepositry<Department> , IDepartmentRepositry
    {
     
        public DepartmentRepositry(CompanyDbcontext dbcontext):base(dbcontext)
        {
            
        }

        public IQueryable<Department> GetByName(string Name) 
        {
           return _dbcontext.Departments.Where(D => D.Name.ToLower() == Name);
        }





    }
}
