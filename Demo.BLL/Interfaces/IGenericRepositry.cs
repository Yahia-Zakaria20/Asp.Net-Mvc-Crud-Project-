using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepositry<T> where T : BaseModel
    {
       Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        void Create(T Entity);

        void Update(T Entity);

        void Delete(T Entity);

        
    }
}
