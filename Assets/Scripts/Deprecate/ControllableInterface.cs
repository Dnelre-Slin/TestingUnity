using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllable
{
    int GameobjectId { get; set; }
    void TakePossession();
}
