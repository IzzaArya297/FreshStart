using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyEnum;

public class InputSource : MonoBehaviour
{

    public PlayerControl player;
    public float deltaDistance, deltaSpawnElectro;
    public Transform source;

    public GameObject electro;

    public int maxLine;

    [HideInInspector]
    public List<Vector3> points = new List<Vector3>();

    [HideInInspector]
    public LineRenderer cable;

    [HideInInspector]
    public Condition inputCondition = Condition.Idle;

    // Start is called before the first frame update
    void Start()
    {
        cable = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (inputCondition == Condition.Taken)
            cable.SetPosition(1, player.transform.position);
        else if (inputCondition == Condition.Bought)
            drawLine();
        //else if (inputCondition == Condition.Connected)
        //    StartCoroutine(SpawnElectro());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!player.currentInput && inputCondition != Condition.Connected || player.currentInput == gameObject)
            {
                inputCondition = Condition.Taken;
                resetLine();
            }
            collision.gameObject.layer = 11;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!player.currentInput && inputCondition != Condition.Connected || player.currentInput == gameObject)
            {
                inputCondition = Condition.Bought;
                player.currentInput = gameObject;
            }
            collision.gameObject.layer = 0;
        }
    }

    void resetLine()
    {
        points.Clear();
        cable.positionCount = 2;
        cable.SetPosition(0, source.position);
        cable.SetPosition(1, source.position);
        points.Add(source.position);
        points.Add(source.position);
    }

    void drawLine()
    {
        if (DistanceToLastPoint(player.transform.position) > deltaDistance)
        {
            if (points.Count > maxLine)
            {
                inputCondition = Condition.Idle;
                resetLine();
                return;
            }
            points.Add(player.transform.position);
            cable.positionCount = points.Count;
            cable.SetPositions(points.ToArray());
        }
    }

    float DistanceToLastPoint(Vector3 point)
    {
        return Vector3.Distance(points.Last(), point);
    }

    public void SpawnElectro()
    {
        Electro go = Instantiate(electro, source.position, Quaternion.identity).GetComponent<Electro>();
        go.inputPath = this;

    }
}
