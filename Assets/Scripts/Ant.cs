using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ant : MonoBehaviour
{
    [SerializeField] private Transform chainIkTarget;
    private Transform _playerTransform;
    
    private bool _isBiting = false;

    private Rigidbody _rigidbody;

    [SerializeField] private float speed;
    [SerializeField] private float gravitySpeed;
    [SerializeField] private Vector3 planetCenter;
    [SerializeField] private float planetRadius = 60f;

    [SerializeField] private float targetingDistance = 10f;

    [SerializeField] private Vector3 targetPosition;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerTransform = FindObjectOfType<Player>().transform;
        StartCoroutine(ChangeTargetPoint());
    }

    private IEnumerator ChangeTargetPoint()
    {
        targetPosition = planetCenter + Random.onUnitSphere * planetRadius;
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        StartCoroutine(ChangeTargetPoint());
    }

    private void Update()
    {
        if (!_isBiting)
        {
            chainIkTarget.position = _playerTransform.position;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) < targetingDistance)
        {
            var force = (_playerTransform.position - transform.position).normalized * speed;
            _rigidbody.AddForce(force);
        }
        else
        {
            var force = (targetPosition - transform.position).normalized * speed;
            _rigidbody.AddForce(force);
        }

        var gravity = (planetCenter - transform.position).normalized * gravitySpeed;
        _rigidbody.AddForce(gravity);
    }
}
