using UnityEngine;
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner instance;

    [Header("Setup")]
    public GameObject ballPrefab;
    public Transform center;
    public Text ballCountText;

    [Header("Balle settings")]
    public float allowedRadius = 5f;
    public float destroyDelay = 2f;

    public float tailleMin = 0.1f;
    public float tailleMax = 0.5f;

    private int ballCount = 1; // 1 balle déjà présente au début

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateBallCount(int change)
    {
        ballCount += change;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (ballCountText != null)
        {
            ballCountText.text = "Balles : " + ballCount;
        }
    }

    public void SpawnTwoBalls()
    {
        for (int i = 0; i < 2; i++)
        {
            Vector2 localOffset = Random.insideUnitCircle * (allowedRadius - 0.3f);
            Vector3 spawnPos = center.TransformPoint(new Vector3(localOffset.x, localOffset.y, 0f));

            GameObject newBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

            // Appliquer une couleur aléatoire
            Renderer rend = newBall.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = Random.ColorHSV();
            }

            // Appliquer une taille aléatoire
            float randomScale = Random.Range(tailleMin, tailleMax);
            newBall.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            BallBehavior behavior = newBall.GetComponent<BallBehavior>();
            behavior.allowedRadius = allowedRadius;
            behavior.destroyDelay = destroyDelay;

            UpdateBallCount(1); // Compter la nouvelle balle
        }
    }

    public void SpawnFirstBall()
    {
        GameObject ball = Instantiate(ballPrefab, center.position, Quaternion.identity);

        // Appliquer une couleur aléatoire
        Renderer rend = ball.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Random.ColorHSV();
        }

        // Appliquer une taille aléatoire
        float randomScale = Random.Range(tailleMin, tailleMax);
        ball.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        BallBehavior behavior = ball.GetComponent<BallBehavior>();
        behavior.allowedRadius = allowedRadius;
        behavior.destroyDelay = destroyDelay;

        UpdateBallCount(1); // Compter la première balle aussi
    }
}
