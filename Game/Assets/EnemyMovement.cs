using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject Player;

    List<Vector3> pathToFollow;

    NodeAndPathHandler nodeAndPathHandler;

    Vector3 lastKnownPlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        nodeAndPathHandler = GameObject.Find("PathHandler").GetComponent<NodeAndPathHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromWalls = 0.8f;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(-distanceFromWalls, 0, 0), out hit, distanceFromWalls))
        {
            if (hit.transform.tag != "Player")
            {
                float resistPower = 0.1f - hit.distance / (distanceFromWalls * 10);
                transform.position += new Vector3(resistPower, 0, 0);
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(distanceFromWalls, 0, 0), out hit, distanceFromWalls))
        {
            if (hit.transform.tag != "Player")
            {
                float resistPower = 0.1f - hit.distance / (distanceFromWalls * 10);
                transform.position += new Vector3(-resistPower, 0, 0);
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(0, 0, distanceFromWalls), out hit, distanceFromWalls))
        {
            if (hit.transform.tag != "Player")
            {
                float resistPower = 0.1f - hit.distance / (distanceFromWalls * 10);
                transform.position += new Vector3(0, 0, -resistPower);
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(0, 0, -distanceFromWalls), out hit, distanceFromWalls))
        {
            if (hit.transform.tag != "Player")
            {
                float resistPower = 0.1f - hit.distance / (distanceFromWalls * 10);
                transform.position += new Vector3(0, 0, resistPower);
            }
        }

        if (Physics.Raycast(transform.position, new Vector3(distanceFromWalls, 0, distanceFromWalls), out hit, distanceFromWalls))
        {
            if (hit.transform.tag != "Player")
            {
                float resistPower = 0.1f - hit.distance / (distanceFromWalls * 10);
                transform.position += new Vector3(-resistPower, 0, -resistPower);
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(distanceFromWalls, 0, -distanceFromWalls), out hit, distanceFromWalls))
        {
            if (hit.transform.tag != "Player")
            {
                float resistPower = 0.1f - hit.distance / (distanceFromWalls * 10);
                transform.position += new Vector3(-resistPower, 0, resistPower);
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(-distanceFromWalls, 0, distanceFromWalls), out hit, distanceFromWalls))
        {
            if (hit.transform.tag != "Player")
            {
                float resistPower = 0.1f - hit.distance / (distanceFromWalls * 10);
                transform.position += new Vector3(resistPower, 0, -resistPower);
            }
        }
        if (Physics.Raycast(transform.position, new Vector3(-distanceFromWalls, 0, -distanceFromWalls), out hit, distanceFromWalls))
        {
            if (hit.transform.tag != "Player")
            {
                float resistPower = 0.1f - hit.distance / (distanceFromWalls * 10);
                transform.position += new Vector3(resistPower, 0, resistPower);
            }
        }

        //Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.white, 0.5f);
        if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out hit, Vector3.Distance(transform.position, Player.transform.position)) && hit.transform.tag == "Player")
        {
            pathToFollow = new List<Vector3>() { Player.transform.position };
        }
        else
        {
            if (pathToFollow == null || pathToFollow.Count == 0)// || nodeAndPathHandler.NodeNearestPlayer() != lastKnownPlayerPosition)
            {
                Debug.Log("updating path");
                pathToFollow = nodeAndPathHandler.CalculatePath(transform.position);
                //lastKnownPlayerPosition = pathToFollow[pathToFollow.Count - 1];
            }
            //Debug.Log(pathToFollow.Count);
            //foreach (Vector3 point in pathToFollow)
            //{
                //Debug.Log(point.x + ", " + point.y);
            //}
        }

        transform.Translate(Vector3.Normalize(pathToFollow[0] - transform.position) * Time.deltaTime * 2);
        if (Vector3.Distance(transform.position, pathToFollow[0]) < 0.1f)
            pathToFollow.RemoveAt(0);
    }
}
