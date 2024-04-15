using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using Zenject;

public class MainGameManager : MonoBehaviour
{
    [Inject] CombinationManager _combinationManager;
    [Inject] UIPanel _panel;
    [Inject] FXManager _fxManager;

    [SerializeField] readonly List<int[]> _obtainedCombinations = new();
    [SerializeField] int _reward = 9;
    private int _money = 0;

    private StoneInfo _currentStone;
    public StoneType CurrentStoneType => _currentStone != null ? _currentStone.type : StoneType.None;
    public void SetStone(StoneInfo newStone) => _currentStone = newStone;
    public bool IsSummonState { get; private set; } = false;

    private void Awake()
    {
        EventsBus.Subscribe<OnSelectButton>(this, OnSelectButton);
        EventsBus.Subscribe<OnPlaceStone>(this, OnPlaceStone);
        EventsBus.Subscribe<OnStartSummon>(this, OnStartSummon);
        EventsBus.Subscribe<OnFinishSummon>(this, OnFinishSummon);
    }

    private void OnSelectButton(OnSelectButton data)
    {
        if (data.StoneType == StoneType.None)
        {
            _currentStone = null;
        }
    }

    private void OnPlaceStone(OnPlaceStone data)
    {
        _currentStone = null;
    }

    private void OnStartSummon(OnStartSummon data)
    {
        var result = _combinationManager.StartSummon();

        if (result == null)
        {
            Debug.LogAssertion("Failed to get result!");
            return;
        }

        var combination = result.Combination.ToArray();

        if (_obtainedCombinations.Any(x => x.SequenceEqual(combination)))
        {
            _panel.SetAnimalText("repeat");
        }
        else if (result.Id == "monster")
        {
            _panel.SetAnimalText("monster");
        }
        else
        {
            Debug.Log("Add new combination");
            _money += _reward;
            _panel.OnChangeMoney(_money);
            _panel.SetAnimalText(result.EnLocalization);
            _obtainedCombinations.Add(combination);

        }
        _fxManager.SetActive("Fog", false);
        IsSummonState = true;
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        _fxManager.SetActive("Fog", true);
        IsSummonState = false;
    }
}
