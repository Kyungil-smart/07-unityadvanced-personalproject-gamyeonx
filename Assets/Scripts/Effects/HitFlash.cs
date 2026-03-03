using UnityEngine;
using System.Collections;

public class HitFlash : MonoBehaviour
{
    [SerializeField] private Color _hitFlashColor = Color.red;
    [SerializeField] private float _colorChangeTime = 0.1f;

    private SpriteRenderer _sprite;
    private MaterialPropertyBlock _mpb;
    private Coroutine _coroutine;

    private static readonly int ColorID = Shader.PropertyToID("_Color");

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _mpb = new MaterialPropertyBlock();
    }

    public void hitFlash()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(HitFlashCoroutine());
    }

    private IEnumerator HitFlashCoroutine()
    {
        _sprite.GetPropertyBlock(_mpb);
        _mpb.SetColor(ColorID, _hitFlashColor);
        _sprite.SetPropertyBlock(_mpb);

        yield return new WaitForSeconds(_colorChangeTime);

        _sprite.SetPropertyBlock(null);
        _coroutine = null;
    }
}
