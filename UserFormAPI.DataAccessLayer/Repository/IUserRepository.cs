using System.Collections.Generic;
using UserFormAPI.DataAccessLayer.Models;

namespace UserFormAPI.DataAccessLayer.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        void Insert(User entity);
        void Update(User entity, int Id);
        void Delete(int id);
        void Save();
        User GetUserById(int Id);
    }
}
