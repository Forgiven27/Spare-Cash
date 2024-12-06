using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class SqliteTest : MonoBehaviour
{
    

    
    void Awake()
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/My_Database";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand dbcmd;
        IDataReader reader;

        dbcmd = dbcon.CreateCommand();
        string q_createTable =
          "DROP TABLE IF EXISTS my_table;CREATE TABLE IF NOT EXISTS my_table (id INTEGER UNIQUE PRIMARY KEY, val INTEGER )";

        dbcmd.CommandText = q_createTable;
        reader = dbcmd.ExecuteReader();

        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO my_table (id, val) VALUES (0, 5)";
        cmnd.ExecuteNonQuery();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        string query = "SELECT * FROM my_table";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader(); 
        while (reader.Read())
        {
            Debug.Log("id: " + reader[0].ToString());
            Debug.Log("val: " + reader[1].ToString());
        }
        dbcon.Close();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
