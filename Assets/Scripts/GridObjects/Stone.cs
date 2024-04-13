using DG.Tweening;
using UnityEngine;

public class Stone: MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private StoneType _type;
    public StoneType StoneType => _type;
    public string Id { get; private set; }
    public bool IsPlaced { get; private set; }
    public bool IsRemovable { get; private set; } = true;

    public void OnDoubleClick()
    {
        DOTween.Kill(transform);
        transform.DOPunchScale(Vector3.one * 1.2f, 0.3f).SetLoops(2).OnComplete(() => Destroy(gameObject));
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
    public void OnPlaced()
    {
        gameObject.SetActive(true);
        IsPlaced = true;
    }

    public void OnRemoved()
    {
        gameObject.SetActive(false);
        IsPlaced = false;
    }

    public void MoveToWorldPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, 0);
        OnPlaced();
    }
}
