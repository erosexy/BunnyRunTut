using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PointerEnterMessage : MonoBehaviour {

    private GameObject txt;

    public void MouseOver()
    {
        print("Mouse em cima do botão, amiiigo");
        txt = GameObject.Find("Testando");
        txt.GetComponent<Text>().text = "PODEER"; 
    }

    public void MouseNotOver()
    {
        print("Mouse SAIU de cima do botão, amiiigo");
        txt.GetComponent<Text>().text = "AAAAAAH";
    }
}