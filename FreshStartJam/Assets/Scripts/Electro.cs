using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electro : MonoBehaviour
{
    //Set speed
    public float startSpeed, maxSpeed = 0.2f;
    float speed;
    //Increasing value for lerp
    float moveSpeed;
    //Linerenderer's position index
    int indexNum;
    

    public InputSource inputPath;

    // Start is called before the first frame update
    void Start()
    {
        setNormalSpeed();
    }

    void FixedUpdate()
    {
        MovePosition();
    }

    private void Update()
    {
        if (inputPath.points[inputPath.points.Count - 1] == transform.position)
        {
            inputPath.lineCreated = true;

            Destroy(gameObject);
        }
    }

    void MovePosition()
    {
        
        //round lerp value down to int
        indexNum = Mathf.FloorToInt(moveSpeed);
        //increase lerp value relative to the distance between points to keep the speed consistent.
        moveSpeed += speed / Vector3.Distance(inputPath.points[indexNum + 1], inputPath.points[indexNum + 2]);
        //and lerp
        transform.position = Vector3.Lerp(inputPath.points[indexNum + 1], inputPath.points[indexNum + 2], moveSpeed - indexNum);
    }

    public void setNormalSpeed()
    {
        speed = (startSpeed + (maxSpeed - startSpeed) * inputPath.points.Count / inputPath.maxLine) * inputPath.multiplier;
    }
}
