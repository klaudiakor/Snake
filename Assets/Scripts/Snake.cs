
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector2 _direction = Vector2.up;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;
    private int currentScore = 0;
    private int maxScore = 0;


    private void Start()
    {
        ResetState();
    }
    private enum Direction
    {
        Up,
        Down,
        Right,
        Left
    };
    private void RotateAndSetDirection(Direction directionToSet)
    {
        switch (directionToSet)
        {
            case Direction.Up:
                {
                    int z = 90;

                    if (_direction == Vector2.left) { z = -90; }
                    transform.Rotate(0, 0, z);
                    _direction = Vector2.up;
                    break;
                }

            case Direction.Down:
                {
                    int z = 90;
                    if (_direction == Vector2.right) { z = -90; }
                    transform.Rotate(0, 0, z);
                    _direction = Vector2.down;
                    break;
                }
            case Direction.Right:
                {

                    int z = 90;
                    if (_direction == Vector2.up) { z = -90; }
                    transform.Rotate(0, 0, z);
                    _direction = Vector2.right;
                    break;
                }
            case Direction.Left:
                {
                    int z = 90;
                    if (_direction == Vector2.down) { z = -90; }
                    transform.Rotate(0, 0, z);
                    _direction = Vector2.left;
                    break;
                }


            default: break;

        }

    }


    private void Update()
    {

        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateAndSetDirection(Direction.Up);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                RotateAndSetDirection(Direction.Down);
            }

        }
        else if (_direction.y != 0f)
        {

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RotateAndSetDirection(Direction.Left);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                RotateAndSetDirection(Direction.Right);
            }


        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + _direction.x,
            Mathf.Round(transform.position.y) + _direction.y,
            0.0f
        );

    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
        currentScore += 10;
    }

    private void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        // reset position of a head of a snake
        this.transform.position = Vector3.zero;

        SetMaxScore();
        currentScore = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
        }
        else if (other.CompareTag("Obstacle"))
        {
            ResetState();
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
    public int GetMaxScore()
    {
        return maxScore;
    }
    private void SetMaxScore()
    {
        if (currentScore > maxScore)
        {
            maxScore = currentScore;
        }
    }




}
