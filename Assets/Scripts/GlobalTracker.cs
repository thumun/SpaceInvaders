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

	public int lives = -1;
	public GameObject lifeCount;

	public GameObject GameOverScreen; 
	public GameObject HighScoreObj; 
	public GameObject ScoreObj;
	public GameObject Loss;
	public GameObject Win;
	public GameObject inputName;
	public Button returnBtn; 

	private HighScoreOut scoreInfo = new HighScoreOut(false, null);	

	// Start is called before the first frame update
	void Start()
    {
        var scoreDetail = PlayerPrefs.GetString("name_0");
		if (scoreDetail == "")
		{
			highScore = 0;
		}
		else
		{
			var scoreItems = scoreDetail.Split(",");
			highScore = int.Parse(scoreItems[0]);
		}

		highScoreTxt.GetComponent<TextMeshProUGUI>().text = highScore.ToString();

        currScore = 0;
		ufotimer = 0;
		ufoSpawnPeriod = 10.0f; // change to higher number

		/*
		foreach (Transform child in lifeCount.transform)
		{
			lives++;
		}
		*/

		lives = 1;

		GameOverScreen.SetActive(false);

		returnBtn.onClick.AddListener();
	}

    // Update is called once per frame
    void Update()
    {
		currScoreTxt.GetComponent<TextMeshProUGUI>().text = currScore.ToString();

		if (ufotimer > ufoSpawnPeriod)
		{

			ufotimer = 0;

			//float width = Screen.width;
			//float height = Screen.height;

			//float horizontalPos = UnityEngine.Random.Range(0.0f, width);
			//float verticalPos = UnityEngine.Random.Range(0.0f, height);

			//horizontalPos = UnityEngine.Random.Range(0.0f, width);
			//verticalPos = UnityEngine.Random.Range(0.0f, height);
			//verticalPos = ship.transform.position.y;

			//Instantiate(ufo, new Vector3(UnityEngine.Random.Range(-9.0f, 10.0f), 0, UnityEngine.Random.Range(-14.0f, 14.0f)), Quaternion.identity);

			// add UFO at the top of the screen 

			// instantiate to the left of the screen 
			// make it go across the screen? 
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
		HighScoreOut scoreInfo = checkHighScore(currScore);

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

	private void OnDisable()
	{
		if (scoreInfo.Output)
		{
			string newName = inputName.GetComponent<TMP_InputField>().text;
			scoreInfo.TopScores.Add(new Score(currScore, newName));

			scoreInfo.TopScores = scoreInfo.TopScores.OrderBy(x => x.ScoreNumber).ToList();

			for (int i = 0; i < 5; i++)
			{
				PlayerPrefs.SetString($"name_{i}", scoreInfo.TopScores[i].stringify());
			}
			PlayerPrefs.Save();
		}
		
	}
}
