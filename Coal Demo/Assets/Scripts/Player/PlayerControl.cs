using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
	Animator anim;
	SpriteRenderer render;
	Rigidbody2D rigid;
	
	[SerializeField]
	HealthControl HC;
	
	public bool mendel = false;
	public bool pushing = false;
	
	float moveX;
	public int speedX;
	[SerializeField]
	int normalSpeed = 4, dashSpeed = 6;
	bool dash = false;
	bool mayMove = true;
	float dashingTime;
	[SerializeField]
	float dashTime = 1;
	
	public int maxJumps = 1;
	int jumps = 0;
	bool jumpTimer = false;//pulo com Raycast
	float jumpingTime;
	[SerializeField]
	float jumpFixTime = 1;
	
	public int health = 30;
	public int maxHealth = 30;
	public bool damageTaken = false;
	float currentTime;
	[SerializeField]
	float invulTime = 1.6f;
	[SerializeField]
	float damageMoveLock = 0.7f;
	int heal;
	int healthMath;
	
	public bool poisoned = false;
	float poisonTimer = 2;
	int poisonProcs = 0;
	
	bool attacking = false;
	float attackingTime;
	[SerializeField]
	float totalAttackTime = 1;
	
	[SerializeField]
	GameObject meleeAttack, rangedAttack1R, rangedAttack1L, coalWall, firePillarR, firePillarL;
	
	public bool heat = false;
	public int flameType = 0;
	float heatTime;
	[SerializeField]
	float fireDamageTime = 2;
	bool pillarUse = true;
	float pillarCD = 1;
	
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		render = GetComponent<SpriteRenderer>();
		rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if(health<=0)
			SceneManager.LoadScene("Menu");
		
        if (Input.GetKey(KeyCode.A) && mayMove)
		{
			WalkLeft();
		}
		else if (Input.GetKey(KeyCode.D) && mayMove)
		{
			WalkRight();
		}
		else
		{
			anim.SetBool("MovementX", false);
			moveX = 0;
		}
		
		if (Input.GetKeyDown(KeyCode.W) && jumps>0 && mayMove)
		{
			JumpLow();
		}
		if (Input.GetKeyDown(KeyCode.Space) && jumps>0 && mayMove)
		{
			JumpHigh();
		}
		
		if (Input.GetKeyDown(KeyCode.I) && damageTaken == false && attacking == false)
		{
			if(heat == false)
			{
				MeleeAttack();
			}
			else if(heat == true && flameType == 0)
			{
				RangedAttack1();
			}
		}
		
		if(damageTaken == true)
		{
			currentTime += Time.deltaTime;
			if(currentTime>damageMoveLock)
			{
				anim.SetBool("Damaged", false);
				mayMove = true;
			}
			else
			{
				anim.SetBool("Damaged", true);
				mayMove = false;
			}
			if(currentTime>invulTime)
			{
				currentTime = 0;
				damageTaken = false;
			}
		}
		
		AttackTimer();
		
		JumpingTimer();
		
		RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 0.77f, 0), Vector2.down, 0.9f);//pulo com Raycast
		Debug.DrawLine(transform.position - new Vector3(0, 0.77f, 0), transform.position + Vector3.down*0.9f, Color.magenta);
		if(hit.collider!=null && hit.collider.gameObject.tag=="JumpReset" && jumpTimer == false)
		{
			anim.SetBool("Jump", false);
			jumps = maxJumps;
		}
		
		
		if(mendel == true)
		{
			if(Input.GetKeyDown(KeyCode.P) && heat == true && mayMove)
			{
				anim.SetInteger("HeatType", 0);
				anim.SetTrigger("Heat");
				heat = false;
			}
			else if(Input.GetKeyDown(KeyCode.P) && heat == false && flameType == 0 && mayMove)
			{
				anim.SetInteger("HeatType", 1);
				anim.SetTrigger("Heat");
				heat = true;
			}
			
			if(heat == true)
			{
				heatTime += Time.deltaTime;
				if(heatTime>fireDamageTime)
				{
					HC.damage = 1;
					HC.RemoveLives();
					heatTime = 0;
				}
			}
			else
			{
				heatTime = 0;
			}
			
			if(Input.GetKeyDown(KeyCode.O) && heat == true && mayMove)
			{
				dash = true;
			}
			
			if(Input.GetKeyDown(KeyCode.U) && heat == false && mayMove)
			{
				CoalWall();
			}
			else if(Input.GetKeyDown(KeyCode.U) && heat == true && flameType == 0 && mayMove && pillarUse == true)
			{
				FirePillar();
			}
			
			if (pillarUse == false)
			{
				pillarCD -= Time.deltaTime;
				if (pillarCD<=0)
				{
					pillarUse = true;
					pillarCD = 1;
				}
			}
		}
		else
		{
			if(pushing == true)
			{
				anim.SetBool("Pushing", true);
			}
			else
			{
				anim.SetBool("Pushing", false);
			}
		}
		
		if(poisoned)
			PoisonDamage();
		
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
    }
	
	private void FixedUpdate()
	{
		if(dash == false)
		{
			anim.SetBool("Dash", false);
			speedX = normalSpeed;
		}
		else
		{
			anim.SetBool("Dash", true);
			speedX = dashSpeed;
			dashingTime+=Time.deltaTime;
			if(dashingTime>dashTime)
			{
				dash = false;
				dashingTime = 0;
			}
		}
		rigid.velocity = new Vector2(moveX * speedX, rigid.velocity.y);
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		/*if (collision.gameObject.tag == "JumpFix")//pulo com colisão
		{
			anim.SetBool("Jump", false);
			jumps = maxJumps;
		}*/
		
		if (collision.gameObject.tag == "Estalag" && damageTaken == false)
		{
			HC.damage = 3;
			damageTaken = true;
			HC.RemoveLives();
			Destroy(collision.gameObject);
		}
		
		if (collision.gameObject.tag == "Poison")
		{
			poisoned = true;
			poisonProcs = 0;
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "BossMelee" && damageTaken == false)
		{
			HC.damage = 6;
			damageTaken = true;
			HC.RemoveLives();
		}
		
		if (collision.gameObject.tag == "Die")
			SceneManager.LoadScene("Menu");
		
		if(collision.gameObject.tag == "+hp")
		{
			heal = 6;
			health += heal;
			healthMath = health;
			if(health>maxHealth)
			{
				healthMath -= maxHealth;
				heal -= healthMath;
				health = maxHealth;
			}
			
			for(int i=0; i<heal; i++)
			{
				HC.AddLives();
			}
			Destroy(collision.gameObject);
		}
	}
	
	private void WalkLeft()
	{
		anim.SetBool("MovementX", true);
		render.flipX = true;
		moveX = -1;
	}
	
	private void WalkRight()
	{
		anim.SetBool("MovementX", true);
		render.flipX = false;
		moveX = 1;
	}
	
	private void JumpLow()
	{
		anim.SetBool("Jump", true);
		rigid.AddForce(new Vector2(0, 250));
		jumps--;
		jumpTimer = true;
	}
	
	private void JumpHigh()
	{
		anim.SetBool("Jump", true);
		rigid.AddForce(new Vector2(0, 320));
		jumps--;
		jumpTimer = true;
	}
	
	private void JumpingTimer()//pulo com Raycast
	{
		if(jumpTimer == true)
		{
			jumpingTime+=Time.deltaTime;
			if(jumpingTime>jumpFixTime)
			{
				jumpTimer = false;
				jumpingTime = 0;
			}
		}
	}
	
	private void MeleeAttack()
	{
		anim.SetTrigger("Melee");
		attacking = true;
		GameObject Attack = Instantiate(meleeAttack, transform.position, Quaternion.identity);
		Attack.transform.parent = gameObject.transform;
		if(render.flipX == false)
		{
			Attack.transform.Translate(1, 0, 0);
		}
		else
		{
			Attack.transform.Translate(-1, 0, 0);
		}
	}
	
	private void RangedAttack1()
	{
		anim.SetTrigger("Melee");
		attacking = true;
		if(render.flipX == false)
		{
			Instantiate(rangedAttack1R, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
		}
		else
		{
			Instantiate(rangedAttack1L, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
		}
	}
	
	private void CoalWall()
	{
		anim.SetTrigger("Wall");
		if(render.flipX == false)
		{
			Instantiate(coalWall, transform.position + new Vector3(2.5f, -0.6f, 0), Quaternion.identity);
		}
		else
		{
			Instantiate(coalWall, transform.position + new Vector3(-2.5f, -0.6f, 0), Quaternion.identity);
		}
	}
	
	private void FirePillar()
	{
		anim.SetTrigger("Pillar");
		if(render.flipX == false)
		{
			Instantiate(firePillarR, transform.position + new Vector3(2.5f, -0.6f, 0), Quaternion.identity);
		}
		else
		{
			Instantiate(firePillarL, transform.position + new Vector3(-2.5f, -0.6f, 0), Quaternion.identity);
		}
		pillarUse = false;
	}
	
	private void AttackTimer()
	{
		if(attacking == true)
		{
			attackingTime+=Time.deltaTime;
			if(attackingTime>totalAttackTime)
			{
				attacking = false;
				attackingTime = 0;
			}
		}
	}
	
	private void PoisonDamage()
	{
		poisonTimer -= Time.deltaTime;
		if(poisonTimer <= 0)
		{
			HC.damage = 1;
			HC.RemoveLives();
			poisonProcs++;
			poisonTimer = 2;
		}
		if(poisonProcs>4)
		{
			poisonProcs = 0;
			poisoned = false;
		}
	}
}
