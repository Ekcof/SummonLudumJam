using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SummonButton : MonoBehaviour
{
    [Inject] HexagonalMap _map;
    [Inject] SoundManager _soundManager;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Color _enabledColor = Color.white;
    [SerializeField] private Color _disabledColor = Color.gray;

    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(StartSummon);
        EventsBus.Subscribe<OnPlaceStone>(this, OnChangeStoneCount);
        EventsBus.Subscribe<OnRemoveStone>(this, OnChangeStoneCount);

        _button.interactable = false;
        _image.color = _disabledColor;

    }
    private void StartSummon()
    {
        _soundManager.PlaySound(0);
        EventsBus.Publish(new OnStartSummon());
        OnChangeStoneCount(default);
    }

    private void OnChangeStoneCount(object data)
    {
        _button.interactable = _map.GridObjects.Count > 0;
        _image.color = _map.GridObjects.Count > 0 ? _enabledColor : _disabledColor;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
