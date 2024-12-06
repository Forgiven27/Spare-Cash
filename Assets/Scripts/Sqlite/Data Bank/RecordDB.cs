using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms;

namespace DataBank { 
public class RecordDB : SqliteHelper
{
        private const String Tag = "Riz: RecordDB:\t";

        private const String TABLE_NAME = "Record";
        private const String KEY_ID = "id";
        private const String KEY_SUMMA = "summa";
        private const String KEY_INCOME = "income";
        private const String KEY_DATE = "date";
        private const String KEY_ID_CAT = "id_cat";
        private const String KEY_ID_ACC = "id_acc";
        private const String KEY_ID_USER = "id_user";

        private String[] COLUMNS = new String[] { KEY_ID, KEY_SUMMA, KEY_INCOME, KEY_DATE, KEY_ID_CAT, KEY_ID_ACC, KEY_ID_USER };


        public RecordDB(CategoryDB table_cat, AccountDB table_acc, UserDB table_user) : base()
        {
            IDbCommand dbcmd = GetDbCommand();
            // string drop_table = "DROP TABLE IF EXISTS " + TABLE_NAME + ";";
            dbcmd.CommandText = 
                " CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER UNIQUE PRIMARY KEY , " +
                KEY_SUMMA + " INTEGER , " +
                KEY_INCOME + " INTEGER , " +
                KEY_DATE + "  TEXT , " +
                KEY_ID_CAT + "  INTEGER , " +
                KEY_ID_ACC + "  INTEGER , " +
                KEY_ID_USER + "  INTEGER , " +
                "FOREIGN KEY(" + KEY_ID_CAT +
                ") REFERENCES " + table_cat.Table_Name + "(id)  , " +
                "FOREIGN KEY(" + KEY_ID_ACC +
                ") REFERENCES " + table_acc.Table_Name + "(id)  , " +
                "FOREIGN KEY(" + KEY_ID_USER +
                ") REFERENCES " + table_user.Table_Name + "(id) ) ;";
            dbcmd.ExecuteNonQuery();
        }


        public void addData(RecordEntity description)
        {
            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_ID + ", "
                + KEY_SUMMA + ", "
                + KEY_INCOME + ", "
                + KEY_DATE + ", "
                + KEY_ID_CAT + ", "
                + KEY_ID_ACC + ", "
                + KEY_ID_USER + " ) "


                + "VALUES ( "
                + description.id + ", "
                + description.summa + ", "
                + description.income + ", '"
                + description.date + "', "
                + description.id_cat + ", "
                + description.id_acc + ", "
                + description.id_user + " )";
            dbcmd.ExecuteNonQuery();
        }

        public void DeleteData(int id)
        {
            IDbCommand delcmd = GetDbCommand();
            delcmd.CommandText =
                "DELETE FROM "+
                TABLE_NAME + " WHERE " +
                KEY_ID + "=" + id + ";";
            delcmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Выводит сумму расходов/доходов одной категории у определенного аккаунта
        /// </summary>
        /// <param name="id_cat"></param>
        /// <param name="id_acc"></param>
        /// <param name="income"></param>
        /// <returns></returns>
        public int GetSummCategory(int id_cat, int id_acc, int income, int userId)
        {
            

            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "SELECT sum(" +
                KEY_SUMMA + ") FROM " +
                TABLE_NAME + " WHERE " +
                KEY_ID_CAT + "=" + id_cat +
                " AND " +
                KEY_ID_USER + "=" + userId +
                " AND " +
                KEY_ID_ACC + "=" + id_acc +
                " AND " +
                KEY_INCOME + "=" + income + ";";
            var obj = dbcmd.ExecuteScalar();
            if (obj == DBNull.Value)
            {
                return 0; // Возвращаем значение по умолчанию, если результат DBNull
            }
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// Выводит сумму доходов/расходов по всем категориям определенного аккаунта 
        /// </summary>
        /// <param name="id_cat"></param>
        /// <param name="id_acc"></param>
        /// <param name="income"></param>
        /// <returns></returns>
        public int GetSummAccount(int id_acc, int income, int userId)
        {
            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "SELECT sum(" +
                KEY_SUMMA + ") FROM " +
                TABLE_NAME + " WHERE " +
                KEY_ID_USER + "=" + userId +
                " AND " +
                //KEY_ID_ACC + "=" + id_acc +
                //" AND " +
                KEY_INCOME + "=" + income + ";";
            var obj = dbcmd.ExecuteScalar();
            if (obj == DBNull.Value)
            {
                Debug.LogError("Сумма доходов/расходов по определенной категории не может быть выведена");
                return 0; // Возвращаем значение по умолчанию, если результат DBNull
            }
            return Convert.ToInt32(obj);
        }

        


        public string GetRowById(int id)
        {
            IDbCommand dbcmd = GetDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = " + id + ";";
            var reader = dbcmd.ExecuteReader();
            string row = "";
            while (reader.Read())
            {
                row = reader.GetInt32(0) + " " + reader.GetInt32(1) +
                     " " + reader.GetInt32(2) +
                     " " + reader.GetInt32(3) +
                     " " + reader.GetString(4) +
                     " " + reader.GetInt32(5);
            }


            return row;
        }



        public int CountOfRows()
        {
            return base.CountOfRows(TABLE_NAME);
        }

        public List<List<string>> GetRecordData()
        {
            List<List<string>> data = new List<List<String>>();
            IDataReader reader = null;
            reader = GetAllData();
            while (reader.Read())
            {
                List<string> row = new List<string>();

                row.Add(reader.GetInt32(0).ToString());
                row.Add(reader.GetInt32(1).ToString());
                row.Add(reader.GetInt32(2).ToString());
                row.Add(reader.GetString(3));
                row.Add(reader.GetInt32(4).ToString());
                row.Add(reader.GetInt32(5).ToString());
                data.Add(row);
            }
            return data;
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
        /*
        public IDataReader getNearestLocation(LocationInfo loc)
        {
            Debug.Log(Tag + "Getting nearest centoid from: "
                + loc.latitude + ", " + loc.longitude);
            IDbCommand dbcmd = getDbCommand();

            string query =
                "SELECT * FROM "
                + TABLE_NAME
                + " ORDER BY ABS(" + KEY_LAT + " - " + loc.latitude
                + ") + ABS(" + KEY_LNG + " - " + loc.longitude + ") ASC LIMIT 1";

            dbcmd.CommandText = query;
            return dbcmd.ExecuteReader();
        }

        public IDataReader getLatestTimeStamp()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " ORDER BY " + KEY_DATE + " DESC LIMIT 1";
            return dbcmd.ExecuteReader();
        }

        */


    }
}