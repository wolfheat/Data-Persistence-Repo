using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StartGameScript : MonoBehaviour
{
    public Button startButton;
    public TMP_InputField nameInputField;
    public TMP_Text bestScoreText;
    public RecordHandlerScript recordHandlerScript;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            recordHandlerScript.ResetRecord();
        }


    }
    public void Start()
	{
        recordHandlerScript = GameObject.FindGameObjectWithTag("RecordHandler").GetComponent<RecordHandlerScript>();
        //Run this when loading screen
        //Set value for record
        LoadInRecords();
        SetRecordText();

	}

    

    private void LoadInRecords()
    {
        recordHandlerScript.LoadRecord();

    }
    private void SetRecordText()
    {
        if (recordHandlerScript.allTimeRecord != 0) {
            bestScoreText.text = (recordHandlerScript.allTimeRecordName + ": " + recordHandlerScript.allTimeRecord);
        }
    }

    public void StartButtonClicked()
    {
        // When button is pressed store Name and start game
        //Debug.Log("Name: " + nameInputField.text + " entered. Starting Game.");
        recordHandlerScript.sessionRecordName = nameInputField.text;
        Debug.Log("Name in record holder is now: " + recordHandlerScript.sessionRecordName);

        SceneManager.LoadScene("main");
    }

}
