using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        

        public ExceptionMiddleware(RequestDelegate next,
         ILogger<ExceptionMiddleware> logger,
         IHostEnvironment env)
        {
            this.next = next;
            //next is the reference to the next middleware in the middleware chain. 
            this.logger = logger;
            //from context i think logger is to async console.write from the middlewares. 
            this.env = env;
            //Ihost can check if you are in a development or production env. 
        }

        public async Task InvokeAsync(HttpContext context )
        {
            //HttpContext holds all the data about the current http call

            //here we want to just pass the httpcontext through to the next middleware
            try
            {
                //here if everything goes fine, just pass the context on and do nothing. 
                await next(context);
            } 
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);

                //here we are editing the response headers:
                context.Request.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                
                //for production:
                var response = new ApiException(context.Response.StatusCode,"Internal Server Error");

                if (env.IsDevelopment())
                {
                    response = new ApiException(context.Response.StatusCode,ex.Message,ex.StackTrace?.ToString());
                }

   
                var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var Json = JsonSerializer.Serialize(response, options);
                    
                await context.Response.WriteAsync(Json);
            }
        }
    }
}