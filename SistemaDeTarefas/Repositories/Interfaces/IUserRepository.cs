using SistemaDeTarefas.Models;

namespace SistemaDeTarefas.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // Repositorio é o conjunto de funções de um determinado Model
        Task<List<User>> GetAllUsers();
        Task<User> AddUser(User user);
    }
}
