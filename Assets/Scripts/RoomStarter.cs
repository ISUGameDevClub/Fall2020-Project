using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStarter : MonoBehaviour
{
    public ClosedEnemyDoors ced;
    private void OnTriggerExit2D(Collider2D other)
    {
        ced.ActivateRoom(other);
    }
}
