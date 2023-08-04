using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyAddAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void ResetText() // this is basically called at the animation event.
    {
        text.text =string.Empty;
    }
}
