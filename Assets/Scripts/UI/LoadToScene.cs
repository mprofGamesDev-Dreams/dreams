using UnityEngine;
using System.Collections;

public class LoadToScene : MonoBehaviour 
{
	[SerializeField] private WhiteFlash flash;

	public void OnClickLoadSceneByString(string s)
	{
		StartCoroutine(LoadSceneAfterFlashUsingString(s));
	}

	public void OnClickLoadSceneBySceneID(int i)
	{
		StartCoroutine(LoadSceneAfterFlashUsingInt(i));
	}

	private IEnumerator LoadSceneAfterFlashUsingString(string s)
	{
		flash.RequestFlash();
		
		yield return new WaitForSeconds(2);
		
		Application.LoadLevel(s);
	}

	private IEnumerator LoadSceneAfterFlashUsingInt(int i)
	{
		flash.RequestFlash();
		
		yield return new WaitForSeconds(2);

		Application.LoadLevel(i);
	}
}
