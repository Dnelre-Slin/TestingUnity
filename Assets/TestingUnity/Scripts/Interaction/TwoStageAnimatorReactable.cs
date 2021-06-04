using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationPlayer))]
public class TwoStageAnimatorReactable : BaseReactable
{

    protected AnimationPlayer animationPlayer;
    [SerializeField]
    protected string stageOneDescription = "Open";
    [SerializeField]
    protected string stageTwoDescription = "Close";

    protected bool stageOne;

    // Start is called before the first frame update
    void Start()
    {
        this.animationPlayer = GetComponent<AnimationPlayer>();
        this.stageOne = true;
        this.description = stageOneDescription;
    }

    public override void TriggerReaction()
    {
        if (this.stageOne)
        {
            this.animationPlayer.Play();
            this.description = stageTwoDescription;
            this.stageOne = false;
        }
        else
        {
            this.animationPlayer.PlayReverse();
            this.description = stageOneDescription;
            this.stageOne = true;
        }
    }
}
