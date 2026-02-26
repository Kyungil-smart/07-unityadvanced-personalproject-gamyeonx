using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _offset = new Vector3(0, 2.45f, -10);

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
