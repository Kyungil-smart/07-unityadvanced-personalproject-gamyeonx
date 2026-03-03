using UnityEngine;

public class LvUPMove : MonoBehaviour
{
    [SerializeField] private GameObject _Object;
    [SerializeField] private float _speed = 1;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _Object.transform.position, _speed * Time.deltaTime);
    }
}
