using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChange : MonoBehaviour
{
	Animator anim;
	[SerializeField]
	GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		float playerPosX = player.transform.position.x;
		float playerPosY = player.transform.position.y;
		if(playerPosX>-176 && playerPosX<-116)
		{
			anim.SetInteger("fundo", 2);
		}
		else if(playerPosY>45)
		{
			anim.SetInteger("fundo", 1);
		}
		else
		{
			anim.SetInteger("fundo", 0);
		}
	}
}
