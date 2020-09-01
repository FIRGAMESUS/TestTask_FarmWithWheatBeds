using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Zenject;

public class Ground : MonoBehaviour
{
    [Inject] GameManager GameManager;

    public Tilemap map;
    private Vector2 mousePosition;
    private Vector3 worldPosition;

    public int id;
    public bool withWheat;

    private void Start()
    {
        map = FindObjectOfType<Tilemap>();
        withWheat = false;
        CheckOthers();
    }

    private void OnMouseDown()
    {

    }
    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        Vector3Int worldToCell = map.WorldToCell(worldPosition);
        transform.position = map.CellToWorld(worldToCell);

    }
    private void OnMouseUp()
    {
        CheckOthers();
    }

    private void CheckOthers()
    {
        Ground[] Grounds = FindObjectsOfType<Ground>();
        foreach (var i in Grounds)
        {
            if (id != i.id)
            {
                if (transform.position == i.transform.position)
                {
                    transform.position += Vector3.up;
                    Debug.Log("Object is moved up");
                    CheckOthers();
                }
            }
        }
        GetComponent<SpriteRenderer>().sortingOrder = (int)(-2 * transform.position.y);
    }
}
