using JetBrains.Annotations;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public ParticleSystem particleDestruction;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            ParticleSystem particle = Instantiate(particleDestruction, transform.position, Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Transform fire = particleDestruction.transform.Find("Fire");

            if (fire != null)
            {
                fire.gameObject.SetActive(false);
            }

        }
        Destroy(gameObject);
    }
}
