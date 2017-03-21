using UnityEngine;
using System.Collections;

public class WrongSound : MonoBehaviour {

    public GameObject wrongSound;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            Instantiate(wrongSound, transform.position, transform.rotation);

        }
    }
}
