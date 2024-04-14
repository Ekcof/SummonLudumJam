using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ResultHandler", menuName = "Game Resources/Result Handler")]
[Serializable]
public class ResultHandler : ScriptableObject
{
    public ResultObject[] Combinations;
    [SerializeField] private ResultObject _fallBackResult;

    public ResultObject FindMatchingCombination(List<int> combination)
    {
        foreach (var resultObject in Combinations)
        {
            if (resultObject.Combination.SequenceEqual(combination))
            {
                return resultObject;
            }
        }

        return _fallBackResult;
    }
}