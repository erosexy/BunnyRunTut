using UnityEngine;
using System.Collections;

public class MyUnitySingletonTxt : MonoBehaviour {

    private static MyUnitySingletonTxt instance = null;
    public static MyUnitySingletonTxt Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        //verifica se uma instância do objeto já existe
        // se sim, ele não inicia outra, destrói
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }

        DontDestroyOnLoad(transform.root.gameObject);

        /*works if the object is a child from other, but can occur duplicates
		//if not, use DontDestroyOnLoad (this.gameObject);
		DontDestroyOnLoad (transform.root.gameObject);*/
    }
}
