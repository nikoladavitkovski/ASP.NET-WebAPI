using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SEDC.PhoneBook2.Dto.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.PhoneBook2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private static readonly string _connectionString = "Server=.\\SQLEXPRESS;Database=Notes;Trusted_Connection=True;";

        [HttpGet("")]

        public ActionResult<List<UserDto>> GetAllUsers()
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            List<UserDto> userDtos = connection.Query<UserDto>
                ("SELECT Id, FirstName, LastName, Age, Address, UserName FROM Users")
                .Select(
                x =>
                {
                    x.ContactEntries = new List<ContactEntryDto>();
                    return x;
                }
                ).ToList();

            connection.Close();

            return StatusCode(StatusCodes.Status200OK, userDtos);
        }

        [HttpPost("")]

        public ActionResult<int> CreateNewUser(UserDto userDto)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();

            IDbTransaction dbTransaction = dbConnection.BeginTransaction();
            int userId;

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParameters.Add("@firstname", userDto.FirstName);
            dynamicParameters.Add("@lastname", userDto.LastName);
            dynamicParameters.Add("@address", userDto.Address);
            dynamicParameters.Add("@age", userDto.Age);
            dynamicParameters.Add("@username", userDto.UserName);

            try
            {
                dbConnection.Execute("sp_CreateNewUser", dynamicParameters, dbTransaction, 30, CommandType.StoredProcedure);
                userId = dynamicParameters.Get<int>("@id");
                dbTransaction.Commit();
            }
            catch(Exception ex)
            {
                dbTransaction.Rollback();
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            return StatusCode(StatusCodes.Status201Created, userId);
        }
    }
}
