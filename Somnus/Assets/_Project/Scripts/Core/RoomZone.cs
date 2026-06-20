using Cinemachine;
using UnityEngine;

public class RoomZone : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        vcam.Priority = 20;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        vcam.Priority = 10;
    }
}
