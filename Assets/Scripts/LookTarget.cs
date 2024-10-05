using UnityEngine;

public class LookTarget : MonoBehaviour
{
    [SerializeField] private float lookSpeed = 10f;
    
    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = Vector3.Lerp(transform.position, hit.point, Time.deltaTime * lookSpeed);
        }
    }
}
