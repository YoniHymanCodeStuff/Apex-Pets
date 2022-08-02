using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.DTOs;
using API.Data.Model;
using AutoMapper;


namespace API.helpers
{
    public class AutomapperProfiles : Profile
    {
       
     public AutomapperProfiles()
     {
        CreateMap<User,BaseUserDto>()
        .Include<Customer,CustomerDto>();

        CreateMap<Customer,CustomerDto>();

       CreateMap<CustomerUpdateDto,Customer>();

       CreateMap<Animal,CartAnimalDto>();

       CreateMap<Order,OrderDto>()
       .ForMember(dest=>dest.OrderTimeStamp, opt=>opt.MapFrom(src=>FormatDate(src.OrderTimeStamp)))
       .ForMember(dest=>dest.DeliveryTimeStamp, opt=>opt.MapFrom(src=>FormatDate(src.DeliveryTimeStamp)));
    
      CreateMap<CreateAnimalDto,Animal>();
     }   

     private string FormatDate(DateTime dt)
     {
      
      return dt.ToString("dd/MM/yyyy HH:mm");
     }
    }
}

//not sure why I shouldn't just move this to services... or utilities. seems like a 
//lot of folders are synonymous. 