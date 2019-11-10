using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static bool PortalsDestroyed { get => GameObject.FindGameObjectsWithTag("Portal").Length == 0; }

    public static int PortalsLeft { get => GameObject.FindGameObjectsWithTag("Portal").Length; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
