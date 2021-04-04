using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtCollection : MonoBehaviour
{
	public static ArtCollection Instance = null;

	public Sprite Empty;
	
	public List<Sprite> Walls;

	public Sprite Tube1;
	public Sprite Tube2;
	public Sprite Tube2Line;
	public Sprite Tube3;
	public Sprite Tube4;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance == this)
			Destroy(gameObject);
	}

	public Sprite GetRandomWall()
	{
		var index = Random.Range(0, Walls.Count);
		return Walls[index];
	}
}
