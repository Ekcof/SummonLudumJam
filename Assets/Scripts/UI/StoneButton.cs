using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StoneButton : MonoBehaviour{
    [Inject]
    SoundManager _soundManager;

    [SerializeField] private Button _button;

    [SerializeField] StoneType _type;

    private Action<StoneType> _onClick;

    private void Awake()
    {
        EventsBus.Subscribe<OnSelectButton>(this, OnSelectButton);
        EventsBus.Subscribe<OnRemoveStone>(this, OnRemoveStone);
        EventsBus.Subscribe<OnPlaceStone>(this, OnPlaceStone);
    }

    public void SetAction(Action<StoneType> action)
    {
        _onClick = action;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClick);
    }

    public void HideButton()
    {
        gameObject.SetActive(false);
    }

    public void ShowButton()
    {
        gameObject.SetActive(true);
    }

    private void OnClick()
    {
        _onClick?.Invoke(_type);
        //_soundManager.PlaySound(0);
        EventsBus.Publish(new OnSelectButton { StoneType = _type });

    }

    private void OnPlaceStone(OnPlaceStone data)
    {
        if (_type == data.StoneType)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnRemoveStone(OnRemoveStone data)
    {
        if (_type == data.StoneType)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnSelectButton(OnSelectButton data)
    {
        if (_type == data.StoneType)
        {
            DOTween.Kill(transform);
            transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            DOTween.Kill(transform);
            transform.localScale = Vector3.one;
        }
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
        DOTween.Kill(transform);
    }

}
