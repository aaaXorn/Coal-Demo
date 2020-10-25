using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
	public void Cutscene1()
	{
		SceneManager.LoadScene("Cutscene1");
	}
	
	public void Cutscene2()
	{
		SceneManager.LoadScene("Cutscene2");
	}
	
	public void Part1()
	{
		SceneManager.LoadScene("Scene1");
	}
	
	public void Part1B()
	{
		SceneManager.LoadScene("Scene1.5");
	}
	
	public void Cutscene3()
	{
		SceneManager.LoadScene("Cutscene3");
	}
	
	public void Part2()
	{
		SceneManager.LoadScene("Scene2");
	}
	
	public void Cutscene4()
	{
		SceneManager.LoadScene("Cutscene4");
	}
	
	public void Exit()
	{
		Application.Quit();
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
