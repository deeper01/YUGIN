using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [System.Serializable] class MarketUrun //Market �r�n diye s�n�f�m�z� olu�turduk.Marketimizin �r�nleri ve �r�nler i�in gerekli �zellikleri bu s�n�fta toplad�k.Eri�im belirleyici olarak public se�ildi.
    {
        GameObject resim;
        public Sprite urunResim;
        public int Fiyat;
        public bool Sat�nAl�nd� = false;//Sat�l�p sat�lmama durumunu bool olarak ifade ettik.
    }
    [SerializeField] List<MarketUrun> UrunListe;//�r�nlerimizi listede tutmay� tercih ettik.
    [SerializeField] Animator NoCoinsAnim;//Markette paran�n yetersiz oldu�u durumlar i�in oyuncuya text g�steren animat�r kulland�k.
    [SerializeField] Text ParaText;

   
    
    GameObject UrunOrnk;//Nesnemizi tan�mlad�k.
    GameObject g;//Ba�ka bir oyun nesnesi tan�mlad�k.
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;  // sat�n alma ve satma eylemi i�in iki ayr� buton tan�mlad�k.
    Button satBtn;

   
    void Start()
    {
        

       

       
        UrunOrnk = ShopScrollView.GetChild(0).gameObject;//Tan�mlad���m�z UrunOrnk nesnesini UI deki ScrollView'le ili�kilendirdik.
        int len = UrunListe.Count;//Listede say len e ata

        for(int i = 0;i<len;i++)//len e kadar her ad�mda artt�rarak giden for d�ng�s� listede dola�mam�z� sa�lad�.
        {//Parent_Child ili�kilerine dayanarak nesnemizle aray�z �gelerini ili�kilendirdik.
            g = Instantiate(UrunOrnk,ShopScrollView);//scrollview sistemindeki �r�nlerimizi bir nesneye atad�k.Aray�zle ili�kilendirebilmek ad�na bunu yapt�k.
            g.transform.GetChild(0).GetComponent<Image>().sprite = UrunListe[i].urunResim;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = UrunListe[i].Fiyat.ToString();//integer olan fiyat �zelli�imizi textte g�stermek i�in ToString metodunu kulland�k.
            buyBtn = g.transform.GetChild(2).GetComponent<Button>(); 

            satBtn = g.transform.GetChild(3).GetComponent<Button>();

            buyBtn.interactable = !UrunListe[i].Sat�nAl�nd�;

            satBtn.interactable = UrunListe[i].Sat�nAl�nd�;

            buyBtn.AddEventListener (i,AlButonT�kland�);//ButtonExtension.cs deki event_button ili�kimizi kulland�k.

            satBtn.AddEventListener (i,SatButonT�kland�);

        }


        
        Destroy(UrunOrnk);
        
        SetParaUI();
        
       

    }
    void AlButonT�kland�(int urunIndex)//�r�n indeksi tutan bir parametre al�p butona t�klanma durumunda nas�l davran�laca��n� belirten bir metod kurduk.(Sat�n Al butonu i�in)
    {
        if (Game.Instance.YeterliParaVar(UrunListe[urunIndex].Fiyat))//Game scriptindeki metodumuzu �a��rd�k ve if-else ko�ul d�ng�leriyle UI �gelerini �artlar�m�z� sa�lama durumlar�na g�re kulland�k.
        {//Yeterli para varsa yap�lacaklar
            Game.Instance.KullanPara(UrunListe[urunIndex].Fiyat);//Game scriptindeki metodumuzu �a��rarak listeledi�imiz 
            //Listedeki �r�n�n al�n�p al�nmad���n� kontrol� sa�lar.
            UrunListe[urunIndex].Sat�nAl�nd� = true;//Listedeki �r�n sat�n al�nd�ysa true d�nd�r.

            //butonun etkinli�ini bool ifadelerle ili�kilendirdik.
            buyBtn = ShopScrollView.GetChild(urunIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false; 
            satBtn = ShopScrollView.GetChild(urunIndex).GetChild(3).GetComponent<Button>();
            satBtn.interactable = true;
            buyBtn.transform.GetChild(0).GetComponent<Text>().text = "ALINDI";//�r�n�m�z al�nd���nda buton da "ALINDI" texti g�stermesini sa�lar.
            satBtn.transform.GetChild(0).GetComponent<Text>().text = "Saaat";//�r�n�m�z�n sat�lma durumu aktif oldu�unda buton da "Saaat" texti g�stermesini sa�lar.(Sadece al�nan �r�nler i�in ge�erlidir.)



            PlayerPrefs.SetInt("aktiflik",0); 
            if (UrunListe[urunIndex].Fiyat == 900) //deneme amacli yazilmistir
            {
                Debug.Log("kullan");
                PlayerPrefs.SetInt("aktiflik", 0);
               
                

            }
          

            else if (urunIndex == 0) // resim 1 alindiysa
            {
                Debug.Log("kullannn4");
                PlayerPrefs.SetInt("aktiflik", 1);
                PlayerPrefs.SetInt("alindi", 1);

            }
            else if (urunIndex == 1)   // resim 2 alindiysa
            {
                Debug.Log("kullannn4");
                PlayerPrefs.SetInt("aktiflik", 2);
                PlayerPrefs.SetInt("alindi", 1);

            }


       

            if (urunIndex == 2)  // zaman azaltma -30 alindiysa
            {
                Debug.Log("kullannn");
                PlayerPrefs.SetInt("zami", 1);

            }

            else if (urunIndex == 3)  // zaman azaltma -50 alindiysa
            {
                Debug.Log("kullannn");
                PlayerPrefs.SetInt("zami", 3);

            }
           /* else if (urunIndex == 4)
            {
                Debug.Log("kullannn");
                PlayerPrefs.SetInt("zami", 5);

            } */


            SetParaUI();
        }
        else//(Yeterli para yoksa)
        {
            NoCoinsAnim.SetTrigger("NoCoins");//Animat�r gelir.
            Debug.Log("Yeterli paran yok!!");//Console a Yeterli paran yok yazd�r.
        }
       
    }
    void SatButonT�kland�(int urunIndex)//Sat butonumuz i�in olan metod
    {
        PlayerPrefs.SetInt("aktiflik2", 0);
        PlayerPrefs.SetInt("aktiflik3", 0);
        Game.Instance.AlPara(UrunListe[urunIndex].Fiyat);
        UrunListe[urunIndex].Sat�nAl�nd� = true;

        ////butonun etkinli�ini bool ifadelerle ili�kilendirdik.
        satBtn = ShopScrollView.GetChild(urunIndex).GetChild(3).GetComponent<Button>();

        buyBtn = ShopScrollView.GetChild(urunIndex).GetChild(2).GetComponent<Button>();
        buyBtn.interactable = true;
        satBtn.interactable= false;
        satBtn.transform.GetChild(0).GetComponent<Text>().text = "Sat�ld�";
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Al";

        if(urunIndex == 0)//PlayerPrefs kullanarak �r�nlerimizin �zelliklerini kay�t alt�na ald�k.B�ylece farkl� a�amalarda �a��rma imkan� bulduk.
        {
            Debug.Log("kullannn12"); 
            PlayerPrefs.SetInt("aktiflik2", 9); // resim 1 satildiysa
            PlayerPrefs.SetInt("alindi", 1);
        }
        else if (urunIndex == 1) // resim 2 satildiysa
        {
            Debug.Log("kullannn10");
            PlayerPrefs.SetInt("aktiflik3", 10);
            PlayerPrefs.SetInt("alindi", 1);
        }

        if (urunIndex == 2)  // zaman azaltma -30 alindiysa
        {
            Debug.Log("kullannnzaman");
            PlayerPrefs.SetInt("zami", 2);
        }

        else if (urunIndex == 3) // zaman azaltma -50 alindiysa
        {
            Debug.Log("kullannnzaman1");
            PlayerPrefs.SetInt("zami", 4);

        }

        SetParaUI();
        
    }


    void SetParaUI()
    {
       
        ParaText.text = PlayerPrefs.GetFloat("kaydedilencoin").ToString("0.##");//Param�z� daha �nce kulland���m�z zamanla ili�kilendirip texte g�sterdi�imiz k�s�m.
    }

 

    void Update()
    {
        
    }
    
}
