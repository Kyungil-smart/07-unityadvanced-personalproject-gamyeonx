using UnityEngine;

public class LvItem : MonoBehaviour
{
   [SerializeField] private int _addExp = 500;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        PlayerManager.Instance.EXP = _addExp;
        Destroy(gameObject);
    }
}
