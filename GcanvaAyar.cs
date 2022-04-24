using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GcanvaAyar : MonoBehaviour
{
    [SerializeField] GameObject canvasA;
    void Start()
    {
        canvasA.SetActive(true);
    }
   
    public void canvasKapa()
    {
        canvasA.SetActive(false);


    }
}