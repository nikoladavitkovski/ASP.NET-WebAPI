using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SEDC.PhoneBook2.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoNetController : ControllerBase
    {
        private static readonly string _connectionString = "Server=.\\SQLEXPRESS;Database=Notes;Trusted_Connection=True;";

        [HttpGet("")]

        public ActionResult<List<UserDto>> GetAllUsers()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT Id, FirstName, LastName, Age, Address, UserName FROM Users";
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            List<UserDto> userDtos = new List<UserDto>();

            while (sqlDataReader.Read())
            {
                UserDto userDto = new UserDto()
                {
                    Id = sqlDataReader.GetInt32(0),
                    FirstName = sqlDataReader.GetFieldValue<string>(1),
                    LastName = (string)sqlDataReader["LastName"],
                    Age = sqlDataReader.GetInt32(3),
                    UserName = sqlDataReader.GetString(4),
                    Address = sqlDataReader.GetString(5)
                };
                userDtos.Add(userDto);
            }
            return StatusCode(StatusCodes.Status200OK, userDtos);
        }
        [HttpPost("")]

        public ActionResult<int> CreateNewUser(UserDto userDto)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();

            int userId;

            try
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Transaction = sqlTransaction;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "sp_CreateNewUser";
                sqlCommand.Parameters.AddWithValue("@id",userDto.Id);
                sqlCommand.Parameters.AddWithValue("@firstname",userDto.FirstName);
                sqlCommand.Parameters.AddWithValue("@lastname",userDto.LastName);
                sqlCommand.Parameters.AddWithValue("@age",userDto.Age);
                sqlCommand.Parameters.AddWithValue("@address",userDto.Address);
                sqlCommand.Parameters.AddWithValue("@username",userDto.UserName);

                sqlCommand.ExecuteNonQuery();

                userId = (int)sqlCommand.Parameters["@id"].Value;

                sqlTransaction.Commit();
            }
            catch(Exception ex)
            {
                sqlTransaction.Rollback();
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            return StatusCode(StatusCodes.Status201Created, userId);
        }
    }
}
