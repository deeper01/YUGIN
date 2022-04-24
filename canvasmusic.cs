using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasmusic : MonoBehaviour
{
    [SerializeField] GameObject soundEkrani;
    void Start()
    {
        soundEkrani.SetActive(false);
    }
    public void canvasAc()
    {
        soundEkrani.SetActive(true);
    }

    public void canvasKapa()
    {
        soundEkrani.SetActive(false);


    }
}
//SetActive true olursa istenilen objeyi aktif eder
//SetActive true olursa istenilen objeyi pasif yapar

//tiklanildiginda muzik icin olan canvasi acma/kapama