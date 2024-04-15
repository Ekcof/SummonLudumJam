using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainGameManager : MonoBehaviour
{
    [Inject] HexagonalMap _map;
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
        var combination = CalculateCombination();
        IsSummonState = true;
    }

    private void OnFinishSummon(OnFinishSummon data)
    {
        IsSummonState = false;
    }

    private List<int> CalculateCombination()
    {
        List<int> combination = new();
        foreach (var kvp in _map.GridObjects)
        {
            combination.Add((int)kvp.Value.StoneType);
        }
        combination.Sort();

        string combinationString = string.Join(",", combination);

        Debug.Log($" The combination is {combinationString}");
        return combination;
    }
}
