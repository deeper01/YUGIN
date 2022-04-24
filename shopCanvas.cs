using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shopCanvas : MonoBehaviour
{
    
    [SerializeField] GameObject respan;
    [SerializeField] GameObject respans;
    void Start()
    {

        respan.SetActive(false); //resim 1
        respans.SetActive(false); //resim 2
    }

   
    void Update()
    {
        //respawnsss = GameObject.FindGameObjectsWithTag("resim");

        if ( PlayerPrefs.GetInt("aktiflik3") == 10 && PlayerPrefs.GetInt("aktiflik2") == 9) //eger resim 1 ve resim 2 satilirsa 
        {
            respans.SetActive(false);
            respans.SetActive(false);
            PlayerPrefs.SetInt("aktiflik", 0);
            PlayerPrefs.SetInt("aktiflik3", 0);
            PlayerPrefs.SetInt("aktiflik2", 0);
            Debug.Log("aktif degilplayer");
        }

        else if (PlayerPrefs.GetInt("aktiflik3") == 10) //eger resim 2 satildiysa 
        {
            respans.SetActive(false);
            PlayerPrefs.SetInt("aktiflik", 0);
            PlayerPrefs.SetInt("aktiflik3", 0);
            Debug.Log("aktif degilplayer");
        }
        else if (PlayerPrefs.GetInt("aktiflik2") == 9) //eger resim 1 satildiysa 
        {
            respan.SetActive(false);
            PlayerPrefs.SetInt("aktiflik", 0);
            PlayerPrefs.SetInt("aktiflik2", 0);
            Debug.Log("aktif degilplayer");
        }

        else if (PlayerPrefs.GetInt("aktiflik") == 1 && PlayerPrefs.GetInt("aktiflik2") == 9) //eger resim 1 alindiysa ve satildiysa -> aktif etme
        {
            respan.SetActive(false);
            PlayerPrefs.SetInt("aktiflik",0);
            PlayerPrefs.SetInt("aktiflik2", 0);
            Debug.Log("aktitplaywe");
           
        }
        else if(PlayerPrefs.GetInt("aktiflik") == 2 && PlayerPrefs.GetInt("aktiflik3") == 10) //eger resim 2 alindiysa ve satildiysa ->aktif etme
        {
            respans.SetActive(false);
            PlayerPrefs.SetInt("aktiflik",0);
            PlayerPrefs.SetInt("aktiflik3", 0);
            Debug.Log("aktif degilplayer");
        }

        


        else if(PlayerPrefs.GetInt("aktiflik")== 1) // eger resim 1 alindiysa atkif et 
        {
            respan.SetActive(true);
           
            Debug.Log("aktif degil2");
        }

        else if (PlayerPrefs.GetInt("aktiflik") == 2) // eger resim 2 alindiysa atkif et 
        {
            respans.SetActive(true);
           
            Debug.Log("aktif degil3");
        }
     

    }
}
