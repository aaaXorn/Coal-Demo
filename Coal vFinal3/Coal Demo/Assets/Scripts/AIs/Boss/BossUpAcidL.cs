using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUpAcidL : MonoBehaviour
{
	Animator anim;
	Rigidbody2D rigid;
	SpriteRenderer render;
	
	bool move = true;
	
	bool platFix = true;
	
	[SerializeField]
	float deleteTimer = 6;
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer>();
		
		render.flipX = true;
		rigid.AddForce(new Vector2(0, 350));
    }

    // Update is called once per frame
    void Update()
    {
        deleteTimer -= Time.deltaTime;
		if(deleteTimer<=0)
			Destroy(gameObject);
		
		anim.SetTrigger("Switch");
    }
	
	void FixedUpdate()
	{
		if(move)
			rigid.velocity = new Vector2(-6, rigid.velocity.y);
		else
			rigid.velocity = new Vector2(0, rigid.velocity.y);
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "JumpReset")
		{
			if(platFix)
			{
				anim.SetTrigger("Destroy");
				move = false;
				deleteTimer = 1;
				platFix = false;
			}
		}
	}
}
