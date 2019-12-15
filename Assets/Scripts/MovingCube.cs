using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [HideInInspector]
    public MovingCube CurrentCube;
    public static MovingCube LastCube { get; private set; }

    private Renderer cubeRendererColor;

    private void Awake()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("Start").GetComponent<MovingCube>();

        CurrentCube = GetComponent<MovingCube>();
        cubeRendererColor = GetComponent<Renderer>();

        cubeRendererColor.material.color = GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);

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
        transform.position += transform.forward * (Time.deltaTime * moveSpeed);
       // Debug.LogError(CurrentCube.gameObject.name);
    }

    public void Stop()
    {
        moveSpeed = 0f;
        //Debug.Log("Speed " + moveSpeed + " " + gameObject.name);
        float hangOver = transform.position.z - LastCube.transform.position.z;
        float direction = hangOver > 0 ? 1f : -1f;
        Debug.Log(hangOver);

        SplitCubeOnZ(hangOver, direction);
    }

    private void SplitCubeOnZ(float hangOver, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangOver);
        float fallingBlockSize = transform.localScale.z - newZSize;


        float newZPosition = LastCube.transform.position.z + (hangOver / 2);
        
        //float newZPosition = CurrentCube.transform.localPosition.z / 2; // Bu da calisiyor.....

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);

    }

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