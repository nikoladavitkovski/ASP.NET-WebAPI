using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SEDC.NoteApp2.Dto.Models;
using SEDC.NoteApp2.Shared.Enums;
using SEDC.NotesApp2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.NoteApp2.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdoNetController : ControllerBase
    {
        private IEntityValidationService _entityValidationService;
        private static readonly string _connectionString = "Server.\\SQLEXPRESS;Database=Notes;Trusted_Connection=True;";

        [HttpGet("")]

        public ActionResult<List<UserDto>> GetAllUsers()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT ID,FirstName,LastName,UserName,Address,Age FROM Users";
            SqlDataReader sqlDataReader = command.ExecuteReader();

            List<UserDto> userDtos = new List<UserDto>();

            while (sqlDataReader.Read())
            {
                UserDto userDto = new UserDto()
                {
                    Id = sqlDataReader.GetInt32(0),
                    FirstName = sqlDataReader.GetFieldValue<string>(1),
                    LastName = (string)sqlDataReader["LastName"],
                    UserName = sqlDataReader.GetString(3),
                    Address = sqlDataReader.GetString(4),
                    Age = sqlDataReader.GetFieldValue<int>(5),
                    Notes = new List<NoteDto>()
                };
                userDtos.Add(userDto);
            }
            return StatusCode(StatusCodes.Status200OK, userDtos);
        }
        [HttpGet("{id}")]

        public ActionResult<UserDto> GetUserById(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT ID,FirstName,LastName,UserName,Address,Age FROM Users";
            command.Parameters.AddWithValue("@userId",id);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            UserDto userDto = new UserDto();

            while (sqlDataReader.Read())
            {
                userDto = new UserDto()
                {
                    Id = sqlDataReader.GetInt32(0),
                    FirstName = sqlDataReader.GetFieldValue<string>(1),
                    LastName = (string)sqlDataReader["LastName"],
                    UserName = sqlDataReader.GetString(3),
                    Address = sqlDataReader.GetString(4),
                    Age = sqlDataReader.GetFieldValue<int>(5),
                    Notes = new List<NoteDto>()
                };
            }
            return StatusCode(StatusCodes.Status200OK, userDto);
        }
        [HttpGet("notes")]

        public ActionResult<List<UserDto>> GetAllUsersWithNotes(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Id, Text, Color, Tag, UserId FROM Notes WHERE UserId = @userId";
            cmd.Parameters.AddWithValue("@userId", id);

            SqlDataReader dr = cmd.ExecuteReader();

            List<NoteDto> noteDtos = new List<NoteDto>();

            while (dr.Read())
            {
                NoteDto noteDto = new NoteDto()
                {
                    Id = dr.GetInt32(0),
                    Text = dr.GetFieldValue<string>(1),
                    Color = (string)dr["Color"],
                    Tag = dr.GetFieldValue<TagType>(3),
                    UserId = dr.GetInt32(4),
                    UserFullName = string.Empty
                };

                noteDtos.Add(noteDto);
            }

            dr.Close();

            cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Id, FirstName, LastName, Username, Address, Age FROM Users WHERE Id = @userId";
            cmd.Parameters.AddWithValue("@userId", id);
            dr = cmd.ExecuteReader();

            UserDto userDto = new UserDto();

            while (dr.Read())
            {
                userDto = new UserDto()
                {
                    Id = dr.GetInt32(0),
                    FirstName = dr.GetFieldValue<string>(1),
                    LastName = (string)dr["LastName"],
                    UserName = dr.GetString(3),
                    Address = dr.GetString(4),
                    Age = dr.GetFieldValue<int>(5),
                    Notes = noteDtos
                    .Select(
                        x =>
                        {
                            x.UserFullName = $"{dr.GetFieldValue<string>(1)} {(string)dr["LastName"]}";
                            return x;
                        })
                    .ToList()
                };
            }

            return StatusCode(StatusCodes.Status200OK, userDto);
        }

        [HttpGet("notes")]
        public ActionResult<List<UserDto>> GetAllUsersWithNotes()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Id, Text, Color, Tag, UserId FROM Notes";

            SqlDataReader dr = cmd.ExecuteReader();

            List<NoteDto> noteDtos = new List<NoteDto>();

            while (dr.Read())
            {
                NoteDto noteDto = new NoteDto()
                {
                    Id = dr.GetInt32(0),
                    Text = dr.GetFieldValue<string>(1),
                    Color = (string)dr["Color"],
                    Tag = dr.GetFieldValue<TagType>(3),
                    UserId = dr.GetInt32(4),
                    UserFullName = string.Empty
                };

                noteDtos.Add(noteDto);
            }

            dr.Close();

            cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Id, FirstName, LastName, Username, Address, Age FROM Users";
            dr = cmd.ExecuteReader();

            List<UserDto> userDtos = new List<UserDto>();

            while (dr.Read())
            {
                List<NoteDto> notesForUser =
                    noteDtos
                    .Where(x => x.UserId == dr.GetInt32(0))
                    .Select(
                        x =>
                        {
                            x.UserFullName = $"{dr.GetFieldValue<string>(1)} {(string)dr["LastName"]}";
                            return x;
                        })
                    .ToList();

                UserDto userDto = new UserDto()
                {
                    Id = dr.GetInt32(0),
                    FirstName = dr.GetFieldValue<string>(1),
                    LastName = (string)dr["LastName"],
                    UserName = dr.GetString(3),
                    Address = dr.GetString(4),
                    Age = dr.GetFieldValue<int>(5),
                    Notes = notesForUser
                };

                userDtos.Add(userDto);
            }

            return StatusCode(StatusCodes.Status200OK, userDtos);
        }
        [HttpPost("")]

        public ActionResult CreateNewUser(UserDto userDto)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlTransaction sqlTransaction = connection.BeginTransaction();

            int userId;
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Transaction = sqlTransaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "sp_CreateNewUser";
                command.Parameters.AddWithValue("@firstname", userDto.FirstName);
                command.Parameters.AddWithValue("@lastname", userDto.LastName);
                command.Parameters.AddWithValue("@username", userDto.UserName);
                command.Parameters.AddWithValue("@address", userDto.Address);
                command.Parameters.AddWithValue("@age", userDto.Age);

                command.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output,
                    Value = 0
                });

                command.ExecuteNonQuery();

                userId = (int)command.Parameters["@id"].Value;

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
