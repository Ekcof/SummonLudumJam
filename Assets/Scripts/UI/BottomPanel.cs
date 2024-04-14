using UnityEngine;
using Zenject;

public class BottomPanel : MonoBehaviour
{

    [Inject]
    MainGameManager _uiManager;

    [SerializeField] private SummonButton _button;
    [SerializeField] private StoneButton[] _buttons;


    void Awake()
    {
        foreach (var button in _buttons) {
            button.SetAction(SetStone);
        }
        EventsBus.Subscribe<OnStartSummon>(this, OnStartSummon);
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
    }
    
    private void OnStartSummon(OnStartSummon data)
    {
        foreach (var button in _buttons)
        {
            button.HideButton();
        }
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        _uiManager.SetStone(new StoneInfo(StoneType.None));
        foreach (var button in _buttons)
        {
            button.ShowButton();
        }
    }

    private void SetStone(StoneType type) {
        StoneInfo stone = new StoneInfo(type);
        
        _uiManager.SetStone(stone);
    }
}
