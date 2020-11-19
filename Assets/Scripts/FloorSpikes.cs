using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpikes : MonoBehaviour
{
    public float timeBetweenActivation;
    public float warningTime;
    public float timeActive;
    private Collider2D col;
    public Sprite emptySprite;
    public Sprite armedSprite;
    public Sprite firedSprite;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        col.enabled = false;

        if (warningTime > timeBetweenActivation)
            warningTime = timeBetweenActivation;

        ActivateSpikes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Active()
    {
        while (true)
        {
            sr.sprite = emptySprite;
            yield return new WaitForSeconds(timeBetweenActivation - warningTime - (warningTime / 4));
            sr.sprite = armedSprite;
            yield return new WaitForSeconds(warningTime);
            sr.sprite = firedSprite;
            col.enabled = true;
            yield return new WaitForSeconds(timeActive);
            col.enabled = false;
            sr.sprite = armedSprite;
            yield return new WaitForSeconds(warningTime / 4);
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
        sr.sprite = emptySprite;
    }


}
