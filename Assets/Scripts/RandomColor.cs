using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {

    public Color[] colors;

    void Awake () {
        GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
    }
	
}
