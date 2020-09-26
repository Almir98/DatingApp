using DatingApp.API.Data;
using DatingApp.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Models
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        
        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("Models/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var item in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash,out  passwordSalt);

                item.PasswordHash = passwordHash;
                item.PasswordSalt = passwordSalt;
                item.Username = item.Username.ToLower();

                _context.Users.Add(item);
            }
            _context.SaveChanges();
        }

    }
}
