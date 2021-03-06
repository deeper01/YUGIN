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

	
	public float olacak;//paramızı atacağımız ve zamanla ilişkilendireceğimiz değişkenimizi bildirdik.

	void Start()
	{
		GuncelleParalarUIText();
		
	}
	// Oyunumuzun mantığı gereği zamanla parayı ilişkilendirdiğimiz için float türünde parametre(miktar) alıp paramızın durumuna göre kullanabileceğimiz metodlar yazdık.
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

	public void GuncelleParalarUIText()//Paradaki değişiklikleri textlerde güncellememizi sağlar.
	{
		for (int i = 0; i < paralarUIText.Length; i++)
		{
			paralarUIText[i].text = PlayerPrefs.GetFloat("kaydedilencoin").ToString();
			
		}
	}

}