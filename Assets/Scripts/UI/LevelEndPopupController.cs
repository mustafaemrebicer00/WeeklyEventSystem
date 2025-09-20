using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.EventSystem;
using System.Linq;

public class LevelEndPopupController : MonoBehaviour
{
    public GameObject popupPanel;
    public Image rewardImage;
    public Text messageText;
    public Text rewardText; 
    public Button continueButton; 

    public void ShowPopup(EventData eventData)
    {
        popupPanel.SetActive(true);
        CurrencyManager.AddCurrency(eventData.rewardAmount);

        if (rewardText != null) 
            rewardText.text = $"{eventData.rewardAmount} !";

        rewardImage.sprite = eventData.rewardIcon;
        messageText.text = $"Congratulations! {eventData.eventName} completed.";
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }

    void Start()
    {
        continueButton.onClick.AddListener(OnContinueClicked);
    }

    private void OnContinueClicked()
    {
        popupPanel.SetActive(false);
        var wordValidator = FindObjectOfType<WordValidator>();
        var eventManager = FindObjectOfType<EventManager>();
        var activeEvent = eventManager.GetActiveEvent();

        if (wordValidator != null && activeEvent != null)
        {
            var newHints = activeEvent.eventWords.OrderBy(x => UnityEngine.Random.value).Take(5).ToList();
            wordValidator.hintPanelController.SetHints(newHints);
        }
    }
}