
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data.DataAccess.UnitOfWork;
using API.Data.Model;


namespace API.Data.utils
{
    public class Seed
    {
        public static async Task SeedBaseData(IUoW uow){
             

            if(await uow.animals.TableIsNotEmpty()){
                return;
            } 

            var animalData = await System.IO.File.ReadAllTextAsync("assets/seeding.json");
            
            var animals = JsonSerializer.Deserialize<List<Animal>>(animalData);

            using var hmac = new HMACSHA512();

            var admin= new Admin{
                
                UserName = "admin",
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin")),
                salt = hmac.Key
        
            };

            uow.admins.Add(admin);

            uow.animals.AddRange(animals);
          
            await uow.Complete();
          
        }

    }
}