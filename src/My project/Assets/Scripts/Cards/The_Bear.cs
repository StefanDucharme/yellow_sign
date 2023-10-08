using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class The_Bear : BaseCard
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The_Bear was started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnPlay()
    {
        base.OnPlay();
        Debug.Log("The_Bear was played");

        //Debug.Log("The_Bear is sleeping for 2 seconds");
        //Thread.Sleep(2000);
        //Debug.Log("The_Bear is done sleeping");

        //TODO do things
    }
}
