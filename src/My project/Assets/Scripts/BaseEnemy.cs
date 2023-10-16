using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public enum State
    {
        Unavailable,
        Avialable,
        InPlay,
        IsDead
    }

    public State CurrentState;
    public Guid UniqueID = new Guid();
    public float BaseHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnPlay()
    {
        ChangeState(State.InPlay);
    }

    public virtual void OnAction()
    {
        
    }

    public virtual void OnDeath()
    {
        ChangeState(State.IsDead);
    }

    public virtual void ChangeState(State state)
    {
        Debug.Log("Changing state of " + gameObject.name + " from " + CurrentState.ToString() +  "to " + state.ToString());
        CurrentState = state;
    }

    public virtual void TakeDamage(float dmg)
    {

    }

    public virtual void ApplyEffect()
    {

    }

}
