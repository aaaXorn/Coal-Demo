using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMendelTrue : MonoBehaviour
{
	[SerializeField]
	PlayerControl PC;
    // Start is called before the first frame update
    void Start()
    {
        PC.mendel = true;
		Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
