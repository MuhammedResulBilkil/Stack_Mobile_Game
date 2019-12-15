using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovingCube CurrentCube;
    public CubeSpawner cubeSpawner;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if(CurrentCube != null)
            {
                CurrentCube.Stop();
            }
                

            CurrentCube = cubeSpawner.SpawnCube();
            CurrentCube.SetMoveSpeed(1f);
        }
    }
}
