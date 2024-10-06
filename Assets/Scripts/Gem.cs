using System;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private Player _player;
    private MeshRenderer _renderer;
    
    
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _renderer = GetComponent<MeshRenderer>();
        _renderer.enabled = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) < 10f)
        {
            _renderer.enabled = true;
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
