using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float cameraRotationSpeed = 0.1f;
    [SerializeField] private float jumpSpeed = 1;
    
    private Rigidbody _rigidbody;

    [SerializeField] private Transform _cameraTransform;

    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField] private Transform[] bones;
    
    private int _points;
    private int _attachedAntCount;

    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI antText;

    [SerializeField] private Spawner gemSpawner;

    [SerializeField] private Spawner antSpawner;
    [SerializeField] private float antCooldown = 2f;
    private bool _canAntsBite = false;

    private bool _isDead = false;

    private float _timeSinceLastBurrow = Mathf.Infinity;
    [SerializeField] private float burrowTimeThreshold = 1f;

    [SerializeField] private float cameraLerpSpeed = 10f;
    [SerializeField] private Transform cameraMarkerA;
    [SerializeField] private Transform cameraMarkerB;

    [SerializeField] private Vector3 planetCenter;
    [SerializeField] private float maxDistance;
    [SerializeField] private AnimationCurve distanceCurve;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(DelayAntStart());
    }

    private IEnumerator DelayAntStart()
    {
        yield return new WaitForSeconds(antCooldown);
        _canAntsBite = true;
    }

    private void Update()
    {
        if (_timeSinceLastBurrow > burrowTimeThreshold)
        {
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, cameraMarkerA.position,
                cameraLerpSpeed * Time.deltaTime);
            _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, cameraMarkerA.rotation,
                cameraLerpSpeed * Time.deltaTime);
        }
        else
        {
            _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, cameraMarkerB.position,
                cameraLerpSpeed * Time.deltaTime);
            _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, cameraMarkerB.rotation,
                cameraLerpSpeed * Time.deltaTime);
        }

        _timeSinceLastBurrow += Time.deltaTime;
    }

    public void OnBurrow()
    {
        _timeSinceLastBurrow = 0;
    }

    private void FixedUpdate()
    {
        if (!_isDead)
        {
            _horizontalInput = Input.GetAxis("Mouse X");
            _verticalInput = Input.GetAxis("Mouse Y");

            _rigidbody.AddForce((_cameraTransform.forward + _cameraTransform.up / 2) *
                                (Input.GetAxis("Vertical") * moveSpeed));
            _rigidbody.AddForce(_cameraTransform.right * (Input.GetAxis("Horizontal") * moveSpeed));
            _rigidbody.AddForce(Vector3.up * (Input.GetAxis("Jump") * jumpSpeed));

            _rigidbody.MoveRotation(
                _rigidbody.rotation * Quaternion.Euler(0, _horizontalInput * cameraRotationSpeed, 0));
            _rigidbody.MoveRotation(_rigidbody.rotation *
                                    Quaternion.Euler(-_verticalInput * cameraRotationSpeed, 0, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_canAntsBite)
        {
            return;
        }

        var ant = other.GetComponentInParent<Ant>();
        if (ant && ant.Bite(GetNearestBone(ant.transform.position)))
        {
            OnBitten();
        }
    }

    private Transform GetNearestBone(Vector3 position)
    {
        Transform nearestBone = transform;
        float minDistance = Mathf.Infinity;
        foreach (var bone in bones)
        {
            var distance = Vector3.Distance(bone.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestBone = bone;
            }
        }

        return nearestBone;
    }

    private void OnBitten()
    {
        _attachedAntCount++;
        var quarterAnts = Mathf.RoundToInt(antSpawner.GetStartingCount() / 4f);
        antText.text = "ants attached: " + _attachedAntCount + " / " + quarterAnts;
        if (_attachedAntCount >= quarterAnts)
        {
            antText.text = "ants attached: too many / " + quarterAnts;
            Die();       
        }
    }

    private void Die()
    {
        _isDead = true;
    }

    public void Eat(Gem gem)
    {
        _points++;
        var gemCount = gemSpawner.GetStartingCount();
        gemText.text = "gems eaten: " + _points + " / " + gemCount;
        Destroy(gem.gameObject);
        if (_points >= gemCount)
        {
            Win();
        }
    }

    private void Win()
    {
        _canAntsBite = false;
    }
}
