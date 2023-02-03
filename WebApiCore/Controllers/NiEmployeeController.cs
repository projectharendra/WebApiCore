using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class NiEmployeeController : ControllerBase
    {
        private readonly DemoDbContext _context;
        public NiEmployeeController(DemoDbContext context)
        {
            _context = context;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<TblEmployee> Get()
        {
            return _context.TblEmployee.ToList();
        }
        [HttpGet("{id}")]
        public TblEmployee Get(int id)
        {
            return _context.TblEmployee.FirstOrDefault(o => o.Code == id);
        }

        [HttpPost]
        public APIResponse Post([FromBody] TblEmployee value)
        {
            string result = string.Empty;
            try
            {
                var _emp = _context.TblEmployee.FirstOrDefault(o => o.Code == value.Code);
                if (_emp != null)
                {
                    _emp.Designation = value.Designation;
                    _emp.Email = value.Email;
                    _emp.Name = value.Name;
                    _emp.Phone = value.Phone;
                    _context.SaveChanges();
                    result = "pass";
                }
                else
                {
                    TblEmployee tblEmployee = new TblEmployee()
                    {
                        Name = value.Name,
                        Email = value.Email,
                        Phone = value.Phone,
                        Designation = value.Designation
                    };
                    _context.TblEmployee.Add(tblEmployee);
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
        public APIResponse Delete(int id)
        {
            string result = string.Empty;
            var _emp = _context.TblEmployee.FirstOrDefault(o => o.Code == id);
            if (_emp != null)
            {
                _context.TblEmployee.Remove(_emp);
                _context.SaveChanges();
                result = "pass";
            }
            return new APIResponse { keycode = string.Empty, result = result };
        }
    }
}
