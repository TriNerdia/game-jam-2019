using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject Player;

    List<Vector3> pathToFollow;

    [SerializeField]
    NodeAndPathHandler nodeAndPathHandler;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("frame counter");
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
            Debug.Log("Hit player");
            pathToFollow = new List<Vector3>() { Player.transform.position };
        }
        else
        {
            Debug.Log("Missed player");
            List<Vector3> newPath = nodeAndPathHandler.CalculatePath(transform.position);
            if(pathToFollow == null || newPath[newPath.Count-1] != pathToFollow[pathToFollow.Count-1])
                pathToFollow = newPath;

            //Debug.Log(pathToFollow.Count);
            //foreach (Vector3 point in pathToFollow)
            //{
                //Debug.Log(point.x + ", " + point.y);
            //}
        }

        Debug.Log(pathToFollow);

        transform.Translate(Vector3.Normalize(pathToFollow[0] - transform.position) * Time.deltaTime);
        if (Vector3.Distance(transform.position, pathToFollow[0]) < 0.1f)
            pathToFollow.RemoveAt(0);
    }
}
