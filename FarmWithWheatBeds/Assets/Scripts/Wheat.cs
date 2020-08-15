using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Wheat : MonoBehaviour
{
    GameManager GameManager;
    Vector3 targetSize;
    const float risingTime = 10;
    public bool isComplete = false;

    public bool isMove = false;

    public Vector3 targetPos;


    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        targetPos = GameManager.WheatNowText.transform.position;

        targetSize = transform.localScale;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        StartCoroutine(Rise());
        isMove = false;
    }

    void Update()
    {
        if (isMove) transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * 7);
        if (isMove && Mathf.Abs(transform.position.x - targetPos.x) <= 0.1f && Mathf.Abs(transform.position.y - targetPos.y) <= 0.1f)
        {
            GameManager.CountWheat += 1;
            Destroy(gameObject);
        }
    }

    public IEnumerator Rise()
    {
        while (transform.localScale.x < targetSize.x)
        {
            float temp = risingTime / 1000;
            transform.localScale += new Vector3(temp, temp, temp);
            yield return new WaitForSeconds(risingTime / 100);
        }
        isComplete = true;
    }

}
