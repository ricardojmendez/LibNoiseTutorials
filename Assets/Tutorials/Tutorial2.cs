using UnityEngine;
using System.Collections;
using LibNoise.Generator;

/// <summary>
/// See http://libnoise.sourceforge.net/tutorials/tutorial2.html for an explanation
/// </summary>
public class Tutorial2 : MonoBehaviour 
{

	[SerializeField]
	Vector3 _firstValue = new Vector3(1.25f, 0.75f, 0.50f);

	[SerializeField]
	Vector3 _displacement = 0.0001f * Vector3.one;

	[SerializeField]
	Vector3 _secondValue = new Vector3(14.50f, 20.25f, 75.75f);

	// Use this for initialization
	void Start () 
	{
		Debug.Log("Tutorial conversion for http://libnoise.sourceforge.net/tutorials/tutorial2.html");
		var perlin = new Perlin();
		Debug.Log(string.Format("First value: {0}", perlin.GetValue(_firstValue)));
		Debug.Log(string.Format("First value, displaced: {0}", perlin.GetValue(_firstValue + _displacement)));
		Debug.Log(string.Format("Second value: {0}", perlin.GetValue(_secondValue)));
	}
}
