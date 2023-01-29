using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApiCore.Models;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string procedure_name = "Sp_GetAllDepartments";
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
        public JsonResult Post(Department dep)
        {

            DataTable table = new DataTable();
            string procedure_name = "Sp_InsertDepartment";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Create ADO.NET objects.
            SqlConnection cn = new SqlConnection(sqlDataSource);
            SqlCommand cmd = new SqlCommand(procedure_name, cn);
            SqlDataReader dr;

            // Configure command and add parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@DepartmentName", SqlDbType.VarChar, 500);
            param.Value = dep.DepartmentName;

            // Execute the command.
            cn.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            dr.Close();
            cn.Close();

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {

            DataTable table = new DataTable();
            string procedure_name = "Sp_UpdateDepartment";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Create ADO.NET objects.
            SqlConnection cn = new SqlConnection(sqlDataSource);
            SqlCommand cmd = new SqlCommand(procedure_name, cn);
            SqlDataReader dr;

            // Configure command and add parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@DepartmentName", SqlDbType.VarChar, 500);
            param.Value = dep.DepartmentName;

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
            DataTable table = new DataTable();
            string procedure_name = "Sp_DeleteDepartment";
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Create ADO.NET objects.
            SqlConnection cn = new SqlConnection(sqlDataSource);
            SqlCommand cmd = new SqlCommand(procedure_name, cn);
            SqlDataReader dr;

            // Configure command and add parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;
            param = cmd.Parameters.Add("@DepartmentId", SqlDbType.Int);
            param.Value = id;

            // Execute the command.
            cn.Open();
            dr = cmd.ExecuteReader();
            table.Load(dr);
            dr.Close();
            cn.Close();

            return new JsonResult("Deleted Successfully");
        }


        //[HttpGet]
        //public JsonResult Get()
        //{
        //string query = @"
        //        select DepartmentId, DepartmentName from dbo.Department";
        //DataTable table = new DataTable();
        //string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //SqlDataReader myReader;
        //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //{
        //    myCon.Open();
        //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //    {
        //        myReader = myCommand.ExecuteReader();
        //        table.Load(myReader); ;

        //        myReader.Close();
        //        myCon.Close();
        //    }
        //}
        // }




        //[HttpPost]
        //public JsonResult Post(Department dep)
        //{
        //string query = @"
        //        insert into dbo.Department values 
        //        ('" + dep.DepartmentName + @"')
        //        ";
        //DataTable table = new DataTable();
        //string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
        //SqlDataReader myReader;
        //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //{
        //    myCon.Open();
        //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
        //    {
        //        myReader = myCommand.ExecuteReader();
        //        table.Load(myReader); ;

        //        myReader.Close();
        //        myCon.Close();
        //    }
        //}
        // return new JsonResult("Added Successfully");
        //}




        //[HttpPut]
        //public JsonResult Put(Department dep)
        //{
        //    string query = @"
        //            update dbo.Department set 
        //            DepartmentName = '" + dep.DepartmentName + @"'
        //            where DepartmentId = " + dep.DepartmentId + @" 
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
        //            delete from dbo.Department
        //            where DepartmentId = " + id + @" 
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
