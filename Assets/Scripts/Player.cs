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

    private int _points;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Mouse X");
        _verticalInput = Input.GetAxis("Mouse Y");
        
        _rigidbody.AddForce((_cameraTransform.forward + _cameraTransform.up / 2) * (Input.GetAxis("Vertical") * moveSpeed));
        _rigidbody.AddForce(_cameraTransform.right * (Input.GetAxis("Horizontal") * moveSpeed));
        _rigidbody.AddForce(Vector3.up * (Input.GetAxis("Jump") * jumpSpeed));
        
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, _horizontalInput * cameraRotationSpeed, 0));
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(-_verticalInput * cameraRotationSpeed, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        var ant = other.GetComponentInParent<Ant>();
        if (ant)
        {
            ant.Bite(ant.transform.position);
        }
    }

    public void Eat(Gem gem)
    {
        _points++;
        Debug.LogWarning("Player has " + _points + " points");
        Destroy(gem.gameObject);   
    }
}
