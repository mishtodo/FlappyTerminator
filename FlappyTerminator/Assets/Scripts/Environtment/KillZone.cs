using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<SpawnableObject>(out SpawnableObject spawnable))
        {
            spawnable.NotifyDying();
        }
    }
}