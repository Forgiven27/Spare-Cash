using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.UI;
using uiT = UnityEngine.UIElements;
using System.Reflection.Emit;
using System;
using System.Data;
using DataBank;

public class UI : MonoBehaviour
{
    public GameObject circle;
    public int countCategories = 3;

    


    public UnityEngine.UI.Image a;
    
    public int speedTime = 10;
    public Dictionary<GameObject, int[]> bunchCircles = new Dictionary<GameObject, int[]>();
    public GameObject parentPieGraph;

    public void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        uiT.Button button = root.Q<uiT.Button>("Button");
        uiT.Button inButton = root.Q<uiT.Button>("In");
        uiT.Button deButton = root.Q<uiT.Button>("De");

        //TextField textField = root.Q<TextField>("TextField");
        //uiT.Label label = root.Q<uiT.Label>("Label");

        uiT.DropdownField months = root.Q<uiT.DropdownField>("DropdownField");

        uiT.Label label1 = new uiT.Label("Январь");

        months.choices = new List<string> {"Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

        months.value = months.choices[Convert.ToInt32(UnityEngine.Random.Range(0, 1.1f) * 10)];

        uiT.IntegerField summa = root.Q<uiT.IntegerField>("Summa");
        uiT.RadioButtonGroup income = root.Q<uiT.RadioButtonGroup>("income");
        uiT.DropdownField accounts = root.Q<uiT.DropdownField>("Dropdown_acc");
        uiT.DropdownField categories = root.Q<uiT.DropdownField>("Dropdown_cat");


        List<String> list_acc = new List<String>();
        List<String> list_cat = new List<String>();
        UserDB userDB = new UserDB();
        AccountDB accountDB = new AccountDB(userDB);
        CategoryDB categoryDB = new CategoryDB(userDB);
        

        IDataReader reader = categoryDB.GetAllData();
        while (reader.Read())
        {
            list_cat.Add(reader.GetString(1));
        }
        reader = accountDB.GetAllData();
        while (reader.Read())
        {
            list_acc.Add(reader.GetString(1));
        }

        accounts.choices = list_acc;
        categories.choices = list_cat;
        categoryDB.Close();
        accountDB.Close();
        
        button.clicked += () => AddRecord();








        float summ = 0f;

        for (int i = 0; i < countCategories; i++)
        {
            int[] ints = new int[2] { UnityEngine.Random.Range(0, 0xFFFFFF), UnityEngine.Random.Range(1, 100) };
            bunchCircles.Add(Instantiate(circle, new Vector3(0,0,0), new Quaternion(), parentPieGraph.transform) as GameObject, ints);
            
            summ += ints[1];
        }
        
        foreach (KeyValuePair<GameObject, int[]> circleI in bunchCircles)
        {
            Debug.Log($"{circleI.Key } {circleI.Value[0].ToString("X")}");   
        }

        foreach (GameObject circ in bunchCircles.Keys)
        {
            string hexColor = "#" + bunchCircles[circ][0].ToString();
            float percent = bunchCircles[circ][1] / summ;
            Debug.Log($"Цвет = {hexColor}, Процент = {percent}");

            
            Transform transform = circ.GetComponent<Transform>();
            //transform.SetParent(parentPieGraph.transform);
            transform.Rotate(new Vector3(0, 0, percent * 360));

            UnityEngine.UI.Image image = circ.GetComponent<UnityEngine.UI.Image>();
            image.fillAmount = percent; // Процент визуализации круга

            Color color;
            

            if (UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out color))
            {

                image.color = color;
                circ.SetActive(true);
                transform.localScale = new Vector3(1, 1, 1);

            }
            else
            {
                Debug.LogError("Неверный формат шестнадцатеричного цвета.");
            }
            transform.localScale = new Vector3(1, 1, 1);
            //transform.Translate(new Vector3(0.97f, 410.03f, 0f));
            transform.position = new Vector3 (0.97f, 410.03f, 0.0f);

        }

        /*
        for (int i = 0; i < countCategories; i++)
        {
            GameObject circleLocale = bunchCircles.Keys;


        }
        
        
        foreach (GameObject circ in )
        {
            circ.SetActive(true);
            UnityEngine.UI.Image image = circ.GetComponent<UnityEngine.UI.Image>();
            Color color;
            int hexColor = 
            if (UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out color))
            {

                im.color = color;

            }
            else
            {
                Debug.LogError("Неверный формат шестнадцатеричного цвета.");
            }

        }

        */


        /*
        button.clicked += () => Debug.Log("Hello");

        button.clicked += () => circle.transform.Rotate(new Vector3(0, 0, 10));
        button.clicked += () => changeColor(circle);

        inButton.clicked += () => inCircle();
        deButton.clicked += () => deCircle();

        */
        
    }
    private void inCircle()
    {
        float im_amount = a.fillAmount;
        
        float amount = a.fillAmount + 0.1f;

        a.fillAmount = amount;
        
        
        
    }

    private void deCircle()
    {
        float im_amount = a.fillAmount;
        
        float amount = a.fillAmount - 0.1f;

        a.fillAmount = amount;
        

    }

    private void AddRecord()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        uiT.IntegerField summa = root.Q<uiT.IntegerField>("Summa");
        uiT.IntegerField income = root.Q<uiT.IntegerField>("Income");
        uiT.DropdownField accounts = root.Q<uiT.DropdownField>("Dropdown_acc");
        uiT.DropdownField categories = root.Q<uiT.DropdownField>("Dropdown_cat");
        UserDB userDB = new UserDB();
        var accountDB = new AccountDB(userDB);
        var categoryDB = new CategoryDB(userDB);
        
        RecordDB recordDB = new RecordDB(categoryDB, accountDB, userDB);

        int id = recordDB.CountOfRows() + 1;

        string today = DateTime.Today.ToString("u");

        var reader = categoryDB.GetDataByString(categories.value);
        int id_cat = 0;
        while (reader.Read())
        {
            id_cat = reader.GetInt32(0);
        }
        reader = accountDB.GetDataByString(accounts.value);
        int id_acc = 0;
        while (reader.Read())
        {
            id_acc = reader.GetInt32(0);
        }
        int id_user = UserInfo.UserID;
        
        

        Debug.Log($"{id}");
        Debug.Log($"{summa.value}");
        Debug.Log($"{income.value}");
        Debug.Log($"{today}");
        Debug.Log($"{id_cat}");
        Debug.Log($"{id_acc}");
        Debug.Log($"{id_user}");
        recordDB.addData(new RecordEntity(id, summa.value, income.value, today, id_cat, id_acc, id_user));
        //Debug.Log(recordDB.GetRowById(id));
        recordDB.Close();
        categoryDB.Close();
        accountDB.Close();
        
    }
    /*

    private void changeColor(GameObject obj)
    {
        System.Random circ_num = new System.Random();

        string hexColor = "#" + bunchCircles[circ_num.Next(0, 9).ToString()].ToString("X");
        Color color;
        UnityEngine.UI.Image im = obj.GetComponent<UnityEngine.UI.Image>();

        if (UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            
            im.color = color;
            
        }
        else
        {
            Debug.LogError("Неверный формат шестнадцатеричного цвета.");
        }
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            circle.transform.Rotate(new Vector3(0, 0, 10) * UnityEngine.Time.deltaTime * speedTime);
            changeColor(circle);
        }
        /*
        if (Input.GetKey(KeyCode.W))
        {
            backCircle.transform.Rotate(new Vector3(0, 0, -10) * UnityEngine.Time.deltaTime * speedTime);
            changeColor(backCircle);
        }
    }*/


}
