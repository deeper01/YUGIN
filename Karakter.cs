
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Karakter : MonoBehaviour
{
    [Header("Bolum Sonu Panel")]
    public GameObject bolumSonu_P;
    [Space]
    public int toplananItem;
  
    private void Update()
    {
        if (PlayerPrefs.GetFloat("bitis") == 1) //bitip bitmedigi
        { 
            Debug.Log("hello");
           sonrakiLevelKontrolcusu();
          



            //level suresine gore yildiz sayisi belirleme
            if (100 > (PlayerPrefs.GetFloat("zaman")) && (PlayerPrefs.GetFloat("zaman")) > 50) 
            {

                toplananItem = 1;
                

            }
            else if ((PlayerPrefs.GetFloat("zaman")) < 50 && (PlayerPrefs.GetFloat("zaman")) > 30)
            {


                toplananItem = 2;
            }
            else if ((PlayerPrefs.GetFloat("zaman")) < 30 && (PlayerPrefs.GetFloat("zaman")) >= 1)
            {

                toplananItem = 3;

            }

        }
        
    }

    public void sonrakiLevelKontrolcusu()
    {
        string currentLevel = levelAdi(SceneManager.GetActiveScene().buildIndex);// (PlayerPrefs.GetString("suankiSecilenLevel");) Yenilendi çünkü level ekranından geçişte kaydettiğimiz leveli alıyorduk fakat sonraki levele bu sahneden geçince kayıtlı level eskisi kalıyor o yüzden direk aktif sahne build indexinden adını çağırıp işlem yaptırıyoruz.
        int currentLevelID = int.Parse(currentLevel.Split('_')[1]); //Level_id biçiminde olusturduk butun levelleri ki gezebilelim
        yildizci(currentLevelID);
        int nextLevel = PlayerPrefs.GetInt("level") + 1;

        if (currentLevelID == PlayerPrefs.GetInt("seviyeSayisi"))
        {
            Debug.Log("oyun bitti");

            bolumSonu_P.transform.GetChild(1).gameObject.SetActive(false); //sonraki level butonunu kapat
        }
        else
        {
            if (nextLevel - currentLevelID == 1)
                PlayerPrefs.SetInt("level", nextLevel);
            else
                Debug.Log("onceden acilmis bir bolum");

            bolumSonu_P.transform.GetChild(1).gameObject.SetActive(true); // sonraki level butonu aktif   ilerleyip tekrar bitirilmis bolume girmek icin
        }
        bolumSonuPanel(); 
    }

    public string orjinal;
    public void yildizci(int level_ID)
    {
        orjinal = PlayerPrefs.GetString("yildizlar"); //"0,0,0"

        if (toplananItem >int.Parse( orjinal.Substring((level_ID - 1) * 2, 1)))   //3>0
        {
            orjinal = orjinal.Remove((level_ID - 1) * 2, 1);//",0,0,"
            orjinal = orjinal.Insert((level_ID - 1) * 2, toplananItem.ToString()); //3 "3"
        }
        PlayerPrefs.SetString("yildizlar",orjinal); //"3,0,0"
    }

    
    private void Start()
    {
        bolumSonu_P.SetActive(false);
    }

    private void bolumSonuPanel()
    {
        bolumSonu_P.SetActive(true);//Panel Aç
        for (int i = 0; i < toplananItem; i++)///Kazandıgı kadar yildizin alpha değerlerini 255 yap
        {
            bolumSonu_P.transform.GetChild(0).GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255); //yildizlar panelinin ilk cocugunun cocuklari(yildizlar_G->yildiz1/yildiz2/yildiz_3) 
        }
    }

    string levelAdi(int id)//id den level'in ismini dondurur
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(id);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        return sceneName;
    } 



}
