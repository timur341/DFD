using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints; // Массив точек патрулирования
    public float speed = 2f; // Скорость врага
    public float detectionRange = 5f; // Радиус обнаружения игрока
    public float chaseSpeed = 4f; // Скорость при преследовании

    private int currentWaypointIndex = 0;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, player.position));
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            // Преследуем игрока
            ChasePlayer();
            
        }
        else
        {
            // Патрулирование
            Patrol();
        }
    }

    void Patrol()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Смена точки
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }
}
