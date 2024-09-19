using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	// referenced this: https://www.youtube.com/watch?v=9A9yj8KnM8c

	public IEnumerator Shake(float duration, float magnitude)
	{
		Vector3 originalPos = transform.localPosition;
		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			float x = Random.Range(-0.5f, 0.5f) * magnitude;
			float z = Random.Range(-0.2f, 0.2f) * magnitude;

			transform.localPosition = new Vector3(originalPos.x, originalPos.y, originalPos.z + z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = originalPos;
	}

}
