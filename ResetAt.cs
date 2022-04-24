using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAt : MonoBehaviour
{
    public void DeleteHepsi()
    {
        PlayerPrefs.DeleteAll();
    }
}
