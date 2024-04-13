using UnityEngine;

public abstract class GridObject: MonoBehaviour
{
    public string Id { get; private set; }
    public bool IsPlaced { get; private set; }
    public bool IsRemovable { get; private set; } = true;

    public virtual void OnPlaced()
    {
        gameObject.SetActive(true);
        IsPlaced = true;
    }

    public virtual void OnRemoved()
    {
        gameObject.SetActive(false);
        IsPlaced = false;
    }

    public virtual void MoveToWorldPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, 0);
        OnPlaced();
    }
}
