using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isPlayerBullet;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Якщо це куля гравця і вона попала в сніговика
        if (isPlayerBullet && other.CompareTag("Snowman"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // Якщо це куля ворога і вона попала в Player
        else if (!isPlayerBullet && other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        // Щоб куля не знищувалась об того, хто її випустив
        else if (isPlayerBullet && other.CompareTag("Player"))
        {
            return;
        }
        else if (!isPlayerBullet && other.CompareTag("Snowman"))
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}