using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstalagFall : MonoBehaviour
{
	[SerializeField]
	PlayerControl PC;
	[SerializeField]
	HealthControl HC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnCollisionEnter2D(Collision2D collision)//dano no script do player
	{
		if(collision.gameObject.tag == "JumpReset")
			Destroy(gameObject);
	}
}
