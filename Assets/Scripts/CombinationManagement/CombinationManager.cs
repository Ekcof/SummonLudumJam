using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CombinationManager : MonoBehaviour
{
    [Inject] HexagonalMap _map;
    private StoneInfo _currentStone;

    private void Awake()
    {
        EventsBus.Subscribe<OnStartSummon>(this, OnStartSummon);
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
