using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace DataBank{ 
public class UserDB: SqliteHelper
{
        private const String Tag = "Riz: AccountDB:\t";

        public const String TABLE_NAME = "User";
        public String Table_Name
        {
            get { return TABLE_NAME; }
        }
        private const String KEY_ID = "id";
        private const String KEY_LOGIN = "login";
        private const String KEY_PASSWORD = "password";
        private const String KEY_TYPE_OF_USER = "type";

        private String[] COLUMNS = new String[] { KEY_ID, KEY_LOGIN, KEY_PASSWORD, KEY_TYPE_OF_USER };


        public UserDB() : base()
        {
            IDbCommand dbcmd = GetDbCommand();
            // string drop_table = "DROP TABLE IF EXISTS " + TABLE_NAME + ";";
            dbcmd.CommandText = " CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER UNIQUE PRIMARY KEY , " +
                KEY_LOGIN + " TEXT ," +
                KEY_PASSWORD + " TEXT ," +
                KEY_TYPE_OF_USER + " TEXT" +
                " );";
            dbcmd.ExecuteNonQuery();
        }


        public void addData(UserEntity description)
        {
            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_ID + ", "
                + KEY_LOGIN + ", "
                + KEY_PASSWORD + ", "
                + KEY_TYPE_OF_USER + " ) "

                + "VALUES ( "
                + description.id + ", '"
                + description.login + "', '"
                + description.password + "', '"
                + description.typeOfUser + "' )";
            dbcmd.ExecuteNonQuery();
        }




        public int CountOfRows()
        {
            return base.CountOfRows(TABLE_NAME);
        }




        public override IDataReader GetDataByString(string str)
        {

            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_LOGIN + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public List<List<string>> GetUserData()
        {
            List<List<string>> data = new List<List<String>>();
            IDataReader reader = null;
            reader = GetAllData();
            while (reader.Read())
            {
                List<string> row = new List<string>();

                row.Add(reader.GetInt32(0).ToString());
                row.Add(reader.GetString(1));
                row.Add(reader.GetString(2));
                row.Add(reader.GetString(3));
                data.Add(row);
            }
            return data;
        }
        public override void DeleteDataByString(string id)
        {
            Debug.Log(Tag + "Deleting Location: " + id);

            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void DeleteDataById(int id)
        {
            Debug.Log(Tag + "Deleting Location: " + id);

            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void DeleteAllData()
        {
            Debug.Log(Tag + "Deleting Table");

            base.DeleteAllData(TABLE_NAME);
        }

        public override IDataReader GetAllData()
        {
            return base.GetAllData(TABLE_NAME);
        }


    }
}