using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SinglePlayerGameManager : MonoBehaviour
{
    public GameObject playerCursor;
    public GameObject aiCursor;
    public Button[] allButtons;
    public Text playerIngredientsText;
    public Text aiIngredientsText;
    public AIUtility aiUtility;
    public MainMenu mainMenu;

    private bool playerTurn = true;
    private GameState gameState;
    private int difficultyLevel = 1;

    void Start()
    {
        difficultyLevel = PlayerPrefs.GetInt("DifficultyLevel", 1);
        InitializeGameState();
        playerCursor.SetActive(true);
        aiCursor.SetActive(false);

        if (aiUtility == null)
        {
            Debug.LogError("AIUtility is not assigned.");
        }
    }

    void InitializeGameState()
    {
        gameState = new GameState();
        string[] ingredientNames = { "Strawberry", "Kale", "Mango", "Banana", "Toothpaste", "Tomato", "Blueberries", "Cheese", "Pineapple", "Spinach" };
        foreach (string ingredient in ingredientNames)
        {
            gameState.ingredientAvailability.Add(ingredient, true);
        }
    }

    void UpdatePlayerIngredients()
    {
        gameState.player1Ingredients = playerIngredientsText.text.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        gameState.player2Ingredients = aiIngredientsText.text.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }

    void UpdateIngredientAvailability(string ingredientName, bool available)
    {
        gameState.ingredientAvailability[ingredientName] = available;
    }

    public void OnIngredientButtonClick(Button button)
    {
        if (!playerTurn || !button.interactable) return;

        string ingredientName = button.GetComponentInChildren<Text>().text;

        if (playerCursor.activeSelf)
        {
            playerIngredientsText.text += ingredientName + "\n";
            UpdateIngredientAvailability(ingredientName, false);
        }

        button.interactable = false;
        UpdatePlayerIngredients();

        playerTurn = false;
        playerCursor.SetActive(false);
        aiCursor.SetActive(true);

        StartCoroutine(AITurn());
    }

    IEnumerator AITurn()
    {
        yield return new WaitForSeconds(1);

        if (aiUtility == null)
        {
            Debug.LogError("AIUtility is not assigned.");
            yield break;
        }

        string aiIngredients = aiIngredientsText.text;

        int bestMove = aiUtility.GetBestMove(allButtons, aiIngredients, difficultyLevel);

        if (bestMove >= 0 && bestMove < allButtons.Length && allButtons[bestMove].interactable)
        {
            Button button = allButtons[bestMove];
            if (button != null)
            {
                string ingredientName = button.GetComponentInChildren<Text>().text;
                aiIngredientsText.text += ingredientName + "\n";
                button.interactable = false;
                UpdateIngredientAvailability(ingredientName, false);
                UpdatePlayerIngredients();
            }
            else
            {
                Debug.LogError("Button at best move index is null.");
            }
        }
        else
        {
            Debug.LogError("Invalid best move index: " + bestMove);
        }

        playerTurn = true;
        aiCursor.SetActive(false);
        playerCursor.SetActive(true);
    }
}
