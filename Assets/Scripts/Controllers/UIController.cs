using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	[SerializeField]
	private Image firstChapterFill;
	[SerializeField]
	private Image secondChapterFill;
	[SerializeField]
	private LevelController levelController;

	GameManager gameManager;
	
	void Start()
    {
		if (gameManager == null)
			gameManager = GameManager.Instance;

		levelController.OnLevelProgress += OnLevelProgress;
    }

    private void OnLevelProgress(LevelPhase levelPhase, float levelProgress)
	{
		switch (levelPhase)
		{
			case LevelPhase.FirstChapter:
				firstChapterFill.fillAmount = levelProgress;
				return;
			case LevelPhase.SecondChapter:
				secondChapterFill.fillAmount = levelProgress;
				return;
		}
	}
}
