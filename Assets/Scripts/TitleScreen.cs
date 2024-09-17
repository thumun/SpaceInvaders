using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class TitleScreen : MonoBehaviour
{
    public GameObject highScore; 
    public Button start;
	public Button score;
    public Button quit; 
    // Start is called before the first frame update
    void Start()
    {

		if (!PlayerPrefs.HasKey("name_0"))
		{
			for (int i = 0; i < 5; i++)
			{
				PlayerPrefs.SetString($"name_{i}", "");
				PlayerPrefs.Save();
			}
		}

		/*
		if (!PlayerPrefs.HasKey("highScore"))
		{
			PlayerPrefs.SetInt($"highScore", 0);
			PlayerPrefs.Save();
		}
		*/

        //highScore.gameObject.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("highScore").ToString();

		start.onClick.AddListener(StartGame);
		score.onClick.AddListener(ScoreScreen);
		quit.onClick.AddListener(QuitGame);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
		SceneManager.LoadScene("GameplayScene");
	}

	void ScoreScreen()
	{
		SceneManager.LoadScene("ScoreScreen");
	}
    void QuitGame()
    {
        Application.Quit();
	}
}
