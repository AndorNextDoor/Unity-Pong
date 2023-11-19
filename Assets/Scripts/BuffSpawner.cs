using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpawner : MonoBehaviour
{
    public GameObject [] prefabToInstantiate;
    public Vector2 boxSize = new Vector2(10f, 8f); // Adjust the size of your box as needed
    public LayerMask collisionLayer;
    private int maxAttempts = 50; // Adjust as needed
    public float instantiationInterval = 5f; // Time interval between instantiations
    private int buffsCount;



    void Start()
    {
        StartCoroutine(InstantiateTrianglesWithDelay());
    }

    IEnumerator InstantiateTrianglesWithDelay()
    {
        while (true)
        {
            InstantiatePrefabsRandomly();
            maxAttempts = 50;
            buffsCount = Random.Range(1,3);
            yield return new WaitForSeconds(instantiationInterval);
        }
    }
    void InstantiatePrefabsRandomly()
    {
        for (int i = 0; i < buffsCount; i++)
        {
            Vector2 randomPosition = GetRandomPositionInsideBox();

            int attempts = 0;

            while (CheckCollision(randomPosition))
            {
                randomPosition = GetRandomPositionInsideBox();

                attempts++;

                if (attempts >= maxAttempts)
                {
                    Debug.LogWarning("Max attempts reached. Unable to find a suitable position for the triangle.");
                    return;
                }
            }

            Instantiate(prefabToInstantiate[Random.Range(0,2)], randomPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomPositionInsideBox()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-5f, 5f) + 1f;

        return new Vector2(randomX, randomY);
    }

    bool CheckCollision(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(position, new Vector2(0.5f, 0.5f), 0f, collisionLayer);

        return colliders.Length > 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(boxSize.x, boxSize.y, 1f));
    }
}
