using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : GridObject
{
    //[SerializeField] Collider2D _collider;
    [SerializeField] HexagonalDirection _outDirection;
    [SerializeField] SpriteRenderer _spriteRenderer;

    //public Collider2D Collider => _collider;
    public HexagonalDirection Direction => _outDirection;

    public override void OnPlaced()
    {
        base.OnPlaced();
        Debug.Log($"Prism {Id} has been placed");
    }

    public override void OnRemoved()
    {
        base.OnRemoved();
        Debug.Log($"Prism {Id} has been removed");
    }

    public void Rotate(bool left)
    {

    }
}
