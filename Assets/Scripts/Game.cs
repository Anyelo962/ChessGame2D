using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject chessPiece;
    private GameObject[,] position = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(chessPiece,new Vector3(-0.29f,-2.29f,0), Quaternion.identity);        

        playerWhite = new GameObject[]
        {
            //Column 0 
            Create("white_rook", 0,0), Create("white_knight",1,0),
            Create("white_bishop",2,0),Create("white_queen",3,0),Create("white_king",4,0),
            Create("white_bishop",5,0),Create("white_knight",6,0),Create("white_rook",7,0),
            //column 1
            Create("white_pawn",0,1), Create("white_pawn",1,1),Create("white_pawn",2,1),
            Create("white_pawn",3,1), Create("white_pawn",4,1),Create("white_pawn",5,1),
            Create("white_pawn",6,1),Create("white_pawn",7,1)
        };

        playerBlack = new GameObject[]
        {
            Create("black_rook",0,7),Create("black_knight",1,7),Create("black_bishop",2,7),
            Create("black_queen",3,7),Create("black_king",4,7),Create("black_bishop",5,7),
            Create("black_knight",6,7),Create("black_rook",7,7),Create("black_bishop",5,7),
            Create("black_knight",6,7), Create("black_rook",7,7),
            
            Create("black_pawn",0,6),
            Create("black_pawn",1,6),Create("black_pawn",2,6),Create("black_pawn",3,6),
            Create("black_pawn",4,6),Create("black_pawn",5,6),Create("black_pawn",6,6),
            Create("black_pawn",7,6)
        };

        //Set all piece positions on the position board

        for(int i = 0; i < playerBlack.Length; i++)
        {
            setPosition(playerBlack[i]);
            setPosition(playerWhite[i]);
        }
    }


    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chessPiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();

        cm.name = name;
        cm.setBoardX(x);
        cm.setBoardY(y);
        cm.activate();

        return obj;
    }

    public void setPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        position[cm.getBoardX(), cm.getBoardY()] = obj;
    }

    public void setPositionEmpty(int x, int y)
    {
        position[x, y] = null;
    }

    public GameObject getPosition(int x, int y)
    {

        return position[x, y];
    }
    public bool positionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= position.GetLength(0) || y >= position.GetLength(1))
            return false;
        return true;
    }
}
