using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public UserController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public JsonResult GetAllUsers()
        {
            string procedure_name = "Sp_GetAllUsers";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader dr;
            using (SqlConnection cn = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand myCommand = new SqlCommand(procedure_name, cn))
                {
                    // Configure command and add parameters.
                    myCommand.CommandType = CommandType.StoredProcedure;

                    // Execute the command.
                    cn.Open();
                    dr = myCommand.ExecuteReader();
                    table.Load(dr);

                    dr.Close();
                    cn.Close();
                }
            }
            return new JsonResult(table);
        }

        // GET api/<UserController>/5
        [HttpGet("GetUserDetailsById/{id}")]
        //[Route("GetUserDetailsById")]
        public JsonResult GetUserDetailsById(int id)
        {
            string procedure_name = "Sp_GetUserDetailsById";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Create ADO.NET objects.
            SqlConnection cn = new SqlConnection(sqlDataSource);
            SqlCommand cmd = new SqlCommand(procedure_name, cn);
            SqlDataReader dr;

            // Configure command and add parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@Id", SqlDbType.Int);
            param.Value = id;

            // Execute the command.
            cn.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            dr.Close();
            cn.Close();

            return new JsonResult(table);
        }



        //// GET: api/<UserController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
