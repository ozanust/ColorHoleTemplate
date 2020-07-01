using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDeformator : MonoBehaviour
{
	[SerializeField]
	private CollectorController collectorController;
	[SerializeField]
	private PolygonCollider2D groundCollider;
	[SerializeField]
	private PolygonCollider2D holeCollider;
	[SerializeField]
	private MeshCollider generatedMeshCollider;
	[SerializeField]
	private float initialScale = 5f;
	[SerializeField]
	private float collectorStartPositionZ = 0;

	private Mesh generatedMesh;
	private float staticDifference = 0;

	private void Start()
	{
		CalculateStartPositionDifference();
		collectorController.OnCollectorMoved += OnCollectorMoved;
	}

	private void OnCollectorMoved(Transform collectorTransform)
	{
		generatedMeshCollider.transform.localScale = new Vector3(1f / transform.localScale.x, 1f / transform.localScale.y, 1f / transform.localScale.z);
		holeCollider.transform.position = new Vector2(collectorTransform.position.x / transform.localScale.x, collectorTransform.position.z / transform.localScale.z + staticDifference);
		holeCollider.transform.localScale = new Vector3(
			collectorTransform.localScale.x * generatedMeshCollider.transform.localScale.x, 
			collectorTransform.localScale.z * generatedMeshCollider.transform.localScale.z, 
			collectorTransform.localScale.z * generatedMeshCollider.transform.localScale.z) * initialScale;

		CreateHole();
		GenerateMeshCollider();
	}

	private void CreateHole()
	{
		Vector2[] pointPositions = holeCollider.GetPath(0);

		for(int i = 0; i < pointPositions.Length; i++)
		{
			pointPositions[i] = holeCollider.transform.TransformPoint(pointPositions[i]);
		}

		groundCollider.pathCount = 2;
		groundCollider.SetPath(1, pointPositions);
	}

	private void GenerateMeshCollider()
	{
		if (generatedMesh != null)
			Destroy(generatedMesh);

		generatedMesh = groundCollider.CreateMesh(true, true);
		generatedMeshCollider.sharedMesh = generatedMesh;
	}

	public void CalculateStartPositionDifference()
	{
		staticDifference = collectorController.transform.position.z - collectorStartPositionZ;
	}
}
