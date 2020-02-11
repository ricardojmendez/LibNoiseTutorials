using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

/// <summary>
/// See http://libnoise.sourceforge.net/tutorials/tutorial5.html for an explanation
/// </summary>
public class Tutorial5 : MonoBehaviour 
{
	[SerializeField] Gradient _gradient;
	
	[SerializeField] float _left = 6;
	
	[SerializeField] float _right = 10;
	
	[SerializeField] float _top = 1;
	
	[SerializeField] float _bottom = 5;

	[SerializeField] int _tutorialStep = 1;
	

	void Start() 
	{
		// STEP 1
		// Gradient is set directly on the object
		var mountainTerrain = new RidgedMultifractal();
		RenderAndSetImage(mountainTerrain);

		// Stop rendering if we're only getting as far as this tutorial
		// step. It saves me from doing multiple files.
		if (_tutorialStep <= 1) return;

		// STEP 2
		var baseFlatTerrain = new Billow();
		baseFlatTerrain.Frequency = 2.0;
		RenderAndSetImage(baseFlatTerrain);


		if (_tutorialStep <= 2) return;

		// STEP 3
		var flatTerrain = new ScaleBias(0.125, -0.75, baseFlatTerrain);
		RenderAndSetImage(flatTerrain);

		if (_tutorialStep <= 3) return;

		// STEP 4
		var terrainType = new Perlin();
		terrainType.Frequency = 0.5;
		terrainType.Persistence = 0.25;

		var finalTerrain = new Select(flatTerrain, mountainTerrain, terrainType);
		finalTerrain.SetBounds(0, 1000);
		finalTerrain.FallOff = 0.125;
		RenderAndSetImage(finalTerrain);
	}

	void RenderAndSetImage(ModuleBase generator)
	{
		var heightMapBuilder = new Noise2D(256, 256, generator);
		heightMapBuilder.GeneratePlanar(_left, _right, _top, _bottom);
		var image = heightMapBuilder.GetTexture(_gradient);
		GetComponent<Renderer>().material.mainTexture = image;
	}
	
}
