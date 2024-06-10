using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public Text player1IngredientsText;
    public Text player2IngredientsText;
    public Button resultButton;
    public Text resultText;

    private string[] goodIngredients = { "Strawberry", "Mango", "Banana", "Blueberries", "Pineapple" };
    private string[] badIngredients = { "Kale", "Toothpaste", "Tomato", "Cheese", "Spinach" };

    private bool blendingCompleted = false;

    void Start()
    {
        resultButton.onClick.AddListener(CalculateResult);
    }

    public void CalculateResult()
    {
        int player1GoodCount = CountGoodIngredients(player1IngredientsText.text);
        int player2GoodCount = CountGoodIngredients(player2IngredientsText.text);

        if (player1GoodCount > player2GoodCount)
        {
            resultText.text = "Player 1 wins!";
        }
        else if (player2GoodCount > player1GoodCount)
        {
            resultText.text = "Player 2 wins!";
        }
        else
        {
            resultText.text = "It's a tie!";
        }

        
        resultButton.interactable = false;

        StartCoroutine(WaitAndGoToMainMenu());
    }

    private int CountGoodIngredients(string ingredientsText)
    {
        int count = 0;
        foreach (string ingredient in goodIngredients)
        {
            if (ingredientsText.Contains(ingredient))
            {
                count++;
            }
        }
        return count;
    }

    IEnumerator WaitAndGoToMainMenu()
    {
        
        yield return new WaitForSeconds(3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}