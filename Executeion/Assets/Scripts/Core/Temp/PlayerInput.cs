using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }

    [Header("Player 1 Keybindings")]
    public KeyCode player1MoveLeft = KeyCode.A;
    public KeyCode player1MoveRight = KeyCode.D;
    public KeyCode player1Jump = KeyCode.W;
    public KeyCode player1Attack = KeyCode.J;

    [Header("Player 2 Keybindings")]
    public KeyCode player2MoveLeft = KeyCode.LeftArrow;
    public KeyCode player2MoveRight = KeyCode.RightArrow;
    public KeyCode player2Jump = KeyCode.UpArrow;
    public KeyCode player2Attack = KeyCode.M;

    private int playerNumber;

    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
    }

    public void Update()
    {
        if (playerNumber == 1)
        {
            MoveInput = new Vector2(
                (Input.GetKey(player1MoveLeft) ? -1 : 0) + (Input.GetKey(player1MoveRight) ? 1 : 0),
                0
            );
            JumpPressed = Input.GetKeyDown(player1Jump);
            AttackPressed = Input.GetKeyDown(player1Attack);
        }
        else if (playerNumber == 2)
        {
            MoveInput = new Vector2(
                (Input.GetKey(player2MoveLeft) ? -1 : 0) + (Input.GetKey(player2MoveRight) ? 1 : 0),
                0
            );
            JumpPressed = Input.GetKeyDown(player2Jump);
            AttackPressed = Input.GetKeyDown(player2Attack);
        }
    }
}
