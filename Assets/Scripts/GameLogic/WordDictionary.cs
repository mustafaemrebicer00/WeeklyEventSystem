using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.EventSystem; 

public class WordDictionary : MonoBehaviour
{
    public TextAsset wordFile;
    private HashSet<string> validWords;

    public void Start()
    {
        
        var eventManager = EventManager.Instance;
        if (eventManager != null)
        {
            var hints = eventManager.GetActiveEventWords().Take(5).ToList();
            FindObjectOfType<HintPanelController>().SetHints(hints);
        }
        else
        {
            
            FindObjectOfType<HintPanelController>().SetHints(this.GetRandomHints(5));
        }
    }

    void Awake()
    {
        validWords = new HashSet<string>();

        string[] lines = wordFile.text.Split('\n');
        foreach (string line in lines)
        {
            string cleanWord = line.Replace("\r", "").Trim().ToLower(); 
            if (!string.IsNullOrEmpty(cleanWord))
                validWords.Add(cleanWord);
        }
    }

    public bool IsValid(string word)
    {
        return validWords.Contains(word.ToLower());
    }

    public List<string> GetAllWords()
    {
        return new List<string>(validWords);
    }

    public List<string> GetRandomHints(int count)
    {
        return validWords.OrderBy(x => Random.value).Take(count).ToList();
    }
}