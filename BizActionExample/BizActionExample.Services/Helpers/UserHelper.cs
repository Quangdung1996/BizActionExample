using BizActionExample.Domain.Models.Accounts;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BizActionExample.Services.Helpers
{
    public static class UserHelper
    {
        private const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        private const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NUMBERS = "123456789";
        private const string SPECIALS = @"!@$%^&*()#";

        public static string GeneratePassword(bool useLowercase = true,
                                       bool useUppercase = true,
                                       bool useNumbers = true,
                                       bool useSpecial = true,
                                       int passwordSize = 10)
        {
            char[] _password = new char[passwordSize];
            string charSet = "";
            var _random = new Random();
            int counter;

            if (useLowercase) charSet += LOWER_CASE;

            if (useUppercase) charSet += UPPER_CAES;

            if (useNumbers) charSet += NUMBERS;

            if (useSpecial) charSet += SPECIALS;

            for (counter = 0; counter < passwordSize; counter++)
            {
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }

            return String.Join(null, _password);
        }

        public static string HashPassword(string password, string hashSalt)
        {
            using var md5 = MD5.Create();
            var saltAndPassword = String.Concat(password, hashSalt);
            var hashedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        public static string GeneraSalt()
        {
            byte[] bytes = new byte[10];
            using var keyGenerator = RandomNumberGenerator.Create();
            keyGenerator.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public static bool VerifyPassword(UserInfo source, string originalPassword)
        {
            return source.Password.Equals(HashPassword(originalPassword, source.HashSalt));
        }

        public static void HideSensitiveInfo(this UserInfo accountInfo)
        {
            accountInfo.Password = null;
            accountInfo.HashSalt = null;
        }
    }
}