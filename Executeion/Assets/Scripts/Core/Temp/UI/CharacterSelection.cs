using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public void SelectPython(int playerNumber)
    {
        SaveSelectedCharacter("Python", playerNumber);
        CheckIfBothPlayersSelected();
    }

    public void SelectC(int playerNumber)
    {
        SaveSelectedCharacter("C", playerNumber);
        CheckIfBothPlayersSelected();
    }

    public void SelectCSharp(int playerNumber)
    {
        SaveSelectedCharacter("CSharp", playerNumber);
        CheckIfBothPlayersSelected();
    }

    private void SaveSelectedCharacter(string characterName, int playerNumber)
    {
        if (playerNumber == 1)
        {
            CharacterSelectionData.Player1Character = characterName;
        }
        else if (playerNumber == 2)
        {
            CharacterSelectionData.Player2Character = characterName;
        }
    }

    private void CheckIfBothPlayersSelected()
    {
        if (CharacterSelectionData.BothPlayersSelected)
        {
            LoadGameScene();
        }
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("Bens_Scene");
    }
}