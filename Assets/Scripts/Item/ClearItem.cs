using UnityEngine;

public class ClearItem : MonoBehaviour
{
    [SerializeField] private int _addExp = 2000;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        PlayerManager.Instance.EXP += _addExp;
        PlayerManager.Instance.OnItemCollected();
        Destroy(gameObject);
    }
}
