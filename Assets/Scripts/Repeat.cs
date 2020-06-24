using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeat : MonoBehaviour
{
    [SerializeField] private float speed = 30;
    
    private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        startPos = transform.position;

        repeatWidth = GetComponent<BoxCollider>().size.z;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z > startPos.z + repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
