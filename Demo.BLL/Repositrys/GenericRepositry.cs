
using Demo.BLL.Interfaces;
using Demo.DAL.Data.Context;
using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositrys
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : BaseModel
    {
        private protected readonly CompanyDbcontext _dbcontext;

        public GenericRepositry(CompanyDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public  async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee))
            { 
                return (IEnumerable<T>) await _dbcontext.Employees/*.Include(d => d.Department).AsNoTracking()*/.ToListAsync();
            }
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public void Create(T Entity)
        {
           _dbcontext.Set<T>().Add(Entity);

           
        }

        public void Update(T Entity)
        {
            _dbcontext.Set<T>().Update(Entity);

        }

        public void Delete(T Entity)
        {
            _dbcontext.Set<T>().Remove(Entity);
   
        }

       
    }
}
