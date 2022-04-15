
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Transform segmentPrefab;

    public const int initialSize = 4;
    private const int ScoreGrowth = 10;

    private readonly List<Transform> _segments = new List<Transform>();
    private Vector2 _direction = Vector2.up;

    public int CurrentScore
    {
        get;
        private set;
    } = 0;

    public int MaxScore
    {
        get;
        private set;
    } = 0;

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
        void transformWith(Vector2 rotationVec, Vector2 diretionVec)
        {
            transform.Rotate(0, 0, _direction == rotationVec ? -90 : 90);
            _direction = diretionVec;
        }

        switch (directionToSet)
        {
            case Direction.Up: transformWith(Vector2.left, Vector2.up); break;
            case Direction.Down: transformWith(Vector2.right, Vector2.down); break;
            case Direction.Right: transformWith(Vector2.up, Vector2.right); break;
            case Direction.Left: transformWith(Vector2.down, Vector2.left); break;
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
        MoveLastToFront();
        MoveHead();
    }

    private void MoveLastToFront()
    {
        Transform last = _segments[_segments.Count - 1];
        _segments.RemoveAt(_segments.Count - 1);
        _segments.Insert(1, last);
        _segments[1].position = _segments[0].position;
    }

    private void MoveHead()
    {
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
        CurrentScore += ScoreGrowth;
    }

    private void ResetState()
    {
        ClearSnake();
        AddSegments();
        ResetHeadPosition();

        CheckMaxScore();
        CurrentScore = 0;
    }

    private void ResetHeadPosition()
    {
        transform.position = Vector3.zero;
    }

    private void AddSegments()
    {
        _segments.Add(transform);
        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(segmentPrefab));
        }
    }

    private void ClearSnake()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
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

    private void CheckMaxScore()
    {
        if (CurrentScore > MaxScore)
        {
            MaxScore = CurrentScore;
        }
    }

}
