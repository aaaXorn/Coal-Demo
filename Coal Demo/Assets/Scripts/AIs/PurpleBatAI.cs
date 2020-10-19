using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBatAI : MonoBehaviour
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
	
	public int speed = 3;
	[SerializeField]
	int chargeDirection;//quadrante do player em relação ao morcego
	float currentTime;
	[SerializeField]
	float chargeTime = 2;
	bool action;
	
	int health = 1;
	
	[SerializeField]
	float distanceActivate = 10, distanceDeactivate = 13;
	bool active;
	float distanceX;
	float distanceY;
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
			anim.SetBool("Flying", true);
			if(action == false)
			{
				if(distanceX>0 && distanceY>0 || distanceX>0 && distanceY==0)
				{
					chargeDirection = 1;
				}
				else if(distanceX>0 && distanceY<0 || distanceX==0 && distanceY<0)
				{
					chargeDirection = 2;
				}
				else if(distanceX<0 && distanceY<0 || distanceX<0 && distanceY==0)
				{
					chargeDirection = 3;
				}
				else if(distanceX<0 && distanceY>0 || distanceX==0 && distanceY>0)
				{
					chargeDirection = 4;
				}
				
				action = true;
			}
			else
			{
				currentTime += Time.deltaTime;
				if(currentTime>chargeTime)
				{
					action = false;
					currentTime = 0;
				}
			}
		}
		else
		{
			chargeDirection = 0;
			action = true;
		}
		
		switch(chargeDirection)
		{
		case 0:
			anim.SetBool("Flying", false);
			rigid.velocity = new Vector2(0, 0);
			break;
		case 1:
			rigid.velocity = new Vector2(speed, speed);
			render.flipX = true;
			break;
		case 2:
			rigid.velocity = new Vector2(speed, speed * -1);
			render.flipX = true;
			break;
		case 3:
			rigid.velocity = new Vector2(speed * -1, speed * -1);
			render.flipX = false;
			break;
		case 4:
			rigid.velocity = new Vector2(speed * -1, speed);
			render.flipX = false;
			break;
		default:
			break;
		}
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "PlayerFire")
		{
			health--;
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
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "PlayerAttack")
		{
			health--;
		}
	}
	
	IEnumerator Activate()
	{
		while(true)
		{
			distanceX = player.transform.position.x - transform.position.x;
			distanceY = player.transform.position.y - transform.position.y;
			
			if(Mathf.Abs(distanceX)<distanceActivate && Mathf.Abs(distanceY)<distanceActivate && active == false)
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
