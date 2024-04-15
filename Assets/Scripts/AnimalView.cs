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
    private string _currentPortalKey = "SpinPortal";
    private bool _isMonster;

    private void Awake()
    {
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
    }

    public void ShowView(SpriteWrapper head, SpriteWrapper body)
    {
        _isMonster = head.SpriteId == "monster";
        _currentPortalKey = !_isMonster ? "SpinPortal" : "MonsterPortal";
        Debug.Log($"Show view {head.SpriteId} {body.SpriteId}");
        _headRenderer.sprite = head.Sprite;
        _bodyRenderer.sprite = body.Sprite;
        _fxManager.SetActive(_currentPortalKey, true);
        Animate();
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        _fxManager.SetActive(_currentPortalKey, false);
        DOTween.Kill(transform);
        DOTween.Kill(_head);
        gameObject.SetActive(false);
    }

    private void Animate()
    {
        DOTween.Kill(transform);
        DOTween.Kill(_head);
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.6f).SetDelay(1.1f).OnComplete(OnAnimalAppear);
    }

    private void OnAnimalAppear()
    {
        _fxManager.SetActive(_currentPortalKey, false);
        _fxManager.SetActive("MagicFog", true);
        if (_isMonster)
            transform.DOScale(Vector3.one * 0.85f, 3f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        _head.DORotate(Vector3.forward * 1.8f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
        EventsBus.Publish(new OnAnimalAppear());
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(_head);
    }
}
