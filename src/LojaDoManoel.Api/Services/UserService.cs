using Microsoft.EntityFrameworkCore;
using System;
using BCrypt.Net;
using LojaDoManoel.Api.Data;
using LojaDoManoel.Api.Models;
using LojaDoManoel.Api.Services.Interfaces;

namespace LojaDoManoel.Api.Services
{
    public class UserService : IUserService
    {
        private readonly LojaDoManoelDbContext _context;

        public UserService(LojaDoManoelDbContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Senha é obrigatória");

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, 13);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}
