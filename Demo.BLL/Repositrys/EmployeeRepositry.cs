using Demo.BLL.Interfaces;
using Demo.DAL.Data.Context;
using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositrys
{
   public class EmployeeRepositry : GenericRepositry<Employee> ,IEmployeeRepositry
    {
        

        public EmployeeRepositry(CompanyDbcontext dbcontext)
            :base(dbcontext)
        {
        }
       
        public IQueryable<Employee> GetByAddress(string address)
        {
         return  _dbcontext.Employees.Where(e => e.Address == address);
        }

        public IQueryable<Employee> GetByEmployeeByName(string name)
        {
            return  _dbcontext.Employees.Where(e => e.Name.ToLower()==name).Include(e => e.Department);
        }
   }
}
