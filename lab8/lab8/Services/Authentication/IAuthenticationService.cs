using lab8.Models.Entities;
using lab8.Services.Crypto;
using lab8.Services.Repository;
using lab8.Views;
using lab8.ViewModels;
using System.Collections.Generic;

namespace lab8.Services.Authentication
{
    public interface IAuthenticationService
    {
        bool IsUserAuthenticated { get; }
        int AuthenticatedUserId { get;  }
        void LogIn(string login, string password);
        IEnumerable<User> GetDatabaseToList();
        User GetById(int i);
        void Delete(User u);
        void Add(User u);
        void Update(User u);
    }
}
