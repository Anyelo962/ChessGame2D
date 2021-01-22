using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private string Player;

    //References for all the sprite that the chesspiece can be.
    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;

    public void activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        setCoords();

        switch (this.name)
        {
            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black_queen;
                Player = "black";
                break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knight;
                Player = "black";
                break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop;
                Player = "black";
                break;
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = black_king;
                Player = "black";
                break;
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook;
                Player = "black";
                break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn;
                Player = "black";
                break;

                //White
            case "white_queen":
                this.GetComponent<SpriteRenderer>().sprite = white_queen;
                Player = "white";
                break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knight;
                Player = "white";
                break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop;
                Player = "white";
                break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king;
                Player = "white";
                break;
            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook;
                Player = "white";
                break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn;
                Player = "white";
                break;
        }
    }

    public void setCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        this.transform.position = new Vector3(x, y, -1.0f);

    }

    public int getBoardX()
    {
        return xBoard;
    }

    public int getBoardY()
    {
        return yBoard;
    }

    public void setBoardX(int x)
    {
        xBoard = x;
    }

    public void setBoardY(int y)
    {
        yBoard = y;
    }

    public void OnMouseUp()
    {
        DestroyMovePlates();

        InitiateMovePlates();
    }
    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 1);
                LineMovePlate(1, 1);
                LineMovePlate(1, 1);
                LineMovePlate(1, 1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
        }
    }

    public void LineMovePlate(int incrementX, int incrementY)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + incrementX;
        int y = yBoard + incrementX;

        while (sc.positionOnBoard(x, y) && sc.getPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += incrementX;
            y += incrementY;
        }
        if (sc.positionOnBoard(x, y) && sc.getPosition(x, y).GetComponent<Chessman>().Player != Player)
        {
            MovePlateAttackSpawn(x, y);
        }


    }
    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 2);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);




    }

        public void PointMovePlate(int x, int y)
        {
            Game sc = controller.GetComponent<Game>();
            if (sc.positionOnBoard(x, y))
            {
                GameObject cp = sc.getPosition(x, y);

                if (cp == null)
                {
                    MovePlateSpawn(x, y);

                }
                else if (cp.GetComponent<Chessman>().Player != Player)
                {
                    MovePlateAttackSpawn(x, y);
                }
            }
        }
    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.getPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
        }

        if (sc.positionOnBoard(x + 1, y) && sc.getPosition(x + 1, y) != null && sc.getPosition(x + 1, y).
            GetComponent<Chessman>().Player != Player)
        {
            MovePlateAttackSpawn(x - 1, y);
        }

        if (sc.positionOnBoard(x - 1, y) && sc.getPosition(x - 1, y) != null && sc.getPosition(x - 1, y).
            GetComponent<Chessman>().Player != Player)
        {
            MovePlateAttackSpawn(x - 1, y);
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY, bool isAttack = false)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScipt = mp.GetComponent<MovePlate>();
        mpScipt.setReference(gameObject);
        mpScipt.setCoords(matrixX, matrixY);

    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScipt = mp.GetComponent<MovePlate>();
        mpScipt.setReference(gameObject);
        mpScipt.setCoords(matrixX, matrixY);

    }
}