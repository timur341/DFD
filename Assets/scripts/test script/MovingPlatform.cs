using UnityEngine;
using System.Collections.Generic;

public class MovingPlatformWithRotation : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints;
    public float speed = 2f;

    private Vector3 lastPosition;
    private int currentPoint = 0;
    private Dictionary<Transform, CharacterData> playersOnPlatform = new Dictionary<Transform, CharacterData>();

    void Update()
    {
        // Движение платформы
        Vector3 moveDelta = MovePlatform();

        // Перемещение и поворот всех игроков на платформе
        foreach (var kvp in playersOnPlatform)
        {
            if (kvp.Key != null)
            {
                // 1. Перемещаем игрока
                kvp.Key.position += moveDelta;

                // 2. Поворачиваем в сторону движения (если нужно)
                if (ShouldRotatePlayer(kvp.Key, moveDelta, kvp.Value))
                {
                    RotatePlayer(kvp.Key, moveDelta, kvp.Value);
                }
            }
        }
    }

    Vector3 MovePlatform()
    {
        Vector3 startPos = transform.position;

        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentPoint].position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, waypoints[currentPoint].position) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % waypoints.Length;
        }

        Vector3 delta = transform.position - startPos;
        lastPosition = transform.position;
        return delta;
    }

    bool ShouldRotatePlayer(Transform player, Vector3 moveDelta, CharacterData data)
    {
        return moveDelta.magnitude > 0.01f && data.allowRotation;
    }

    void RotatePlayer(Transform player, Vector3 moveDelta, CharacterData data)
    {
        float direction = Mathf.Sign(moveDelta.x);
        if (direction != 0)
        {
            player.localScale = new Vector3(
                direction * Mathf.Abs(data.originalScale.x),
                data.originalScale.y,
                data.originalScale.z
            );
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            playersOnPlatform[col.transform] = new CharacterData()
            {
                originalScale = col.transform.localScale,
                allowRotation = true // Можно вынести в настройки
            };
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            playersOnPlatform.Remove(col.transform);
        }
    }

    private class CharacterData
    {
        public Vector3 originalScale;
        public bool allowRotation;
    }
}