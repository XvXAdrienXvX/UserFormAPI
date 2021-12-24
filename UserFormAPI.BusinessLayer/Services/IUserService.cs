using System.Collections.Generic;
using UserFormAPI.BusinessLayer.DTO;

namespace UserFormAPI.BusinessLayer.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers();
        void AddUser(UserDTO newUser);
        void UpdateUser(UserDTO editedUser);
        void DeleteUser(int userId);
    }
}
