using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DataAccess;
using API.Data.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PetShop.PetShopBackend.API.Extensions
{

    //I should maybe add all application services to here to tidy things up. 
    //currently this is not in use. 
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
        services.AddScoped<IUoW, UoW>();  
   
            
        return services;
        }
    }
}