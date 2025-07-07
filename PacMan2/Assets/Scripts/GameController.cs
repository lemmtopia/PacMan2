using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int score;
    private int lives;
    [SerializeField] private int startingLives = 3;

    /*
     * Awake: Make this the Instance Singleton
     */
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        } 

        Instance = this;
    }

    private void Start()
    {
        NewGame();
    }

    /*
     * NewGame: Resets score and lives
     */
    void NewGame()
    {
        score = 0;
        lives = startingLives;
    }

    public int GetLives()
    {
        return lives;
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void AddLives(int amount)
    {
        lives += amount;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}
