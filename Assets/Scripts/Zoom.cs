using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour
{
    public float zoomSize = 5;

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (zoomSize > 2)
                zoomSize -= 1;
            Debug.Log("Scroll in");
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (zoomSize < 4)
                zoomSize += 1;
            Debug.Log("Scroll out");
        }
        GetComponent<Camera>().orthographicSize = zoomSize;
    }
}
