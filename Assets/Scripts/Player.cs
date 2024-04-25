using UnityEngine;

[System.Serializable]
public class Player
{
    public GameObject tokenPrefab;
    public Color color;
    Vector3[] movesList = new Vector3[27];
    int numberOfMoves = 0;

    public void AddMove(Vector3 move)
    {
        movesList.SetValue(move, numberOfMoves);
        numberOfMoves++;
    }
}
