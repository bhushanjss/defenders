using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: Hit: 0 | Miss: 0";
    }

    public void UpdateScore(int hit, int miss)
    {
        _scoreText.text = "Score: Hit: " + hit.ToString() + " | Miss: " + miss.ToString();
    }
}
