using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int difficultyLevel = 1; // Default to Easy
    public GameObject mainMenuUI;
    public GameObject choiceMenuUI; // Reference to the choice menu UI
    public GameObject difficultyUI; // Reference to the difficulty selection UI

    void Start()
    {
        ShowMainMenu(); // Show the main menu by default
    }

    public void OnPlayButtonClicked()
    {
        HideMainMenu();
        ShowChoiceMenu();
    }

    public void OnSinglePlayerButtonClicked()
    {
        HideChoiceMenu();
        ShowDifficultyMenu();
    }

    public void OnMultiplayerButtonClicked()
    {
        SceneManager.LoadScene("Game"); // Load the multiplayer game scene
    }

    public void OnBackButtonClicked()
    {
        if (difficultyUI.activeSelf)
        {
            HideDifficultyMenu();
            ShowChoiceMenu();
        }
        else if (choiceMenuUI.activeSelf)
        {
            HideChoiceMenu();
            ShowMainMenu();
        }
    }

    public void SetEasyDifficulty()
    {
        difficultyLevel = 1;
        PlayerPrefs.SetInt("DifficultyLevel", difficultyLevel);
        LoadGameScene();
    }

    public void SetMediumDifficulty()
    {
        difficultyLevel = 2;
        PlayerPrefs.SetInt("DifficultyLevel", difficultyLevel);
        LoadGameScene();
    }

    public void SetHardDifficulty()
    {
        difficultyLevel = 3;
        PlayerPrefs.SetInt("DifficultyLevel", difficultyLevel);
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        Cursor.visible = false; // Hide the cursor when starting the game
        SceneManager.LoadScene("changes"); // Replace with your game scene name
    }

    private void ShowMainMenu()
    {
        mainMenuUI.SetActive(true);
        Cursor.visible = true; // Show the cursor when displaying the main menu
    }

    private void HideMainMenu()
    {
        mainMenuUI.SetActive(false);
    }

    private void ShowChoiceMenu()
    {
        choiceMenuUI.SetActive(true);
        Cursor.visible = true; // Show the cursor when displaying the choice menu
    }

    private void HideChoiceMenu()
    {
        choiceMenuUI.SetActive(false);
    }

    private void ShowDifficultyMenu()
    {
        difficultyUI.SetActive(true);
        Cursor.visible = true; // Show the cursor when displaying the difficulty menu
    }

    private void HideDifficultyMenu()
    {
        difficultyUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
