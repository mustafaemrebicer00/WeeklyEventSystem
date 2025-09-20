using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Transform gridParent;
    public WordValidator wordValidator;
    public WordDictionary wordDictionary;
    public bool hasStar = false;
    private List<char> letterPool = new List<char>();
    private List<GameObject> tiles = new List<GameObject>();

    private Text letterText; 
    private Image starIcon; 

    void Start()
    {
        
        if (wordDictionary != null)
        {
            foreach (string word in wordDictionary.GetAllWords())
            {
                foreach (char c in word)
                {
                    letterPool.Add(char.ToUpper(c));
                }
            }
        }
        SpawnTiles();
    }

    private List<char> GetHintLetters()
    {
        var eventManager = Assets.Scripts.EventSystem.EventManager.Instance;
        if (eventManager == null) return new List<char>();
        var hints = eventManager.GetActiveEventWords().Take(5).ToList();
        var letters = new List<char>();
        foreach (var word in hints)
            letters.AddRange(word.ToUpper().ToCharArray());
        return letters;
    }

    private void SpawnTiles()
    {
        
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }
        tiles.Clear();

        int totalTiles = 25;

        
        List<char> hintLetters = GetHintLetters();
        if (hintLetters.Count > totalTiles)
            hintLetters = hintLetters.Take(totalTiles).ToList();

   
        List<char> gridLetters = new List<char>(hintLetters);
        while (gridLetters.Count < totalTiles)
        {
            char randomLetter = letterPool.Count > 0 ? letterPool[Random.Range(0, letterPool.Count)] : (char)Random.Range(65, 91);
            gridLetters.Add(randomLetter);
        }

      
        gridLetters = gridLetters.OrderBy(x => Random.value).ToList();

    
        for (int i = 0; i < totalTiles; i++)
        {
            char letter = gridLetters[i];
            bool hasStar = Random.value < 0.2f;
            GameObject tile = Instantiate(tilePrefab, gridParent);
            tile.GetComponent<Tile>().Setup(letter, hasStar, wordValidator);
            tiles.Add(tile);
        }
    }

    public void ShuffleTiles()
    {
        
        var hintLetters = GetHintLetters();
        var gridLetters = tiles.Select(t => t.GetComponent<Tile>().letterText.text[0]).ToList();
        bool allHintsExist = hintLetters.All(h => gridLetters.Contains(h));

        if (!allHintsExist)
        {
            SpawnTiles();
            return;
        }

     
        SpawnTiles();
    }

    public void SetLetter(char c)
    {
        if (letterText != null)
            letterText.text = c.ToString();

        if (starIcon != null)
            starIcon.enabled = hasStar;
    }

    public void ResetAllTileColors()
    {
        foreach (var tileObj in tiles)
        {
            var tile = tileObj.GetComponent<Tile>();
            if (tile != null)
                tile.ResetColor();
        }
    }
}