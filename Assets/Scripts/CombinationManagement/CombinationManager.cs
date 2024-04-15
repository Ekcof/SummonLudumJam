using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CombinationManager : MonoBehaviour
{
    [Inject] HexagonalMap _map;
    [Inject] ResultHandler _results;
    [Inject(Id ="heads")] SpriteHolder _headSprites;
    [Inject(Id = "bodies")] SpriteHolder _bodySprites;
    [Inject] AnimalView _animal;
    private AnimalChecker _checker = new();

    public int[] StartSummon()
    {
        Debug.Log("OnStartSummon Try to calculate");
        var combination = CalculateCombination();

        var result = _results.FindMatchingCombination(combination);

        var spriteKeys = _checker.CheckAnimals(result.EnLocalization);
        Debug.Log($"OnStartSummon Try to calculate {result.EnLocalization}");

        var headKey = spriteKeys[0];
        var bodyKey = spriteKeys.Count > 1 ? spriteKeys[1] : headKey;

        var headSprite = _headSprites.GetSpriteWrapperById(headKey);
        var bodySprite = _bodySprites.GetSpriteWrapperById(bodyKey);
        _animal.ShowView(headSprite, bodySprite);

        return combination.ToArray();
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
