using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using System;

public class UIAddRecord : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI;
    
    void Awake()
    {
        //this.gameObject.SetActive(false);

        var root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null)
        {
            Debug.LogError("Root VisualElement not found!");
            return;
        }
        UnityEngine.UIElements.Label labelHead = root.Q<UnityEngine.UIElements.Label>("HeadName");
        UnityEngine.UIElements.Button buttonAddRecord = root.Q<UnityEngine.UIElements.Button>("AddRecButton");
        UnityEngine.UIElements.UnsignedIntegerField summField = root.Q<UnityEngine.UIElements.UnsignedIntegerField>("Summ");
        UnityEngine.UIElements.Toggle toggleIncome = root.Q<UnityEngine.UIElements.Toggle>("Income");
        UnityEngine.UIElements.DropdownField dropdownDate = root.Q<UnityEngine.UIElements.DropdownField>("EasyDate");
        UnityEngine.UIElements.DropdownField dropdownCategory = root.Q<UnityEngine.UIElements.DropdownField>("CategoriesDropdown");
        


        labelHead.text = "Добавление записи";
        
        toggleIncome.label = "Это доход?";
        
        buttonAddRecord.text = "Добавить запись";

        dropdownDate.choices = new List<string> { "Сегодня", "Вчера", "Позавчера" };

        UserDB userDB = new UserDB();
        AccountDB accountDB = new AccountDB(userDB);
        CategoryDB categoryDB = new CategoryDB(userDB);

        List<List<string>> listCat = categoryDB.GetCategoryData();

        List<string> categories = new List<string>();

        foreach (List<string> cat in listCat)
        {
            categories.Add(cat[1]);
        }

        dropdownCategory.choices = categories; 

        //VisualElement textFieldContainer = summField.Q<VisualElement>(null, "unity-unsigned-integer-field__input");
        //Label textFieldLabel = summField.Q<Label>(null, "unity-unsigned-integer-field__label");
        
        // textFieldLabel.style.width = 25;



        buttonAddRecord.clicked += buttonAddClicked;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            OnBackButtonPressed();
        }
    }

    private void buttonAddClicked()
    {
        bool answer = AddRecord();
        if (answer)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnBackButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    private bool AddRecord()
    {
        bool flag = true;

        var root = GetComponent<UIDocument>().rootVisualElement;
        
        UnityEngine.UIElements.UnsignedIntegerField summField = root.Q<UnityEngine.UIElements.UnsignedIntegerField>("Summ");
        UnityEngine.UIElements.Toggle toggleIncome = root.Q<UnityEngine.UIElements.Toggle>("Income");
        UnityEngine.UIElements.DropdownField dropdownDate = root.Q<UnityEngine.UIElements.DropdownField>("EasyDate");
        UnityEngine.UIElements.DropdownField dropdownCategory = root.Q<UnityEngine.UIElements.DropdownField>("CategoriesDropdown");

        if (summField.value != 0)
        {
            UserDB userDB = new UserDB();
            AccountDB accountDB = new AccountDB(userDB);
            RecordDB recordDB = new RecordDB(new CategoryDB(userDB), accountDB, userDB);
            int id = recordDB.CountOfRows() + 1;
            int summa = Convert.ToInt32(summField.value);
            int income = 0;
            if (toggleIncome.value)
            {
                income = 1;
            }
            string date = DateTime.Today.ToString("u"); ;
            switch (dropdownDate.value)
            {
                case "Сегодня":
                    date = DateTime.Today.ToString("u");
                    break;
                case "Вчера":
                    date = DateTime.Today.AddDays(-1).ToString("u");
                    break;
                case "Позавчера":
                    date = DateTime.Today.AddDays(-2).ToString("u");
                    break;
            }
            int id_cat = dropdownCategory.choices.IndexOf(dropdownCategory.value) + 1;
            int id_acc = 1;
            int id_user = UserInfo.UserID;
            recordDB.addData(new RecordEntity(id, summa, income, date, id_cat, id_acc, id_user));

        }
        else
        {
            return flag = false;
        }
        return flag;
    }

    /*
    public void SetActiveUI()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        // UnityEngine.UIElements.VisualElement mainContainer = root.Q<UnityEngine.UIElements.VisualElement>("main-container");
        UnityEngine.UIElements.Button buttonAddRecord = root.Q<UnityEngine.UIElements.Button>("AddRecButton");
        UnityEngine.UIElements.TextField summField = root.Q<UnityEngine.UIElements.TextField>("Summ");
        UnityEngine.UIElements.DropdownField catDropdown = root.Q<UnityEngine.UIElements.DropdownField>("CategoriesDropdown");

        

        if (root.visible == true)
        {
            Debug.Log("TRUE");
            root.visible = true;
            //buttonAddRecord.visible = true;
            //summField.visible = true;
            //catDropdown.visible = true;
        }
        else
        {
            Debug.Log("FALSE");
            root.visible = true;
            //buttonAddRecord.visible = false;
            //summField.visible = false;
            //catDropdown.visible = false;
        }
    }*/
    
    public void TestPlus()
    {
        SetActiveUI(false);
    }
    
    public void SetActiveUI(bool flag)
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        // UnityEngine.UIElements.VisualElement mainContainer = root.Q<UnityEngine.UIElements.VisualElement>("main-container");
        UnityEngine.UIElements.Button buttonAddRecord = root.Q<UnityEngine.UIElements.Button>("AddRecButton");
        UnityEngine.UIElements.TextField summField = root.Q<UnityEngine.UIElements.TextField>("Summ");
        UnityEngine.UIElements.DropdownField catDropdown = root.Q<UnityEngine.UIElements.DropdownField>("CategoriesDropdown");

        

        if (flag)
        {
            root.visible = true;
            //buttonAddRecord.visible = true;
            //summField.visible = true;
            //catDropdown.visible = true;
        }
        else
        {
            root.visible = false;
            //buttonAddRecord.visible = false;
            //summField.visible = false;
       
            //catDropdown.visible = false;
        }
    }
}
