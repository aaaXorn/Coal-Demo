using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanged1L : MonoBehaviour
{
	Animator anim;
    SpriteRenderer render;
	Rigidbody2D rigid;
	
	float currentTime;
	[SerializeField]
	float lifeTime = 2, bugFixTime = 1;
	bool collided = false;
	
	[SerializeField]
	float speedX = -6;
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
		rigid = GetComponent<Rigidbody2D>();
		
		render.flipX = true;
		anim.SetTrigger("Start");
    }

    // Update is called once per frame
    void Update()
    {
        currentTime+=Time.deltaTime;
		if(currentTime>lifeTime)
		{
			Destroy(gameObject);
		}
		
		if(collided == true)
		{
			bugFixTime -= Time.deltaTime;
			if(bugFixTime<=0)
				Destroy(gameObject);
		}
		
		rigid.velocity = new Vector2(speedX, rigid.velocity.y);
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		anim.SetTrigger("Collision");
		collided = true;
		currentTime = 1.5f;
	}
}
