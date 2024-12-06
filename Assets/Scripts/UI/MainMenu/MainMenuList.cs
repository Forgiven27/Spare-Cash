using DataBank;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuList : MonoBehaviour
{
    [SerializeField]
    private VisualTreeAsset listItemTemplate;

    private ListView listView;
    private VisualElement mainContainer;

    
    List<List<string>> list_data;
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        mainContainer = root.Q<VisualElement>("main-container");
        listView = root.Q<ListView>("list-view");
        VisualElement head = root.Q<VisualElement>("Head");
        VisualElement pieGraph = root.Q<VisualElement>("PieGraph");
        VisualElement floor = root.Q<VisualElement>("Floor");
        head.style.height = Length.Percent(10);
        pieGraph.style.height = Length.Percent(40);
        floor.style.height = Length.Percent(10);
        // Устанавливаем размеры контейнера и ListView
        /*mainContainer.style.width = Length.Percent(100);
        mainContainer.style.height = Length.Percent(50);
        */
        listView.style.width = Length.Percent(100);
        //listView.style.height = Length.Percent(50);

        // Устанавливаем скроллбар как невидимый
        listView.Q<ScrollView>().verticalScroller.style.display = DisplayStyle.None;

        UserDB userDB = new UserDB();
        AccountDB accountDB = new AccountDB(userDB);
        CategoryDB categoryDB = new CategoryDB(userDB);
        
        RecordDB recordDB = new RecordDB(categoryDB, accountDB, userDB);


        list_data = new List<List<string>>();

        UpdateListData();/*
        foreach (var item in list_data)
        {
            foreach (var s in item)
            {
                Debug.Log(s);   
            }
        }
        */
        // Создаем список данных для ListView
        var data = new[]
        {
            new { text = "Item 1", value = "16.7%", color = Color.red, logo = Resources.Load<Texture2D>("logo1") },
            new { text = "Item 2", value = "22.0%", color = Color.green, logo = Resources.Load<Texture2D>("logo2") },
            new { text = "Item 3", value = "30.7%", color = Color.blue, logo = Resources.Load<Texture2D>("logo3") },
            // Добавьте больше элементов для проверки скроллинга
        };

        // Устанавливаем источник данных для ListView
        listView.itemsSource = list_data;

        // Создаем элементы списка
        listView.makeItem = () =>
        {
            var item = listItemTemplate.CloneTree();
            item.Q<VisualElement>("circle").style.backgroundColor = new StyleColor(Color.clear);
            return item;
        };
        UpdateValues();
        // Привязываем данные к элементам списка
        /*listView.bindItem = (element, index) =>
        {
            var item = data[index];
            var circle = element.Q<VisualElement>("circle");
            var logo = element.Q<Image>("logo");
            var text = element.Q<Label>("text");
            var value = element.Q<Label>("value");

            circle.style.backgroundColor = new StyleColor(item.color);
            logo.image = item.logo;
            text.text = item.text;
            value.text = item.value;
        };
        */
        /*
        listView.bindItem = (element, index) =>
        {
            var item = list_data[index];
            var circle = element.Q<VisualElement>("circle");
            //var logo = element.Q<Image>("logo");
            var text = element.Q<Label>("text");
            var value = element.Q<Label>("value");

            //circle.style.backgroundColor = new StyleColor(item.color);
            string hexColor = item[2];
            UnityEngine.Color color;
            if (UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out color))
            {
                circle.style.backgroundColor = color;
            }
            else
            {
                Debug.LogError("Неверный формат шестнадцатеричного цвета.");
            }

            //logo.image = item.logo;
            text.text = item[0];
            value.text = item[1];
        };*/
        // Отключаем изменение стиля при наведении или нажатии
        listView.selectionType = SelectionType.None;

        listView.Q<ScrollView>().contentContainer.RegisterCallback<MouseEnterEvent>(evt =>
        {
            if (evt.target is VisualElement element)
            {
                element.style.backgroundColor = new StyleColor(Color.clear);
            }
        });

        listView.Q<ScrollView>().contentContainer.RegisterCallback<MouseLeaveEvent>(evt =>
        {
            if (evt.target is VisualElement element)
            {
                element.style.backgroundColor = new StyleColor(Color.clear);
            }
        });

        
        foreach (var item in listView.itemsSource)
        {
            if (item is VisualElement element)
            {
                // element.style.height = element.resolvedStyle.width;
                element.style.height = 0;
            }
            
        }

    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        
        if (evt.target is VisualElement element)
        {
            element.style.height = element.resolvedStyle.width;
            
        }
    }
    public void UpdateValues()
    {


        listView.bindItem = (element, index) =>
        {
            var item = list_data[index];
            var circle = element.Q<VisualElement>("circle");
            //var logo = element.Q<Image>("logo");
            var text = element.Q<Label>("text");
            var value = element.Q<Label>("value");

            //circle.style.backgroundColor = new StyleColor(item.color);
            string hexColor = item[2];
            UnityEngine.Color color;
            if (UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out color))
            {
                circle.style.backgroundColor = color;
            }
            else
            {
                Debug.LogError("Неверный формат шестнадцатеричного цвета.");
            }

            //logo.image = item.logo;
            text.text = item[0];
            
            value.text = item[1];
        };
    }
    public void UpdateListData()
    {
        UserDB userDB = new UserDB();
        AccountDB accountDB = new AccountDB(userDB);
        CategoryDB categoryDB = new CategoryDB(userDB);
        
        RecordDB recordDB = new RecordDB(categoryDB, accountDB, userDB);
        list_data.Clear();
        List<List<string>> cat_list = categoryDB.GetCategoryData();
        for (int i = 0; cat_list.Count > i; i++)
        {
            string percent;
            int accId = 0;
            float summ = recordDB.GetSummAccount(accId, 0, UserInfo.UserID);
            float catSum = recordDB.GetSummCategory(i, accId, 0, UserInfo.UserID);
            if (summ > 0 && catSum > 0)
            {
                percent = ((catSum / summ) * 100).ToString("F1") + "%";
            }
            else
            {
                continue;
            }
            string name_cat = cat_list[i][1];
            string color = cat_list[i][2];
            List<string> list_temp = new List<string>()
            {
                name_cat,
                percent,
                color
            };
            list_data.Add(list_temp);
        }

    }

}
