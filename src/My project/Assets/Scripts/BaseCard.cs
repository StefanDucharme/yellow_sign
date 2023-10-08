using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    public enum State
    {
        Unavailable,
        InDeck,
        InHand,
        InPlay,
        InDiscard
    }

    public State CurrentState;
    public Guid UniqueID = new Guid();

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

    public virtual void OnDraw()
    {
        ChangeState(State.InHand);
    }

    public virtual void OnDiscard()
    {
        ChangeState(State.InDiscard);
    }

    public virtual void OnReturnToDeck()
    {
        ChangeState(State.InDeck);
    }

    public virtual void ChangeState(State state)
    {
        Debug.Log("Changing state of " + gameObject.name + " from " + CurrentState.ToString() +  "to " + state.ToString());
        CurrentState = state;
    }

}
