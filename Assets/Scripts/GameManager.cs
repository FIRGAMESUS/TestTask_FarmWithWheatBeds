using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using UnityEngine.Tilemaps;
using TMPro;

public class GameManager : MonoBehaviour
{

    [Header("Изометрическая сетка")]
    [SerializeField] private Transform IsometricGrid;
    [SerializeField] private GameObject IsometricTile;
    [SerializeField] private Tilemap Tilemap;
    [SerializeField] private int deltaX;
    [SerializeField] private int deltaY;
    [SerializeField] private const int step = 1;

    [Header("Грядка")]
    [SerializeField] private GameObject GroundPrefab;
    [SerializeField] public GameObject WheatPrefab;
    [SerializeField] private const int limitGround = 20;
    [SerializeField] public TMP_Text GroundNowText, WheatNowText;

    private int _countGround, _countWheat;
    private int CountGround
    {
        get => _countGround;
        set
        {
            _countGround = value;
            GroundNowText.text = value.ToString() + "/" + limitGround;
        }
    }
    public int CountWheat
    {
        get => _countWheat;
        set
        {
            _countWheat = value;
            WheatNowText.text = value.ToString();
        }
    }


    private void Start()
    {
        Tilemap = FindObjectOfType<Tilemap>();
        deltaX = 10; deltaY = 6;
        SetIsometricGrid();

        CountGround = 0;
    }
    public void SetNewGround()
    {
        if (CountGround < limitGround)
        {
            CountGround += 1;
            GameObject _currentGround = Instantiate(GroundPrefab, IsometricGrid.GetChild(1));
            _currentGround.GetComponent<Ground>().id = CountGround - 1;
        }
    }

    public void SetIsometricGrid()
    {
        for (float i = -1 * deltaX; i <= deltaX; i += step)
        {
            for (float j = -1 * deltaY; j <= deltaY; j += step)
            {
                GameObject Tile = Instantiate(IsometricTile, IsometricGrid.GetChild(0));
                Tile.transform.position = Tilemap.CellToWorld(Tilemap.WorldToCell(new Vector2(i, j)));
                Tile.name = "Tile " + Tile.transform.position;
            }
        }
    }


}
