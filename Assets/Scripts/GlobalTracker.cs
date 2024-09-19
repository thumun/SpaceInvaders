using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HighScoreOut
{
    public bool Output { get; set; }
    public List<Score> TopScores { get; set; }

    public HighScoreOut(bool output, List<Score> topScores)
    {
        Output = output;
		TopScores = topScores;
    }
}

public class Score
{
	public int ScoreNumber { get; set; }
	public string Name { get; set; }

	public Score(int scoreNumber, string name)
	{
		ScoreNumber = scoreNumber;
		Name = name;
	}

	public Score(string csv)
	{
		var items = csv.Split(',');
		Name = items[0];
		ScoreNumber = Int32.Parse(items[1]);

	}

	public string stringify()
	{
		return $"{Name},{ScoreNumber}";
	}
}

public class GlobalTracker : MonoBehaviour
{
    public int highScore; 
    public int currScore;

    public GameObject highScoreTxt;
	public GameObject currScoreTxt;

	public float ufoSpawnPeriod;
	public float ufotimer;

	public int lives = 0;
	public GameObject lifeCount;

	public GameObject GameOverScreen; 
	public GameObject HighScoreObj; 
	public GameObject ScoreObj;
	public GameObject Loss;
	public GameObject Win;
	public GameObject inputName;
	public Button returnBtn;

	public GameObject ufoPrefab;
	public bool ufoActive;

	private HighScoreOut scoreInfo = new HighScoreOut(false, null);
	private string newName;

	// Start is called before the first frame update
	void Start()
    {
		//var fieldEvent = new InputField.SubmitEvent();
		//fieldEvent.AddListener();
		string scoreDetail = PlayerPrefs.GetString("name_0");
		if (scoreDetail == ",-1")
		{
			highScore = 0;
		}
		else
		{
			var scoreItems = scoreDetail.Split(',');
			highScore = int.Parse(scoreItems[1]);
		}

		highScoreTxt.GetComponent<TextMeshProUGUI>().text = highScore.ToString();

        currScore = 0;
		ufotimer = 0;
		ufoSpawnPeriod = 5.0f; // change to higher number
		ufoActive = false; 

		foreach (Transform child in lifeCount.transform)
		{
			lives++;
		}

		//lives = 1;

		GameOverScreen.SetActive(false);

		returnBtn.onClick.AddListener(ReturnTitle);
	}

    // Update is called once per frame
    void Update()
    {
		if (!ufoActive)
		{
			ufotimer += Time.deltaTime;
		}

		currScoreTxt.GetComponent<TextMeshProUGUI>().text = currScore.ToString();

		if (ufotimer > ufoSpawnPeriod && !ufoActive)
		{
			ufotimer = 0;

			Instantiate(ufoPrefab, new Vector3(-30.8999996f, 12.5100012f, -5.5f), Quaternion.identity);
			ufoActive = true;
		}
	}

	public void shipLifeController()
	{
		bool lifeUsed = false;
		if (lives > 0)
		{
			Transform child = lifeCount.transform.GetChild(lives - 1);
			child.gameObject.SetActive(false);
			lives--;

			lifeUsed = true;

			if (lives <= 0)
			{
				lifeUsed = false;
			}
		}

		if (!lifeUsed)
		{
			GameOver(false);
		}
	}

	private HighScoreOut checkHighScore(int score)
	{
		bool returnVal = false;
		List<Score> topScores = new List<Score>();
		for (int i = 0; i < 5; i++)
		{
			string csv = PlayerPrefs.GetString($"name_{i}");

			if (csv != "")
			{
				topScores.Add(new Score(csv));
			}
			else
			{
				topScores.Add(new Score(-1, ""));
			}
		}

		topScores = topScores.OrderBy(x => x.ScoreNumber).ToList();

		if (score > topScores[0].ScoreNumber)
		{
			returnVal = true;
		}

		return new HighScoreOut(returnVal, topScores);
	}

	public void GameOver(bool aliensDefeated)
	{
		scoreInfo = checkHighScore(currScore);

		if (aliensDefeated)
		{
			Win.SetActive(true);
			Loss.SetActive(false);
		}
		else
		{
			Loss.SetActive(true);
			Win.SetActive(false);
		}

		if (scoreInfo.Output)
		{
			// highsscore screen 
			HighScoreObj.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = currScore.ToString();
			HighScoreObj.gameObject.SetActive(true);
			ScoreObj.gameObject.SetActive(false);
		}
		else
		{
			ScoreObj.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = currScore.ToString();
			ScoreObj.gameObject.SetActive(true);
			HighScoreObj.gameObject.SetActive(false);
		}

		GameOverScreen.gameObject.SetActive(true);
	}

	public void ReturnTitle()
	{
		newName = inputName.GetComponent<TMP_InputField>().text;
		SceneManager.LoadScene("TitleScreen");
	}

	private void OnDisable()
	{
		if (scoreInfo.Output)
		{
			//string newName = inputName.GetComponent<TMP_InputField>().text;
			scoreInfo.TopScores.Add(new Score(currScore, newName));

			scoreInfo.TopScores = scoreInfo.TopScores.OrderByDescending(x => x.ScoreNumber).ToList();

			for (int i = 0; i < 5; i++)
			{
				PlayerPrefs.SetString($"name_{i}", scoreInfo.TopScores[i].stringify());
			}
		}

		PlayerPrefs.Save();

	}
}
