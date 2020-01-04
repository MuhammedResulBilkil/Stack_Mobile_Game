using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    
    public GameObject cubePrefab;

    public MovingCube SpawnCube()
    {
        GameObject cube = Instantiate(cubePrefab);
        
        //Debug.Log("Prefab Y Position = " + cube.transform.position.y);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y + cubePrefab.transform.localScale.y, transform.position.z);
        cube.transform.localScale = GameManager.Instance.previouslyCube.localScale;
        //Debug.Log("asdasdasd");

        return cube.GetComponent<MovingCube>();
    }

    public void SpawnerGoingUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + cubePrefab.transform.localScale.y, transform.position.z);

        //cube.transform.position = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}