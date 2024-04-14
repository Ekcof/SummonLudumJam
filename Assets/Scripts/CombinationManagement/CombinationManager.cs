using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CombinationManager : MonoBehaviour
{
    [Inject] HexagonalMap _map;
    [Inject] ResultHandler _results;
    [Inject] SpriteHolder _sprites;
    [Inject] AnimalView _animal;

    private void Awake()
    {
        EventsBus.Subscribe<OnStartSummon>(this, OnStartSummon);
    }
    private void OnStartSummon(OnStartSummon data)
    {
        var combination = CalculateCombination();

        var result = _results.FindMatchingCombination(combination);
        var sprites = _sprites.GetSpriteWrapperById(result.Id);
        _animal.SetView(sprites);
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
