using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainGameManager : MonoBehaviour
{
    [Inject] HexagonalMap _map;
    private StoneInfo _currentStone;
    public StoneType CurrentStoneType => _currentStone.type;
    public void SetStone(StoneInfo newStone) => _currentStone = newStone;

    private void Awake()
    {
        EventsBus.Subscribe<OnSelectButton>(this, OnSelectButton);
        EventsBus.Subscribe<OnPlaceStone>(this, OnPlaceStone);
        EventsBus.Subscribe<OnStartSummon>(this, OnStartSummon);
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
