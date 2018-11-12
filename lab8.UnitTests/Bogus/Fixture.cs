using lab8.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using lab8.Services.Crypto;

namespace lab8.UnitTests.Bogus
{
    public class Fixture
    {

        private Faker<User> _userFaker;

        public string NotHashedPassword => "motDePasseEnClair";
        public string NotEncryptedCreditCard => "5105105105105100";
        public string Last4DigitCreditCard => NotEncryptedCreditCard.Substring(NotEncryptedCreditCard.Length - 4);
        public string EncryptionKey => "--AnEncryptionKey--";

        public Fixture()
        {
            InitializeFakersWithRules();
        }

        public User BuildUser()
        {
            return _userFaker.Generate();
        }
        public List<User> BuildUserList(int numberOfUsers)
        {
            return _userFaker.Generate(numberOfUsers);
        }

        private void InitializeFakersWithRules()
        {
            var crypto = new CryptoService();
            var salt = crypto.GenerateSalt();
            _userFaker = new Faker<User>()
                .StrictMode(true)
                .RuleFor(u => u.Login, f => f.Person.Email)
                .RuleFor(u => u.PasswordSalt, f => salt)
                .RuleFor(u => u.CreditCard, f => crypto.Encrypt(NotEncryptedCreditCard, EncryptionKey))
                .RuleFor(u => u.HashedPassword, f => crypto.HashSHA512(NotHashedPassword, salt))
                .RuleFor(u => u.Id, f => f.IndexFaker);
        }
    }
}
