using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileGrid : Tile
{
    protected List<Tile> tiles = new();

    private void Start()
    {
        foreach (Transform t in transform)
        {
            Tile tile = t.GetComponent<Tile>();

            if (tile != null)
                tiles.Add(tile);
        }

        if (tiles.Count != 9)
            Debug.LogError("TileGrid.tiles.Count != 9: " + tiles.Count);
    }

    public Tile GetTile(Vector2Int coord) => GetTile(coord.x, coord.y);
    public Tile GetTile(int x, int y) => tiles[x + y * 3];
    public Vector2Int GetCoord(Tile tile)
    {
        int index = tiles.IndexOf(tile);
        return new Vector2Int(index % 3, index / 3);
    }
    public Value GetTileValue(Vector2Int coord) => GetTileValue(coord.x, coord.y);
    public Value GetTileValue(int x, int y) => GetTile(x, y).GetValue();
    public void SetTileValue(Value value, Vector2Int coord) => SetTileValue(value, coord.x, coord.y);
    public void SetTileValue(Value value, int x, int y) => GetTile(x, y).SetValue(value);

    public void UpdateGrid(int v) => SetValue(IntToValue(v));
}
