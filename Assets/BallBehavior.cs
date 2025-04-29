using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float allowedRadius = 5f;
    public float destroyDelay = 2f;

    private bool isOutOfCircle = false;
    private float timer = 0f;

    private void Update()
    {
        if (BallSpawner.instance == null) return;

        float distance = Vector3.Distance(transform.position, BallSpawner.instance.center.position);

        if (distance > allowedRadius)
        {
            if (!isOutOfCircle)
            {
                isOutOfCircle = true;
                timer = destroyDelay;
            }
        }
        else
        {
            isOutOfCircle = false;
        }

        if (isOutOfCircle)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                BallSpawner.instance.SpawnTwoBalls();
                BallSpawner.instance.UpdateBallCount(-1);
                Destroy(gameObject);
            }
        }
    }
}
