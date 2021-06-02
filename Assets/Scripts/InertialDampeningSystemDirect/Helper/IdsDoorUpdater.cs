using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct DoorFollowerPair
{
    public Transform doorFollowee;
    public Transform doorFollower;
    public DoorFollowerPair(Transform doorFollowee, Transform doorFollower)
    {
        this.doorFollowee = doorFollowee;
        this.doorFollower = doorFollower;
    }
}

public class IdsDoorUpdater
{
    List<DoorFollowerPair> doorFollowerPairs;

    public IdsDoorUpdater(int capacity = 0)
    {
        doorFollowerPairs = new List<DoorFollowerPair>(capacity);
    }

    public void AddDoorPair(Transform followee, Transform follower)
    {
        DoorFollowerPair doorFollowerPair = new DoorFollowerPair(followee, follower);
        this.doorFollowerPairs.Add(doorFollowerPair);
    }

    public void UpdateDoors()
    {
        for (int i = 0; i < doorFollowerPairs.Count; i++)
        {
            doorFollowerPairs[i].doorFollower.transform.localPosition = doorFollowerPairs[i].doorFollowee.transform.localPosition;
            doorFollowerPairs[i].doorFollower.transform.localRotation = doorFollowerPairs[i].doorFollowee.transform.localRotation;
            doorFollowerPairs[i].doorFollower.transform.localScale = doorFollowerPairs[i].doorFollowee.transform.localScale;
        }
    }

    public override string ToString()
    {
        return "IdsDoorUpdater: " + this.doorFollowerPairs + ". Count : " + this.doorFollowerPairs.Count;
    }

}
