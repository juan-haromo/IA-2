using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float minDistance;
    Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.position - transform.position;
        if (direction.magnitude > minDistance)
        {
            transform.Translate(Time.deltaTime * speed * direction.normalized);    
        }
    }
}
