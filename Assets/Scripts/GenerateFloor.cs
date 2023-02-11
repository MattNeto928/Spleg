using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GenerateFloor : NetworkBehaviour
{

    public GameObject tiles;
    public Transform barrier;

    public int layers = 4;

    Vector3 barrierScale;
    Vector3 barrierPosition;
    Vector3 barrierRotation;

    float[] barrierRotationY;
    float[] barrierRotationZ;
    

    //Vectors that change the size and positions of the BARRIERS 
    private Vector3[] scaleVector;
    private Vector3[] posVector;

    public float side = 20;

    private float[] barrierLenghts;


    // Start is called before the first frame update
    void Start()
    {
        barrierAttributes();

        for (int y = 1; y > -layers*100 + 200; y -= 100)
        {
            for (int i = 0; i < side; i++)
            {
                for (int k = 0; k < side; k++)
                {
                    Debug.Log("repeating");
                    GameObject inst = Instantiate(tiles, new Vector3(10.5f * i, y, 10.5f * k), Quaternion.identity);
                    NetworkServer.Spawn(inst);
                }
            }
        }


        for (int i = 0; i < 4; i++)
        {
            barrier.localScale += scaleVector[i];
            Instantiate(barrier, posVector[i], Quaternion.Euler(0, barrierRotationY[i], barrierRotationZ[i]));
            barrier.localScale -= scaleVector[i];
        }




    }

    //Determines attributes (ie. position and scale values) of the barrier
    void barrierAttributes()
    {
        //barrier scales
        scaleVector = new Vector3[4];
        scaleVector[0] = new Vector3(layers * 10, 0, side);
        scaleVector[1] = new Vector3(layers * 10, 0, side);
        scaleVector[2] = new Vector3(layers * 10, 0, side);
        scaleVector[3] = new Vector3(layers * 10, 0, side);

        //barrier positions
        posVector = new Vector3[4];
        posVector[0] = new Vector3(-6f, 0, 10.5f * side / 2 - 5f);
        posVector[1] = new Vector3(10.5f * side - 4f, 0, 10.5f * side / 2 - 5f);
        posVector[2] = new Vector3(10.5f * side / 2 - 5f, 0, 10.5f * side - 6f);
        posVector[3] = new Vector3(10.5f * side / 2 - 5f, 0, -7f);

        //barrier rotations
        barrierRotationY = new float[4];
        barrierRotationY[0] = 0;
        barrierRotationY[1] = 0;
        barrierRotationY[2] = 90;
        barrierRotationY[3] = 90;

        barrierRotationZ = new float[4];
        barrierRotationZ[0] = 90;
        barrierRotationZ[1] = 90;
        barrierRotationZ[2] = 90;
        barrierRotationZ[3] = 90;
    }

}
