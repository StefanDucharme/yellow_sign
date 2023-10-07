using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("The_Bear was played");
    }
}
