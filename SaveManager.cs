using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public Data data;
    string dataFile = "veri.dat";


    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
            Destroy(this.gameObject);

        
    }

    private void Start()
    {
        Load();
    }
   
    private void Update()
    {

    }

    public void Save() 
    {
        string filePath = Application.persistentDataPath + "/" + dataFile;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(filePath);
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        string filePath = Application.persistentDataPath + "/" + dataFile;
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(filePath))
        {
            FileStream file = File.Open(filePath, FileMode.Open);
            Data loaded = (Data)bf.Deserialize(file);
            data = loaded;
            file.Close();
        }
    }

  
}
[System.Serializable]
public class Data
{
    public int level = 1;
    public float time = 0;

    public Data()
    {
        level = 1;
        time = 0;
    }


}
