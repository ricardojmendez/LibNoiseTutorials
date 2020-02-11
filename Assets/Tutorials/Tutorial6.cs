using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

/// <summary>
/// See http://libnoise.sourceforge.net/tutorials/tutorial6.html for an explanation
/// </summary>
public class Tutorial6 : MonoBehaviour 
{
	[SerializeField] Gradient _gradient;
	
	[SerializeField] float _left = 6;
	
	[SerializeField] float _right = 10;
	
	[SerializeField] float _top = 1;
	
	[SerializeField] float _bottom = 5;

	[SerializeField] float _frequency = 4;

	[SerializeField] float _power = 0.125f;

	void Start() 
	{
		var mountainTerrain = new RidgedMultifractal();

		var baseFlatTerrain = new Billow();
		baseFlatTerrain.Frequency = 2.0;

		var flatTerrain = new ScaleBias(0.125, -0.75, baseFlatTerrain);

		var terrainType = new Perlin();
		terrainType.Frequency = 0.5;
		terrainType.Persistence = 0.25;

		// Create the selector for turbulence
		var terrainSelector = new Select(flatTerrain, mountainTerrain, terrainType);
		terrainSelector.SetBounds(0, 1000);
		terrainSelector.FallOff = 0.125f;
		
		var finalTerrain = new Turbulence(terrainSelector);
		finalTerrain.Frequency = _frequency;
		finalTerrain.Power = _power;

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
