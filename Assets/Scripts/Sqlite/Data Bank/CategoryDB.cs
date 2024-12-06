using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.Linq;
using System.Text;

namespace DataBank { 
public class CategoryDB : SqliteHelper
{
        private const String Tag = "Riz: CategoryDB:\t";
        
        public const String TABLE_NAME = "Category";
        public String Table_Name
        {
            get { return TABLE_NAME; }
        }

        private const String KEY_ID = "id";
        private const String KEY_NAME = "name";
        private const String KEY_COLOR = "color";
        private const String KEY_ID_USER = "id_user";
        private String[] COLUMNS = new String[] { KEY_ID, KEY_NAME, KEY_COLOR, KEY_ID_USER };


        public CategoryDB(UserDB table_user) : base()
        {
            IDbCommand dbcmd = GetDbCommand();
            // string drop_table = "DROP TABLE IF EXISTS " + TABLE_NAME + ";";
            dbcmd.CommandText = 
                "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER UNIQUE PRIMARY KEY , " +
                KEY_NAME + " TEXT, " +
                KEY_COLOR + " TEXT," +
                KEY_ID_USER + " INTEGER , " +
                "FOREIGN KEY(" + KEY_ID_USER +
                ") REFERENCES " + table_user.Table_Name + "(id)" +
                ") ;";
            dbcmd.ExecuteNonQuery();
        }


        public void addData(EntityCategory description)
        {
            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_ID + ", "
                + KEY_NAME + ", "
                + KEY_COLOR + ", "
                + KEY_ID_USER + " ) "

                + "VALUES ( "
                + description.id + ", '"
                + description.name + "', '"
                + description.color + "', '"
                + description.id_user + "' )";
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
        public string GetColorById(int id)
        {
            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "SELECT "+ KEY_COLOR + " FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = " + id + "";
            var obj = dbcmd.ExecuteScalar();
            if (obj == DBNull.Value)
            {
                Debug.LogError("Отсутствует цвет по данному ID");
                return ""; // Возвращаем значение по умолчанию, если результат DBNull
            }
            return obj.ToString();
        }
        public List<List<string>> GetCategoryData()
        {
            List<List<string>> data = new List<List<String>>();
            IDataReader reader = null;
            reader = GetAllData();
            while (reader.Read()) 
            {
                List<string> row = new List<string>();
                Debug.Log("ID " + reader.GetInt32(0).ToString());
                Debug.Log("NAME " + reader.GetString(1));
                Debug.Log("COLOR " + reader.GetString(2));
                Debug.Log("ID_USER " + reader.GetInt32(3).ToString());

                row.Add(reader.GetInt32(0).ToString());
                row.Add(reader.GetString(1));
                row.Add(reader.GetString(2));
                row.Add(reader.GetInt32(3).ToString());
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
