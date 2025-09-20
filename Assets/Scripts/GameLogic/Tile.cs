using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Text letterText;
    public Image starIcon;
    private Button button;

    private char letter;
    public bool hasStar { get; set; } 
    private WordValidator wordValidator;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Setup(char _letter, bool _hasStar, WordValidator validator)
    {
        letter = _letter;
        hasStar = _hasStar;
        wordValidator = validator;

        letterText.text = letter.ToString();
        starIcon.enabled = hasStar;
        button.image.color = Color.white;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnTileClicked);
    }

    private void OnTileClicked()
    {
        
        if (button != null && button.targetGraphic != null)
            button.targetGraphic.color = new Color(0.3f, 0.3f, 0.3f);

        if (wordValidator != null)
            wordValidator.AddLetter(letter, hasStar, this); 
    }

    public void ResetColor()
    {
        if (button != null && button.targetGraphic != null)
            button.targetGraphic.color = Color.white;
    }

    public void SetLetter(char c)
    {
        letter = c;
        if (letterText != null)
            letterText.text = c.ToString();

        if (starIcon != null)
            starIcon.enabled = hasStar;
    }
}