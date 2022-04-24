using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public class zaman_goster : MonoBehaviour
{
    public Text yazi;
	private static GameObject star0;
	private static GameObject star1;
	private static GameObject star2;
	private static GameObject star3;

	void Start()
    {
        yazi.text=(PlayerPrefs.GetFloat("zaman")).ToString();


		star0 = GameObject.FindGameObjectWithTag("yildiz");
		star0.SetActive(false);

		star1 = GameObject.FindGameObjectWithTag("yildiz1");
		star1.SetActive(false);

		star2 = GameObject.FindGameObjectWithTag("yildiz2");
		star2.SetActive(false);


		star3 = GameObject.FindGameObjectWithTag("yildiz3");
		star3.SetActive(false);

		if (PlayerPrefs.GetFloat("bitis")==1)
        {
			if (200 > (PlayerPrefs.GetFloat("zaman")) && (PlayerPrefs.GetFloat("zaman")) > 100)
			{
				//gamma
				star0.SetActive(true);
				star1.SetActive(false);
				star2.SetActive(false);
				star3.SetActive(false);

			}
			else if ((PlayerPrefs.GetFloat("zaman")) < 100 && (PlayerPrefs.GetFloat("zaman")) > 51)
			{
				//beta
				star0.SetActive(true);
				star1.SetActive(true);
				star2.SetActive(false);
				star3.SetActive(false);
			}
			else if ((PlayerPrefs.GetFloat("zaman")) < 51 && (PlayerPrefs.GetFloat("zaman")) >= 1)
			{


				//alpha
				star0.SetActive(true);
				star1.SetActive(true);
				star2.SetActive(true);
				star3.SetActive(false);

			}
			else if ((PlayerPrefs.GetFloat("zaman")) > 200)
			{
				//delta
				star0.SetActive(false);
				star1.SetActive(false);
				star2.SetActive(false);
				star3.SetActive(true);
			}
			
		}


		

		else
        {
			if (PlayerPrefs.GetInt("w") == 0)
			{
				star0.SetActive(false);
				star1.SetActive(false);
				star2.SetActive(false);
				star3.SetActive(true);
			}

			else if (PlayerPrefs.GetInt("w") == 1)
			{
				star0.SetActive(true);
				star1.SetActive(false);
				star2.SetActive(false);
				star3.SetActive(false);
			}

			else if (PlayerPrefs.GetInt("w") == 2)
			{
				star0.SetActive(true);
				star1.SetActive(true);
				star2.SetActive(false);
				star3.SetActive(false);
			}

			else if (PlayerPrefs.GetInt("w") == 3)
			{
				star0.SetActive(true);
				star1.SetActive(true);
				star2.SetActive(true);
				star3.SetActive(false);
			}


		}
		PlayerPrefs.SetFloat("bitis", 0);

	}
}
