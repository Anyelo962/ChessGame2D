using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    int matrixX;
    int matrixY;


    //false: movement and true equal attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            //Change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().getPosition(matrixX, matrixY);

            Destroy(cp);
        }

        controller.GetComponent<Game>().setPositionEmpty(reference.GetComponent<Chessman>().getBoardX(),
            reference.GetComponent<Chessman>().getBoardY());


        reference.GetComponent<Chessman>().setBoardX(matrixX);
        reference.GetComponent<Chessman>().setBoardY(matrixY);
        reference.GetComponent<Chessman>().setCoords();


        controller.GetComponent<Game>().setPosition(reference);
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }

    public void setCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void setReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject getReference()
    {
        return reference;
    }

}
