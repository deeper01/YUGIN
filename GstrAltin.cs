using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GstrAltin : MonoBehaviour
{

    public Text para; // genel coin miktari
    public Text paracýk; // bulunulan level bitiminde kazanilan coin miktari
    public void Start()
    {

      
    }

    public void Update()
    {

        para.text = PlayerPrefs.GetFloat("kaydedilencoin").ToString("0.##"); // genel coin miktarini (## virgulden sonra 2 basamak alarak) yazdirdik
        paracýk.text = PlayerPrefs.GetFloat("yeniiii").ToString("0.##"); // level sonu coin miktarini yazdirdik
        PlayerPrefs.DeleteKey("yeniiii"); //her level sonunda kazanilan coin  sifirlandi ki tekrar eklenmesin
    }

}

