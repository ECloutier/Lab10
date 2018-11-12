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
        void AuthenticateUser(string u, string p);
        bool IsUserAuthenticated { get; }
        int UserID { get; }
    }
}
