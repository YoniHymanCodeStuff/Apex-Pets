using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace API.helpers
{
    public class AutomapperProfiles : Profile
    {
       
     public AutomapperProfiles()
     {
        //CreateMap<Model,Dto>();
     }   
    }
}

//not sure why I shouldn't just move this to services... or utilities. seems like a 
//lot of folders are synonymous. 