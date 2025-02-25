using UnityEngine;

public class CharacterLoader : MonoBehaviour
{
    [SerializeField] private GameObject pythonPrefab;
    [SerializeField] private GameObject cPrefab;
    [SerializeField] private GameObject cSharpPrefab;

    [SerializeField] private Transform player1SpawnPoint;
    [SerializeField] private Transform player2SpawnPoint;

    private void Start()
    {
        LoadSelectedCharacter(1, player1SpawnPoint.position); // Load Player 1
        LoadSelectedCharacter(2, player2SpawnPoint.position); // Load Player 2
    }

    private void LoadSelectedCharacter(int playerNumber, Vector3 spawnPosition)
    {
        string selectedCharacter = playerNumber == 1 ? CharacterSelectionData.Player1Character : CharacterSelectionData.Player2Character;

        GameObject characterPrefab = null;

        switch (selectedCharacter)
        {
            case "Python":
                characterPrefab = pythonPrefab;
                break;
            case "C":
                characterPrefab = cPrefab;
                break;
            case "CSharp":
                characterPrefab = cSharpPrefab;
                break;
        }

        if (characterPrefab != null)
        {
            Instantiate(characterPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Character prefab not found for: " + selectedCharacter);
        }
    }
}