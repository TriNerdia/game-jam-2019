using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAndPathHandler : MonoBehaviour
{
    GameObject[] nodes;
    Point[] points;

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject Enemy;

    class Point
    {
        Vector3 m_pos;
        char m_state = 'u';
        float m_score = 0;
        Point m_prevPoint;

        List<Point> m_connectedPoints = new List<Point>();
        List<Point> m_potentialPrevPoints = new List<Point>();
        List<float> m_connectedPointScores = new List<float>();

        public Point(Vector3 pos, char state = 'u')
        {
            m_pos = pos;
            m_state = state;
        }

        public char GetState()
        {
            return m_state;
        }

        public Vector3 GetPos()
        {
            return m_pos;
        }

        public List<Point> GetConnectedPoints()
        {
            return m_connectedPoints;
        }

        public Point GetPrevPoint()
        {
            return m_prevPoint;
        }

        public float GetScore()
        {
            return m_score;
        }

        public List<Point> GetPotentialPrevPoints()
        {
            return m_potentialPrevPoints;
        }

        public List<float> GetConnectedPointScores()
        {
            return m_connectedPointScores;
        }

        public void AddConnectedPoint(Point point)
        {
            m_connectedPoints.Add(point);
        }

        public void AddPotentialPrevPoint(Point point)
        {
            m_potentialPrevPoints.Add(point);
        }

        public void SetPrevPoint(Point point)
        {
            m_prevPoint = point;
        }

        public void SetState(char newState)
        {
            m_state = newState;
        }

        public void SetScore(float newScore)
        {
            m_score = newScore;
        }

        public void AddScore(float newScore)
        {
            m_connectedPointScores.Add(newScore);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        points = new Point[nodes.Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Point(nodes[i].transform.position);
        }

        /***Connect them together***/
        foreach (Point point in points) //Could be optimized to not go through each connection twice
        {
            foreach (Point point2 in points)
            {
                float distance = Vector3.Distance(point.GetPos(), point2.GetPos());
                if (!Physics.Raycast(point.GetPos(), point2.GetPos() - point.GetPos(), distance))
                {
                    //Debug.DrawRay(point.GetPos(), point2.GetPos() - point.GetPos(), Color.white, 1);
                    point.AddConnectedPoint(point2);
                    point.AddScore(distance);
                }
            }
        }

        CalculatePath(Enemy.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Vector3> CalculatePath(Vector3 EnemyPosition)
    {
        //take starting point
        bool firstPoint = true;
        float shortestDistance = 0;
        Point prevPoint = new Point(new Vector3());
        foreach (Point point in points)
        {
            point.SetState('o');
            float distance = Vector3.Distance(EnemyPosition, point.GetPos());
            if (!Physics.Raycast(EnemyPosition, point.GetPos() - EnemyPosition, distance))
            {
                if (firstPoint)
                {
                    point.SetState('e');
                    shortestDistance = distance;
                    prevPoint = point;
                    firstPoint = false;
                }
                else if (distance < shortestDistance)
                {
                    point.SetState('e');
                    shortestDistance = distance;
                    prevPoint.SetState('o');
                    prevPoint = point;
                }

            }
        }

        Debug.Log(shortestDistance);
        Debug.Log(prevPoint.GetPos().x);
        //find player/node nearest player
        firstPoint = true;
        shortestDistance = 0;
        prevPoint = new Point(new Vector3());
        foreach(Point point in points)
        {
            point.SetState('o');
            float distance = Vector3.Distance(Player.transform.position, point.GetPos());
            if (!Physics.Raycast(Player.transform.position, point.GetPos() - Player.transform.position, distance))
            {
                if (firstPoint)
                {
                    point.SetState('e');
                    shortestDistance = distance;
                    prevPoint = point;
                    firstPoint = false;
                }
                else if (distance < shortestDistance)
                {
                    point.SetState('e');
                    shortestDistance = distance;
                    prevPoint.SetState('o');
                    prevPoint = point;
                }

            }
        }

        Debug.Log(shortestDistance);
        Debug.Log(prevPoint.GetPos().x);
        //find path from starting point to player/node nearest player
        List<Vector3> path = new List<Vector3>();
        return path;
    }

}
