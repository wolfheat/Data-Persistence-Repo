using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class RecordHandlerScript : MonoBehaviour
{

    public string allTimeRecordName;
    public int allTimeRecord;
    public string sessionRecordName;
    public int sessionRecord;


    // Creates a static single instance of this object
    public static RecordHandlerScript Instance;
    // 
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //ResetRecord();

        LoadRecord();
    }

	public void ResetRecord()
	{
		sessionRecord = 0;
        sessionRecordName = "NoName";
        SaveRecord();
        LoadRecord();
	}

	/*
     * INSERTED CODE
     */
	[System.Serializable] // makes items possible to save into json files
    class SaveData
    {
        public string recordHolder;
        public int recordHoldersRecord;
        //public Color TeamColor;
    }

    public void SaveRecord()
    {
        Debug.Log("Before saving data name is: "+sessionRecordName);
        SaveData data = new SaveData();
        data.recordHolder = sessionRecordName;
        data.recordHoldersRecord = sessionRecord;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //Debug.Log("Saving records its holder: " + data.recordHolder + " score: " + data.recordHoldersRecord);


    }
    public void LoadRecord()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("Loading records its holder: "+data.recordHolder+" score: "+data.recordHoldersRecord);
            // Set the display of recordholder
            allTimeRecordName = data.recordHolder;
            allTimeRecord = data.recordHoldersRecord;
        }
    }
}
