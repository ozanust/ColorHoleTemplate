using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectorController : MonoBehaviour
{
	[SerializeField]
	private float dpiConstant = 0.05f;

	private Touch touch;
	private Camera cam;
	bool isOnPlayer = true;
	private LevelBorder limits;

	public Action<Transform> OnCollectorMoved;

	private void Start()
	{
		cam = Camera.main;
		dpiConstant = 1.4f / Screen.dpi;
	}

	private void Update()
	{
		if (isOnPlayer)
		{
			if (Input.touchCount > 0)
			{
				touch = Input.GetTouch(0);

				if (touch.phase == TouchPhase.Moved)
				{
					float clampedX = Mathf.Clamp(transform.position.x + touch.deltaPosition.x * dpiConstant * (Mathf.Abs(transform.position.z - cam.transform.position.z) / (transform.position - cam.transform.position).magnitude), limits.left, limits.right);
					float clampedZ = Mathf.Clamp(transform.position.z + touch.deltaPosition.y * dpiConstant * (Mathf.Abs(transform.position.z - cam.transform.position.z) / (transform.position - cam.transform.position).magnitude), limits.back, limits.front);

					transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
				}
			}
		}
	}

	private void FixedUpdate()
	{
		if (transform.hasChanged)
		{
			transform.hasChanged = false;
			OnCollectorMoved?.Invoke(transform);
		}
	}

	public void TakeControl()
	{
		isOnPlayer = false;
	}

	public void Release()
	{
		isOnPlayer = true;
	}

	public void SetLimits(LevelBorder levelBorder)
	{
		limits = levelBorder;
	}
}
