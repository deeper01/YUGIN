using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [System.Serializable] class MarketUrun //Market ürün diye sýnýfýmýzý oluþturduk.Marketimizin ürünleri ve ürünler için gerekli özellikleri bu sýnýfta topladýk.Eriþim belirleyici olarak public seçildi.
    {
        GameObject resim;
        public Sprite urunResim;
        public int Fiyat;
        public bool SatýnAlýndý = false;//Satýlýp satýlmama durumunu bool olarak ifade ettik.
    }
    [SerializeField] List<MarketUrun> UrunListe;//Ürünlerimizi listede tutmayý tercih ettik.
    [SerializeField] Animator NoCoinsAnim;//Markette paranýn yetersiz olduðu durumlar için oyuncuya text gösteren animatör kullandýk.
    [SerializeField] Text ParaText;

   
    
    GameObject UrunOrnk;//Nesnemizi tanýmladýk.
    GameObject g;//Baþka bir oyun nesnesi tanýmladýk.
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;  // satýn alma ve satma eylemi için iki ayrý buton tanýmladýk.
    Button satBtn;

   
    void Start()
    {
        

       

       
        UrunOrnk = ShopScrollView.GetChild(0).gameObject;//Tanýmladýðýmýz UrunOrnk nesnesini UI deki ScrollView'le iliþkilendirdik.
        int len = UrunListe.Count;//Listede say len e ata

        for(int i = 0;i<len;i++)//len e kadar her adýmda arttýrarak giden for döngüsü listede dolaþmamýzý saðladý.
        {//Parent_Child iliþkilerine dayanarak nesnemizle arayüz ögelerini iliþkilendirdik.
            g = Instantiate(UrunOrnk,ShopScrollView);//scrollview sistemindeki ürünlerimizi bir nesneye atadýk.Arayüzle iliþkilendirebilmek adýna bunu yaptýk.
            g.transform.GetChild(0).GetComponent<Image>().sprite = UrunListe[i].urunResim;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = UrunListe[i].Fiyat.ToString();//integer olan fiyat özelliðimizi textte göstermek için ToString metodunu kullandýk.
            buyBtn = g.transform.GetChild(2).GetComponent<Button>(); 

            satBtn = g.transform.GetChild(3).GetComponent<Button>();

            buyBtn.interactable = !UrunListe[i].SatýnAlýndý;

            satBtn.interactable = UrunListe[i].SatýnAlýndý;

            buyBtn.AddEventListener (i,AlButonTýklandý);//ButtonExtension.cs deki event_button iliþkimizi kullandýk.

            satBtn.AddEventListener (i,SatButonTýklandý);

        }


        
        Destroy(UrunOrnk);
        
        SetParaUI();
        
       

    }
    void AlButonTýklandý(int urunIndex)//ürün indeksi tutan bir parametre alýp butona týklanma durumunda nasýl davranýlacaðýný belirten bir metod kurduk.(Satýn Al butonu için)
    {
        if (Game.Instance.YeterliParaVar(UrunListe[urunIndex].Fiyat))//Game scriptindeki metodumuzu çaðýrdýk ve if-else koþul döngüleriyle UI ögelerini þartlarýmýzý saðlama durumlarýna göre kullandýk.
        {//Yeterli para varsa yapýlacaklar
            Game.Instance.KullanPara(UrunListe[urunIndex].Fiyat);//Game scriptindeki metodumuzu çaðýrarak listelediðimiz 
            //Listedeki ürünün alýnýp alýnmadýðýný kontrolü saðlar.
            UrunListe[urunIndex].SatýnAlýndý = true;//Listedeki ürün satýn alýndýysa true döndür.

            //butonun etkinliðini bool ifadelerle iliþkilendirdik.
            buyBtn = ShopScrollView.GetChild(urunIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false; 
            satBtn = ShopScrollView.GetChild(urunIndex).GetChild(3).GetComponent<Button>();
            satBtn.interactable = true;
            buyBtn.transform.GetChild(0).GetComponent<Text>().text = "ALINDI";//Ürünümüz alýndýðýnda buton da "ALINDI" texti göstermesini saðlar.
            satBtn.transform.GetChild(0).GetComponent<Text>().text = "Saaat";//Ürünümüzün satýlma durumu aktif olduðunda buton da "Saaat" texti göstermesini saðlar.(Sadece alýnan ürünler için geçerlidir.)



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
            NoCoinsAnim.SetTrigger("NoCoins");//Animatör gelir.
            Debug.Log("Yeterli paran yok!!");//Console a Yeterli paran yok yazdýr.
        }
       
    }
    void SatButonTýklandý(int urunIndex)//Sat butonumuz için olan metod
    {
        PlayerPrefs.SetInt("aktiflik2", 0);
        PlayerPrefs.SetInt("aktiflik3", 0);
        Game.Instance.AlPara(UrunListe[urunIndex].Fiyat);
        UrunListe[urunIndex].SatýnAlýndý = true;

        ////butonun etkinliðini bool ifadelerle iliþkilendirdik.
        satBtn = ShopScrollView.GetChild(urunIndex).GetChild(3).GetComponent<Button>();

        buyBtn = ShopScrollView.GetChild(urunIndex).GetChild(2).GetComponent<Button>();
        buyBtn.interactable = true;
        satBtn.interactable= false;
        satBtn.transform.GetChild(0).GetComponent<Text>().text = "Satýldý";
        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Al";

        if(urunIndex == 0)//PlayerPrefs kullanarak ürünlerimizin özelliklerini kayýt altýna aldýk.Böylece farklý aþamalarda çaðýrma imkaný bulduk.
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
       
        ParaText.text = PlayerPrefs.GetFloat("kaydedilencoin").ToString("0.##");//Paramýzý daha önce kullandýðýmýz zamanla iliþkilendirip texte gösterdiðimiz kýsým.
    }

 

    void Update()
    {
        
    }
    
}
