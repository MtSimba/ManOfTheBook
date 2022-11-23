using UnityEngine;

public class teleporter : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        Player.transform.SetPositionAndRotation(teleportTarget.transform.position, Quaternion.LookRotation(teleportTarget.transform.up));
        }
}