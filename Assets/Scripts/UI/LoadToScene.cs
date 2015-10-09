using UnityEngine;
using System.Collections;

public class LoadToScene : MonoBehaviour 
{
	[SerializeField] private WhiteFlash flash;

	public void OnClickLoadScene(string s)
	{
		StartCoroutine(LoadSceneAfterFlash(s));
	}

	private IEnumerator LoadSceneAfterFlash(string s)
	{
		flash.RequestFlash();

		yield return new WaitForSeconds(3);

		Application.LoadLevel(s);

	}
}
