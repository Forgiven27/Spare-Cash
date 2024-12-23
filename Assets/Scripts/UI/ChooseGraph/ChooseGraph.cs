using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ChooseGraph : MonoBehaviour
{
    VisualElement root;
    Button buttonGraph1;
    Button buttonGraph2;
    Button buttonGraph3;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        buttonGraph1 = root.Q<Button>("Graph1");
        buttonGraph2 = root.Q<Button>("Graph2");
        buttonGraph3 = root.Q<Button>("Graph3");

        buttonGraph1.clicked += LoadAverageScene;
    }

    void LoadAverageScene()
    {
        SceneManager.LoadScene("AverageGraphScene");
    }
}
