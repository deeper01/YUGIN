using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{


	public Text timerText;
	private float startTime;
	

	private static GameObject go;
	private static GameObject gos;

	private static GameObject txtgo;
	private static GameObject txtgos;




	public GameObject[] cells;
	private GameObject[] random_cells;
	private GameObject[] r_cells;
	private int[,] r_grid;
	public static GameObject[,] grid;
	public static Vector3[,] position;
	public int[] checkmas;
	private static bool win = false;

	public float startPosX;
	public float startPosY;
	public float offsetX;
	public float offsetY;

	private int sum = 0;
	private int zero = 0;
	private int h;
	private int v;

	private static GameObject txt;
	private static GameObject panel;
	
	private Component[] boxes;
	private Component[] p_script;

	private static GameObject star0;
	private static GameObject star1;
	private static GameObject star2;
	private static GameObject star3;
	public static int finishend = 0;




	void Start()
	{

		PlayerPrefs.SetFloat("time", 0);
		finishend = 0;
		
		Time.timeScale = 1;
		startTime = Time.time;

		
		r_grid = new int[4, 4];
		txt = GameObject.FindGameObjectWithTag("congratulations");


		/*go = GameObject.FindGameObjectWithTag("ggo");
		go.SetActive(false);
		gos = GameObject.FindGameObjectWithTag("ggos");
		gos.SetActive(false);
		txtgo = GameObject.FindGameObjectWithTag("txtggo");
		txtgo.SetActive(false);
		txtgos = GameObject.FindGameObjectWithTag("txtggos");
		txtgos.SetActive(false);


		star0  = GameObject.FindGameObjectWithTag("yildiz");
		star0.SetActive(false);

		star1 = GameObject.FindGameObjectWithTag("yildiz1");
		star1.SetActive(false);

		star2 = GameObject.FindGameObjectWithTag("yildiz2");
		star2.SetActive(false);


		star3 = GameObject.FindGameObjectWithTag("yildiz3");
		star3.SetActive(false); */


		//panel = GameObject.FindGameObjectWithTag("panelim");



		boxes = GetComponentsInChildren<BoxCollider2D>();
		p_script = GetComponentsInChildren<Puzzle>();
		checkmas = new int[16];
		random_cells = new GameObject[cells.Length];
		r_cells = new GameObject[cells.Length];
		float posXreset = startPosX;
		position = new Vector3[4, 4];
		for (int y = 0; y < 4; y++)
		{
			startPosY -= offsetY;
			for (int x = 0; x < 4; x++)
			{
				startPosX += offsetX;
				position[x, y] = new Vector3(startPosX, startPosY, 0);
			}
			startPosX = posXreset;

		}
		RandomPuzzle(true);
		
	}

	void Update()
	{

		//zamanı bul ve text icine yazdir
		float t = Time.time - startTime;

		string minutes = ((int)t / 60).ToString();
		string seconds = (t % 60).ToString("f2");

		timerText.text = minutes + ":" + seconds;



		//eger bir kere bittiyse ve marketten  zaman azaltma alindiysa islemi yap zaman playerprefs'ini sifirla
		//eger zaman alinmadiysa normal zamani kullan
		//eger finishend birden farkliysa finishend'i ve bitis'i sifir yap
		if (finishend == 1)
        {

            if (PlayerPrefs.GetInt("zami") == 1)
            {
				t = t - 30;
				PlayerPrefs.SetInt("zami",0);

			}
			PlayerPrefs.SetFloat("zaman", t);
			PlayerPrefs.SetFloat("bitis", 1);

			
			
		}
        else
        {

			finishend = 0;
			PlayerPrefs.SetFloat("bitis", 0);
		}

	}


   
	
	public void StartNewGame()
	{
		
		win = false;    //kazanmayi false yap
		txt.GetComponent<Text>().color = new Color(0, 0, 0, 0); 
		panel.GetComponent<Image>().color = new Color(0, 0, 0, 0);   //panel ve txt'in rengini beyaz ve görünmez yap

		RandomPuzzle(true); //puzzle'ı karistir
	}


	//oyun ekranindan cik
	public void ExitGame()
	{
		Application.Quit();
	}

	public void Possibility(int[] mas)
	{
		//puzzle'in cozumu var mi kontrol et eger yoksa tekrar karistir
		for (int i = 0; i < 16; i++)
		{
			if (mas[i] == 0)
			{
				zero = i / 4 + 1;
			}
			else
				for (int k = i; k < 16; k++)
				{
					if (mas[i] > mas[k] && mas[k] != 0)
					{
						sum++;
					}
				}
		}


		if ((zero + sum) % 2 == 0)
		{
			Debug.Log("cozum bu");
		}
		else
		{
			Debug.Log("Sonuc yok ... yeniden karistiriliyor... ");
			CreatePuzzle();
		}
	}


	//oyunu tekrar baslat
	public void RestartGame()
	{
		if (transform.childCount > 0)
		{
			//var olan butun nesneleri sil
			for (int j = 0; j < transform.childCount; j++)
			{
				Destroy(transform.GetChild(j).gameObject);
			}
		}
		grid = new GameObject[4, 4];
		GameObject clone = new GameObject();
		//her bir [x,y] 'yi gezerek yeniden olusturuyor
		int i = 0;
		for (int y = 0; y < 4; y++)
		{
			for (int x = 0; x < 4; x++)
			{
				int j = checkmas[i];
				if (j >= 0)
				{
					// Instantiate ile yeniden obje olustur  (Instantiate-> 3 method alır. 1)uretilecek obje  2)nerede uretilecegi 3)objenin dogrultusu)
					grid[x, y] = Instantiate(cells[j], position[x, y], Quaternion.identity) as GameObject;
					//objenin adi ID'si ve kacinci dongudeyse onun toplamidir
					grid[x, y].name = "ID-" + i;
					//transform'u parent'in icine atar
					grid[x, y].transform.parent = transform;
				}
				i++;
			}
		}
		//Destroy(clone);

	}


	void CreatePuzzle()
	{
		

		if (transform.childCount > 0)
		{
			//eski nesneleri siler
			for (int j = 0; j < transform.childCount; j++)
			{
				Destroy(transform.GetChild(j).gameObject);
			}
		}
		int i = 0;
		int ii = 0;
		grid = new GameObject[4, 4];
		h = Random.Range(0, 3);
		v = Random.Range(0, 3);

		GameObject clone = new GameObject();
		grid[h, v] = clone; // rastgele [x,y] hücrelerinden birine bir obje koyar
		float posXreset = startPosX;

		for (int y = 0; y < 4; y++)
		{

			for (int x = 0; x < 4; x++)
			{

				if (grid[x, y] == null)
				{
					startPosX += offsetX;
					
					grid[x, y] = Instantiate(random_cells[i], position[x, y], Quaternion.identity) as GameObject; //Quaternion.identity->rotasyon yok
					grid[x, y].name = "ID-" + i;
				
					checkmas[ii] = grid[x, y].GetComponent<Puzzle>().ID;    //Puzzle script'inden ID alır 
					grid[x, y].transform.parent = transform;
					i++;
					ii++;
				}
				else
				{
					checkmas[ii] = 0;
					ii++;
				}
			}
		}
		
		foreach (BoxCollider2D box2d in boxes)   //dongu ile eger box2d boxes icindeyse box2d'yi kullanilabilirligini true yap  foreach(degiskenTuru degiskenAdi in dizi)
			box2d.enabled = true;
		
		foreach (Puzzle puz in p_script)  //dongu ile eger puz p_script icindeyse puz'ın kullanilabilirligini true yap
			puz.enabled = true;

		//kontrol amacli ekrana yazdir
		for (i = 0; i < 16; i++)
		{
			print("checkmas   " + checkmas[i]);
		}


		Destroy(clone);
		for (int q = 0; q < cells.Length; q++)
		{
			Destroy(random_cells[q]);
		}
		Possibility(checkmas); //olabiletesini kontrol et 
	}

	void RandomPuzzle(bool r_s)
	{
		if (r_s == true)
		{
			int[] tmp = new int[cells.Length]; //tmp adinda yedek bir dizi olustur, olusturdugumuz cells dizisinin uzunlugunda olsun
			for (int i = 0; i < cells.Length; i++) //tmp'nin her ogesine 1 ata
			{
				tmp[i] = 1;
			}
			int c = 0;
			while (c < cells.Length)   //c cells'in uzunlugundan kucuk oldugu surece 
			{
				int r = Random.Range(0, cells.Length);  //r'ye random deger ata
				if (tmp[r] == 1)
				{
					random_cells[c] = Instantiate(cells[r], new Vector3(0, 10, 0), Quaternion.identity) as GameObject; //random_cells'in c'ninci ogesine objeyi olusturup ata
					r_cells[c] = random_cells[c]; //olusturulan objeyi r_cells'in c'inci ogesine esitle
					tmp[r] = 0; //birdaha kullanilmasin diye sifirla
					c++;
				}
			}
			CreatePuzzle(); 
		}
		else
		{
			CreatePuzzle();
		}
	}
	//oyun bitis kontrol 
	static public void GameFinish()
	{
		int i = 1;
		for (int y = 0; y < 4; y++) //iki dongu ile butun diziyi gez ve ID ile kontrol et eger eslesiyorsa donguyu bir arttır eslesmiyorsa bir azalt
		{
			for (int x = 0; x < 4; x++)
			{
				if (grid[x, y]) { if (grid[x, y].GetComponent<Puzzle>().ID == i) i++; } else i--;
			}
		}
		if (i == 15) //hepsi eslesiyorsa
		{
			for (int y = 0; y < 4; y++)
			{
				for (int x = 0; x < 4; x++)
				{
					if (grid[x, y]) Destroy(grid[x, y].GetComponent<Puzzle>());
				}
			}
			win = true; //kazandiniz!

			//panel.GetComponent<Image>().color = new Color(255, 255, 255, 255);

			/*
			 * gos.SetActive(true);
			go.SetActive(true);
			txtgos.SetActive(true);
			txtgo.SetActive(true);
			*/
			Time.timeScale = 0f; //zamani durdur
			Debug.Log("bitti!");

			finishend = 1;
			PlayerPrefs.SetFloat("time",1);
			
		}
	}


	
	

}
