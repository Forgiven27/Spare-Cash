using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartApp : MonoBehaviour
{
    /// <summary>
    /// ��� ��� ��������������� ��������� �������� �������� �� �������� ������������ � ���������
    /// </summary>
    
    [SerializeField]
    Dictionary<Transform, int> ratioCanvas = new Dictionary<Transform, int>();

    /*
    public void OnEnable()
    {
        foreach(Transform canvas in ratioCanvas.Keys)
        {
            canvas.transform.position = new Vector3(0);
        }
        

    }*/
}
