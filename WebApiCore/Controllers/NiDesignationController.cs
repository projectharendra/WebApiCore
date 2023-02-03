using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NiDesignationController : ControllerBase
    {
        private readonly DemoDbContext _context;
        public NiDesignationController(DemoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<TblDesignation> Get()
        {
            return _context.TblDesignation.ToList();
        }
        [HttpGet("{id}")]
        public TblDesignation Get(string id)
        {
            return _context.TblDesignation.FirstOrDefault(o => o.Code == id);
        }
        [HttpPost]
        public APIResponse Post([FromBody] TblDesignation value)
        {
            string result = string.Empty;
            try
            {
                var _emp = _context.TblDesignation.FirstOrDefault(o => o.Code == value.Code);
                if (_emp != null)
                {
                    _emp.Name = value.Name;
                    _context.SaveChanges();
                    result = "pass";
                }
                else
                {
                    TblDesignation tblEmployee = new TblDesignation()
                    {
                        Name = value.Name,
                        Code = value.Code
                    };
                    _context.TblDesignation.Add(tblEmployee);
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
    }
}
