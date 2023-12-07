using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomPlacement : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public float score;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        RandomizePosition();
    }

    private void Update()
    {
        scoreText.text = "" + score;
    }
    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    private void OnTriggerEnter2D(Collider2D other )
    {
       if (other.tag == "Player")
        {
            RandomizePosition();
            score += 1;
        }
    }
}
