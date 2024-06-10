using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class AIUtility : MonoBehaviour
{
    private string[] goodIngredients = { "Strawberry", "Mango", "Banana", "Blueberries", "Pineapple" };
    private string[] badIngredients = { "Kale", "Toothpaste", "Tomato", "Cheese", "Spinach" };

    private GameState gameState;

    public int GetBestMove(Button[] allButtons, string aiIngredients, int difficultyLevel)
    {
        gameState = new GameState();

        int bestMove = -1;
        int bestMoveScore = int.MinValue;

        bool anyMovesLeft = false;

        for (int i = 0; i < allButtons.Length; i++)
        {
            if (allButtons[i].interactable)
            {
                anyMovesLeft = true; // At least one move is available
                string ingredient = allButtons[i].GetComponentInChildren<Text>().text;
                string newAIIngredients = aiIngredients + ingredient + "\n";
                allButtons[i].interactable = false;

                int moveScore = Minimax(allButtons, gameState.player1Ingredients.ToArray(), newAIIngredients.Split('\n'), 0, true, int.MinValue, int.MaxValue, difficultyLevel);

                if (difficultyLevel == 1)
                {
                    moveScore += Random.Range(-2, 3); // Add randomness to the score
                }
                else if (difficultyLevel == 2)
                {
                    moveScore += Random.Range(-1, 2); // Add slight randomness to the score
                }

                // On hard difficulty, directly prioritize moves that are good ingredients
                if (difficultyLevel == 3 && goodIngredients.Contains(ingredient))
                {
                    moveScore += 1000; // High value for good ingredients on hard difficulty
                }

                if (moveScore > bestMoveScore)
                {
                    bestMoveScore = moveScore;
                    bestMove = i;
                }

                allButtons[i].interactable = true;
            }
        }

        if (!anyMovesLeft)
        {
            Debug.Log("No more moves left for the AI.");
            return -1; // Return -1 when no moves are left
        }

        if (bestMove == -1)
        {
            Debug.LogError("Invalid best move index: -1");
        }

        return bestMove;
    }

    private int Minimax(Button[] allButtons, string[] playerIngredients, string[] aiIngredients, int depth, bool isMaximizing, int alpha, int beta, int difficultyLevel)
    {
        if (depth >= 3) // Limit the depth of recursion to prevent performance issues
        {
            return EvaluateMove(playerIngredients, aiIngredients, difficultyLevel, gameState);
        }

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            foreach (var button in allButtons)
            {
                if (button.interactable)
                {
                    string ingredient = button.GetComponentInChildren<Text>().text;
                    button.interactable = false;
                    string[] newAIIngredients = aiIngredients.Append(ingredient).ToArray();
                    int eval = Minimax(allButtons, playerIngredients, newAIIngredients, depth + 1, false, alpha, beta, difficultyLevel);

                    // Prioritize good ingredients on hard difficulty
                    if (difficultyLevel == 3 && goodIngredients.Contains(ingredient))
                    {
                        eval += 1000;
                    }

                    Debug.Log($"Evaluating {ingredient} at depth {depth}: {eval}"); // Debug statement

                    maxEval = Mathf.Max(maxEval, eval);
                    alpha = Mathf.Max(alpha, eval);
                    button.interactable = true;

                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var button in allButtons)
            {
                if (button.interactable)
                {
                    string ingredient = button.GetComponentInChildren<Text>().text;
                    button.interactable = false;
                    string[] newPlayerIngredients = playerIngredients.Append(ingredient).ToArray();
                    int eval = Minimax(allButtons, newPlayerIngredients, aiIngredients, depth + 1, true, alpha, beta, difficultyLevel);
                    minEval = Mathf.Min(minEval, eval);
                    beta = Mathf.Min(beta, eval);
                    button.interactable = true;

                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return minEval;
        }
    }

    private int EvaluateMove(string[] playerIngredients, string[] aiIngredients, int difficultyLevel, GameState gameState)
    {
        int goodIngredientWeight = 0;
        int badIngredientPenalty = 0;

        switch (difficultyLevel)
        {
            case 1: // Easy
                goodIngredientWeight = 3;
                badIngredientPenalty = 2;
                break;
            case 2: // Medium
                goodIngredientWeight = 4;
                badIngredientPenalty = 3;
                break;
            case 3: // Hard
                goodIngredientWeight = 5; // Increased weight for good ingredients on hard difficulty
                badIngredientPenalty = 2;
                break;
            default:
                break;
        }

        int playerScore = playerIngredients.Count(ing => goodIngredients.Contains(ing)) * goodIngredientWeight
            - playerIngredients.Count(ing => badIngredients.Contains(ing)) * badIngredientPenalty;

        int aiScore = aiIngredients.Count(ing => goodIngredients.Contains(ing)) * goodIngredientWeight
            - aiIngredients.Count(ing => badIngredients.Contains(ing)) * badIngredientPenalty;

        return aiScore - playerScore;
    }
}
