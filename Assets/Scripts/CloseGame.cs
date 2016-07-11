using UnityEngine;
using System.Collections;

public class CloseGame : MonoBehaviour {

	public void GameClosed()
    {
        Application.Quit();
    }
}
