using UnityEngine;
using System.Collections;

public class DestroyOnTouch : MonoBehaviour {

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
