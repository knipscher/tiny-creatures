using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private int numberToSpawn;
    [SerializeField] private float radius;
    [SerializeField] private bool fillSphere;
        
    private void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            var position = transform.position;
            if (fillSphere)
            {
                position += new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius),
                    Random.Range(-radius, radius));
                position += Random.onUnitSphere * Random.Range(0, radius);
            }
            else
            {
                position += Random.onUnitSphere * radius;
            }

            if (!fillSphere || Vector3.Distance(transform.position, position) < radius)
            {
                Instantiate(prefabToSpawn, position, Random.rotation, transform);
            }
        }
    }
}
