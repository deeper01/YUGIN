using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hesapla : MonoBehaviour
{

    private float yeniCoin;
    private float equ;
    private float startTime;
    private float yedek;
    private float t;
    private int c = 1;

    void Start()
    {

       


    }


    void Update()
    {
        Time.timeScale = 1f;
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        if (PlayerPrefs.GetFloat("time") == 1)
        {
            PlayerPrefs.SetFloat("of", t);
            Time.timeScale = 0f;
            
            if (c == 1)
            {
                fonk();
                c = c + 1;
            }
        }
  
    }

    public void fonk()
    {
        if (GameObject.FindGameObjectWithTag("bolum").active == true) //bolum sonu oldugunu anlamak icin objeye bolum tag'ini verdim
        {

            yeniCoin = 0; 

            yedek = PlayerPrefs.GetFloat("of"); // level bitimine kadar gecen zamani aldik
            if (yedek >= 100) //eger zaman 100 sn'den fazla ise 0 coin alir
            {
                yeniCoin = 0;
            }
            else 
            {
                yeniCoin =100 - yedek; // 100 sn'den az ise zaman -> kazanilan para = 100- (gecen zaman)
            }
            
           
            equ = yeniCoin + PlayerPrefs.GetFloat("kaydedilencoin"); // genel coin'i arttirmak icin 
            PlayerPrefs.SetFloat("yeniiii", yeniCoin);

            
            PlayerPrefs.SetFloat("kaydedilencoin", equ); 
            Time.timeScale = 0f; //zamani durdur
            Debug.Log("oldu0");

            PlayerPrefs.DeleteKey("of"); //zamani sifirlama
        }
        Debug.Log("oldu1");
    }
    

}
