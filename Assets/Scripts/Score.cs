
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Snake snake;
    public Text scoreText;
    public Text maxScoreText;

    private static readonly string scoreStr = "SCORE: ";
    private static readonly string maxScoreStr = "MAX SCORE: ";

    void Update()
    {
        scoreText.text = scoreStr + snake.CurrentScore.ToString();
        maxScoreText.text = maxScoreStr + snake.MaxScore.ToString();
    }
}
