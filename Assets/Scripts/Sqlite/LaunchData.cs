using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using System.Data;
using System;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LaunchData : MonoBehaviour
{

    void OnEnable()
    {
        DeleteTables();
    }
    
    
    private void RefillTables()
    {
        UserDB userDB = new UserDB();
        
        if (userDB.CountOfRows() == 0)
        {   
            userDB.addData(new UserEntity(0, "test", "0000", "b"));
            userDB.addData(new UserEntity(1, "andrey_01", "1111", "b"));
        }
        
        AccountDB accountDB = new AccountDB(userDB);
        
        if (accountDB.CountOfRows() == 0)
        {
            accountDB.addData(new AccountEntity(0, 1, "Наличные"));
            accountDB.addData(new AccountEntity(1, 1, "Крипто-валюта"));
            accountDB.addData(new AccountEntity(2, 1, "Банковская карта Н"));
        }
        
        CategoryDB categoryDB = new CategoryDB(userDB);
        if (categoryDB.CountOfRows() == 0)
        {
            categoryDB.addData(new EntityCategory(0, "Food", "#ef476f", 1));
            categoryDB.addData(new EntityCategory(1, "Car", "#f78c6b", 1));
            categoryDB.addData(new EntityCategory(2, "Meds", "#06d6a0", 1));
            categoryDB.addData(new EntityCategory(3, "Other", "#073b4c", 1));
            categoryDB.addData(new EntityCategory(4, "Toys", "#118ab2", 1));
            categoryDB.addData(new EntityCategory(5, "Work", "#ffd166", 1));

        }
        RecordDB recordDB = new RecordDB(categoryDB, accountDB, userDB);
        if (recordDB.CountOfRows() == 0)
        {
            recordDB.addData(new RecordEntity(0, 100, 0, DateTime.Today.ToString("dd-MM-yyyy"), 0, 1, 1));
            recordDB.addData(new RecordEntity(1, 150, 0, DateTime.Today.ToString("dd-MM-yyyy"), 1, 1, 1));
            recordDB.addData(new RecordEntity(2, 200, 0, DateTime.Today.ToString("dd-MM-yyyy"), 2, 1, 1));
            recordDB.addData(new RecordEntity(3, 100, 0, DateTime.Today.ToString("dd-MM-yyyy"), 3, 1, 1));
            recordDB.addData(new RecordEntity(4, 150, 0, DateTime.Today.ToString("dd-MM-yyyy"), 4, 1, 1));
            recordDB.addData(new RecordEntity(5, 200, 0, DateTime.Today.ToString("dd-MM-yyyy"), 5, 1, 1));
            recordDB.addData(new RecordEntity(6, 100, 0, DateTime.Today.ToString("dd-MM-yyyy"), 1, 1, 1));
            recordDB.addData(new RecordEntity(7, 150, 0, DateTime.Today.ToString("dd-MM-yyyy"), 0, 1, 1));

        }
        categoryDB.Close();
        accountDB.Close();
        recordDB.Close();

    }
    
    private void DeleteTables() // Ломает всё
    {
        UserDB userDB = new UserDB(1);
        AccountDB accountDB = new AccountDB(userDB, 1);
        CategoryDB categoryDB = new CategoryDB(userDB, 1);
        RecordDB recordDB = new RecordDB(categoryDB, accountDB, userDB, 1);

        //categoryDB.Close();
        //accountDB.Close();
        //recordDB.Close();

        RefillTables();
        //SceneManager.LoadScene("MainScene");
    }
    
    
}
