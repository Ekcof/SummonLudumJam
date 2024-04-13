using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class Stone : GridObject
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private StoneType _type;
    public StoneType StoneType => _type;

    public void OnDoubleClick()
    {
        DOTween.Kill(transform);
        transform.DOPunchScale(Vector3.one * 1.2f, 0.3f).SetLoops(2).OnComplete(()=>Destroy(gameObject));
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
