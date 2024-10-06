using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private Vector3 planetCenter;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private AudioSource audioSourceA;
    [SerializeField] private AudioSource audioSourceB;

    [SerializeField] private float maxDistance = 32f;
    
    private void Update()
    {
        var distance = Vector3.Distance(playerTransform.position, planetCenter);

        audioSourceA.volume = Mathf.Lerp(0, 1, distance / maxDistance);
        audioSourceB.volume = Mathf.Lerp(0, 1, 1 - distance / maxDistance);
    }
}
