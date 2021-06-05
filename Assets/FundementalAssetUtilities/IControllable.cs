using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    void AquirePossession();
    void Possess(IControllable newNext);
    void Unpossess();
}
