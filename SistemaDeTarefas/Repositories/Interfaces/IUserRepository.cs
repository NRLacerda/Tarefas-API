﻿using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // Repositorio é o conjunto de funções de um determinado Model
        Task<List<UserModel>> FetchAllUsers();
        Task<UserModel> FetchUser(int id);
        Task<UserModel> AddUser(UserModel user);
        Task<UserModel> UpdateUser(UserModel user, int id);
        Task<bool> DeleteUser(int id);
    }
}
