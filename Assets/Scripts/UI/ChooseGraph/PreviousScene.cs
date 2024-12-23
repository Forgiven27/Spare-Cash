using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousScene : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonPressed();
        }
    }
    private void OnBackButtonPressed()
    {
        SceneManager.LoadScene("MainScene");
    }

}
