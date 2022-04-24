using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	#region SIngleton:Game

	public static Game Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	#endregion

	[SerializeField] Text[] paralarUIText;

	
	public float olacak;//param�z� ataca��m�z ve zamanla ili�kilendirece�imiz de�i�kenimizi bildirdik.

	void Start()
	{
		GuncelleParalarUIText();
		
	}
	// Oyunumuzun mant��� gere�i zamanla paray� ili�kilendirdi�imiz i�in float t�r�nde parametre(miktar) al�p param�z�n durumuna g�re kullanabilece�imiz metodlar yazd�k.
	public void KullanPara(float miktar)
	{
		PlayerPrefs.DeleteKey("gardasa");
		olacak = PlayerPrefs.GetFloat("kaydedilencoin");
		olacak -=miktar;
	
		PlayerPrefs.SetFloat("kaydedilencoin", olacak);
	}

	public void AlPara(float alinan)
	{
		PlayerPrefs.DeleteKey("gardasa");
		olacak = PlayerPrefs.GetFloat("kaydedilencoin");
		olacak += alinan;

		PlayerPrefs.SetFloat("kaydedilencoin", olacak);
	}

	public bool YeterliParaVar(float miktar)
	{
		return (PlayerPrefs.GetFloat("kaydedilencoin") >= miktar);
	}

	public void GuncelleParalarUIText()//Paradaki de�i�iklikleri textlerde g�ncellememizi sa�lar.
	{
		for (int i = 0; i < paralarUIText.Length; i++)
		{
			paralarUIText[i].text = PlayerPrefs.GetFloat("kaydedilencoin").ToString();
			
		}
	}

}