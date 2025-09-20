using UnityEngine;
using Assets.Scripts.EventSystem;

public class StarSpawner : MonoBehaviour
{
    public EventData currentEvent;
    public GameObject tilePrefab;
    public Transform gridParent;

    public void SpawnTiles()
    {
        int progress = EventProgressManager.GetProgress(currentEvent);
        float percent = (float)progress / currentEvent.goal;

        int starCount = GetStarCount(percent);

        for (int i = 0; i < 25; i++)
        {
            GameObject tile = Instantiate(tilePrefab, gridParent);
            Tile tileScript = tile.GetComponent<Tile>();
            tileScript.SetLetter(RandomLetter());

            
            tileScript.hasStar = (i < starCount); 
        }
    }

    private int GetStarCount(float percent)
    {
        if (percent < 0.25f) return 3;
        if (percent < 0.75f) return 2;
        return 1;
    }

    private char RandomLetter()
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return letters[Random.Range(0, letters.Length)];
    }
}