using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleDrawerWithCollider : MonoBehaviour
{
    public int segments = 100;
    public float radius = 5f;
    public float gapDegrees = 20f;
    public GameObject colliderPrefab;
    public float rotationSpeed = 30f; // <-- vitesse de rotation en degrés/seconde

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;

        float totalAngle = 360f - gapDegrees;
        lineRenderer.positionCount = segments + 1;

        CreatePoints(totalAngle);
        CreateColliders(totalAngle);
    }

    private void Update()
    {
        // On tourne l'objet autour de son centre
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void CreatePoints(float totalAngle)
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float currentAngle = angle / segments * totalAngle;
            float x = Mathf.Cos(Mathf.Deg2Rad * currentAngle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * currentAngle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));

            angle++;
        }
    }

    private void CreateColliders(float totalAngle)
    {
        Vector3 previousPoint = lineRenderer.GetPosition(0);

        for (int i = 1; i <= segments; i++)
        {
            Vector3 currentPoint = lineRenderer.GetPosition(i);

            GameObject col = Instantiate(colliderPrefab, transform);

            // Position au milieu entre deux points
            col.transform.position = (previousPoint + currentPoint) / 2f;

            // Rotation alignée avec la ligne
            Vector3 dir = currentPoint - previousPoint;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            col.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // Longueur adaptée
            float distance = dir.magnitude;
            col.transform.localScale = new Vector3(distance, col.transform.localScale.y, col.transform.localScale.z);

            previousPoint = currentPoint;
        }
    }
}
