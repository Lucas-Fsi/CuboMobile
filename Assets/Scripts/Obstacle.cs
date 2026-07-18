using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public ParticleSystem particleDestruction;

    [Header("Cinemachine")]
    private CinemachineImpulseSource _impulseSource;
    private PlayerController player;

    private void Start()
    {
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        player = FindAnyObjectByType<PlayerController>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            ParticleSystem particle = Instantiate(particleDestruction, transform.position, Quaternion.identity);
            var distance = Vector3.Distance(transform.position, player.transform.position);
            var force = 1 / distance;
            _impulseSource.GenerateImpulse(force);
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
