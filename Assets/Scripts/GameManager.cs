using System.Collections;
using TMPro;
using UnityEngine;

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



    public void GameOver()
    {
        gameOver = true;

    }
}
