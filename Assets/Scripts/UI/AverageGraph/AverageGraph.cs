using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DataBank;
using System.Data;
using System;
using UnityEngine.UIElements;

public class AverageGraph : MonoBehaviour
{
    public List<float> data;
    public int forecastDays = 5;


    VisualElement chartContainer;
    VisualElement mainContainer;
    Button button;

    UserDB userDB;
    AccountDB accountDB;
    CategoryDB categoryDB;
    RecordDB recordDB;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        /*
        chartContainer = new VisualElement();
        chartContainer.style.width = Length.;
        chartContainer.style.height = Length.Auto();
        root.Add(chartContainer);
        Debug.Log(chartContainer.layout.height);
        Debug.Log(chartContainer.layout.width);
        */
        VisualElement mainContainer = root.Q<VisualElement>("smth");

        button = new UnityEngine.UIElements.Button();
        mainContainer.Add(button);

        Debug.Log(button.layout.height + " " + button.layout.width);

        userDB = new UserDB();
        accountDB = new AccountDB(userDB);
        categoryDB = new CategoryDB(userDB);
        recordDB = new RecordDB(categoryDB, accountDB, userDB);

        IDataReader reader = recordDB.GetAllData();
        data = new List<float>();
        List<DateTime> time = new List<DateTime>();
        while (reader.Read())
        {
            if (reader.GetInt32(2) == 0 && reader.GetInt32(6) == UserInfo.UserID)
            {
                data.Add(reader.GetInt32(1));
                // time.Add(reader.GetDateTime(3));
            }
        }



        DrawChart();
    }


    void DrawChart()
    {
        if (data == null || data.Count == 0)
        {
            Debug.LogWarning("Нет данных для построения");
            return;
        }
        List<float> forecast = Forecast.GenerateAverageForecast(data, forecastDays);

        List<float> allData = new List<float>(data);
        allData.AddRange(forecast);



        LineRenderer lineRenderer = new LineRenderer();
        // chartContainer.Add(lineRenderer);
        mainContainer.Add(lineRenderer);
        for (int i = 0; i < allData.Count; i++)
        {
            float x = i * (chartContainer.layout.height / allData.Count);
            float y = allData[i] * (chartContainer.layout.height / 200f);
            lineRenderer.AddPoint(new Vector2(x, y));
        }
    }

    public class LineRenderer : VisualElement
    {
        public void AddPoint(Vector2 point)
        {
            VisualElement dot = new VisualElement();
            dot.style.width = 20;
            dot.style.height = 20;
            dot.style.borderTopLeftRadius = 10;
            dot.style.borderTopRightRadius = 10;
            dot.style.borderBottomLeftRadius = 10;
            dot.style.borderBottomRightRadius = 10;
            dot.style.backgroundColor = Color.blue;
            dot.style.position = Position.Absolute;
            dot.style.left = point.x;
            dot.style.right = point.y;


            Add(dot);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonPressed();
        }
    }
    private void OnBackButtonPressed()
    {
        SceneManager.LoadScene("ChooseGraph");
    }



}
