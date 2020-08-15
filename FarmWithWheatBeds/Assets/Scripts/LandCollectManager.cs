using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class LandCollectManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Inject] GameManager GameManager;

    [SerializeField] private bool isLand;
    Camera cam;
    Vector2 mousePos, startPos, startSize;
    Ground _ground;
    Wheat _wheat;

    private void Start()
    {
        cam = Camera.main;
        startPos = transform.position;
        startSize = transform.localScale;
    }

    private void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localScale /= 2;
    }

    public void OnDrag(PointerEventData eventData)
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.transform != null && hit.transform.GetComponent<Ground>())
        {
            _ground = hit.transform.GetComponent<Ground>();
            if (isLand && !_ground.withWheat)
            {
                Instantiate(GameManager.WheatPrefab, _ground.transform).GetComponent<Wheat>();
                _ground.withWheat = true;
            }
            else if (!isLand && _ground.transform.childCount > 0 && _ground.transform.GetChild(0).GetComponent<Wheat>().isComplete && _ground.withWheat)
            {
                _ground.transform.GetChild(0).GetComponent<Wheat>().isMove = true;
                _ground.withWheat = false;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = startPos;
        transform.localScale = startSize;
    }


}
