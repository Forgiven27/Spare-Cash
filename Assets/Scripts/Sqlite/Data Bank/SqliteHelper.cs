using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace DataBank
{

    public class SqliteHelper
    {
        private const string Tag = "Riz: SqliteHelper:\t";

        private const string database_name = "my_db";

        public string db_connection_string;
        public IDbConnection db_connection;

        public SqliteHelper()
        {
            db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
            Debug.Log("db_connection_string" + db_connection_string);
            db_connection = new SqliteConnection(db_connection_string);
            db_connection.Open();
        }

        ~SqliteHelper()
        {
            db_connection.Close();
        }

        // virtual functions

        public virtual int CountOfRows(string table_name)
        {
            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "SELECT COUNT(*) FROM " +
                table_name;
            var obj = dbcmd.ExecuteScalar();
            return Convert.ToInt32(obj);
        }
        /*
        public virtual IDataReader GetDataById(int id)
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }
        */
        public virtual IDataReader GetDataByString(string str)
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual void DeleteDataById(int id)
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }
        public virtual void DeleteDataByIdUser(int id_user)
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        public virtual void DeleteDataByString(string id)
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        public virtual IDataReader GetAllData()
        {
            Debug.Log(Tag + "This function is not implemented");
            throw null;
        }

        public virtual void DeleteAllData()
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        public virtual IDataReader GetNumOfRows()
        {
            Debug.Log(Tag + "This function is not implemnted");
            throw null;
        }

        //helper functions
        public IDbCommand GetDbCommand()
        {
            return db_connection.CreateCommand();
        }

        public IDataReader GetAllData(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + table_name;
            IDataReader reader = dbcmd.ExecuteReader();
            return reader;
        }

        public void DeleteAllData(string table_name)
        {
            
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
            dbcmd.ExecuteNonQuery();
            
        }

        public IDataReader GetNumOfRows(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText =
                "SELECT COALESCE(MAX(id)+1, 0) FROM " + table_name;
            IDataReader reader = dbcmd.ExecuteReader();
            return reader;
        }

        public void Close()
        {
            db_connection.Close();
        }
    }
}