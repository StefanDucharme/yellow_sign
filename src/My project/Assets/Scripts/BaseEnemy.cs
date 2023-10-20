using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public enum State
    {
        Unavailable,
        Available,
        InPlay,
        IsDead
    }

    public State CurrentState;
    public Guid UniqueID;
    public float BaseHealth = 10;
    public List<BaseStatusEffect> statusEffects = new List<BaseStatusEffect>();
    public NotificationsManager NotificationsManager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.UniqueID = Guid.NewGuid();
        NotificationsManager = GameObject.Find("NotificationsManager").GetComponent<NotificationsManager>();
        NotificationsManager.AddListener(this, "OnGameStateChanged");
        NotificationsManager.AddListener(this, "OnPhaseChanged");
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

    public virtual void ApplyEffect(BaseStatusEffect effect, int stacks)
    {
        var existingEffect = statusEffects.Find(s => s.GetType() == effect.GetType());
        if (existingEffect == null)
        {
            //existingEffect = effect;
            //statusEffects.Add(effect);
        }
    }
    public void OnGameStateChanged(NotificationData data)
    {
        Debug.Log("OnGameStateChanged.");
    }

    public void OnPhaseChanged(NotificationData notification)
    {
        var data = notification.Data;
        if (data.GetType() != typeof(Enums.Phase))
        {
            Debug.Log($"OnPhaseChanged. unexpected data type {data.GetType()}");
            return;
        }

        var phase = (Enums.Phase)notification.Data;
        Debug.Log($"OnPhaseChanged. {phase}");
    }

}
