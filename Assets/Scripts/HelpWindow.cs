using UnityEngine;
using System.Collections;

public class HelpWindow : MonoBehaviour
{
    private Rect windowRect0 = new Rect((Screen.width - 1200)/2, (Screen.height - 390)/2, 1200, 390);
    private GameObject player;
    //private Rect windowRect1 = new Rect(20, 100, 120, 50);

    /// <summary>
    /// toda janela precisa de um id, um retângulo para aparecer, uma função para executar ao se clicar nela e um título
    /// </summary>
    void OnGUI()
    {
        var windowSize = GUI.skin.GetStyle("Window");
        windowSize.fontSize = 30;
        windowSize.border.top = 1;
       
        player = GameObject.Find("Bunny");
        if (player == null)
        {
            player = GameObject.Find("Bunnie");
            player.GetComponent("bunnieController");
        }
        else
        {
            player.GetComponent("bunnyController");
        }
        //player.SetActive(false);
        windowRect0 = GUI.Window(0, windowRect0, DoMyWindow, "HELLO,  FRIENDS!"); 
        //windowRect1 = GUI.Window(1, windowRect1, DoMyWindow, "My Window");
        //windowRect2 = GUI.Window(2, windowRect2, DoMyWindow, "UHUL");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="windowID">id da janela que foi clicada</param>
    void DoMyWindow(int windowID)
    {
        var buttonSize = GUI.skin.GetStyle("Button");
        buttonSize.fontSize = 50;

        if (windowID == 0)
        {
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter; //defina estilos para poder personalizar as janelas
            centeredStyle.fontSize = 50;
            GUI.Label(new Rect(10, 30, 1200, 280), "This game is super easy! Tap the screen to jump, tap again to do a second jump!\nAvoid the cactus and collect as many easter eggs as you can!\nCan you run for more than 100 meters?", centeredStyle);
            stopTime();
        }

        if (GUI.Button(new Rect((windowRect0.width - 350) / 2, (windowRect0.height + 260) / 2, 350, 60), "Start Game", buttonSize))
        {
            Debug.Log(Time.timeScale);
            startTime();
            Destroy(this);
            print("Got a click in window " + windowID);
        }
        

        //GUI.DragWindow(new Rect(0, 0, 10000, 10000)); //permite arrastar a janela
    }

    public void stopTime()
    {
        Time.timeScale = 0.0f;
        player.SetActive(true);
    }

    public void startTime()
    {
        Time.timeScale = 1.0f;
        player.SetActive(true);
    }
}