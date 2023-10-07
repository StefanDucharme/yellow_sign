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
        Debug.Log("BaseCard was started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnPlay()
    {
        Debug.Log("BaseCard was played");
    }

    public virtual void OnDraw()
    {
        Debug.Log("BaseCard was drawn");

    }

    public virtual void OnDiscard()
    {
        Debug.Log("BaseCard was discarded");
    }

    public virtual void ChangeState(State state)
    {
        Debug.Log($"BaseCard state changed {state}");
        CurrentState = state;
    }

}
