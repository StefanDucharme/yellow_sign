
using UnityEngine;

public class Poison : BaseStatusEffect
{
    public Enums.Phase ApplyPhase = Enums.Phase.Start;
    public int CurrentStacks = 0;

    public override void OnApply(int stacks)
    {
        Debug.Log("Poison applied");
        CurrentStacks = stacks;
    }
}
