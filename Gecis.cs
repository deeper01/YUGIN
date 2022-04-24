using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gecis : MonoBehaviour
{
    public void DigerSahne(string sahneName)
    {
        SceneManager.LoadScene(sahneName);
    }
}

