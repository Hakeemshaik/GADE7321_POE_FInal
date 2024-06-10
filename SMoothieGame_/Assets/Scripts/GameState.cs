using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class GameState
{
    public List<string> player1Ingredients;
    public List<string> player2Ingredients;
    public Dictionary<string, bool> ingredientAvailability;

    public GameState()
    {
        player1Ingredients = new List<string>();
        player2Ingredients = new List<string>();
        ingredientAvailability = new Dictionary<string, bool>();
    }
}
