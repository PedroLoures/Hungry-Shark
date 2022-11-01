using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;

    public static GameManager instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<GameManager>();
            }

            return _Instance;
        }
    }

    public Transform livesParent;
    public GameObject livesPrefab;
    public int livesCount;

    public TextMeshProUGUI scoreText;

    public int baseFishCount = 10;
    public int maxFishCount = 20;
    public float fishEverySeconds = 30f;
    public GameObject fishPrefab;
    public Color targetColor;

    private List<GameObject> lives = new List<GameObject>();

    private int currentScore;
    private List<GameObject> availableFishes;
    private GameObject targetFish;

    private void Awake()
    {
        //Set Lives
        for (int i = 0; i < livesCount; i++)
        {
            lives.Add(Instantiate(livesPrefab, livesParent));
        }
    }

    public void ChangeScore(int value)
    {
        currentScore += value;
        if (currentScore > 0)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    public void AddLives(int livesToAdd)
    {
        var missingLives = lives.Where(x => !x.activeInHierarchy).ToList();
        while (missingLives.Count > 0)
        {
            if (livesToAdd > 0)
            {
                missingLives[0].SetActive(true);
                missingLives.RemoveAt(0);
                livesToAdd--;
            } else
            {
                break;
            }
        }
    }

    public void RemoveLives(int livesToRemove)
    {
        var remainingLives = lives.Where(x => x.activeInHierarchy).ToList();
        while (remainingLives.Count > 0)
        {
            if (livesToRemove > 0)
            {
                remainingLives[0].SetActive(false);
                remainingLives.RemoveAt(0);
                livesToRemove--;
            }
            else
            {
                break;
            }
        }
    }

    public void CreateFish()
    {

    }
}