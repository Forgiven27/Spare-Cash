using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIAls : MonoBehaviour
{
    
    private ListView transactionListView;
    private VisualElement headerVE;
    private VisualElement pieGraphVE;
    public StyleSheet listUSS;
    

    // ������ ������ ��� ������ ����������
    private List<TransactionData> transactions = new List<TransactionData>
    {
        new TransactionData { Name = "��������", Color = Color.green },
        new TransactionData { Name = "��������", Color = Color.blue },
        new TransactionData { Name = "��������", Color = Color.green },
        new TransactionData { Name = "��������", Color = Color.blue },
        new TransactionData { Name = "��������", Color = Color.green },
        new TransactionData { Name = "��������", Color = Color.blue },
        new TransactionData { Name = "��������", Color = Color.green },
        new TransactionData { Name = "��������", Color = Color.blue },
        new TransactionData { Name = "��������", Color = Color.green },
        new TransactionData { Name = "��������", Color = Color.blue },
        new TransactionData { Name = "��������", Color = Color.green },
        new TransactionData { Name = "��������", Color = Color.blue },
        new TransactionData { Name = "��������", Color = Color.green },
        new TransactionData { Name = "��������", Color = Color.blue },
        new TransactionData { Name = "��������", Color = Color.green },
        new TransactionData { Name = "��������", Color = Color.blue },
        new TransactionData { Name = "���������", Color = Color.red }
    };

    void Start()
    {
        // �������� �������� ������� UI
        var root = GetComponent<UIDocument>().rootVisualElement;

        // ������� ListView �� �����
        transactionListView = root.Q<ListView>("ListView");
        headerVE = root.Q<VisualElement>("Head");
        pieGraphVE = root.Q<VisualElement>("PieGraph");

        // ������������� �������� ������
        transactionListView.itemsSource = transactions;

        // ��������� ������� �������� ������
        transactionListView.makeItem = () =>
        {
            // ������� ���������� ������� ��� ������� �������� ������
            var item = new VisualElement();
            var colorCircle = new VisualElement();
            var label = new Label();

            colorCircle.style.width = 20;
            colorCircle.style.height = 20;
            //colorCircle.style.borderTopLeftRadius = new StyleLength(50);
            //colorCircle.style.borderTopRightRadius = new StyleLength(50); // ������ ������� ������
            colorCircle.style.marginRight = 10; // ������ ����� ������� � �������
            item.Add(colorCircle);
            item.Add(label);

            return item;
        };

        // �������� ������ � ������� ��������
        transactionListView.bindItem = (element, index) =>
        {
            var transactionData = transactions[index];

            var colorCircle = element.ElementAt(0); // ������ ������� � ��� ������
            colorCircle.style.backgroundColor = transactionData.Color;

            var label = (Label)element.ElementAt(1); // ������ ������� � ��� �����
            label.text = transactionData.Name;
            label.style.backgroundColor = transactionData.Color;
            label.style.justifyContent = Justify.Center;
        };

        // ������������� ������ �������� ������
        transactionListView.fixedItemHeight = 30;
        headerVE.style.flexGrow = 1;
        pieGraphVE.style.flexGrow = 1;
        transactionListView.style.flexGrow = 1;

        headerVE.style.height = new Length(10, LengthUnit.Percent);
        pieGraphVE.style.height = new Length(40, LengthUnit.Percent);

        transactionListView.style.height = new Length(50, LengthUnit.Percent);
        transactionListView.style.width = new Length(100, LengthUnit.Percent);

        //var scrollView = transactionListView.sc

        transactionListView.styleSheets.Add(listUSS);
        //("--unity-scrollbar-width", new StyleLength(0));
    }
}

public class TransactionData
{
    public string Name { get; set; }
    public Color Color { get; set; }
}
