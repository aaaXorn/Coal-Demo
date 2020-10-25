using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch2 : MonoBehaviour
{
    public AudioClip temple;
	
	MusicScript MS;
    // Start is called before the first frame update
    void Start()
    {
        MS = FindObjectOfType<MusicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			if(temple != null)
				MS.BGMusic(temple);
			Destroy(gameObject);
		}
	}
}
