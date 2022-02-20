﻿using FootballManager.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        string IPasswordHasher.PasswordHasher(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }

  
            using var sha256Hash = SHA256.Create();

           
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

         
            var builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
