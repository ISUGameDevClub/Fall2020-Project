using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float lingerTime = 0.1f;
    void Start()
    {
        Destroy(gameObject, lingerTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
