using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad
{
    InventoryManager invToSave;

    [System.Serializable]
    public class SaveData
    {
        public InventoryManager SavedInventory;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
			SaveGame();
        }

		if (Input.GetKeyDown(KeyCode.CapsLock)) {
			LoadGame();
        }
    }


    void SaveGame()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath
					 + "/MySaveData.dat");
		SaveData data = new SaveData();
		data.SavedInventory = invToSave;
		bf.Serialize(file, data);
		file.Close();
		Debug.Log("Game data saved!");
	}

	void LoadGame()
	{
		if (File.Exists(Application.persistentDataPath
					   + "/MySaveData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file =
					   File.Open(Application.persistentDataPath
					   + "/MySaveData.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			file.Close();
			invToSave = data.SavedInventory;
			Debug.Log("Game data loaded!");
		}
		else
			Debug.LogError("There is no save data!");
	}
}
