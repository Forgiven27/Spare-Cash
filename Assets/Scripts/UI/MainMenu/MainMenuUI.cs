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
using System.Drawing;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject circlePrefab;
    [SerializeField] GameObject parentGraph;
    
    List<GameObject> circles = new List<GameObject>();
    GameObject themeCircle;

    public Vector2 referenceResolution = new Vector2(265, 530);

    UserDB userDB;
    AccountDB accountDB;
    CategoryDB categoryDB;
    RecordDB recordDB;


    void Start()
    {


        var root = GetComponent<UIDocument>().rootVisualElement;

        UnityEngine.UIElements.Button buttonAddRec = root.Q<UnityEngine.UIElements.Button>("AddRec");
        //UnityEngine.UIElements.Label label1 = root.Q<UnityEngine.UIElements.Label>("Label1");
        //UnityEngine.UIElements.Label label2 = root.Q<UnityEngine.UIElements.Label>("Label2");
        //UnityEngine.UIElements.Button buttonTest = root.Q<UnityEngine.UIElements.Button>("Test");
        UnityEngine.UIElements.Button buttonAnalis = root.Q<UnityEngine.UIElements.Button>("Graph");
        UnityEngine.UIElements.Button buttonAboutDev = root.Q<UnityEngine.UIElements.Button>("AboutDev");
        /*
        label1.text = Screen.width.ToString();
        label2.text = Screen.height.ToString();
        */



        userDB = new UserDB();
        accountDB = new AccountDB(userDB);
        categoryDB = new CategoryDB(userDB);
        recordDB = new RecordDB(categoryDB, accountDB, userDB);

        
        
        List<List<string>> dataCat = categoryDB.GetUserCategoryData(UserInfo.UserID);
        List<List<string>> dataAcc = accountDB.GetUserAccountData(UserInfo.UserID);
        List<List<string>> dataRec = recordDB.GetUserRecordData(UserInfo.UserID);



        Debug.Log("Количество категорий = " + dataCat.Count);
        Debug.Log("Количество аккаунтов = " + dataAcc.Count);
        Debug.Log("Количество записей = " + dataRec.Count);
        if (dataCat.Count > 0 && dataCat != null)
        {
            if (themeCircle != null) { Destroy(themeCircle); }
            for (int i = 0; i < dataCat.Count; i++)
            {
                circles.Add(CreateCircle());
            }
            UpdateCircles(categoryDB, accountDB, recordDB, 0, 1);
            RectTransform rectTransform = circles[0].GetComponent<RectTransform>();
            //label1.text = rectTransform.transform.localScale.x.ToString();
            //label2.text = rectTransform.transform.localScale.y.ToString();

            buttonAddRec.clicked += OpenMenuAddRec;
            buttonAboutDev.clicked += OpenAboutDevScene;
        }
        else
        {
            themeCircle = Instantiate(circlePrefab, parentGraph.transform.position, new Quaternion(), parentGraph.transform) as GameObject;
            UnityEngine.UI.Image background = themeCircle.GetComponent<UnityEngine.UI.Image>();
            background.color = UnityEngine.Color.gray;
            background.fillAmount = 1;
        }

        buttonAnalis.clicked += GraphWindow;
        //buttonTest.clicked += Test;
    }

    void GraphWindow()
    {
        SceneManager.LoadScene("ChooseGraph");
    }
    void OpenAboutDevScene()
    {
        SceneManager.LoadScene("AboutDevScene");
    }

    void UpdateCircles(CategoryDB categoryDB, AccountDB accountDB, RecordDB recordDB, int isIncome, int accId)
    {
        int userId = UserInfo.UserID;
        
        float summ = recordDB.GetSummAccount(accId, isIncome, userId);
        Debug.Log("Сумма по всем категориям " + summ);
        float startingAngle = 0;
        for (int i = 0;i < circles.Count;i++) 
        {
            if (gameObject != null)
            {
                UnityEngine.UI.Image background = circles[i].GetComponent<UnityEngine.UI.Image>();
                // Площадь кусочка
                float catSum = recordDB.GetSummCategory(i, accId, isIncome, userId);
                Debug.Log("Сумма по категории "+i+" = " + catSum);
                if (summ > 0 && catSum > 0)
                {
                    background.fillAmount = catSum / summ;
                }else
                {
                    background.fillAmount = 0;
                    continue;
                }

                // Позиция кусочка относительно Z
                //
                //circles[i].transform.rotation = new Quaternion(0, 0, startingAngle, 1);
                circles[i].transform.Rotate(0, 0, startingAngle);
                float angle;
                angle = background.fillAmount * 360;
                startingAngle += angle;
                // Цвет кусочка
                string hexColor = categoryDB.GetColorById(i);
                Debug.Log("Цвет категории " + hexColor);
                UnityEngine.Color color;
                if (UnityEngine.ColorUtility.TryParseHtmlString(hexColor, out color))
                {
                    background.color = color;
                }
                else
                {
                    Debug.LogError("Неверный формат шестнадцатеричного цвета.");
                }
                circles[i].SetActive(true);
                
                /*
                RectTransform rectTransform = circles[i].GetComponent<RectTransform>();

                Vector2 screenSize = new Vector2(Screen.width, Screen.height);
                Debug.Log("Ширина экрана "+Screen.width);
                Debug.Log("Высота экрана "+Screen.height);



                // Вычисляем коэффициент масштабирования
                float scaleFactor;
                //float scaleFactor = Mathf.Min(screenSize.x / referenceResolution.x, screenSize.y / referenceResolution.y);
                scaleFactor = Screen.width / screenSize.x;
                // Применяем коэффициент масштабирования к UI элементу
                rectTransform.localScale = new Vector3(1f, 1f, 1f);

                //RectTransform rectTransformParent = parentGraph.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0f, Screen.height * 0.2f);
                */
            }
            
        }

    }

    GameObject CreateCircle()
    {

        GameObject circ = Instantiate(circlePrefab, parentGraph.transform.position + new Vector3 (0, Screen.height * 0.0005f,0), new Quaternion(), parentGraph.transform) as GameObject;
        UnityEngine.UI.Image background = circ.GetComponent<UnityEngine.UI.Image>();
        background.fillAmount = 0;
        return circ;
    }

    private void OpenMenuAddRec()
    {
        SceneManager.LoadScene("AddRecScene");
    }

    public void SetActiveUI(bool flag)
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        UnityEngine.UIElements.ListView list = root.Q<UnityEngine.UIElements.ListView>("list-view");
        UnityEngine.UIElements.Button button = root.Q<UnityEngine.UIElements.Button>("AddRec");
        /*if (list.visible == false && button.visible == false)
        {
            list.visible = true;
            button.visible = true;
        }
        else
        {
            list.visible = false;
            button.visible = false;
        }*/
        if (flag)
        {
            root.visible = true;
        }
        else
        {
            root.visible = false;
        }

    }
}
