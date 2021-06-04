using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoStageAnimatorLinkedReactable : TwoStageAnimatorReactable
{
    [SerializeField]
    protected TwoStageAnimatorLinkedReactable partner = null;

    public override void TriggerReaction()
    {
        base.TriggerReaction();
        if (this.partner != null)
        {
            this.partner.TriggerFromPartner();
        }
    }

    protected void TriggerFromPartner()
    {
        base.TriggerReaction();
    }

    public void SetPartner(TwoStageAnimatorLinkedReactable newPartner)
    {
        this.partner = newPartner;
    }

}
