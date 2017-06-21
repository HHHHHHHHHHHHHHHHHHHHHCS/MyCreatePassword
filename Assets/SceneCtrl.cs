using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{

	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(0);
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(1);
        }

    }
}
