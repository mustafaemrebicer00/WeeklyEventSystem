using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelQuitPopupController : MonoBehaviour
{
    public GameObject popupPanel;

    public void ShowPopup()
    {
        popupPanel.SetActive(true);
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }

    public void ConfirmQuit()
    {
        Application.Quit(); 
    }
}