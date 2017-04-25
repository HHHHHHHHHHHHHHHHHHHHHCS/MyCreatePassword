using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        long time1 = System.DateTime.Now.Ticks;
        GetPCInfo pcInfo = new GetPCInfo();
        pcInfo.CreatePsw("qwert");
        CreatePassword cp = new CreatePassword();
        cp.Create("a384b6463fc216a5f8ecb6670f86456a");
        EqualPassWord ep = new EqualPassWord();
        Debug.Log(ep.EqualPassword("qwert"));
        Debug.Log((System.DateTime.Now.Ticks - time1)/ 10000000f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
