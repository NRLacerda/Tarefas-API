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
        public async Task<List<UserModel>> FetchAllUsers()
        {
            return await dbcontex.Users.ToListAsync();
        }

        public async Task<UserModel> FetchUser(int id)
        {
            return await dbcontex.Users.FirstOrDefaultAsync(x=>x.Id == id);
        }
        public async Task<UserModel> AddUser(UserModel user)
        {
            await dbcontex.Users.AddAsync(user);
            dbcontex.SaveChanges();

            return user; 
        }
        public async Task<UserModel> UpdateUser(UserModel user, int id)
        {
            UserModel actualUser = await FetchUser(id);
            if(actualUser == null) 
            {
                throw new Exception($"Usuario para o id {id} não encontrado.");
            }
            else
            {
                actualUser.Name = user.Name;
                actualUser.Password = user.Password;
                actualUser.Email = user.Email;
                actualUser.EmailConfirmed = user.EmailConfirmed;
                dbcontex.Update(actualUser);
                dbcontex.SaveChanges();
                return actualUser;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            UserModel actualUser = await FetchUser(id);
            if (actualUser == null)
            {
                throw new Exception($"Usuario para o id {id} não encontrado.");
            }
            else
            {
                dbcontex.Remove(actualUser);
                dbcontex.SaveChanges();
                return true;
            }
        }
    }
}
