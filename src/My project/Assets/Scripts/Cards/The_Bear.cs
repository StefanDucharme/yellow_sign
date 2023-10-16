using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class The_Bear : BaseCard
{
    public GameManager GM;
    public GameObject Target;
    public float BaseDamage = 2;
    public float CurrentDamage;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The_Bear was started");
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        this.CurrentDamage = this.BaseDamage;
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

        if (GM.InPlayEnemies.Count <= 0)
        {
            Debug.Log("The_Bear has no valid target");
            return;
        }
        
        Target = GM.InPlayEnemies[0];
        Target.GetComponent<BaseEnemy>().TakeDamage(BaseDamage);
        
}
}
