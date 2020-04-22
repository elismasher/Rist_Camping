using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
namespace Rist_Camping.Models.db
{
    public class RepositoryBase : IRepositoryBase
    {
        protected string _connectionString = "Server=localhost; Database=db_Rist_Camping; Uid=root; Pwd=Klexi2408;";
        protected MySqlConnection _connection;

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
    }
}