using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum EditorWindowType
{
    AddCurrency,
    SetLevel
}

public class MyEditorWindow : EditorWindow
{
    private static EditorWindowType editorWindowType;
    private CurrencyType currencyType;
    private static int increaseCurrencyValue = 100;
    private static int levelValue = 1;

    [MenuItem("My Editor Window/Add Currency", false, 1)]
    private static void AddCurrency()
    {
        editorWindowType = EditorWindowType.AddCurrency;
        EditorWindow window = GetWindow(typeof(MyEditorWindow));
        window.Show();
    }

    [MenuItem("My Editor Window/Set Level", false, 1)]
    private static void SetLevel()
    {
       editorWindowType = EditorWindowType.SetLevel;
       EditorWindow window = GetWindow(typeof(MyEditorWindow));
       window.Show();
    }

    void OnGUI()
    {
        switch (editorWindowType)
        {
            case EditorWindowType.AddCurrency:
                AddCurrencyWindow();
                break;
            case EditorWindowType.SetLevel:
                SetLevelWindow();
                break;
            default:
                break;
        }

    }

    private void AddCurrencyWindow()
    {
        currencyType = (CurrencyType)EditorGUILayout.EnumPopup("Currency:", currencyType);
        increaseCurrencyValue = EditorGUILayout.IntField("Enter the value", increaseCurrencyValue);
        if (GUILayout.Button("Add!"))
        {
            var currencyDatas = new List<CurrencyData>(Resources.LoadAll<CurrencyData>("Currencies"));
            var currency = currencyDatas.Find(o => o.CurrencyType == currencyType);
            currency.SetAmount(increaseCurrencyValue);
            Close();
        }

    }

    private void SetLevelWindow()
    {
        levelValue = EditorGUILayout.IntField("Set Level", levelValue);
        if (GUILayout.Button("Set!"))
        {
            var levelNo = levelValue - 1;
            if (levelNo < 0)
                levelNo = 0;
            SaveData.GameLevel = levelNo;
            Close();
        }
    }

}
