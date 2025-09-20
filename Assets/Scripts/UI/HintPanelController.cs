using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HintPanelController : MonoBehaviour
{
    public List<Text> hintTexts; 
    private List<string> currentHints = new List<string>();
    private HashSet<string> foundWords = new HashSet<string>();

    public void SetHints(List<string> hints)
    {
        currentHints = hints;
        Debug.Log($"Hints updated: {string.Join(", ", hints)}");

        for (int i = 0; i < hintTexts.Count; i++)
        {
            if (i < hints.Count)
            {
                hintTexts[i].text = hints[i];
                hintTexts[i].color = Color.white; 
            }
            else
            {
                hintTexts[i].text = "";
            }
        }
        foundWords.Clear();
    }

    public void MarkFound(string word)
    {
        int idx = currentHints.IndexOf(word);
        if (idx >= 0 && !foundWords.Contains(word))
        {
            hintTexts[idx].color = new Color(0.3f, 0.3f, 0.3f); // Daha koyu gri renk
            foundWords.Add(word);
        }
    }

    public bool AreAllWordsFound()
    {
        return foundWords.Count == currentHints.Count; 
    }

    public bool IsFound(string word)
    {
        return foundWords.Contains(word);
    }

    public List<string> GetCurrentHints()
    {
        return new List<string>(currentHints);
    }
}