using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTestManager : MonoBehaviour
{
    GameHandler gh;

    public int numberOfGamesPlayed = 0;
    public int timesWonX = 0;
    public int timesDraw = 0;
    public int timesWonO = 0;

    private bool waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        gh = FindObjectOfType<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Board b = gh.GetBoard();

        if(!waiting && b.GetPossibleMoves().Count == 0)
        {
            switch (b.GetBoardValue())
            {
                case 0:
                    timesDraw++;
                    break;
                case 1:
                    timesWonX++;
                    break;
                case -1:
                    timesWonO++;
                    break;
            }

            numberOfGamesPlayed++;

            waiting = true;
            StartCoroutine(Restart());
        }
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);

        // new game
        gh.SetBoard(new());
        waiting = false;
    }
}
