using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainManager : MonoBehaviour
{
    public RecordHandlerScript recordHandlerScript;


    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

	public Text ScoreText;
	public Text ScoreTextBest;
    public GameObject GameOverText;

    public string m_Name;
    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false;

    public void BackToMenu()
    {
        SceneManager.LoadScene("Start Menu Scene");

    }

    public void Awake()
    {
        loadData();
        


        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

	private void loadData()
	{
        // Get hold of the recordHandler
        recordHandlerScript = GameObject.FindGameObjectWithTag("RecordHandler").GetComponent<RecordHandlerScript>();
        
        //Set Name
        m_Name = recordHandlerScript.sessionRecordName;

        //Set Text
        ScoreText.text = (m_Name +" : "+ m_Points);
        ScoreTextBest.text = ("Best Score: "+recordHandlerScript.allTimeRecordName +" : "+ recordHandlerScript.allTimeRecord);


    }

	

	private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                BackToMenu();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = (m_Name+": "+ m_Points);
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        recordHandlerScript.sessionRecord = m_Points;
        recordHandlerScript.sessionRecordName = m_Name;//? needed?

        //Check if players score is higher than record
        if (m_Points > recordHandlerScript.allTimeRecord)
		{
            // Save the data for new recordholder to file
            recordHandlerScript.SaveRecord();
            BackToMenu();
            
        }

    }
    
}
