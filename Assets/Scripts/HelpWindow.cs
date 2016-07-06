using UnityEngine;
using System.Collections;

public class HelpWindow : MonoBehaviour
{
    public Rect windowRect0 = new Rect(120, 150, 600, 80);
    //public Rect windowRect1 = new Rect(20, 100, 120, 50);

    /// <summary>
    /// toda janela precisa de um id, um retângulo para aparecer, uma função para executar ao se clicar nela e um título
    /// </summary>
    void OnGUI()
    {
        windowRect0 = GUI.Window(0, windowRect0, DoMyWindow, "Hello, my friends!"); 
        //windowRect1 = GUI.Window(1, windowRect1, DoMyWindow, "My Window");
        //windowRect2 = GUI.Window(2, windowRect2, DoMyWindow, "UHUL");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="windowID">id da janela que foi clicada</param>
    void DoMyWindow(int windowID)
    {
        
        if (windowID == 0)
        {
            var centeredStyle = GUI.skin.GetStyle("Label");
            centeredStyle.alignment = TextAnchor.UpperCenter; //defina estilos para poder personalizar as janelas
            GUI.Label(new Rect(10, 20, 600, 60), "This game is super easy! Tap the screen to jump, tap again to do a second jump!\nAvoid the cactus and collect as many easter eggs as you can!\nCan you run for more than 100 meters?", centeredStyle);
        }

        //if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
        //    print("Got a click in window " + windowID);

        //GUI.DragWindow(new Rect(0, 0, 10000, 10000)); //permite arrastar a janela
    }
}