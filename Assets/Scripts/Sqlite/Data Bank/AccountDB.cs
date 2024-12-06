using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;

namespace DataBank { 
public class AccountDB : SqliteHelper
{
        
        private const String Tag = "Riz: AccountDB:\t";

        public const String TABLE_NAME = "Account";
        public String Table_Name
        {
            get { return TABLE_NAME; }
        }
        private const String KEY_ID = "id";
        private const String KEY_ID_USER = "id_user";
        private const String KEY_NAME = "name";
        
        private String[] COLUMNS = new String[] { KEY_ID, KEY_ID_USER, KEY_NAME };


        public AccountDB(UserDB table_user) : base()
        {
            IDbCommand dbcmd = GetDbCommand();
            // string drop_table = "DROP TABLE IF EXISTS " + TABLE_NAME + ";";
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER UNIQUE PRIMARY KEY , " +
                KEY_ID_USER + " INTEGER," +
                KEY_NAME + " TEXT," +
                "FOREIGN KEY(" + KEY_ID_USER +
                ") REFERENCES " + table_user.Table_Name + "(id)" +
                ") ;";
            dbcmd.ExecuteNonQuery();
        }


        public void addData(AccountEntity description)
        {
            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_ID + ", "
                + KEY_ID_USER + ", "
                + KEY_NAME  + " ) "

                + "VALUES ( "
                + description.id + ", "
                + description.id_user + ", '"
                + description.name +  "' )";
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
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_NAME + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public List<List<string>> GetAccountData()
        {
            List<List<string>> data = new List<List<String>>();
            IDataReader reader = null;
            reader = GetAllData();
            while (reader.Read())
            {
                List<string> row = new List<string>();

                row.Add(reader.GetInt32(0).ToString());
                row.Add(reader.GetInt32(1).ToString());
                row.Add(reader.GetString(2));
                
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
        public override void DeleteDataByIdUser(int id)
        {
            Debug.Log(Tag + "Deleting Location: " + id);

            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID_USER + " = '" + id + "'";
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
