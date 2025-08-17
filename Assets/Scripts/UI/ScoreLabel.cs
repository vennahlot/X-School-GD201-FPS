using TMPro;
using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
    }

    void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
    }

    void UpdateScore(float score)
    {
        text.text = ((int)score).ToString();
    }

}
