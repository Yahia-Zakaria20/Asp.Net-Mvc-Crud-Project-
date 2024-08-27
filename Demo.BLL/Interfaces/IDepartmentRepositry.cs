﻿using Demo.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IDepartmentRepositry:IGenericRepositry<Department>
    {
        public IQueryable<Department> GetByName(string Name);

    }
}
