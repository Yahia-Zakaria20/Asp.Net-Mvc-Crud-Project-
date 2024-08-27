using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
            CreateMap<RoleViewModel,IdentityRole>()
                .ForMember(R => R.Name,O => O.MapFrom(r => r.RoleName))
                .ReverseMap();
        }
    }
}
