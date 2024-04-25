using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] playerList;
    private int currentPlayer = 0;
    int numberOfPlayers;
    int[,,] moves = new int[3,3,3] { { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } },
        { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } },
        { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } } };

    // Start is called before the first frame update
    void Start()
    {
        numberOfPlayers = playerList.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayInCell(PlayCell cell)
    {
        GameObject newToken = Instantiate(playerList[currentPlayer].tokenPrefab, cell.transform);
        newToken.transform.position = cell.transform.position;
        newToken.GetComponent<Renderer>().material.color = playerList[currentPlayer].color;
        newToken.GetComponent<Collider>().enabled = false;
        cell.SetToken(newToken);
        SavePlayerMove(cell);
        NextPlayer();
    }

    void NextPlayer()
    {
        currentPlayer = (currentPlayer + 1) % numberOfPlayers;
        Shader.SetGlobalColor("_CurrentPlayerColor", playerList[currentPlayer].color);
    }

    void SavePlayerMove(PlayCell cell)
    {
        playerList[currentPlayer].AddMove(cell.transform.position);
        int x = (int)cell.transform.position.x + 1;
        int y = (int)cell.transform.position.y + 1;
        int z = (int)cell.transform.position.z + 1;
        moves[x, y, z] = currentPlayer;
        DidSomeoneWin();
    }

    void DidSomeoneWin()
    {
        // On vérifie toutes les lignes dans chaque dimension
        int resultat = -1;
        for(int i=0; i<3; i++)
        {
            for(int j=0; j<3; j++)
            {
                resultat = Mathf.Max(resultat, CheckColonne(i, j));
                resultat = Mathf.Max(resultat, CheckLigneX(i, j));
                resultat = Mathf.Max(resultat, CheckLigneZ(i, j));
            }
        }

        // On vérifie les diagonales
        for(int i=0; i<3; i++)
        {
            resultat = Mathf.Max(resultat, CheckFlatDiagonalX(i));
            resultat = Mathf.Max(resultat, CheckFlatDiagonalY(i));
            resultat = Mathf.Max(resultat, CheckFlatDiagonalZ(i));
        }

        resultat = Mathf.Max(resultat, CheckLargeDiagonals());


        if(resultat != -1)
        {
            Debug.Log("Le gagnant est le joueur " + currentPlayer.ToString());
        }
    }

    int CheckColonne(int x, int z)
    {
        if(moves[x, 0, z] == moves[x, 1, z] && moves[x, 1, z] == moves[x, 2, z] && moves[x, 0, z] != -1)
        {
            return moves[x, 0, z];
        }
        return -1;
    }

    int CheckLigneX(int y, int z)
    {
        if (moves[0, y, z] == moves[1, y, z] && moves[1, y, z] == moves[2, y, z] && moves[0, y, z] != -1)
        {
            return moves[0, y, z];
        }
        return -1;
    }
    int CheckLigneZ(int x, int y)
    {
        if (moves[x, y, 0] == moves[x, y, 1] && moves[x, y, 1] == moves[x, y, 2] && moves[x, y, 0] != -1)
        {
            return moves[x, y, 0];
        }
        return -1;
    }

    int CheckFlatDiagonalX(int x)
    {
        if(moves[x, 0, 0] == moves[x, 1, 1] && moves[x, 1, 1] == moves[x, 2, 2] && moves[x, 0, 0] != -1)
        {
            return moves[x, 0, 0];
        }
        else if (moves[x, 0, 2] == moves[x, 1, 1] && moves[x, 1, 1] == moves[x, 2, 0] && moves[x, 0, 2] != -1)
        {
            return moves[x, 0, 2];
        }
        return -1;
    }

    int CheckFlatDiagonalY(int y)
    {
        if (moves[0, y, 0] == moves[1, y, 1] && moves[1, y, 1] == moves[2, y, 2] && moves[0, y, 0] != -1)
        {
            return moves[0, y, 0];
        }
        else if (moves[0, y, 2] == moves[1, y, 1] && moves[1, y, 1] == moves[2, y, 0] && moves[0, y, 2] != -1)
        {
            return moves[0, y, 2];
        }
        return -1;
    }

    int CheckFlatDiagonalZ(int z)
    {
        if(moves[0, 0, z] == moves[1, 1, z] && moves[1, 1, z] == moves[2, 2, z] && moves[0, 0, z] != -1)
        {
            return moves[0, 0, z];
        }
        else if (moves[0, 2, z] == moves[1, 1, z] && moves[1, 1, z] == moves[2, 0, z] && moves[0, 2, z] != -1)
        {
            return moves[0, 2, z];
        }
        return -1;
    }

    int CheckLargeDiagonals()
    {
        if(moves[0, 0, 0] == moves[1, 1, 1] && moves[1, 1, 1] == moves[2, 2, 2] && moves[0, 0, 0] != -1)
        {
            return moves[0, 0, 0];
        }
        else if(moves[2, 0, 0] == moves [1, 1, 1] && moves[1, 1, 1] == moves[0, 2, 2] && moves[2, 0, 0] != -1){
            return moves[2, 0, 0];
        }
        else if (moves[2, 0, 2] == moves[1, 1, 1] && moves[1, 1, 1] == moves[0, 2, 0] && moves[2, 0, 2] != -1)
        {
            return moves[2, 0, 2];
        }
        else if (moves[0, 0, 2] == moves[1, 1, 1] && moves[1, 1, 1] == moves[2, 2, 0] && moves[0, 0, 2] != -1)
        {
            return moves[0, 0, 2];
        }
        return -1;
    }
}
