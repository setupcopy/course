using CourseModels;
using CourseRepositorys;
using CourseUtility;
using CourseUtility.DataValidator;
using CourseUtility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseUserApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> AddUser(User user)
        {
            try
            {
                return await _userRepository.AddUser(user);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUser(User user)
        {
            try
            {
                return await _userRepository.DeleteUser(user);;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginationList<User>> GetUsers(int pageSize, int pageNumber)
        {
            try
            {
                return await _userRepository.GetUsers(pageSize, pageNumber); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            try
            {
                return await _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                return await _userRepository.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ValidateDataOfUserRequest(string email, string password, string passwordConfirmed)
        {
            var userForValdation = new UserForValdation();
            userForValdation.Email = email;
            userForValdation.Password = password;
            userForValdation.PasswordConfirmed = passwordConfirmed;

            var userValidator = new UserRequestValidator();
            var validatorResult = userValidator.Validate(userForValdation);

            return validatorResult.IsValid;
        }

        public void SetHashedPassword(User user)
        {
            var salt = PasswordHasher.GenerateSalt();
            var passwordHashed = PasswordHasher.HashPassword(user.Password, salt);
            user.Password = passwordHashed;
            user.Key = salt;
            user.Iv = salt;
        }
    }
}
