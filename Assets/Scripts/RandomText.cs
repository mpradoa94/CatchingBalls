using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomText : MonoBehaviour {

    public string[] texts;
    public Text txt;
    public bool isNotRed;
    public Color[] colors;

    void Awake()
    {
        txt.text = texts[Random.Range(0, texts.Length)];
        if (isNotRed)
        {
            txt.color = colors[Random.Range(0, colors.Length)];
        }
    }
}
