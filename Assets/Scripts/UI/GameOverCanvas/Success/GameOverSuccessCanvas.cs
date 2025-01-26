using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FormulaExtentions;

public class GameOverSuccessCanvas : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rewardAmounText;
    [SerializeField]
    private Transform coinIcon;
    private int defaultRewardAmount = 50;
    private float rewardMultiplier = 1.25f;
    private int rewardAmount;
    [SerializeField]
    private Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        rewardAmount = Mathf.RoundToInt(Formulas.GetValueByLevelMultiplier(defaultRewardAmount, rewardMultiplier, SaveData.GameLevel));
        rewardAmounText.text = string.Format("+{0}", rewardAmount);
        continueButton.onClick.AddListener(OnContinueButtonTap);
    }

    private void OnContinueButtonTap()
    {
       // UIManager.Instance.CurrencyCanvas.CurrencyFlowAnim(CurrencyType.Coin, coinIcon.position, rewardAmount, FlowFinish);
        continueButton.interactable = false;
    }

    private void FlowFinish()
    {
        GameManager.Instance.LoadScene();
    }

}
