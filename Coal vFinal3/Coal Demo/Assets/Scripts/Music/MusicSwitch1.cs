using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch1 : MonoBehaviour
{
	public AudioClip boss;
	
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
			if(boss != null)
				MS.BGMusic(boss);
		}
	}
}
