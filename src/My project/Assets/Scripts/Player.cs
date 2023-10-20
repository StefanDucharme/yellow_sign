using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State
    {
        Unavailable,
        IsDead
    }

    public State CurrentState;
    public Guid UniqueID = new Guid();
    public float MaxHealth = 10;
    public float CurrentHealth;
    public List<BaseStatusEffect> statusEffects = new List<BaseStatusEffect>();

    // Start is called before the first frame update
    void Start()
    {
        this.CurrentHealth = this.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void OnAction()
    {
        
    }

    public void OnDeath()
    {
        ChangeState(State.IsDead);
    }

    public void ChangeState(State state)
    {
        Debug.Log("Changing state of " + gameObject.name + " from " + CurrentState.ToString() +  "to " + state.ToString());
        CurrentState = state;
    }

    public void TakeDamage(float dmg)
    {
        if (dmg >= CurrentHealth)
        {
            CurrentHealth = 0;
            Debug.Log("Player died");
            ChangeState(State.IsDead);

            return;
        }

        CurrentHealth -= dmg;
        Debug.Log($"Player took {dmg} dmg");
    }

    public virtual void ApplyEffect()
    {

    }

}
