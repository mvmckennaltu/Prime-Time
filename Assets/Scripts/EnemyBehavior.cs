using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for patrolling
    public float waypointThreshold = 1f; // Distance threshold to consider waypoint reached
    public float playerDetectionRange = 5f; // Range to detect the player
    public float chaseSpeed = 3f; // Speed when chasing the player
    public float patrolSpeed = 1.5f; // Speed when patrolling

    private int currentWaypointIndex = 0;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    public int enemyHP = 10;
    public int attackPower = 10;
    private HealthManager healthManager;
    private MoveProjectile projectile;
    private Renderer meshRenderer;
    private AudioSource audioSource;
    public AudioClip hitClip;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent.speed = patrolSpeed;
        GoToNextWaypoint();
        meshRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance <= waypointThreshold)
        {
            GoToNextWaypoint();
        }

        if (Vector3.Distance(transform.position, playerTransform.position) <= playerDetectionRange)
        {
            ChasePlayer();
        }
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        navMeshAgent.speed = patrolSpeed;
        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    private void ChasePlayer()
    {
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.SetDestination(playerTransform.position);
    }
  public void EnemyTakeDamage(int damage)
    {
        enemyHP = enemyHP - damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death Barrier"))
        {
            enemyHP = 0;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.TakeDamage(attackPower);
        }
        if (collision.gameObject.CompareTag("Beam") || collision.gameObject.CompareTag("Missile"))
        {
            Destroy(collision.gameObject);
            projectile = collision.gameObject.GetComponent<MoveProjectile>();
            int damage = projectile.attackDamage;
            StartCoroutine(FlashColor());
            audioSource.PlayOneShot(hitClip);
            EnemyTakeDamage(damage);

        }
    }
    IEnumerator FlashColor()
    {
        Color originalColor = meshRenderer.material.color;
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = originalColor;
    }
}
