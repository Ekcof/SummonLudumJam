using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimalView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _headRenderer;
    [SerializeField] private SpriteRenderer _bodyRenderer;
    private Transform _head => _headRenderer.transform;
    private void Awake()
    {
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
    }

    public void SetView(SpriteWrapper head, SpriteWrapper body)
    {
        _headRenderer.sprite = head.Sprite;
        _bodyRenderer.sprite = body.Sprite;
        gameObject.SetActive(true);
        Animate();
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        DOTween.Kill(_head);
        gameObject.SetActive(false);
    }

    private void Animate()
    {
        DOTween.Kill(_head);
        _head.DORotate(Vector3.forward * 1.2f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }
}
