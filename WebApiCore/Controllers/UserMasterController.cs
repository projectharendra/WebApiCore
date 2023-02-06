using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {

        private readonly DemoDbContext _context;
        public UserMasterController(DemoDbContext context)
        {
            _context = context;
        }

        // GET: api/<UserMasterController>
        [HttpGet]
        public IEnumerable<TblUserMaster> Get()
        {
            return _context.TblUserMaster.ToList();
        }

        // GET api/<UserMasterController>/5
        [HttpGet("{id}")]
        public TblUserMaster Get(string id)
        {
            return _context.TblUserMaster.FirstOrDefault(o => o.Userid == id);
        }

        [HttpPost("Save")]
        public APIResponse Save([FromBody] TblUserMaster value)
        {
            string result = string.Empty;
            try
            {
                var _emp = _context.TblUserMaster.FirstOrDefault(o => o.Userid == value.Userid);
                if (_emp != null)
                {
                    _emp.Role = value.Role;
                    _emp.Email = value.Email;
                    _emp.Username = value.Username;
                    _emp.Isactive = value.Isactive;
                    _context.SaveChanges();
                    result = "pass";
                }
                else
                {
                    TblUserMaster tblUser = new TblUserMaster()
                    {
                        Username = value.Username,
                        Email = value.Email,
                        Userid = value.Userid,
                        Role = value.Role,
                        // Password = value.Password,
                        Isactive = true
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

        [HttpPost("ActivateUser")]
        public APIResponse ActivateUser([FromBody] TblUserMaster value)
        {
            string result = string.Empty;
            try
            {
                var _emp = _context.TblUserMaster.FirstOrDefault(o => o.Userid == value.Userid);
                if (_emp != null)
                {
                    _emp.Role = value.Role;
                    _emp.Isactive = value.Isactive;
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

        [HttpDelete("{id}")]
        public APIResponse Delete(string id)
        {
            string result = string.Empty;
            var _emp = _context.TblUserMaster.FirstOrDefault(o => o.Userid == id);
            if (_emp != null)
            {
                _context.TblUserMaster.Remove(_emp);
                _context.SaveChanges();
                result = "pass";
            }
            return new APIResponse { keycode = string.Empty, result = result };
        }

        
    }
}
