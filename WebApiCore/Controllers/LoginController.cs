﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DemoDbContext _context;
        private readonly JWTSetting _setting;
        private readonly IRefreshTokenGenerator _tokenGenerator;
        public LoginController(DemoDbContext context, IOptions<JWTSetting> options, IRefreshTokenGenerator refreshToken)
        {
             _context = context;
             _setting = options.Value;
             _tokenGenerator = refreshToken;
        }


        [NonAction]
        public TokenResponse Authenticate(string username, Claim[] claims)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var tokenkey = Encoding.UTF8.GetBytes(_setting.securitykey);
            var tokenhandler = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(2),
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)

                );
            tokenResponse.JWTToken = new JwtSecurityTokenHandler().WriteToken(tokenhandler);
            tokenResponse.RefreshToken = _tokenGenerator.GenerateToken(username);

            return tokenResponse;
        }

        [Route("Authenticate")]
        [HttpPost]
        public IActionResult Authenticate([FromBody] usercred user)
        {
            TokenResponse tokenResponse = new TokenResponse();
            var _user = _context.TblUserMaster.FirstOrDefault(o => o.Userid == user.username && o.Password == user.password);
            if (_user == null)
                return Unauthorized();

            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(_setting.securitykey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, _user.Userid)
                        //,
                        //new Claim(ClaimTypes.Role, _user.Role)

                    }
                ),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            string finaltoken = tokenhandler.WriteToken(token);

            tokenResponse.JWTToken = finaltoken;
            tokenResponse.RefreshToken = _tokenGenerator.GenerateToken(user.username);
            
            return Ok(tokenResponse);
        }


        [Route("Refresh")]
        [HttpPost]
        public IActionResult Refresh([FromBody] TokenResponse token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();           
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token.JWTToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_setting.securitykey)),
                ValidateIssuer = false,
                ValidateAudience = false,

            }, out securityToken);

            var _token = securityToken as JwtSecurityToken;
            if (token != null && !_token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                return Unauthorized();
            }
            var username = principal.Identity?.Name;

            //var _reftable = _context.TblRefreshtoken.FirstOrDefault(o => o.UserId == username && o.RefreshToken == token.RefreshToken);
            //if (_reftable == null)
            //{
            //    return Unauthorized();
            //}
            TokenResponse _result = Authenticate(username, principal.Claims.ToArray());
            return Ok(_result);
        }

        [HttpPost("Register")]
        public APIResponse Register([FromBody] TblUserMaster value)
        {
            string result = string.Empty;
            try
            {
                var _emp = _context.TblUserMaster.FirstOrDefault(o => o.Userid == value.Userid);
                if (_emp != null)
                {
                    result = string.Empty;
                }
                else
                {
                    TblUserMaster tblUser = new TblUserMaster()
                    {
                        Username = value.Username!=null?value.Username:value.Userid,
                        Email = value.Email,
                        Userid = value.Userid,
                        Role = "user",
                        Password = value.Password
                    };
                    _context.TblUserMaster.Add(tblUser);
                    _context.SaveChanges();
                    result = "pass";
                }
            }
            catch (Exception ex)
            {
                result = string.Empty;
            }
            return new APIResponse { keycode = string.Empty, result = result };
        }

        //var tokenHandler = new JwtSecurityTokenHandler();
        //var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token.JWTToken);
        //var username = securityToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;


        ////var username = principal.Identity.Name;
        //var _reftable = _context.TblRefreshtoken.FirstOrDefault(o => o.UserId == username && o.RefreshToken == token.RefreshToken);
        //if (_reftable == null)
        //{
        //    return Unauthorized();
        //}
        //TokenResponse _result = Authenticate(username, securityToken.Claims.ToArray());
        //return Ok(_result);


        // GET: api/<LoginController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<LoginController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<LoginController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<LoginController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<LoginController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
