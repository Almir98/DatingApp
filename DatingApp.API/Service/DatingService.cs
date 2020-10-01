using DatingApp.API.Data;
using DatingApp.API.Interface;
using DatingApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Service
{
    public class DatingService:IDating
    {
        private readonly DataContext _context;
        public DatingService(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(e => e.ID == id);
            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Include(e=>e.Photos).FirstOrDefaultAsync(e=>e.ID==id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return  await _context.Users.Include(e=>e.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
