using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepositry : IGenericRepositry<Employee>
    {
        public IQueryable<Employee> GetByAddress(string name);

        public IQueryable<Employee> GetByEmployeeByName(string address);
    }
}
