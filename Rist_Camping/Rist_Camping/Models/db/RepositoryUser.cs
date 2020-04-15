﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Rist_Camping.Models.db
{
    public class RepositoryUser
    {
        private string _connectionString = "Server=localhost; Database=db_Rist_Camping; Uid=root; Pwd=Klexi2408;";
        private MySqlConnection _connection;

        public void Open()
        {
            if (this._connection == null)
            {
                this._connection = new MySqlConnection(this._connectionString);
            }
            if (this._connection.State != ConnectionState.Open)
            {
                this._connection.Open();
            }
        }

        public void Close()
        {
            if ((this._connection != null) && (this._connection.State != ConnectionState.Closed))
            {
                this._connection.Close();
            }
        }

        public bool Insert(User user)
        {
            if (user == null)
            {
                return false;
            }

            DbCommand cmdInsert = this._connection.CreateCommand();
            cmdInsert.CommandText = "INSERT INTO users  VALUES(null, @firstname, @lastname, @email, @gender, @userRole, @username, sha2(@password, 256))";

            DbParameter paramFirstname = cmdInsert.CreateParameter();
            paramFirstname.ParameterName = "firstname";
            paramFirstname.Value = user.Firstname;
            paramFirstname.DbType = DbType.String;

            DbParameter paramLastname = cmdInsert.CreateParameter();
            paramLastname.ParameterName = "lastname";
            paramLastname.Value = user.Lastname;
            paramLastname.DbType = DbType.String;

            DbParameter paramEmail = cmdInsert.CreateParameter();
            paramEmail.ParameterName = "email";
            paramEmail.Value = user.Email;
            paramEmail.DbType = DbType.String;

            DbParameter paramGender = cmdInsert.CreateParameter();
            paramGender.ParameterName = "gender";
            paramGender.Value = user.Gender;
            paramGender.DbType = DbType.Int32;

            DbParameter paramRole = cmdInsert.CreateParameter();
            paramRole.ParameterName = "userRole";
            paramRole.Value = user.UserRole;
            paramRole.DbType = DbType.Int32;

            DbParameter paramUsername = cmdInsert.CreateParameter();
            paramUsername.ParameterName = "username";
            paramUsername.Value = user.Username;
            paramUsername.DbType = DbType.String;

            DbParameter paramPassword = cmdInsert.CreateParameter();
            paramPassword.ParameterName = "password";
            paramPassword.Value = user.Password;
            paramPassword.DbType = DbType.String;

            // Parameter mit dem Command verbinden
            cmdInsert.Parameters.Add(paramFirstname);
            cmdInsert.Parameters.Add(paramLastname);
            cmdInsert.Parameters.Add(paramEmail);
            cmdInsert.Parameters.Add(paramGender);
            cmdInsert.Parameters.Add(paramRole);
            cmdInsert.Parameters.Add(paramUsername);
            cmdInsert.Parameters.Add(paramPassword);

            return cmdInsert.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            DbCommand cmdDel = this._connection.CreateCommand();
            cmdDel.CommandText = "DELETE FROM users WHERE id=@userId";

            DbParameter paramId = cmdDel.CreateParameter();
            paramId.ParameterName = "userId";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            cmdDel.Parameters.Add(paramId);

            return cmdDel.ExecuteNonQuery() == 1;
        }

        public User GetUser(int id)
        {
            DbCommand cmdGetUser = this._connection.CreateCommand();
            cmdGetUser.CommandText = "SELECT * FROM users WHERE id=@uid";

            DbParameter paramId = cmdGetUser.CreateParameter();
            paramId.ParameterName = "uid";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            cmdGetUser.Parameters.Add(paramId);

            using (DbDataReader reader = cmdGetUser.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();
                return new User
                {
                    ID = Convert.ToInt32(reader["id"]),
                    Firstname = Convert.ToString(reader["firstname"]),
                    Lastname = Convert.ToString(reader["lastname"]),
                    Email = Convert.ToString(reader["email"]),
                    Gender = (Gender)Convert.ToInt32(reader["gender"]),
                    UserRole = (UserRole)Convert.ToInt32(reader["userRole"]),
                    Username = Convert.ToString(reader["username"]),
                    Password = ""
                };

            }
        }

        public User Login(UserLogin user)
        {
            DbCommand cmdLogin = this._connection.CreateCommand();
            cmdLogin.CommandText = "SELECT * FROM users WHERE username=@username AND password=sha2(@password, 256)";

            DbParameter paramUsername = cmdLogin.CreateParameter();
            paramUsername.ParameterName = "username";
            paramUsername.Value = user.Username;
            paramUsername.DbType = DbType.String;

            DbParameter paramPwd = cmdLogin.CreateParameter();
            paramPwd.ParameterName = "password";
            paramPwd.Value = user.Password;
            paramPwd.DbType = DbType.String;

            cmdLogin.Parameters.Add(paramUsername);
            cmdLogin.Parameters.Add(paramPwd);

            using (DbDataReader reader = cmdLogin.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();
                return new User
                {
                    ID = Convert.ToInt32(reader["id"]),
                    Firstname = Convert.ToString(reader["firstname"]),
                    Lastname = Convert.ToString(reader["lastname"]),
                    Email = Convert.ToString(reader["email"]),
                    Gender = (Gender)Convert.ToInt32(reader["gender"]),
                    UserRole = (UserRole)Convert.ToInt32(reader["userRole"]),
                    Username = Convert.ToString(reader["username"]),
                    Password = ""
                };
            }
        }

        public bool UpdateUserData(int id, User newUserData)
        {
            DbCommand cmdUpdate = this._connection.CreateCommand();
            cmdUpdate.CommandText = "UPDATE users SET firstname=@firstname, lastname=@lastname, email=@email, gender=@gender, userRole=@userRole, username=@username, password=sha2(@password, 256) WHERE id=@uId";

            DbParameter paramId = cmdUpdate.CreateParameter();
            paramId.ParameterName = "uId";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            DbParameter paramFirstname = cmdUpdate.CreateParameter();
            paramFirstname.ParameterName = "firstname";
            paramFirstname.Value = newUserData.Firstname;
            paramFirstname.DbType = DbType.String;

            DbParameter paramLastname = cmdUpdate.CreateParameter();
            paramLastname.ParameterName = "lastname";
            paramLastname.Value = newUserData.Lastname;
            paramLastname.DbType = DbType.String;

            DbParameter paramEmail = cmdUpdate.CreateParameter();
            paramEmail.ParameterName = "email";
            paramEmail.Value = newUserData.Lastname;
            paramEmail.DbType = DbType.String;

            DbParameter paramGender = cmdUpdate.CreateParameter();
            paramGender.ParameterName = "gender";
            paramGender.Value = newUserData.Gender;
            paramGender.DbType = DbType.Int32;

            DbParameter paramRole = cmdUpdate.CreateParameter();
            paramRole.ParameterName = "userRole";
            paramRole.Value = newUserData.UserRole;
            paramRole.DbType = DbType.Int32;

            DbParameter paramUsername = cmdUpdate.CreateParameter();
            paramUsername.ParameterName = "username";
            paramUsername.Value = newUserData.Username;
            paramUsername.DbType = DbType.String;

            DbParameter paramPassword = cmdUpdate.CreateParameter();
            paramPassword.ParameterName = "password";
            paramPassword.Value = newUserData.Password;
            paramPassword.DbType = DbType.String;

            cmdUpdate.Parameters.Add(paramFirstname);
            cmdUpdate.Parameters.Add(paramLastname);
            cmdUpdate.Parameters.Add(paramEmail);
            cmdUpdate.Parameters.Add(paramGender);
            cmdUpdate.Parameters.Add(paramRole);
            cmdUpdate.Parameters.Add(paramUsername);
            cmdUpdate.Parameters.Add(paramPassword);

            return cmdUpdate.ExecuteNonQuery() == 1;
        }
    }
}