using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalMFAI : MonoBehaviour
{
	Animator anim;
	SpriteRenderer render;
	Rigidbody2D rigid;
	
	[SerializeField]
	GameObject player;
	[SerializeField]
	PlayerControl PC;
	[SerializeField]
	HealthControl HC;
	
	public int speed1 = 2;
	public int speed2 = 5;
	float currentTime;
	[SerializeField]
	float patternTime1 = 1, patternTime2 = 2.5f;
	
	int health = 1;
	
	float distanceActivate = 9, distanceDeactivate = 13, distanceActivateY = 4;
	bool active;
	float distanceX;
	float distanceY;
	float direction;
	
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		render = GetComponent<SpriteRenderer>();
		rigid = GetComponent<Rigidbody2D>();
		
		StartCoroutine("Activate");
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
		{
			Destroy(gameObject);
		}
    }
	
	void FixedUpdate()
	{
		if(active == true)
		{
			anim.SetBool("Active", true);
			if(distanceX != 0)
			{
				direction = distanceX / Mathf.Abs(distanceX);
				if(direction<0)
					render.flipX = true;
				else
					render.flipX = false;
			}
			
			if(currentTime<=patternTime2)
				currentTime+=Time.deltaTime;
			if(currentTime>patternTime1 && currentTime<patternTime2)
			{
				anim.SetTrigger("Attack1");
				rigid.velocity = new Vector2(direction*speed1, rigid.velocity.y);
			}
			else if(currentTime>patternTime2)
			{
				anim.SetTrigger("Attack2");
				rigid.velocity = new Vector2(direction*speed2, rigid.velocity.y);
			}
		}
		else
		{
			anim.SetBool("Active", false);
			currentTime = 0;
		}
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "PlayerFire")
		{
			health--;
		}
		if(collision.gameObject.tag == "BluePFire")
		{
			health -= 2;
		}
	}
	
	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && PC.damageTaken == false)
		{
			HC.damage = 3;
			PC.damageTaken = true;
			HC.RemoveLives();
		}
	}
	
	IEnumerator Activate()
	{
		while(true)
		{
			distanceX = player.transform.position.x - transform.position.x;
			distanceY = player.transform.position.y - transform.position.y;
			
			if(Mathf.Abs(distanceX)<distanceActivate && Mathf.Abs(distanceY)<distanceActivateY && active == false)
			{
				active = true;
			}
			else if(Mathf.Abs(distanceX)>distanceDeactivate && active == true || Mathf.Abs(distanceY)>distanceActivate && active == true)
			{
				active = false;
			}
			
			yield return new WaitForSeconds(0.3f);
		}
	}
}
