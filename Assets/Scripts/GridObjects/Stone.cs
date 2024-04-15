using DG.Tweening;
using UnityEngine;
using Zenject;

public class Stone : MonoBehaviour
{
    [Inject] GridObjectPool _pool;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private StoneType _type;
    public StoneType StoneType => _type;
    public string Id { get; private set; }
    public bool IsPlaced { get; private set; }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
    public void OnPlaced()
    {
        gameObject.SetActive(true);
        IsPlaced = true;
        EventsBus.Publish(new OnPlaceStone { StoneType = _type });
        transform.localScale = Vector3.one;
    }

    public void OnRemoved()
    {
        DOTween.Kill(transform);
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InSine).OnComplete(Hide);
        EventsBus.Publish(new OnRemoveStone { StoneType = _type });
    }

    public void Hide()
    {
        IsPlaced = false;
        gameObject.SetActive(false);
        _pool.Pool(this);
    }

    public void MoveToWorldPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, 0);
        OnPlaced();
    }
}
