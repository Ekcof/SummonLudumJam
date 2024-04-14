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
    }

    public void OnRemoved()
    {
        DOTween.Kill(transform);
        transform.DOPunchScale(Vector3.one * 1.2f, 0.3f).SetLoops(2).OnComplete(() => _pool.Pool(this));
        gameObject.SetActive(false);
        IsPlaced = false;
        EventsBus.Publish(new OnRemoveStone { StoneType = _type });
    }

    public void MoveToWorldPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, 0);
        OnPlaced();
    }
}
