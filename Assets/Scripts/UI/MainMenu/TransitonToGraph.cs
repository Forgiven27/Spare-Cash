using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class TransitonToGraph : MonoBehaviour
{
    Label label;
    void OnEnable()
    {
        LeanTouch.OnFingerSwipe += OnFingerSwipe;
        var root = GetComponent<UIDocument>().rootVisualElement;

        label = root.Q<Label>("LabelTest");

        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnFingerSwipe(new LeanFinger());
        }
    }

    private void OnFingerSwipe(LeanFinger finger)
    {
        Vector2 swipeDirection = finger.SwipeScreenDelta;

        if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
        {
            if (swipeDirection.x > 0)
            {
                Debug.Log("Swipe Right");
                label.text = "Swipe Right";
                // Добавьте здесь вашу логику для свайпа вправо
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("Swipe Left");
                label.text = "Swipe Left";
                // Добавьте здесь вашу логику для свайпа влево
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            if (swipeDirection.y > 0)
            {
                Debug.Log("Swipe Up");
                label.text = "Swipe Up";
                // Добавьте здесь вашу логику для свайпа вверх
            }
            else
            {
                Debug.Log("Swipe Down");
                label.text = "Swipe Down";
                // Добавьте здесь вашу логику для свайпа вниз
            }
        }
    }
}
