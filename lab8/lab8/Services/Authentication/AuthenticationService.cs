using lab8.Models.Entities;
using lab8.Services.Crypto;
using lab8.Services.Repository;
using System.Collections.Generic;

namespace lab8.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private ICryptoService _cryptoService;
        private IRepository<User> _userDb;
        public AuthenticationService(ICryptoService cryptoService, IRepository<User> userDb)
        {
            _cryptoService = cryptoService;
            _userDb = userDb;
        }

        public bool AuthenticateUser(string username, string password)
        {
            IEnumerable<User> listOfUsers = GetDatabaseToList();
            foreach (User user in listOfUsers)
            {
                if (username == user.Login)
                {
                    string salt = user.PasswordSalt;
                    string authHashedPassword = _cryptoService.HashSHA512(password, salt);

                    if (authHashedPassword == user.HashedPassword) { 
                        return true;
                    }
                    return false;
                }
            }
            return false;

        }

        //************************************//
        //************* REPOSITORY ***********//
        //************************************//
        public IEnumerable<User> GetDatabaseToList()
        {
            return _userDb.GetAll();
        }

        public User GetById(int userId)
        {
            return _userDb.GetById(userId);
        }

        public void Delete(User user)
        {
            _userDb.Delete(user);
        }

        public void Add(User user)
        {
            _userDb.Add(user);
        }

        public void Update(User user)
        {
            _userDb.Update(user);
        }
    }
}
