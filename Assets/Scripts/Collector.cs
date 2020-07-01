using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{ 
	[SerializeField]
	private float vortexEffectAmount = 5f;
	[SerializeField]
	private Collider collider;
	[SerializeField]
	private MeshRenderer collectorRenderer;

	private void OnTriggerEnter(Collider other)
	{
		Rigidbody otherRB;
		other.TryGetComponent(out otherRB);

		if (otherRB != null)
		{
			Vector3 direction = (transform.position - other.gameObject.transform.position).normalized;

			if (otherRB.isKinematic)
				otherRB.isKinematic = false;

			otherRB.AddForce(direction * vortexEffectAmount, ForceMode.Impulse);
		}
	}

	public void EnableCollider(bool isOn)
	{
		collider.enabled = isOn;
	}

	public void SetCollectorColor(Color color)
	{
		collectorRenderer.material.SetColor(Constants.HOLE_BORDER_COLOR_REFERENCE, color);
	}
}
