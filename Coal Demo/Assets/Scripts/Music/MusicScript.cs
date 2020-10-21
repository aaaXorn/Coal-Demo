using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
	public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void BGMusic(AudioClip bgm)
	{
		if(music.clip.name == bgm.name)
			return;
		
		music.Stop();
		music.clip = bgm;
		music.Play();
	}
}