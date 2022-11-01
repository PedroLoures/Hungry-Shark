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

    [Header("Lives")]
    public Transform livesParent;
    public GameObject livesPrefab;
    public int livesCount;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public int targetValue = 100;

    [Header("Fish")]
    public Transform fishList;
    public int baseFishCount = 10;
    public int maxFishCount = 20;
    public float fishEverySeconds = 30f;
    public GameObject fishPrefab;
    public Transform fishTank;


    [Header("Hunt")]
    public Color targetColor;

    private List<GameObject> lives = new List<GameObject>();

    private int currentScore;
    private List<GameObject> availableFishes = new List<GameObject>();
    private GameObject targetFish;

    private float timerFishCreation = 0;
    private void Update()
    {
        if (availableFishes.Count < maxFishCount)
        {
            if (timerFishCreation < fishEverySeconds)
            {
                timerFishCreation += Time.deltaTime;
            }
            else
            {
                timerFishCreation = 0;
                CreateFish();
            }
        }
    }

    private void Awake()
    {
        //Set Lives
        for (int i = 0; i < livesCount; i++)
        {
            lives.Add(Instantiate(livesPrefab, livesParent));
        }

        //Create base fishes
        for (int i = 0; i < baseFishCount; i++)
        {
            CreateFish();
        }

        targetFish = availableFishes[Random.Range(0, availableFishes.Count)].GetComponent<ChangeColorRenderer>().ChangeColor(targetColor);
    }

    public void CreateFish()
    {
        //Halfing before to avoid double calculations
        Vector3 halfScale = new Vector3(fishTank.localScale.x / 2 - fishPrefab.transform.localScale.x * 1.5f,
                                        fishTank.localScale.y / 2 - fishPrefab.transform.localScale.y * 1.5f,
                                        fishTank.localScale.z / 2 - fishPrefab.transform.localScale.z * 1.5f);
        Vector3 position = new Vector3(
            Random.Range(fishTank.position.x - halfScale.x, fishTank.position.x + halfScale.x),
            Random.Range(fishTank.position.y - halfScale.y, fishTank.position.y + halfScale.y),
            Random.Range(fishTank.position.z - halfScale.z, fishTank.position.z + halfScale.z)
            );
        availableFishes.Add(Instantiate(fishPrefab, position, Quaternion.identity, fishList));
    }

    public void KillFish(GameObject fish)
    {
        if (fish == targetFish)
        {
            ChangeTargetFish();
            ChangeScore(targetValue);
        } else
        {
            RemoveLives(1);
        }
        availableFishes.Remove(fish);
        Destroy(fish);
    }

    public void ChangeTargetFish()
    {
        targetFish = availableFishes[Random.Range(0, availableFishes.Count)].GetComponent<ChangeColorRenderer>().ChangeColor(targetColor);
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

        if (remainingLives.Count == 0)
        {
            GameOver();
        }
    }

    bool isGameOver = false;
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("GAME OVER");
        //Show Game Over Screen
    }
}