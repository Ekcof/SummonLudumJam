using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

public class AnimalView : MonoBehaviour
{
    [Inject] FXManager _fxManager;
    [SerializeField] private SpriteRenderer _headRenderer;
    [SerializeField] private SpriteRenderer _bodyRenderer;
    private Transform _head => _headRenderer.transform;
    private void Awake()
    {
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
    }

    public void ShowView(SpriteWrapper head, SpriteWrapper body)
    {
        Debug.Log($"Show view {head.SpriteId} {body.SpriteId}");
        _headRenderer.sprite = head.Sprite;
        _bodyRenderer.sprite = body.Sprite;
        _fxManager.SetActive("SpinPortal", true);
        Animate();
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        _fxManager.SetActive("SpinPortal", false);
        DOTween.Kill(_head);
        gameObject.SetActive(false);
    }

    private void Animate()
    {
        DOTween.Kill(transform);
        DOTween.Kill(_head);
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.6f).SetDelay(1.1f).OnComplete(() => _fxManager.SetActive("SpinPortal", false));
        _head.DORotate(Vector3.forward * 1.8f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(_head);
    }
}
