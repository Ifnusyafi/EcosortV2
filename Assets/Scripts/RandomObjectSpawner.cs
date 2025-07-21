using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] myObjects;
    public Transform spawnAreaParent; // Drag parent "Spawner" ke sini di Inspector
    private Collider[] spawnAreas;

    void Start()
    {
        // Ambil semua BoxCollider dari anak-anak Spawner
        spawnAreas = spawnAreaParent.GetComponentsInChildren<Collider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           SpawnRandomObject();
        }
    }

    // Ambil posisi acak dalam bounds collider
    Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.center.y, // Gunakan center agar tetap di atas tanah
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    void SpawnRandomObject()
    {
        Collider randomArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
        Vector3 randomPosition = GetRandomPointInBounds(randomArea.bounds);

        // Raycast dari atas ke bawah untuk cari tanah/jalan
        Vector3 rayOrigin = new Vector3(randomPosition.x, 50f, randomPosition.z);
        Ray ray = new Ray(rayOrigin, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            // Spawn object tepat di atas permukaan yang kena raycast
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 spawnPosition = hit.point;
            Instantiate(myObjects[randomIndex], spawnPosition, Quaternion.identity);
        }
    }

}
