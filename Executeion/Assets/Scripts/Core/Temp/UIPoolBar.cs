using UnityEngine;
using UnityEngine.UI;

public class UIPoolBar : MonoBehaviour
{
    public Image bar; // Reference to the Image component for the health bar fill

    public void SetFillAmount(float fillAmount)
    {
        bar.fillAmount = fillAmount;
    }
}