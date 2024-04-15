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
        EventsBus.Subscribe<OnAnimalAppear>(this, OnAnimalAppear);
        Initialize();
    }

    private void Initialize()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(StartSummon);

        _image.sprite = _summonSprite;
        DisableButton();
    }

    private void DisableButton()
    {
        _button.interactable = false;
        _image.color = _disabledColor;
    }

    private void StartSummon()
    {
        _soundManager.PlaySound("summon");
        EventsBus.Publish(new OnStartSummon());
        OnChangeStoneCount(default);
        ChangeButton();
    }

    private void ChangeButton()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnTryAgain);
        _image.sprite = _tryAgainSprite;
        DisableButton();
    }

    private void OnTryAgain()
    {
        EventsBus.Publish(new OnFinishSummon());
        Initialize();
    }

    private void OnAnimalAppear(OnAnimalAppear data)
    {
        _button.interactable = true;
        _image.color = _enabledColor;
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
