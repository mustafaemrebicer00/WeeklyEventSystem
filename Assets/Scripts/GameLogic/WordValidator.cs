using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.EventSystem;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class WordValidator : MonoBehaviour
{
    public Text wordDisplayText;
    public Slider eventProgressBar;
    public WordDictionary wordDictionary;
    public EventData currentEvent;
    public HintPanelController hintPanelController;
    public Text resultText;
    public EventUIController eventUIController;
    public LevelEndPopupController levelEndPopupController; 

    private string currentWord = "";
    private int starsCollected = 0;
    private HashSet<string> foundWords = new HashSet<string>();
    private List<Tile> selectedTiles = new List<Tile>();

    public void AddLetter(char letter, bool hasStar, Tile tile)
    {
        currentWord += letter;
        wordDisplayText.text = currentWord;

        if (hasStar)
            starsCollected++;

        
        selectedTiles.Add(tile);
    }

   
    public void SubmitWord()
    {
        string wordToCheck = currentWord.ToLower();

        if (IsValidWord(wordToCheck) && !foundWords.Contains(wordToCheck))
        {
           
            EventProgressManager.AddProgress(currentEvent, starsCollected);

            
            int updatedProgress = EventProgressManager.GetProgress(currentEvent);
            eventUIController.SetupEventUI(currentEvent, updatedProgress);
            eventProgressBar.value = updatedProgress;

            
            foundWords.Add(wordToCheck);
            if (hintPanelController != null)
                hintPanelController.MarkFound(wordToCheck);


            StartCoroutine(ShowResult("Successful!", Color.green));

            if (updatedProgress >= eventProgressBar.maxValue)
            {
                levelEndPopupController.ShowPopup(currentEvent);
            }

           
            if (hintPanelController != null && hintPanelController.AreAllWordsFound())
            {
                levelEndPopupController.ShowPopup(currentEvent);
            }
           

           
            FindObjectOfType<TileManager>().ShuffleTiles();
        }
        else if (foundWords.Contains(wordToCheck))
        {
            StartCoroutine(ShowResult("This word has already been found!", Color.yellow));
        }
        else
        {
            StartCoroutine(ShowResult("Invalid word!", Color.red));
        }

        
        ResetWord();
    }

    private IEnumerator ShowResult(string msg, Color color)
    {
        resultText.text = msg;
        resultText.color = color;
        resultText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        resultText.gameObject.SetActive(false);
    }

    private void ResetWord()
    {
        currentWord = "";
        starsCollected = 0;
        wordDisplayText.text = "";

        
        ResetTileColors();
        selectedTiles.Clear();
    }

    private void ResetTileColors()
    {
        foreach (Tile t in selectedTiles)
            t.ResetColor(); 
    }

    private bool IsValidWord(string word)
    {
        
        if (wordDictionary != null && wordDictionary.IsValid(word))
            return true;

   
        if (hintPanelController != null)
        {
            var hints = hintPanelController.GetCurrentHints();
            if (hints.Contains(word))
                return true;
        }

        return false;
    }
}