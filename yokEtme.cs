using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yokEtme : MonoBehaviour
{
    void Awake()
    {
        GameObject[] mObj = GameObject.FindGameObjectsWithTag("music");
        if (mObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
