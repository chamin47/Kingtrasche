using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
	public AudioClip audioClip;
	private void OnTriggerEnter2D(Collider2D other)
	{
		Managers.Sound.Play("FG", Define.Sound.Bgm);
	}
}
