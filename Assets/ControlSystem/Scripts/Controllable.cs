using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IControllScheme))]
public class Controllable : MonoBehaviour, IControllable
{
    public static event Action<Controllable> OnNewControlled;

    [SerializeField]
    private Camera playerCamera = null;
    private ControllableManagment controllableManagment = null;
    private IControllScheme controllScheme = null;

    private Controllable next = null;
    private Controllable prev = null;

    private bool _isControlled = false;
    public bool isControlled
    {
        get { return this._isControlled; }
    }

    public virtual void Awake()
    {
        this.controllableManagment = GameObject.FindObjectOfType<ControllableManagment>();
        this.controllScheme = this.GetComponent<IControllScheme>();
    }

    // Used to set this controllable as the initial controllable. Should only be called be ControllableManagment
    public void InitControll()
    {
        this.Controll();
    }

    public void AquirePossession()
    {
        if (this.controllableManagment != null)
        {
            Controllable currentControllable = this.controllableManagment.GetCurrentControlledControllable();
            currentControllable.Possess(this);
        }
    }

    public void Possess(IControllable newNext)
    {
        Controllable nextControllable = (Controllable)newNext;
        if (nextControllable == this)
        {
            throw new ArgumentException("Cannot possess itself");
        }
        if (this.next != null)
        {
            throw new UnityException("Not the head of a possesion chain");
        }
        this.next = nextControllable;
        this.UnControll();
        nextControllable.HandlePossess(this);
    }

    private void HandlePossess(Controllable newPrev)
    {
        if (this.prev != null || this.next != null)
        {
            throw new UnityException("Already in a possesion chain");
        }
        this.prev = newPrev;
        this.Controll();
    }

    public void Unpossess()
    {
        if (this.next != null)
        {
            throw new UnityException("Not the head of a possesion chain");
        }
        if (this.prev == null)
        {
            throw new UnityException("Cannot unpossess root chain node");
        }
        this.UnControll();
        this.prev.HandleUnPossess();
        this.prev = null;
    }

    private void HandleUnPossess()
    {
        if (this.next == null)
        {
            throw new UnityException("Control returned to a node that does not have a child");
        }
        this.next = null;
        this.Controll();
    }

    private void Controll()
    {
        if (!this.isControlled)
        {
            if (this.playerCamera != null)
            {
                this.playerCamera.enabled = true;
            }

            this.controllScheme.Enable();

            this._isControlled = true;
            OnNewControlled(this);
        }
    }

    private void UnControll()
    {
        if (this.isControlled)
        {
            this._isControlled = false;

            this.controllScheme.Disable();

            if (this.playerCamera != null)
            {
                this.playerCamera.enabled = false;
            }
        }
    }

    public void SetPlayerCamera(Camera newPlayerCamera)
    {
        this.playerCamera = newPlayerCamera;
    }
}
