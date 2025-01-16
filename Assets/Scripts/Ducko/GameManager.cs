using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Estas instrucciones harán que Unity renderice el juego a 10 cuadros por segundo.
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 10;
    }

    
}
