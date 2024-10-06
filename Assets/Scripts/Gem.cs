using UnityEngine;

public class Gem : MonoBehaviour
{
    private Player _player;
    [SerializeField] private MeshRenderer meshRenderer;
    private Collider _collider;

    [SerializeField] private ParticleSystem particles;
    
    private bool _hasBeenCollected = false;
    
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _collider = GetComponent<Collider>();
        meshRenderer.enabled = false;
    }

    private void Update()
    {
        if (!_hasBeenCollected && Vector3.Distance(transform.position, _player.transform.position) < 15f)
        {
            meshRenderer.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.Eat(this);
            _collider.enabled = false;
            meshRenderer.enabled = false;
            _hasBeenCollected = true;
            particles.Play();
        }
    }
}
