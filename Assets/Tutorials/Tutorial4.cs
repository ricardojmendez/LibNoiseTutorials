using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Generator;

/// <summary>
/// See http://libnoise.sourceforge.net/tutorials/tutorial4.html for an explanation
/// </summary>
public class Tutorial4 : MonoBehaviour 
{
	[SerializeField] Gradient _gradient = GradientPresets.Terrain;

	[SerializeField] float _left = 6;

	[SerializeField] float _right = 10;

	[SerializeField] float _top = 1;

	[SerializeField] float _bottom = 5;

	[SerializeField] int _octaveCount = 1;

	[SerializeField] float _frecuency = 1;

	[SerializeField] float _persistence = 0.5f;

	void Start()
	{
		var perlin = new Perlin();
		perlin.OctaveCount = _octaveCount;
		perlin.Frequency = _frecuency;
		perlin.Persistence = _persistence;
		// Unlike on the base LibNoise tutorial, we don't have a separate heightMap target
		// to set - we will instead build it after.  We also initialize the resulting size
		// on the constructor instead of passing a separate destination size.
		var heightMapBuilder = new Noise2D(256, 256, perlin);
		heightMapBuilder.GeneratePlanar(_left, _right, _top, _bottom);

		// Get the image
		var image = heightMapBuilder.GetTexture(_gradient);

		// Set it. It may appear inverted from the example on the LibNoise site depending 
		// on the angle at which the object is rotated/viewed.
		GetComponent<Renderer>().material.mainTexture = image;

		// We don't do the light changes for the texture, since that's beyond the scope of 
		// this port
	}
}
