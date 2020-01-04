using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MovingCube[] CurrentCube;
    public CubeSpawner[] cubeSpawner;

    public Transform previouslyCube;

    public static GameManager Instance { get; private set; }

    private int count = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(count == 0)
        {
            CurrentCube[count].transform.position += Vector3.right * (Time.deltaTime * CurrentCube[count].moveSpeed);
        }
        else
        {
            CurrentCube[count].transform.position += transform.forward * (Time.deltaTime * CurrentCube[count].moveSpeed);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if(CurrentCube[count] != null)
            {
                CurrentCube[count].Stop();
                previouslyCube = CurrentCube[count].transform;
            }
            //Debug.Log("Local Scale = " + currentCube[count].currentCube.transform.localScale);
           // cubeSpawner[count].cubePrefab = currentCube[count].currentCube.gameObject;

            CurrentCube[count] = cubeSpawner[count].SpawnCube();

            foreach (var spawnpoint in cubeSpawner)
                spawnpoint.SpawnerGoingUp();

            CurrentCube[count].SetMoveSpeed(1f);
            count++;

            if (count >= 2)
                count = 0;
        }
    }
}
