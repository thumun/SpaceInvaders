using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreLogic : MonoBehaviour
{
	public GameObject scoreLst;
	public Button returnBtn; 

    // Start is called before the first frame update
    void Start()
    {
		returnBtn.onClick.AddListener(TitleScreenShift);

		// set scores
		if (PlayerPrefs.HasKey("name_0"))
		{
			bool nullVal = false;
			string[] csv = { "test", "test" };

			for (int i = 0; i < 5; i++)
			{
				if (PlayerPrefs.GetString($"name_{i}") == "")
				{
					nullVal = true;

				}

				else
				{
					csv = PlayerPrefs.GetString($"name_{i}").Split(',');
					if (int.Parse(csv[1]) == -1)
					{
						nullVal = true;
					}
				}

				if (nullVal)
				{
					scoreLst.GetComponent<TextMeshProUGUI>().text += $"{i + 1}.\t None\n";
				}
				else
				{
					scoreLst.GetComponent<TextMeshProUGUI>().text += $"{i + 1}.\t{csv[0]}\t{csv[1]}\n";
				}
			}
		}

	}

    // Update is called once per frame
    void Update()
    {
        
    }
	void TitleScreenShift()
	{
		SceneManager.LoadScene("TitleScreen");
	}
}
