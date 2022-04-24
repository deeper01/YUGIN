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

	
	public float olacak;//paramýzý atacaðýmýz ve zamanla iliþkilendireceðimiz deðiþkenimizi bildirdik.

	void Start()
	{
		GuncelleParalarUIText();
		
	}
	// Oyunumuzun mantýðý gereði zamanla parayý iliþkilendirdiðimiz için float türünde parametre(miktar) alýp paramýzýn durumuna göre kullanabileceðimiz metodlar yazdýk.
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

	public void GuncelleParalarUIText()//Paradaki deðiþiklikleri textlerde güncellememizi saðlar.
	{
		for (int i = 0; i < paralarUIText.Length; i++)
		{
			paralarUIText[i].text = PlayerPrefs.GetFloat("kaydedilencoin").ToString();
			
		}
	}

}