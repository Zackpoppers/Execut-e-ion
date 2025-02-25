public static class CharacterSelectionData
{
    public static string Player1Character { get; set; }
    public static string Player2Character { get; set; }

    public static bool BothPlayersSelected => !string.IsNullOrEmpty(Player1Character) && !string.IsNullOrEmpty(Player2Character);
}