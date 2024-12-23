using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

public class SwitchSprite : MonoBehaviour
{
    // Start is called before the first frame update
    VisualElement root;
    VisualElement noexp;
    VisualElement exp;
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        exp = root.Q<VisualElement>("exp");
        noexp = root.Q<VisualElement>("noexp");
        Button button = root.Q<Button>("Button");
        exp.style.display = UnityEngine.UIElements.DisplayStyle.None;

        button.text = "—глаживание";

        button.clicked += ButtonClicked;
    }
    void ButtonClicked()
    {
        if (exp.style.display == UnityEngine.UIElements.DisplayStyle.None)
        {
            exp.style.display = UnityEngine.UIElements.DisplayStyle.Flex;
            noexp.style.display = UnityEngine.UIElements.DisplayStyle.None;
        }else
        {
            noexp.style.display = UnityEngine.UIElements.DisplayStyle.Flex;
            exp.style.display = UnityEngine.UIElements.DisplayStyle.None;
        }   
    }
}
