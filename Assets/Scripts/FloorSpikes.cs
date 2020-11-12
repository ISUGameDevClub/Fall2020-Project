using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpikes : MonoBehaviour
{
    public float timeBetweenActivation;
    public float timeActive;
    private Collider2D col;
    public Sprite frame1;
    public Sprite frame2;
    public Sprite frame3;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Active()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenActivation);
            col.enabled = true;
            yield return new WaitForSeconds(timeActive);
            col.enabled = false;
        }
    }
    public void ActivateSpikes()
    {
        StartCoroutine(Active());
    }
    
    public void DeactivateSpikes()
    {
        StopAllCoroutines();
        col.enabled = false;
        
    }


}
