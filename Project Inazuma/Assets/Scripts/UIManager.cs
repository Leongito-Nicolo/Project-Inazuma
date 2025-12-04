using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text winText;

    public void ShowWin()
    {
        winText.text = "Goal!!!!";
    }


}