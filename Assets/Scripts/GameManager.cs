using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player[] playerList;
    private int currentPlayer = 0;
    int numberOfPlayers;

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
    }
}
