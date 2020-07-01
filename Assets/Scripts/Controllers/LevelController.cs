using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelController : MonoBehaviour
{
	[SerializeField]
	private Level currentLevel;
	[SerializeField]
	private CollectorController collectorController;
	[SerializeField]
	private CollectableDestroyer collectableDestroyer;
	[SerializeField]
	private Collector collector;

	private LevelPhase levelPhase;

	int neededCollectableCountToPassChapter;
	int collectedCount;

	public Action OnFirstChapterCompleted;
	public Action OnSecondChapterCompleted;
	public Action<LevelPhase, float> OnLevelProgress;

	GameManager gameManager;

	private Coroutine chapterPassAnimationCoroutine = null;

	private void Awake()
	{
		if (gameManager == null)
			gameManager = GameManager.Instance;
	}

	private void Start()
	{
		neededCollectableCountToPassChapter = currentLevel.FirstChapterCollectableCount;

		collectorController.SetLimits(currentLevel.FirstChapterBorder);
		collector.SetCollectorColor(currentLevel.LevelColor.firstChapterSectionColor);

		levelPhase = LevelPhase.FirstChapter;

		collectableDestroyer.OnCollectedCollectable += OnCollectedObject;
	}

	private void OnCollectedObject(bool isCollectable)
	{

#if UNITY_ANDROID && !UNITY_EDITOR
		AndroidVibrator.Vibrate(50);
#endif

		if (isCollectable)
		{
			collectedCount++;
			OnLevelProgress?.Invoke(levelPhase, (float)collectedCount / neededCollectableCountToPassChapter);
		}
		else
		{
			gameManager.OnLevelFailed();
			return;
		}

		if(collectedCount == neededCollectableCountToPassChapter)
		{
			if(levelPhase == LevelPhase.FirstChapter)
			{
				FirstChapterCompleted();
			}
			else
			{
				SecondChapterCompleted();
			}
		}
	}

	private void FirstChapterCompleted()
	{
		if (chapterPassAnimationCoroutine == null)
			chapterPassAnimationCoroutine = StartCoroutine(CMovePlayerToSecondChapter());
	}

	private void SecondChapterCompleted()
	{
		OnSecondChapterCompleted?.Invoke();
		gameManager.OnLevelCompleted();
	}

	private void DeactivateBaits(Collider[] baits)
	{
		foreach(Collider co in baits)
		{
			co.enabled = false;
			co.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	IEnumerator CMovePlayerToSecondChapter()
	{
		float timer = 0;
		Camera camera = Camera.main;

		collectorController.TakeControl();
		currentLevel.FirstChapterGroundCollider.enabled = false;
		currentLevel.TransitionGroundCollider.enabled = true;

		DeactivateBaits(currentLevel.FirstChapterBaits);

		Vector3 snapPosition = new Vector3(currentLevel.MiddleCoordinateOfLevel.x, collectorController.transform.position.y, collectorController.transform.position.z);
		Vector3 horizontalSnapStartPosition = collectorController.transform.position;
		while (Vector3.Distance(collectorController.transform.position, snapPosition) > 0.001f)
		{
			timer += Time.deltaTime * 0.75f;
			collectorController.transform.position = Vector3.Lerp(horizontalSnapStartPosition, snapPosition, timer);
			yield return new WaitForFixedUpdate();
		}

		collectorController.transform.position = snapPosition;
		timer = 0;

		Vector3 chapterTransitionStartPosition = collectorController.transform.position;
		Vector3 cameraStartPosition = camera.transform.position;
		while (Vector3.Distance(collectorController.transform.position, currentLevel.SecondChapterStartPosition) > 0.001f)
		{
			timer += Time.deltaTime * 0.5f;
			collectorController.transform.position = Vector3.Lerp(chapterTransitionStartPosition, currentLevel.SecondChapterStartPosition, timer);
			camera.transform.position = Vector3.Lerp(cameraStartPosition, currentLevel.SecondChapterCameraPosition, timer);
			yield return new WaitForFixedUpdate();
		}

		collectorController.transform.position = currentLevel.SecondChapterStartPosition;
		camera.transform.position = currentLevel.SecondChapterCameraPosition;
		timer = 0;

		currentLevel.TransitionGroundCollider.enabled = false;
		currentLevel.SecondChapterGroundCollider.enabled = true;

		OnFirstChapterCompleted?.Invoke();
		neededCollectableCountToPassChapter = currentLevel.SecondChapterCollectableCount;
		collectorController.SetLimits(currentLevel.SecondChapterBorder);
		collectedCount = 0;
		levelPhase = LevelPhase.SecondChapter;
		collectorController.Release();

		chapterPassAnimationCoroutine = null;
	}
}
