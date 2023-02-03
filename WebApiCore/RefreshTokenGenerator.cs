using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WebApiCore.Models;

namespace WebApiCore
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly DemoDbContext _context;

        public RefreshTokenGenerator(DemoDbContext context)
        {
            _context = context;
        }
        [NonAction]
        public string GenerateToken(string username)
        {
            var randomnumber = new byte[32];
            using (var randomnumbergenerator = RandomNumberGenerator.Create())
            {
                randomnumbergenerator.GetBytes(randomnumber);
                string RefreshToken = Convert.ToBase64String(randomnumber);

                var _user = _context.TblRefreshtoken.FirstOrDefault(o => o.UserId == username);
                if (_user != null)
                {
                    _user.RefreshToken = RefreshToken;
                    _context.SaveChanges();
                }
                else
                {
                    TblRefreshtoken tblRefreshtoken = new TblRefreshtoken()
                    {
                        UserId = username,
                        TokenId = new Random().Next().ToString(),
                        RefreshToken = RefreshToken,
                        IsActive = true
                    };
                }

                return RefreshToken;
            }
        }
    }
}
