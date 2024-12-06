using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using System.Data;

public class TestingCategory : MonoBehaviour
{
    
    void Start()
    {
        UserDB userDB = new UserDB();
        AccountDB accountDB = new AccountDB(userDB);
        CategoryDB categoryDB = new CategoryDB(userDB);

        categoryDB.addData(new EntityCategory(1, "Food", "#111111", 1) );
        categoryDB.addData(new EntityCategory(2, "Car", "#222222", 1));
        categoryDB.addData(new EntityCategory(3, "Meds", "#333333", 1));
        categoryDB.addData(new EntityCategory(4, "Other", "#444444", 1));
        categoryDB.addData(new EntityCategory(5, "Toys", "#555555", 1));
        categoryDB.addData(new EntityCategory(6, "Work", "#666666", 1));
        IDataReader reader = categoryDB.GetAllData();

        while (reader.Read())
        {
            Debug.Log(reader.GetInt32(0) + " " +  reader.GetString(1) + " " + reader.GetString(2));
        }



        accountDB.addData(new AccountEntity(0, 1, "Наличные"));
        accountDB.addData(new AccountEntity(1, 1, "Крипто-валюта"));
        accountDB.addData(new AccountEntity(2, 1, "Банковская карта Н"));



        RecordDB recordDB = new RecordDB(categoryDB, accountDB, userDB);

        categoryDB.Close();
        accountDB.Close();
        recordDB.Close();
        





        /*
        IDataReader reader = categoryDB2.getAllData();

        List<EntityCategory> list = new List<EntityCategory>();

        int fieldCount = reader.FieldCount;
        
        while (reader.Read())
        {
            EntityCategory entity = new EntityCategory(reader[0].ToString(),
                                    reader[1].ToString(),
                                    reader[2].ToString());

            Debug.Log("id: " + entity._id + "  это чтооо");
            list.Add(entity);
        }
        */

    }

    
}
