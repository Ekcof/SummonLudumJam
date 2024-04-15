using System;
using System.Collections.Generic;

public class AnimalChecker
{
    readonly List<string> _animals = new List<string>()
    {
        "hare", "toad", "fox", "bear", "panda", "fish", "cat", "swan", "elephant", "crocodile"
    };

    public List<string> CheckAnimals(string input)
    {
        input = input.ToLower();
        foreach (var firstAnimal in _animals)
        {
            if (input.StartsWith(firstAnimal))
            {
                string remaining = input.Substring(firstAnimal.Length);

                foreach (var secondAnimal in _animals)
                {
                    if (remaining == secondAnimal)
                    {
                        Console.WriteLine($"'{input}' consists of '{firstAnimal}' and '{secondAnimal}'");
                        return new List<string> { firstAnimal, secondAnimal };
                    }
                }
                return new List<string> { firstAnimal };
            }
        }
        return new List<string> { "monster" };
    }
}