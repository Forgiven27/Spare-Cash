using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddCategoryScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            BackToAddRecScene();
        }
    }
    private void OnBackButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }
    void BackToAddRecScene()
    {
        SceneManager.LoadScene("AddRecScene");
    }
}
