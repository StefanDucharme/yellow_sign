using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class The_Inquisitor : BaseEnemy
{
    public float BaseDamage = 1;
    public float CurrentDamage;
    public float MaxHealth = 10;
    public float CurrentHealth;

    void Start()
    {
        Debug.Log("The_Inquisitor was started");
        this.CurrentDamage = this.BaseDamage;
        this.CurrentHealth = this.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnPlay()
    {
        base.OnPlay();
        Debug.Log("The_Inquisitor was played");

        //Debug.Log("The_Bear is sleeping for 2 seconds");
        //Thread.Sleep(2000);
        //Debug.Log("The_Bear is done sleeping");

        //TODO do things
    }

    public override void OnAction()
    {
        var player = GameObject.Find("Player");
        var target = player.GetComponent<Player>();
        target.GetComponent<BaseEnemy>().TakeDamage(BaseDamage);
    }

    public override void TakeDamage(float dmg)
    {
        if (dmg >= CurrentHealth)
        {
            CurrentHealth = 0;
            Debug.Log("The_Inquisitor died");
            base.ChangeState(State.IsDead);

            return;
        }
        
        CurrentHealth -= dmg;
        Debug.Log($"The_Inquisitor took {dmg} dmg");
    }
}
