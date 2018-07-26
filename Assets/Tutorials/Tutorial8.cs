using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

/// <summary>
/// See http://libnoise.sourceforge.net/tutorials/tutorial8.html for an explanation.
/// </summary>
public class Tutorial8 : MonoBehaviour 
{
	[SerializeField] Gradient _gradient;
	[SerializeField] float _west = -180;
	[SerializeField] float _east = 180;
	[SerializeField] float _north = -90;
	[SerializeField] float _south = 90;

	void Start() 
	{
		var perlin = new Perlin();

		var heightMapBuilder = new Noise2D(512, 256, perlin);
		heightMapBuilder.GenerateSpherical(_north, _south, _west, _east);

		var image = heightMapBuilder.GetTexture(_gradient);
		GetComponent<Renderer>().material.mainTexture = image;



	}

}
