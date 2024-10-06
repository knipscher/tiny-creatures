using UnityEngine;

public class Gem : MonoBehaviour
{
    private Player _player;
    [SerializeField] private MeshRenderer meshRenderer;
    
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        meshRenderer.enabled = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 10f)
        {
            meshRenderer.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Eat(this);
        }
    }
}
