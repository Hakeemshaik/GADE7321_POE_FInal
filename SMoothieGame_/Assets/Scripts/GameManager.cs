using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject player1Cursor;
    public GameObject player2Cursor;
    public Button[] allButtons;
    public Text player1IngredientsText;
    public Text player2IngredientsText;

    private bool player1Selected;
    private bool player2Selected;

    private GameState gameState;

    void Start()
    {
        InitializeGameState();
        player1Cursor.SetActive(false);
        player2Cursor.SetActive(false);

        
        player1Cursor.SetActive(true);

        
        Vector3 initialMousePosition = Input.mousePosition;
        player1Cursor.transform.position = initialMousePosition;
        player2Cursor.transform.position = initialMousePosition;
    }

    void InitializeGameState()
    {
        gameState = new GameState();
        // Initialize ingredient availability
        string[] ingredientNames = { "Strawberry", "Kale", "Mango", "Banana", "Toothpaste", "Tomato", "Blueberries", "Cheese", "Pineapple", "Spinach" };
        foreach (string ingredient in ingredientNames)
        {
            gameState.ingredientAvailability.Add(ingredient, true);
        }
    }

    void UpdatePlayerIngredients()
    {
        gameState.player1Ingredients = player1IngredientsText.text.Split('\n').ToList();
        gameState.player2Ingredients = player2IngredientsText.text.Split('\n').ToList();
    }

    void UpdateIngredientAvailability(string ingredientName, bool available)
    {
        gameState.ingredientAvailability[ingredientName] = available;
    }

    public void OnIngredientButtonClick(Button button)
    {
        string ingredientName = button.GetComponentInChildren<Text>().text;

        if (player1Cursor.activeSelf)
        {
            player1IngredientsText.text += ingredientName + "\n";
            player1Selected = true;
            UpdateIngredientAvailability(ingredientName, false);
        }
        else if (player2Cursor.activeSelf)
        {
            player2IngredientsText.text += ingredientName + "\n";
            player2Selected = true;
            UpdateIngredientAvailability(ingredientName, false);
        }

        button.interactable = false;

        UpdatePlayerIngredients();

        if (player1Selected && player2Selected)
        {
            Debug.Log("Both players have selected ingredients.");
        }

        SwitchPlayerCursors();
    }

    void SwitchPlayerCursors()
    {
        player1Cursor.SetActive(!player1Cursor.activeSelf);
        player2Cursor.SetActive(!player2Cursor.activeSelf);
    }
}