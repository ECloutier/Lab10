using lab8.Models.Entities;
using lab8.Services.Crypto;
using lab8.Services.Repository;
using System.Collections.Generic;
using System.Linq;

namespace lab8.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private ICryptoService _cryptoService;
        private IRepository<User> _userDb;

        private bool _isUserAuthenticated;
        private int _userID;

        public bool IsUserAuthenticated { get=>_isUserAuthenticated; set => _isUserAuthenticated = value; }
        public int UserID { get => _userID; }

        public AuthenticationService(ICryptoService cryptoService, IRepository<User> userDb)
        {
            _cryptoService = cryptoService;
            _userDb = userDb;
        }

        public void AuthenticateUser(string username, string password)
        {
            User authUser = _userDb.GetAll().Where<User>(user => user.Login == username).FirstOrDefault();

            if (authUser != null && username == authUser.Login)
            {
                string salt = authUser.PasswordSalt;
                string authHashedPassword = _cryptoService.HashSHA512(password, salt);

                if (authHashedPassword == authUser.HashedPassword) {
                    IsUserAuthenticated = true;
                    _userID = authUser.Id;
                }
                else
                {
                    IsUserAuthenticated = false;
                }
            }
            else
            {
                IsUserAuthenticated = false;
            }
        }
    }
}
