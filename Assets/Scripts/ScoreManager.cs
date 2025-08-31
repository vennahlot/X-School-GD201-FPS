using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public float TotalScore { get; private set; } = 0.0f;
    public float TotalTimeToHit { get; private set; } = 0.0f;
    public int NumTargets { get; private set; } = 0;
    
    public static event Action<float> OnScoreChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(float baseScore, float timeToHit = 1)
    {
        TotalScore += baseScore / timeToHit;
        TotalTimeToHit += timeToHit;
        NumTargets += 1;
        OnScoreChanged?.Invoke(TotalScore);
    }

    public float GetAverageTimeToHit()
    {
        return TotalTimeToHit / NumTargets;
    }
}
