using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionSystem
{
    private Action<PossessionSystem> onNewControlledCallback;

    private ControllableManagment controllableManagment = null;
    private Camera playerCamera = null;
    private IControllScheme controllScheme = null;

    private PossessionSystem next = null;
    private PossessionSystem prev = null;

    private bool _isControlled = false;
    public bool isControlled
    {
        get { return this._isControlled; }
    }

    public PossessionSystem(ControllableManagment controllableManagment, Camera playerCamera, IControllScheme controllScheme, Action<PossessionSystem> onNewControlledCallback)
    {
        this.controllableManagment = controllableManagment;
        this.playerCamera = playerCamera;
        this.controllScheme = controllScheme;
        this.onNewControlledCallback = onNewControlledCallback;
    }

    // Used to set this controllable as the initial controllable. Should only be called be ControllableManagment
    public void InitControll()
    {
        this.Controll();
    }

    public void Possess(PossessionSystem newNext)
    {
        if (newNext == this)
        {
            throw new ArgumentException("Cannot possess itself");
        }
        if (this.next != null)
        {
            throw new UnityException("Not the head of a possesion chain");
        }
        this.next = newNext;
        this.UnControll();
        newNext.HandlePossess(this);
    }

    private void HandlePossess(PossessionSystem newPrev)
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
            this.onNewControlledCallback(this);
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
