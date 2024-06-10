using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultButtonManager : MonoBehaviour
{
    public Button resultButton;
    private bool player1Selected;
    private bool player2Selected;

    void Start()
    {
        resultButton.gameObject.SetActive(false);
    }

    public void IngredientSelectedForPlayer(int player)
    {
        if (player == 1)
        {
            player1Selected = true;
        }
        else if (player == 2)
        {
            player2Selected = true;
        }

        if (player1Selected && player2Selected)
        {
            resultButton.gameObject.SetActive(true);
        }
    }
}
