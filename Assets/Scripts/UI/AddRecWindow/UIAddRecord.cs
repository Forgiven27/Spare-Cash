using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataBank;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using System;
using Unity.VisualScripting;

public class UIAddRecord : MonoBehaviour
{
    UnityEngine.UIElements.Label labelHead;
    UnityEngine.UIElements.Button buttonAddRecord;  
    UnityEngine.UIElements.UnsignedIntegerField summField;
    UnityEngine.UIElements.Toggle toggleIncome;
    UnityEngine.UIElements.DropdownField dropdownDay;
    UnityEngine.UIElements.DropdownField dropdownMonth;
    UnityEngine.UIElements.DropdownField dropdownYear;
    UnityEngine.UIElements.DropdownField dropdownCategory;
    UnityEngine.UIElements.Button buttonToAddCatScene;
    UserDB userDB;
    AccountDB accountDB;
    CategoryDB categoryDB;
    RecordDB recordDB;

    List<string> monthsEnum = new List<string>()
    {
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"
    };
    int currentYear;
    
    void Awake()
    {
        //this.gameObject.SetActive(false);

        var root = GetComponent<UIDocument>().rootVisualElement;
        if (root == null)
        {
            Debug.LogError("Root VisualElement not found!");
            return;
        }
        // Инициализация элементов UI Toolkit
        labelHead = root.Q<UnityEngine.UIElements.Label>("HeadName");
        buttonAddRecord = root.Q<UnityEngine.UIElements.Button>("AddRecButton");
        summField = root.Q<UnityEngine.UIElements.UnsignedIntegerField>("Summ");
        toggleIncome = root.Q<UnityEngine.UIElements.Toggle>("Income");
        dropdownDay = root.Q<UnityEngine.UIElements.DropdownField>("DayDateField");
        dropdownMonth = root.Q<UnityEngine.UIElements.DropdownField>("MonthDateField");
        dropdownYear = root.Q<UnityEngine.UIElements.DropdownField>("YearDateField");
        dropdownCategory = root.Q<UnityEngine.UIElements.DropdownField>("CategoriesDropdown");
        buttonToAddCatScene = root.Q<UnityEngine.UIElements.Button>("AddCat");
        
        // Добавление статический подписей 
        labelHead.text = "Добавление записи";
        toggleIncome.label = "Это доход?";
        buttonAddRecord.text = "Добавить запись";


        DateTime currentDateTime = DateTime.Now;
        currentYear = currentDateTime.Year;
        dropdownDay.choices = CountDaysInMonth(currentDateTime.Month, currentYear);
        dropdownMonth.choices = ShiftList(currentDateTime.Month.ToString(), monthsEnum);    
        dropdownYear.choices = new List<string>()
        {
            currentYear.ToString(),
            (currentYear - 1).ToString(),
            (currentYear - 2).ToString(),
            (currentYear - 3).ToString(),
            (currentYear - 4).ToString(),
            (currentYear - 5).ToString()
        };
        dropdownDay.value = currentDateTime.Day.ToString();
        dropdownMonth.value = currentDateTime.Month.ToString();
        dropdownYear.value = currentYear.ToString();

        userDB = new UserDB();
        accountDB = new AccountDB(userDB);
        categoryDB = new CategoryDB(userDB);
        recordDB = new RecordDB(categoryDB, accountDB, userDB);

        List<List<string>> listCat = categoryDB.GetUserCategoryData(UserInfo.UserID);

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
        buttonToAddCatScene.clicked += buttonToAddCatSceneClicked;


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
            SceneManager.LoadScene("MainScene");
        }
    }
    void buttonToAddCatSceneClicked()
    {
        SceneManager.LoadScene("AddCatScene");
    }

    private void OnBackButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

    private bool AddRecord()
    {
        bool flag = true;

        if (summField.value != 0)
        {
            
            int id = recordDB.CountOfRows() + 1;
            int summa = Convert.ToInt32(summField.value);
            int income = 0;
            if (toggleIncome.value)
            {
                income = 1;
            }

            int day = Convert.ToInt32(dropdownDay.value);
            int month = Convert.ToInt32(dropdownMonth.value);
            int year = Convert.ToInt32(dropdownYear.value);
            string date = new DateTime(year, month, day).ToString("dd-MM-yyyy"); ;

            
            int id_cat = dropdownCategory.choices.IndexOf(dropdownCategory.value);
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

    List<string> CountDaysInMonth(int month, int year)
    {
        List<string> days = new List<string>();
        int countDays = DateTime.DaysInMonth(year, month);
        for (int i = 1;i <= countDays; i++)
        {
            days.Add(i.ToString());
        }
        return days;
    }

    List<string> ShiftList(string targetElement, List<string> listOfElement)
    {
        List<string> answer = new List<string>();
        int startIndex = listOfElement.IndexOf(targetElement);
        for (int i = 0; i < listOfElement.Count; i++)
        {
            if (startIndex > 0)
            {
                answer.Add(listOfElement[startIndex]);
                startIndex --;
            }
            else
            {
                answer.Add(listOfElement[startIndex]);
                startIndex += listOfElement.Count - 1;
            }
        }
        return answer;
    }

    
}
