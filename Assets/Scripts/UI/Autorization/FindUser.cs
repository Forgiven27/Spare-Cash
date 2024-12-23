using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Data;

public class FindUser : MonoBehaviour
{
    TextField loginField;
    TextField passwordField;
    UserDB userDB;
    void Start()
    {
        userDB = new UserDB();
        var root = GetComponent<UIDocument>().rootVisualElement;
        var buttonLogin = root.Q<Button>("LoginButton");
        var buttonGuest = root.Q<Button>("GuestButton");
        loginField = root.Q<TextField>("LoginTextField");
        passwordField = root.Q<TextField>("PasswordTextField");
        buttonLogin.clicked += ButtonLogin_clicked;
        buttonGuest.clicked += ButtonGuest_clicked;
        

    }

    private void ButtonLogin_clicked()
    {
        string log = loginField.text;
        string pass = passwordField.value;
        Debug.Log(log + " " + pass);
        /*
        IDataReader reader = userDB.GetDataByString(log);
        if (reader != null)
        {
            Debug.Log("Строка найдена");
        }
        else
        {
            Debug.Log("Строка не найдена");
        }
        */
        
        int userId = userDB.IsUser(log, pass);
        if (userId >= 0)
        {
            Debug.Log("true " + userId);
            UserInfo.UserID = userId;
            OpenMainScene();
        }
    }
    
    void ButtonGuest_clicked()
    {
        Debug.Log("Вы вошли как гость");
        UserInfo.UserID = 0;
        OpenMainScene();
    }

    void OpenMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
