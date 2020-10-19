using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour
{
	Animator anim;
	[SerializeField]
	PlayerControl PC;
	[SerializeField]
	Image prefabHP;
	Stack<Image> healthbar;
	
	public int damage = 0;
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
        healthbar = new Stack<Image>();
		for(int i=0; i<PC.health; i++)
		{
			AddLives();
		}
    }

    // Update is called once per frame
    void Update()
    {
		anim.SetBool("Mendel", PC.mendel);
		anim.SetBool("Heat", PC.heat);
    }
	
	public void RemoveLives()
	{
		for(int i=0; i<damage; i++)
		{
			Image obj = healthbar.Pop();
			Destroy(obj);
		}
		PC.health -= damage;
	}
	
	public void AddLives()
	{
		float posX = prefabHP.rectTransform.sizeDelta.x * healthbar.Count * 0.94f + 0.82f;//1.52f
		healthbar.Push(
					   Instantiate(prefabHP,
								   transform.position + new Vector3(posX-2.2f, 0, 0),
								   Quaternion.identity,
								   gameObject.transform)
					  );
	}
}
