using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class The_Avalanche : BaseCard
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The_Avalanche was started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPlay()
    {
        base.OnPlay();
        Debug.Log("The_Avalanche was played");

        //Debug.Log("The_Avalanche is sleeping for 2 seconds");
        //Thread.Sleep(2000);
        //Debug.Log("The_Avalanche is done sleeping");

        //TODO do things
    }
}
