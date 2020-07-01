using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoManager<GameManager>
{
	private int levelIndex = 1;

    void Awake()
    {
		DontDestroyOnLoad(this);
    }

	private void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex == 0)
			SceneManager.LoadScene(levelIndex);
	}

	public void OnLevelCompleted()
	{
		if (levelIndex < SceneManager.sceneCountInBuildSettings - 1)
		{
			levelIndex++;
		}
		else
		{
			//Game completed!
			//Returning to first scene to continue testing and observing
			levelIndex = 1;
		}

		SceneManager.LoadScene(levelIndex);
	}

	public void OnLevelFailed()
	{
		SceneManager.LoadScene(levelIndex);
	}
}
