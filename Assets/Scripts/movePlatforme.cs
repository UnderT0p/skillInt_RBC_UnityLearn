using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatforme : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform point;
    [SerializeField] private float radiusMotionPlatform;
    [SerializeField] private bool direction;

    private bool MoveUp;
    private float activeCoordinatePoint;
    private Vector2 vectorPlus;
    private Vector2 vectorMinus;
    private float activePoint;
    // Start is called before the first frame update
    void Start()
    {
        MoveUp = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
        {
            activeCoordinatePoint = transform.position.y;
            activePoint = point.position.y;
            vectorPlus = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            vectorMinus = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
        else
        {
            activeCoordinatePoint = transform.position.x+1.17f;
            activePoint = point.position.x;
            vectorPlus = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            vectorMinus = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
        if (activeCoordinatePoint > activePoint + radiusMotionPlatform)
        {
            MoveUp = false;
        }
        else if (activeCoordinatePoint < activePoint - radiusMotionPlatform)
        {
            MoveUp = true;
        }

        if (MoveUp)
        {
            transform.position = vectorPlus;
        }
        else
        {
            transform.position = vectorMinus;
        }
    }
   
}
