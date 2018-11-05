using System;
using lab8.Services.Crypto;
using Xunit;


namespace lab8.UnitTests.Crypto
{
    public class CryptoTests
    {
        private readonly CryptoService _cryptoService;

        public CryptoTests()
        {
            _cryptoService = new CryptoService();

        }
        [Fact]
        public void HashSHA516_WhenCall_ShouldGenerateHashValue()
        {
            const string VALUE_TO_HASH = "a string to hash";
            var salt = _cryptoService.GenerateSalt();

            var hashedValue = _cryptoService.HashSHA512(VALUE_TO_HASH, salt);

            Assert.NotEqual(VALUE_TO_HASH, hashedValue);
        }

        [Fact]
        public void Decrypt_WithClearValueEncrypted_ShouldDecryptIt()
        {
            const string CLEAR_VALUE = "valeur à encrypter";
            var encryptionKey = Guid.NewGuid().ToString();
            var encryptedValue = _cryptoService.Encrypt(CLEAR_VALUE, encryptionKey);

            var valueDecrypted = _cryptoService.Decrypt(encryptedValue, encryptionKey);

            Assert.Equal(CLEAR_VALUE, valueDecrypted);

        }
    }
}
