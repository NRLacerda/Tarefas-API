using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagerDBContext dbcontex; 
        public UserRepository(TaskManagerDBContext taskmanagersysDBContext) 
        { 
         dbcontex= taskmanagersysDBContext;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await dbcontex.Users.ToListAsync();
        }

        public async Task<User> AddUser(User user)
        {
            await dbcontex.Users.AddAsync(user);
            await dbcontex.SaveChangesAsync();

            return user; 
        }
    }
}
