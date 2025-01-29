using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Button continueButton;

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonTap);
    }

    public void Initialize(bool isGameSuccess)
    {
        resultText.text = isGameSuccess ? "SUCCESS!" : "FAIL!";
        resultText.color = isGameSuccess ? Color.green : Color.red;
        
    }

    private void OnContinueButtonTap()
    {
        ServiceSystem.GetService<GameService>().LoadScene();
    }
}
