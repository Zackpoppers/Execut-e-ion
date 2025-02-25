using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Character Prefabs")]
    [SerializeField] GameObject pythonPrefab;
    [SerializeField] GameObject cPrefab;
    [SerializeField] GameObject cSharpPrefab;

    [Header("Spawn Points")]
    [SerializeField] Transform player1SpawnPoint;
    [SerializeField] Transform player2SpawnPoint;

    public void Start()
    {
        SpawnPlayer(1, player1SpawnPoint.position);
        SpawnPlayer(2, player2SpawnPoint.position);
    }

    public void SpawnPlayer(int playerNumber, Vector3 spawnPosition)
    {
        string selectedCharacter = playerNumber == 1 ? CharacterSelectionData.Player1Character : CharacterSelectionData.Player2Character;
        GameObject characterPrefab = GetCharacterPrefab(selectedCharacter);

        if (characterPrefab != null)
        {
            GameObject player = Instantiate(characterPrefab, spawnPosition, Quaternion.identity);
            PlayerInput playerInput = player.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.SetPlayerNumber(playerNumber);
            }
        }
    }

    private GameObject GetCharacterPrefab(string characterName)
    {
        switch (characterName)
        {
            case "Python":
                return pythonPrefab;
            case "C":
                return cPrefab;
            case "CSharp":
                return cSharpPrefab;
            default:
                return null;
        }
    }
}
