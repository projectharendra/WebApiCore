using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApiCore.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string procedure_name = "Sp_GetAllEmployees";
                  
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
                    table.Load(dr); ;

                    dr.Close();
                    cn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string procedure_name = "Sp_InsertEmployee";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Create ADO.NET objects.
            SqlConnection cn = new SqlConnection(sqlDataSource);
            SqlCommand cmd = new SqlCommand(procedure_name, cn);
            SqlDataReader dr;

            // Configure command and add parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@EmployeeName", SqlDbType.VarChar, 500);
            param.Value = emp.EmployeeName;
            param = cmd.Parameters.Add("@Department", SqlDbType.VarChar, 500);
            param.Value = emp.Department;
            param = cmd.Parameters.Add("@DateOfJoining", SqlDbType.DateTime);
            param.Value = emp.DateOfJoining;
            param = cmd.Parameters.Add("@PhotoFileName", SqlDbType.VarChar, 500);
            param.Value = emp.PhotoFileName;

            // Execute the command.
            cn.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            dr.Close();
            cn.Close();

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string procedure_name = "Sp_UpdateEmployee";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Create ADO.NET objects.
            SqlConnection cn = new SqlConnection(sqlDataSource);
            SqlCommand cmd = new SqlCommand(procedure_name, cn);
            SqlDataReader dr;

            // Configure command and add parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@EmployeeId", SqlDbType.Int);
            param.Value = emp.EmployeeId;
            param = cmd.Parameters.Add("@EmployeeName", SqlDbType.VarChar, 500);
            param.Value = emp.EmployeeName;
            param = cmd.Parameters.Add("@Department", SqlDbType.VarChar, 500);
            param.Value = emp.Department;
            param = cmd.Parameters.Add("@DateOfJoining", SqlDbType.DateTime);
            param.Value = emp.DateOfJoining;
            param = cmd.Parameters.Add("@PhotoFileName", SqlDbType.VarChar, 500);
            param.Value = emp.PhotoFileName;

            // Execute the command.
            cn.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            dr.Close();
            cn.Close();

            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string procedure_name = "Sp_DeleteEmployee";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Create ADO.NET objects.
            SqlConnection cn = new SqlConnection(sqlDataSource);
            SqlCommand cmd = new SqlCommand(procedure_name, cn);
            SqlDataReader dr;

            // Configure command and add parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@EmployeeId", SqlDbType.Int);
            param.Value = id;           

            // Execute the command.
            cn.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            dr.Close();
            cn.Close();

            return new JsonResult("Deleted Successfully");
        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("anonymous.png");
            }
        }
        [HttpGet]
        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            string procedure_name = "Sp_GetAllDepartmentNames";
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
                    table.Load(dr); ;

                    dr.Close();
                    cn.Close();
                }
            }
            return new JsonResult(table);
        }

        //[HttpGet]
        //public JsonResult Get()
        //{
        //    string query = @"
        //            select EmployeeId, EmployeeName, Department,
        //            convert(varchar(10),DateOfJoining,120) as DateOfJoining
        //            ,PhotoFileName
        //            from dbo.Employee
        //            ";
        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //        {
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader); ;

        //            myReader.Close();
        //            myCon.Close();
        //        }
        //    }

        //    return new JsonResult(table);
        //}


        //[HttpPost]
        //public JsonResult Post(Employee emp)
        //{
        //    string query = @"
        //            insert into dbo.Employee 
        //            (EmployeeName,Department,DateOfJoining,PhotoFileName)
        //            values 
        //            (
        //            '" + emp.EmployeeName + @"'
        //            ,'" + emp.Department + @"'
        //            ,'" + emp.DateOfJoining + @"'
        //            ,'" + emp.PhotoFileName + @"'
        //            )
        //            ";
        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //        {
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader); ;

        //            myReader.Close();
        //            myCon.Close();
        //        }
        //    }

        //    return new JsonResult("Added Successfully");
        //}


        //[HttpPut]
        //public JsonResult Put(Employee emp)
        //{
        //    string query = @"
        //            update dbo.Employee set 
        //            EmployeeName = '" + emp.EmployeeName + @"'
        //            ,Department = '" + emp.Department + @"'
        //            ,DateOfJoining = '" + emp.DateOfJoining + @"'
        //            where EmployeeId = " + emp.EmployeeId + @" 
        //            ";
        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //        {
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader); ;

        //            myReader.Close();
        //            myCon.Close();
        //        }
        //    }

        //    return new JsonResult("Updated Successfully");
        //}


        //[HttpDelete("{id}")]
        //public JsonResult Delete(int id)
        //{
        //    string query = @"
        //            delete from dbo.Employee
        //            where EmployeeId = " + id + @" 
        //            ";
        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //        {
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader); ;

        //            myReader.Close();
        //            myCon.Close();
        //        }
        //    }

        //    return new JsonResult("Deleted Successfully");
        //}






    }
}
