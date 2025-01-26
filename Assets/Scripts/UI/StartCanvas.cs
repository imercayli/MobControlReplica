using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private TextMeshProUGUI levelNoText;

    // Start is called before the first frame update
    void Start()
    {
        SetUI();
    }

    private void SetUI()
    {
        levelNoText.text = string.Format("Level {0}", (SaveData.GameLevel+1).ToString());
        startButton.onClick.AddListener(OnStartButtonTap);
    }
    private void OnStartButtonTap()
    {
        GameManager.Instance.StartGame();
    }
}
