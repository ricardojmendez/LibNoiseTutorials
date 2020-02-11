using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

/// <summary>
/// See http://libnoise.sourceforge.net/tutorials/tutorial7.html for an explanation.
/// 
/// This source only partly covers Tutorial 7, since LiNoise does not contain the
/// code for writing Teragen files yet.  We will still render an image.
/// </summary>
public class Tutorial7 : MonoBehaviour 
{
	[SerializeField] Gradient _gradient;
	[SerializeField] float _left = 6;
	[SerializeField] float _right = 10;
	[SerializeField] float _top = 1;
	[SerializeField] float _bottom = 5;
	[SerializeField] float _frequency = 4;
	[SerializeField] float _power = 0.125f;
	[SerializeField] float _scale = 375;
	[SerializeField] float _bias = 375;

	void Start() 
	{
		var mountainTerrain = new RidgedMultifractal();

		var baseFlatTerrain = new Billow();
		baseFlatTerrain.Frequency = 2.0;

		var flatTerrain = new ScaleBias(0.125, -0.75, baseFlatTerrain);

		var terrainType = new Perlin();
		terrainType.Frequency = 0.5;
		terrainType.Persistence = 0.25;

		var terrainSelector = new Select(flatTerrain, mountainTerrain, terrainType);
		terrainSelector.SetBounds(0, 1000);
		terrainSelector.FallOff = 0.125f;

		/*
		 * From the tutorial text:
		 * 
		 * Next, you'll apply a bias of +375 to the output from the terrainSelector 
		 * noise module. This will cause its output to range from (-375 + 375) to 
		 * (+375 + 375), or in other words, 0 to 750. You'll apply this bias so 
		 * that most of the elevations in the resulting terrain height map are 
		 * above sea level. 
		 */
		var terrainScaler = new ScaleBias(terrainSelector);
		terrainScaler.Scale = _scale;
		terrainScaler.Bias = _bias;
		
		var finalTerrain = new Turbulence(terrainScaler);
		finalTerrain.Frequency = _frequency;
		finalTerrain.Power = _power;

		RenderAndSetImage(finalTerrain);
	}

	void RenderAndSetImage(ModuleBase generator)
	{
		var heightMapBuilder = new Noise2D(513, 513, generator);
		heightMapBuilder.GeneratePlanar(_left, _right, _top, _bottom);
		var image = heightMapBuilder.GetTexture(_gradient);
		GetComponent<Renderer>().material.mainTexture = image;
	}
	
}
