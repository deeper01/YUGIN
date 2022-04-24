//Puzzle.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle: MonoBehaviour {
	public int ID;
	//Awake -> scriptin bulunduğu obje aktif olmasa bile çalışır projede ilk calisan fonksiyon
	public void Awake(){
		GetComponent<BoxCollider2D> ().enabled = true;
	}

	
	void ReplaceBlocks(int x, int y, int XX, int YY)
	{
		GameController.grid[x,y].transform.position = GameController.position[XX,YY];
		GameController.grid[XX,YY] = GameController.grid[x,y];
		GameController.grid[x,y] = null;
		GameController.GameFinish();
	}

	//mouse tiklamasi ile kontrol etme
	void OnMouseDown()
	{
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{
				if(GameController.grid[x,y])
				{
					if(GameController.grid[x,y].GetComponent<Puzzle>().ID == ID) //tiklanilan objeyi bul
					{
						if(x > 0 && GameController.grid[x-1,y] == null) //eger gidecegi yer bos ise 
						{
							ReplaceBlocks(x,y,x-1,y); //yeniden konumlandir ( sola hareket ettir)
							return;
						}
						else if(x < 3 && GameController.grid[x+1,y] == null)
						{
							ReplaceBlocks(x,y,x+1,y);  //yeniden konumlandir ( saga hareket ettir)
							return;
						}
					}
				}
				if(GameController.grid[x,y])
				{
					if(GameController.grid[x,y].GetComponent<Puzzle>().ID == ID)
					{
						if(y > 0 && GameController.grid[x,y-1] == null)
						{
							ReplaceBlocks(x,y,x,y-1);  //yeniden konumlandir ( yukari hareket ettir)
							return;
						}
						else if(y < 3 && GameController.grid[x,y+1] == null) 
						{
							ReplaceBlocks(x,y,x,y+1);  //yeniden konumlandir ( asagi  hareket ettir)
							return;
						}
					}
				}
			}
		}
	}
}
