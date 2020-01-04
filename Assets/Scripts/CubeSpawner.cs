using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    
    public GameObject cubePrefab;

    public MovingCube SpawnCube()
    {
        GameObject cube = Instantiate(cubePrefab);
        transform.position = new Vector3(transform.position.x, transform.position.y + cubePrefab.transform.localScale.y, transform.position.z);
        Debug.Log("Prefab Y Position = " + cube.transform.position.y);
        if(MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("Start"))
        {
            //cube.transform.position = new Vector3(transform.position.x, MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y, transform.position.z);
        }
        else
        {
            cube.transform.position = transform.position;
        }
        //Debug.Log("asdasdasd");

        return cube.GetComponent<MovingCube>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}