using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    void Start()
    {
        if(GameObject.FindGameObjectsWithTag("Music").Length == 1)
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(gameObject);
    }
}
