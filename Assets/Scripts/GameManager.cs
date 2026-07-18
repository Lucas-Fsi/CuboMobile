using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Coin Settings")]
    [SerializeField] private GameObject coin;
    [SerializeField] private float xCoinSpawn = 7;
    [Range(0f, 20f)]
    [SerializeField] private float yCoinSpawn = 12f;
    [Range(0f, 20f)]
    [SerializeField] private float speedCoinFalling = 13f;

    public float timeCoinSpawn = 5f;

    [Header("Obstaculo Settings")]
    [SerializeField] private GameObject obstacle;

    public float timeSpawn = 2f;
    public bool gameOver = false;
    [Range(0f, 20f)]
    [SerializeField] private float xSpawn = 7;
    [Range(0f, 20f)]
    [SerializeField] private float ySpawn = 11f;
    [Range(0f, 20f)]
    [SerializeField] private float speedFalling = 11f;


    [Header ("Pontuação")]
    private float score;
    private float timeScore;
    public TextMeshProUGUI txtScore;
    public PlayerController playerController;

    [Header("PauseMenu")]
    public GameObject pauseMenu;

    [Header("InicioMenu")]
    public GameObject StartMenu;
    bool gameStarted = false;
    private bool InicioRapido = false;

    [Header("GameOverMenu")]
    public GameObject GameOverMenu;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {

        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnCoins());

    }

    private void Update()
    {
        if (gameOver == true) return;

        Score();

        MetodoPause();

    }


    public void MetodoPause()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (Time.timeScale == 0f)
            {
                StartCoroutine(ScaleTime(0f, 1f, 0.5f));
                pauseMenu.SetActive(false);
            }
            else if (Time.timeScale == 1f)
            {
                StartCoroutine(ScaleTime(1f, 0f, 0.5f));
                pauseMenu.SetActive(true);
            }
        }
    }

    private IEnumerator SpawnObstacle()
    {
        while (!gameOver)
        {
            float x = Random.Range(-xSpawn, xSpawn);

            GameObject objObstacle = Instantiate(obstacle, new Vector3(x, ySpawn, 0f), Quaternion.identity);

            float damping = Random.Range(0f, speedFalling);

            Rigidbody rb = objObstacle.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearDamping = damping;
            }

            yield return new WaitForSeconds(timeSpawn);
        }
    }

    private IEnumerator SpawnCoins()
    {
        while (!gameOver)
        {
            float x = Random.Range(-xCoinSpawn, xCoinSpawn);

            GameObject objcoin = Instantiate(coin, new Vector3(x, yCoinSpawn, 0f), Quaternion.Euler(90f, 0f, 0f));

            float damping = Random.Range(0f, speedCoinFalling);

            Rigidbody rb = objcoin.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearDamping = damping;
            }

            yield return new WaitForSeconds(timeCoinSpawn);
        }
    }

    public void Score()
    {
        timeScore += Time.deltaTime;

        if (timeScore >= 1)
        {
            score++;
            txtScore.text = $"Score : {score + playerController.money}";
            timeScore = 0;
        }
    }

    public void Enabled()
    {
        gameObject.SetActive(true);
        StartMenu.SetActive(false);
    }



    public void GameOver()
    {
        GameOverMenu.SetActive(true);
        gameOver = true;
       

    }

    public IEnumerator ScaleTime(float start, float end, float duration )
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < duration)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer/ duration);

            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            timer += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;

            yield return null;

        }
        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
