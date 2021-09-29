using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Shared.Enums;
using SEDC.NoteApp2.Shared.Helpers;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private IEntityValidationService _entityValidationService;
        private static readonly string _connectionString = "Server.\\SQLEXPRESS;Database=Notes;Trusted_Connection=True;";

        [HttpGet("")]

        public ActionResult<List<UserDto>> GetAllUsers()
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            List<UserDto> userDtos = connection.Query<UserDto>
                ("SELECT ID,FirstName,LastName,UserName,Address,Age FROM Users").Select(x =>
                {
                    x.Notes = new List<NoteDto>();
                    return x;
                }).ToList();

            connection.Close();

            return StatusCode(StatusCodes.Status200OK, userDtos);
        }
        [HttpGet("{id}")]

        public ActionResult<UserDto> GetUserById(int id)
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            object parameters = new {GetUserById = id};
            UserDto userDto = connection.QueryFirstOrDefault("SELECT ID,FirstName,LastName,UserName,Address,Age FROM Users");

            userDto.Notes = new List<NoteDto>();
            
            return StatusCode(StatusCodes.Status200OK, userDto);
        }
        [HttpGet("notes")]

        public ActionResult<List<UserDto>> GetAllUsersWithNotes()
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlMapper.GridReader multiQueryData = connection
                .QueryMultiple("SELECT Id, Text, Color, Tag, UserId FROM Notes WHERE UserId = @userId");

            List<NoteDto> noteDtos = multiQueryData.Read<NoteDto>().ToList();
            List<UserDto> userDtos = multiQueryData.Read<UserDto>().ToList();

            foreach (UserDto item in userDtos)
            {
                item.Notes = noteDtos
                    .Where(q => q.UserId == item.Id)
                    .Select(
                        x =>
                        {
                            x.UserFullName = $"{item.FirstName} {item.LastName}";
                            return x;
                        })
                    .ToList();
            }

            return StatusCode(StatusCodes.Status200OK, userDtos);
        }

        [HttpGet("notes")]
        public ActionResult<List<UserDto>> GetAllUsersWithNotes(int id)
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            object parameters = new {userId = id};
            SqlMapper.GridReader multiQueryData = connection
                .QueryMultiple("SELECT Id, FirstName, LastName, Username, Address, Age FROM Users WHERE Id = @userId", parameters);

            UserDto userDto = multiQueryData.ReadFirstOrDefault<UserDto>();

            userDto.Notes = multiQueryData.Read<NoteDto>()
                .Select(
                x =>
                {
                    x.UserFullName = $"{userDto.FirstName} {userDto.LastName}";
                    return x;
                })
                .ToList();

            return StatusCode(StatusCodes.Status200OK, userDto);
        }
        [HttpPost("")]

        public ActionResult<int> CreateNewUser(RegisterUserDto userDto)
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            IDbTransaction transaction = connection.BeginTransaction();
            int userId;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@firstname", userDto.FirstName);
            parameters.Add("@lastname", userDto.LastName);
            parameters.Add("@username", userDto.UserName);
            parameters.Add("@password", userDto.PassWord.GenerateMD5());
            parameters.Add("@address", userDto.Address);
            parameters.Add("@age", userDto.Age);
            parameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                connection.Execute("sp_CreateNewUser", parameters, transaction, 30, CommandType.StoredProcedure);
                userId = parameters.Get<int>("@id");
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            return StatusCode(StatusCodes.Status201Created, userId);
        }
    }
}
