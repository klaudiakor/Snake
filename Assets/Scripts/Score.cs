
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Snake snake;
    public Text scoreText;
    public Text maxScoreText;
    private readonly string scoreStr = "SCORE: ";
    private readonly string maxScoreStr = "MAX SCORE: ";

    void Update()
    {
        scoreText.text = scoreStr + snake.GetCurrentScore().ToString();
        maxScoreText.text = maxScoreStr + snake.GetMaxScore().ToString();
    }
}
