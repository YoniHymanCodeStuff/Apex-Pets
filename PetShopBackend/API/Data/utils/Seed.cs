using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data.DataAccess;
using API.Data.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace API.Data.utils
{
    public class Seed
    {
        //basic functions to populate the db:
        //eventually this should probably be using the UoW and not operating 
        //directly on the context.  

        public static async Task SeedAnimals(IUoW uow){
             

            if(await uow.animals.TableIsNotEmpty()){
                return;
            } 

            //getting the json:
            var animalData = await System.IO.File.ReadAllTextAsync("assets/seeding.json");

            //converting it:
            var animals = JsonSerializer.Deserialize<List<Animal>>(animalData);

            //victor added username pw and hash here to each item. we might need
            //to do this to users in the future:

            // foreach(var a in animals)
            // {
            // //     using var hmac = new HMACSHA512();
            // //     a.field = a.field.Tolower();
            // //     a.passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            // //     a.passwordsalt = hmac.Key;

            //     ctx.Animals.Add(a);
            
            // }


            uow.animals.AddRange(animals);
            //ctx.Animals.AddRange(animals);

            await uow.Complete();
            //await ctx.SaveChangesAsync();

        }

    }
}