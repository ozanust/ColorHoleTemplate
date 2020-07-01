using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
	[SerializeField]
	private LevelColor levelColor;
	[SerializeField]
	private LevelBorder firstChapterBorder;
	[SerializeField]
	private LevelBorder secondChapterBorder;
	[SerializeField]
	private MeshRenderer firstChapterGroundRenderer;
	[SerializeField]
	private MeshRenderer secondChapterGroundRenderer;
	[SerializeField]
	private MeshRenderer transitionGroundRenderer;
	[SerializeField]
	private MeshCollider firstChapterGroundCollider;
	[SerializeField]
	private MeshCollider transitionGroundCollider;
	[SerializeField]
	private MeshCollider secondChapterGroundCollider;
	[SerializeField]
	private GameObject[] firstChapterCollectables;
	[SerializeField]
	private Collider[] firstChapterBaitColliders;
	[SerializeField]
	private GameObject[] secondChapterCollectables;
	[SerializeField]
	private Transform middleOfLevelTransform;
	[SerializeField]
	private Transform secondChapterStartTransform;
	[SerializeField]
	private Transform secondChapterCameraTransform;

	public LevelColor LevelColor => levelColor;

	public LevelBorder FirstChapterBorder => firstChapterBorder;
	public LevelBorder SecondChapterBorder => secondChapterBorder;

	public int FirstChapterCollectableCount => firstChapterCollectables.Length;
	public int SecondChapterCollectableCount => secondChapterCollectables.Length;

	public Collider[] FirstChapterBaits => firstChapterBaitColliders;

	public Vector3 MiddleCoordinateOfLevel => middleOfLevelTransform.position;
	public Vector3 SecondChapterStartPosition => secondChapterStartTransform.position;
	public Vector3 SecondChapterCameraPosition => secondChapterCameraTransform.position;

	public MeshCollider FirstChapterGroundCollider => firstChapterGroundCollider;
	public MeshCollider TransitionGroundCollider => transitionGroundCollider;
	public MeshCollider SecondChapterGroundCollider => secondChapterGroundCollider;

	private void Start()
	{
		firstChapterGroundRenderer.material.color = levelColor.firstChapterColor;
		secondChapterGroundRenderer.material.color = levelColor.firstChapterColor;
		transitionGroundRenderer.material.color = levelColor.firstChapterColor;
	}
}
