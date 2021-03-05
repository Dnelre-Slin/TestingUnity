using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllableManager : MonoBehaviour
{
    // private Dictionary<int, string> players; // Players currently active. Also a map to what controllable gameObject that player controls
    // private Dictionary<string, List<MonoBehaviour>> controllables;

    // [SerializeField]
    // private GameObject initPlayer1ControlledGameObject;

    // public delegate void PossessionCallback(string gameObjectName);
    // private List<PossessionCallback> subscribers;

    // private int storedGameobjectPlayerId;
    // private string storedGameobjectName;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     subscribers = new List<PossessionCallback>();

    //     this.players = new Dictionary<int, string>(4);
    //     this.players[1] = ""; // Add player 1

    //     storedGameobjectPlayerId = 0;
    //     storedGameobjectName = "";

    //     IEnumerable<IControllable> controlInterfaces = FindObjectsOfType<MonoBehaviour>().OfType<IControllable>();

    //     controllables = new Dictionary<string, List<MonoBehaviour>>(controlInterfaces.Count());

    //     foreach (var c in controlInterfaces)
    //     {
    //         MonoBehaviour mono = (MonoBehaviour)c;
    //         mono.enabled = false;
    //         string gameObjectName = mono.gameObject.name;
    //         if (!controllables.ContainsKey(gameObjectName))
    //         {
    //             controllables[gameObjectName] = new List<MonoBehaviour>();
    //         }
    //         controllables[gameObjectName].Add(mono);
    //     }

    //     if (initPlayer1ControlledGameObject != null)
    //     {
    //         PossessGameobject(1, initPlayer1ControlledGameObject.name); // Player 1 will possess init gameobject on start
    //     }

    // }

    // public void PossessGameobject(int playerId, string gameObjectName, bool storeCurrent = false)
    // {
    //     if (this.players.ContainsKey(playerId) && this.players[playerId] != gameObjectName)
    //     {
    //         Debug.Log("Possessing: " + gameObjectName + " (" + storeCurrent + ")");
    //         // Unpossess old gameobject, if found
    //         string oldGameobjectName = this.players[playerId];
    //         if (this.controllables.ContainsKey(oldGameobjectName))
    //         {
    //             foreach (var ctrl in this.controllables[oldGameobjectName])
    //             {
    //                 ctrl.enabled = false; // Disable controllable scripts
    //             }
    //         }

    //         // Possess new gameobject, if found
    //         if (this.controllables.ContainsKey(gameObjectName))
    //         {
    //             foreach (var ctrl in this.controllables[gameObjectName])
    //             {
    //                 ctrl.enabled = true; // Enable controllable scripts
    //             }
    //         }
    //         if (storeCurrent)
    //         {
    //             this.storedGameobjectPlayerId = playerId;
    //             this.storedGameobjectName = this.players[playerId];
    //         }
    //         this.players[playerId] = gameObjectName;
    //         InformSubscribers(gameObjectName);
    //     }
    // }

    // public void UnpossessGameObject()
    // {
    //     if (this.storedGameobjectName != "")
    //     {
    //         Debug.Log("Unpossessed. Revert to: " + this.storedGameobjectName);
    //         PossessGameobject(this.storedGameobjectPlayerId, this.storedGameobjectName);
    //         this.storedGameobjectPlayerId = 0;
    //         this.storedGameobjectName = "";
    //     }
    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    // public string GetPossessedGameObjectName()
    // {
    //     foreach (var entry in this.players)
    //     {
    //         if (entry.Value.Length != 0)
    //         {
    //             return entry.Value;
    //         }
    //     }
    //     return "";
    // }

    // public void SubscribeToPossession(PossessionCallback callback)
    // {
    //     subscribers.Add(callback);
    // }
    // private void InformSubscribers(string gameObjectName)
    // {
    //     foreach (var sub in subscribers)
    //     {
    //         sub(gameObjectName);
    //     }
    // }
}
