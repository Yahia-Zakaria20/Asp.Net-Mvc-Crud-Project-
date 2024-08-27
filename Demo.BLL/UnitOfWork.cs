using Demo.BLL.Interfaces;
using Demo.BLL.Repositrys;
using Demo.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbcontext _dbcontext;

        public IEmployeeRepositry EmployeeRepositry { get ; set ; }

        public IDepartmentRepositry DepartmentRepositry { get; set; }

        public UnitOfWork(CompanyDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
            EmployeeRepositry = new EmployeeRepositry(dbcontext);
            DepartmentRepositry = new DepartmentRepositry(dbcontext);
        }

        public async Task<int> CompleteAsync()
        {
          return await _dbcontext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
          await  _dbcontext.DisposeAsync();
        }
    }
}
