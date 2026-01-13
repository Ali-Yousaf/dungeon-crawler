using System.Runtime.CompilerServices;
using Unity.Cinemachine;
using UnityEngine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    [SerializeField] Direction direction;
    CinemachineConfiner2D confiner;

    [SerializeField] private float directionMoveValue = 2.5f;

    enum Direction { Up, Down, Left, Right }

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundry;
            UpdatePlayerPos(collision.gameObject);
        }
    }

    private void UpdatePlayerPos(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        { 
            case Direction.Up:
                newPos.y += directionMoveValue;
                break;

            case Direction.Down:
                newPos.y -= directionMoveValue;
                break;

            case Direction.Left:
                newPos.x += directionMoveValue;
                break;

            case Direction.Right:
                newPos.x -= directionMoveValue;
                break;

        }

        player.transform.position = newPos;
    }
}
