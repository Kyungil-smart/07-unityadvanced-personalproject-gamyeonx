using UnityEngine;

public class DwHpTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _bossHp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        _bossHp.SetActive(!_bossHp.activeSelf);
        Destroy(gameObject);
    }
}
