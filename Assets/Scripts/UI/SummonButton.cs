using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class SummonButton : MonoBehaviour
{
    [Inject] HexagonalMap _map;
    [Inject] SoundManager _soundManager;
    [Inject] MainGameManager _mainGameManager;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Color _enabledColor = Color.white;
    [SerializeField] private Color _disabledColor = Color.gray;
    [SerializeField] private Sprite _summonSprite;
    [SerializeField] private Sprite _tryAgainSprite;

    void Start()
    {
        EventsBus.Subscribe<OnPlaceStone>(this, OnChangeStoneCount);
        EventsBus.Subscribe<OnRemoveStone>(this, OnChangeStoneCount);
        Initialize();
    }

    private void Initialize()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(StartSummon);

        _button.interactable = false;
        _image.sprite = _summonSprite;
        _image.color = _disabledColor;
    }

    private void StartSummon()
    {
        _soundManager.PlaySound(0);
        EventsBus.Publish(new OnStartSummon());
        OnChangeStoneCount(default);
        ChangeButton();
    }

    private void ChangeButton() {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnTryAgain);
        _image.sprite = _tryAgainSprite;
    }

    private void OnTryAgain() {
        Initialize();
        EventsBus.Publish(new OnFinishSummon());
    }

    private void OnChangeStoneCount(object data)
    {
        if (_mainGameManager.IsSummonState)
            return;
        _button.interactable = _map.GridObjects.Count >= 3;
        _image.color = _map.GridObjects.Count >= 3 ? _enabledColor : _disabledColor;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
