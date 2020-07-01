using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectableDestroyer : MonoBehaviour
{
	public Action<bool> OnCollectedCollectable;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == Constants.COLLECTABLE_TAG)
			OnCollectedCollectable?.Invoke(true);
		else if(other.tag == Constants.BAIT_TAG)
			OnCollectedCollectable?.Invoke(false);

		other.gameObject.SetActive(false);
	}
}
