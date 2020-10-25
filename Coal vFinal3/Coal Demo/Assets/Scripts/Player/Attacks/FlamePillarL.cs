using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamePillarL : MonoBehaviour
{
    Animator anim;
	Rigidbody2D rigid;
	
	[SerializeField]
	float durationTime = 3;
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
			Destroy(gameObject);
		
		durationTime -= Time.deltaTime;
		if(durationTime<=0.5f)
			anim.SetTrigger("Vanish");
		if(durationTime<=0)
			Destroy(gameObject);
    }
	
	void FixedUpdate()
	{
		rigid.velocity = new Vector2(-2, rigid.velocity.y);
	}
}
