using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoulder : MonoBehaviour
{
	[SerializeField]
	BossAI bAI;
	[SerializeField]
	GameObject boulder;
	
	bool boulderSpawned = false;
	float respawnTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boulderSpawned == false)
		{
			Instantiate(boulder, transform.position, Quaternion.identity);
			boulderSpawned = true;
		}
		
		if(bAI.hitByBoulder == true)
		{
			respawnTime -= Time.deltaTime;
			if(respawnTime<=0)
			{
				bAI.hitByBoulder = false;
				boulderSpawned = false;
				respawnTime = 10;
			}
		}
    }
}
