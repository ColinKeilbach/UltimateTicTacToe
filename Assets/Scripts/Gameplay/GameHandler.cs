using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Tile;

public class GameHandler : MonoBehaviour
{
    private TileGrid fullBoard;
    private Board board; // for saving data
    private bool freeMove = true;
    private Vector2Int targetSubGrid = Vector2Int.zero;
    private Vector2Int lastTargetSubGrid = Vector2Int.zero;
    private TextMeshProUGUI currentTurnTextMesh;

    [SerializeField]
    private GameObject xPlayerThinking;
    [SerializeField]
    private GameObject oPlayerThinking;
    [SerializeField]
    private MoveHistoryView mhv;

    private List<Vector4Int> moveHistory = new();

    private Color defaultColor;

    // Start is called before the first frame update
    void Awake()
    {
        fullBoard = GameObject.Find("Full Board").GetComponent<TileGrid>();
        board = new();
        defaultColor = fullBoard.GetComponent<Image>().color;
        currentTurnTextMesh = GameObject.Find("Current Move").GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (board.GetMovesMade() > 0)
            SetBoard(new());
    }

    public List<Vector4Int> GetMoveHistory() => moveHistory;

    public Board GetBoard() => board.Clone() as Board;
    public void SetBoard(Board board)
    {
        // Stop highlighting last grid
        Image last = fullBoard.GetTile(targetSubGrid).GetComponent<Image>();
        last.color = defaultColor;

        this.board = board;
        moveHistory.Clear();
        DrawFullGrid();
    }

    public Vector4Int TileToCoord(Tile tile)
    {
        TileGrid targetParent = tile?.transform.parent?.GetComponent<TileGrid>();

        if (targetParent != null)
        {
            Vector2Int targetCoord = targetParent.GetCoord(tile);
            Vector2Int parentCoord = fullBoard.GetCoord(targetParent);

            return new Vector4Int(parentCoord, targetCoord);
        }

        return new Vector4Int(-1, -1, -1, -1);
    }

    public bool Move(Vector4Int coord) => Move(coord, board.GetXToMove() ? Tile.Value.X : Tile.Value.O);
    public bool Move(Tile tile, Tile.Value value) => Move(TileToCoord(tile), value);

    public bool Move(Vector4Int coord, Tile.Value value) => Move(coord.x, coord.y, coord.z, coord.w, value);
    public bool Move(int x, int y, int z, int w, Tile.Value value)
    {
        if (x == -1 || y == -1 || z == -1 || w == -1) return false; // Tile was not found

        if (!freeMove && new Vector2Int(x, y) != targetSubGrid) return false; // Not in correct grid

        TileGrid tg = fullBoard.GetTile(x, y) as TileGrid;

        if (tg.GetValue() != Tile.Value.Blank) return false; // Grid already set

        if (tg.GetTileValue(z, w) != Tile.Value.Blank) return false; // Tile already set

        TileGrid grid = fullBoard.GetTile(x, y) as TileGrid;
        Tile t = grid.GetTile(z, w).GetComponent<Tile>();
        t.SetValue(value);
        board.Move(x, y, z, w);

        // Update grids
        int v = board.GetValue(x, y);
        if (v != 0)
            grid.UpdateGrid(v);
        v = board.GetValue();
        if (v != 0)
            fullBoard.UpdateGrid(v);
        UpdateTargetGrid(z, w);
        UpdateVisuals();

        // add move to list
        Vector4Int move = new(x, y, z, w);
        moveHistory.Add(move);
        mhv.AddMove(move);

        return true;
    }

    private void UpdateTargetGrid(int x, int y)
    {
        TileGrid nextGrid = fullBoard.GetTile(x, y) as TileGrid;

        // Update target grid
        if (nextGrid.GetValue() != Value.Blank || board.GetBlanks(x, y) == 0)
        {
            // free move
            freeMove = true;
            lastTargetSubGrid = targetSubGrid;
        }
        else
        {
            // move to next target grid
            lastTargetSubGrid = targetSubGrid;
            targetSubGrid = new Vector2Int(x, y);
            freeMove = false;
        }
    }

    private void UpdateVisuals()
    {
        // Stop highlighting last grid
        Image last = fullBoard.GetTile(lastTargetSubGrid).GetComponent<Image>();
        last.color = defaultColor;

        // Highlight next grid if it is not a free move
        if (!freeMove)
        {
            Image current = fullBoard.GetTile(targetSubGrid).GetComponent<Image>();
            current.color = Color.yellow;
        }

        // Display who's turn it is
        bool xToMove = board.GetXToMove();
        currentTurnTextMesh.text = xToMove ? "X" : "O";

        if (!board.IsGameOver())
        {
            xPlayerThinking.SetActive(xToMove);
            oPlayerThinking.SetActive(!xToMove);
        }
        else
        {
            xPlayerThinking.SetActive(false);
            oPlayerThinking.SetActive(false);
        }
    }

    private void DrawFullGrid()
    {
        int v;

        fullBoard.SetValue(IntToValue(board.GetBoardValue()));

        // draws the grid
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
            {
                TileGrid grid = fullBoard.GetTile(x, y) as TileGrid;

                // Sub Grids
                for (int z = 0; z < 3; z++)
                    for (int w = 0; w < 3; w++)
                    {
                        v = board.GetValueFromSubboard(x, y, z, w);

                        Tile t = grid.GetTile(z, w).GetComponent<Tile>();
                        t.SetValue(IntToValue(v));
                    }

                // Main Grid
                v = board.GetValue(x, y);
                grid.SetValue(IntToValue(v));
            }

        // sets up the turn
        freeMove = board.GetFreeMove();
        if (!freeMove)
        {
            targetSubGrid = board.GetTargetBoard();
            UpdateTargetGrid(targetSubGrid.x, targetSubGrid.y);
        }

        UpdateVisuals();
    }
}
