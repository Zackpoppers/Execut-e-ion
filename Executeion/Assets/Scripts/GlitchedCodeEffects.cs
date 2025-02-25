using UnityEngine;
using TMPro;

public class GlitchCodeEffect : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public string[] codeLines;
    private string displayedText = "";
    private float timer = 0f;
    public float typeSpeed = 0.05f;
    public int maxLines = 15;

    void Start()
    {
        displayedText = "";
        InvokeRepeating("AddRandomLine", 0.1f, typeSpeed);
    }

    void AddRandomLine()
    {
        if (codeLines.Length == 0) return;

        int randomIndex = Random.Range(0, codeLines.Length);
        displayedText += codeLines[randomIndex] + "\n";
        codeText.text = displayedText;

        int lineCount = displayedText.Split('\n').Length;
        if (lineCount > maxLines)
        {
            displayedText = displayedText.Substring(displayedText.IndexOf("\n") + 1);
        }
    }
}
