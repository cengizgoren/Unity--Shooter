using UnityEngine;

public class TextController : MonoBehaviour
{
    private readonly float destroyTime = 3.0f;

    public Vector3 offset = new Vector3(0, 0.5f, 0);
   
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.position += offset;
    }
}
