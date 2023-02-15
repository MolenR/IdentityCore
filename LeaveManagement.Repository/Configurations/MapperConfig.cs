using AutoMapper;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;

namespace LeaveManagement.Repository.Configurations;

public class MapperConfig : Profile
{
    /* Mapping Configuration */
    public MapperConfig() 
    {
        /* Tell AutoMapper it's legal to convert type a => type b */

        CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();
        CreateMap<Employee, EmployeeListViewModel>().ReverseMap();
        CreateMap<Employee, EmployeeAllocationViewModel>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationEditViewModel>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestCreateViewModel>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestViewModel>().ReverseMap();

    }
}
