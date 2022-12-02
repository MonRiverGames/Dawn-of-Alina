using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    public static SaveManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion // SaveManager instance

    public List<string> SaveFiles = new List<string>();

    public void AddSaveFile()
    {

    }

    public void RemoveSaveFile()
    {

    }

}
