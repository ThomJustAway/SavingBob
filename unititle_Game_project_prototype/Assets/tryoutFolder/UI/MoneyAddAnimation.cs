using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyAddAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void ResetText()
    {
        text.text =string.Empty;
    }
}
