using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using UserFormAPI.BusinessLayer.DTO;
using UserFormAPI.DataAccessLayer.Models;
using UserFormAPI.DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using OkResult = Microsoft.AspNetCore.Mvc.OkResult;
using System.Reflection;

namespace UserFormAPI.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(
            IUserRepository userRepository,
            IMapper mapper
        )
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public void AddUser(UserDTO newUser)
        {
            try
            {
                int countId = 0;
                IEnumerable<User> existingUsers = _userRepository.GetAll();
                User userModel = _mapper.Map<User>(newUser);

                if (!existingUsers.Any())
                {
                    countId += 1;
                    userModel.userId = countId;
                    _userRepository.Insert(userModel);
                    _userRepository.Save();
                }
                else
                {
                    int maxId = existingUsers.Max(user => user.userId);
                    maxId += 1;
                    userModel.userId = maxId;
                    _userRepository.Insert(userModel);
                    _userRepository.Save();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to Add new user {0}", ex));
            }             
        }

        public void DeleteUser(int userId)
        {
            try
            {
                _userRepository.Delete(userId);
                _userRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to delete user with Id {0} : {1}", userId ,ex));
            }
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            try
            {
                
                IEnumerable<User> existingUsers = _userRepository.GetAll();
                List<UserDTO> userList = _mapper.Map<List<UserDTO>>(existingUsers);

                return userList;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to get all users {0}", ex));
            }
        }

        public void UpdateUser(UserDTO editedUser)
        {
            try
            {

                User existingUser = _userRepository.GetUserById(editedUser.userId);
                if (existingUser == null)
                {
                    throw new Exception(String.Format("User with Id {0} not found", editedUser.userId));
                }
                User userDetail = _mapper.Map<User>(editedUser);
                int userId = userDetail.userId;
                List<string> requiredProps = new List<string>() { "userId" };
                foreach (PropertyInfo propEdited in userDetail.GetType().GetProperties())
                {
                    foreach (PropertyInfo propExisting in existingUser.GetType().GetProperties())
                    {
                        if ((object.Equals(propExisting.GetValue(existingUser, null), propEdited.GetValue(userDetail, null))) ||
                            requiredProps.Contains(propEdited.Name))
                        {
                            propEdited.SetValue(userDetail, null);
                        }
                    }
                }

                _userRepository.Update(userDetail, userId);
                _userRepository.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to get all users {0}", ex));
            }
        }
    }
}
