using UnityEngine;

public class teleporter : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject Player;
    private Vector3 telePosition;


    void OnTriggerEnter(Collider other)
    {
        if (teleportTarget.transform.rotation.eulerAngles ==  new Vector3(-90f,0f,0f));
        {
            telePosition = new Vector3(teleportTarget.transform.position.x, teleportTarget.transform.position.y,
                teleportTarget.transform.position.z - 1);
        }
        if (teleportTarget.transform.rotation.eulerAngles == new Vector3(90f, 0f, 0f))
        {
            telePosition = new Vector3(teleportTarget.transform.position.x, teleportTarget.transform.position.y,
                teleportTarget.transform.position.z + 1);
        }

        Debug.LogWarning(telePosition);

        Player.transform.SetPositionAndRotation(telePosition, Quaternion.LookRotation(teleportTarget.eulerAngles));
    }
}