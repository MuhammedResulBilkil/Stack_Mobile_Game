using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public float moveSpeed = 1f;
    [HideInInspector]
    public MovingCube currentCube;

    [HideInInspector]
    public MovingCube startCube;

    private Renderer cubeRendererColor;

    private void Awake()
    {
        if (startCube == null)
            startCube = GameObject.Find("Start").GetComponent<MovingCube>();

        currentCube = GetComponent<MovingCube>();
        cubeRendererColor = GetComponent<Renderer>();

        cubeRendererColor.material.color = GetRandomColor();

        transform.localScale = new Vector3(startCube.transform.localScale.x, transform.localScale.y, startCube.transform.localScale.z);

    }

    private Color GetRandomColor()
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);
        return new Color(r, g, b);
    }

    void Update()
    {
        //transform.position += transform.forward * (Time.deltaTime * moveSpeed);
        // Debug.LogError(currentCube.gameObject.name);
    }

    public void Stop()
    {
        moveSpeed = 0f;
        //Debug.Log("Speed " + moveSpeed + " " + gameObject.name);
        //Debug.Log("Moving Cube Position.z = " + transform.position.z);
        //Debug.Log("Last Cube Position.z = " + startCube.transform.position.z);
        //Time.timeScale = 0f;
        float hangOverZ = transform.position.z - startCube.transform.position.z;
        float hangOverX = transform.position.x - startCube.transform.position.x;
        //float direction = hangOver > 0 ? 1f : -1f;
        //Debug.Log(hangOver);


        if (gameObject.CompareTag("MovingCubeLeft"))
            SplitCubeOnZ(hangOverZ);
        else
            SplitCubeOnX(hangOverX);
    }

    private void SplitCubeOnX(float hangOver)
    {
        float fallingBlockSizeOnX = startCube.transform.position.x - Mathf.Abs(hangOver);
        float standingCubeSizeOnX = transform.localScale.x - Mathf.Abs(fallingBlockSizeOnX);

        transform.localScale = new Vector3(standingCubeSizeOnX, transform.localScale.y, transform.localScale.z);

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (transform.position.x < 0)
        {
            transform.position = new Vector3(transform.position.x + Mathf.Abs(transform.position.x / 2), transform.position.y, transform.position.z);

            cube.transform.position = new Vector3(transform.position.x + cubeRendererColor.bounds.center.x - standingCubeSizeOnX / 2, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - Mathf.Abs(transform.position.x / 2), transform.position.y, transform.position.z);

            cube.transform.position = new Vector3(transform.position.x + cubeRendererColor.bounds.center.x + standingCubeSizeOnX / 2, transform.position.y, transform.position.z);
        }

        cube.transform.localScale = new Vector3(-fallingBlockSizeOnX, transform.localScale.y, transform.localScale.z);
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = cubeRendererColor.material.color;

        Destroy(cube.gameObject, 1f);
    }

    private void SplitCubeOnZ(float hangOver)
    {
        float fallingBlockSizeOnZ = startCube.transform.position.z - Mathf.Abs(hangOver);
        float standingCubeSizeOnZ = transform.localScale.z - Mathf.Abs(fallingBlockSizeOnZ);

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, standingCubeSizeOnZ);

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (transform.position.z < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Mathf.Abs(transform.position.z / 2));

            cube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cubeRendererColor.bounds.center.z - standingCubeSizeOnZ / 2);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Mathf.Abs(transform.position.z / 2));
            
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cubeRendererColor.bounds.center.z + standingCubeSizeOnZ / 2);
        }

        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -fallingBlockSizeOnZ);
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = cubeRendererColor.material.color;

        Destroy(cube.gameObject, 1f);


    }
    /*  float newZSize = startCube.transform.localScale.z - Mathf.Abs(hangOver);
        float fallingBlockSize = transform.localScale.z - newZSize;


        float newZPosition = startCube.transform.position.z + (hangOver / 2);
        
        //float newZPosition = currentCube.transform.localPosition.z / 2; // Bu da calisiyor.....

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
        */
    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = cubeRendererColor.material.color;
        Destroy(cube.gameObject, 1f);
    }


    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}