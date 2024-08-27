using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IEmployeeRepositry EmployeeRepositry { get; set; }

        IDepartmentRepositry DepartmentRepositry { get; set;}

       Task<int> CompleteAsync();
    }
}
