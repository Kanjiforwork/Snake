using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour

{
    public GameObject cherry1;
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource music;
    private Rigidbody2D rb;
    [SerializeField] private bool go = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetState();
    }
    private void Awake()
    {
        Time.timeScale = 1f;
    }



    // Update is called once per frame
    void Update()
    {
       

        if (go)
        {
            
            if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down || Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector2.down)
            {
                _direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up || Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector2.up)
            {
                _direction = Vector2.down;
            }
            else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right || Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector2.right)
            {
                _direction = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left || Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector2.left)
            {
                _direction = Vector2.right;
            }
        }
}

    private void FixedUpdate()
    {
        if (go)
        {
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].position = _segments[i - 1].position;
            }

            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
            );
        }

    }
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
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

        this.transform.position = Vector3.zero;
        cherry1.GetComponent<RandomPlacement>().score = 0;
        music.Play();
        go = true;
     



    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        } else if (other.tag == "Obstacle")
        {
            go = false;
            music.Stop();
            deathSound.Play();
            Invoke("ResetState",3);
        }
    }
}
