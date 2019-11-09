using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAndPathHandler : MonoBehaviour
{
    GameObject[] nodes;
    Point[] points;

    [SerializeField]
    GameObject Player;

    class Point
    {
        Vector3 m_pos;
        //states are: u = unchecked, o = opening (point connected to the start point, or connected to a previous point we're checking), 
        //s = starting, e = ending, c = checked
        char m_state = 'u';
        float m_score = 0;
        Point m_prevPoint;

        List<Point> m_connectedPoints = new List<Point>();
        List<Point> m_potentialPrevPoints = new List<Point>();
        //List<float> m_connectedPointScores = new List<float>();

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

        /*public List<float> GetConnectedPointScores()
        {
            return m_connectedPointScores;
        }*/

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

        /*public void AddScore(float newScore)
        {
            m_connectedPointScores.Add(newScore);
        }*/
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

        /***Connect all the points together***/
        foreach (Point point in points)
        {
            foreach (Point point2 in points)
            {
                float distance = Vector3.Distance(point.GetPos(), point2.GetPos());
                if (!Physics.Raycast(point.GetPos(), point2.GetPos() - point.GetPos(), distance))
                {
                    if(point != point2)
                        point.AddConnectedPoint(point2);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Vector3> CalculatePath(Vector3 EnemyPosition)
    {
        foreach (Point point in points)
            point.SetState('u');

        //take starting point
        bool firstPoint = true;
        float shortestDistance = 0;
        Point prevPoint = new Point(new Vector3());
        foreach (Point point in points)
        {
            float distance = Vector3.Distance(EnemyPosition, point.GetPos());
            RaycastHit hit;
            if (!Physics.Raycast(EnemyPosition, point.GetPos() - EnemyPosition, out hit, distance))
            {
                if (firstPoint)
                {
                    point.SetState('s');
                    shortestDistance = distance;
                    prevPoint = point;
                    firstPoint = false;
                }
                else if (distance < shortestDistance)
                {
                    point.SetState('s');
                    shortestDistance = distance;
                    prevPoint.SetState('u');
                    prevPoint = point;
                }
            }
        }

        Point startPoint = prevPoint;

        //find player/node nearest player
        firstPoint = true;
        shortestDistance = 0;
        prevPoint = new Point(new Vector3());
        foreach(Point point in points)
        {
            if (point.GetState() != 's')
            {
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
                        prevPoint.SetState('u');
                        prevPoint = point;
                    }

                }
            }
        }

        Point endPoint = prevPoint;
        foreach (Point point in startPoint.GetConnectedPoints())
        {
            if (point.GetState() != 'e')
            {
                point.SetPrevPoint(startPoint);
                point.SetState('o');
                point.SetScore(Vector3.Distance(EnemyPosition, point.GetPos()) + Vector3.Distance(Player.transform.position, point.GetPos()));
            }
        }

        //find path from starting point to player/node nearest player
        bool searchedAll = false;
        bool foundEnd = false;

        while (!searchedAll)
        {
            searchedAll = true;
            List<Point> foundConnections = new List<Point>();
            foreach(Point point in points)
            {
                if (point.GetState() == 'o')
                {
                    searchedAll = false;
                    List<Point> potentials = point.GetConnectedPoints();

                    foreach(Point potentialPoint in potentials)
                    {
                        if(potentialPoint.GetState() == 'u')
                        {
                            potentialPoint.AddPotentialPrevPoint(point);
                            foundConnections.Add(potentialPoint);
                            potentialPoint.SetScore(Vector3.Distance(EnemyPosition, point.GetPos()) + Vector3.Distance(Player.transform.position, point.GetPos()));
                        }
                        else if (potentialPoint.GetState() == 'e')
                        {
                            //found the exit
                            foundEnd = true;
                        }
                    }
                    point.SetState('c');
                }
            }
            foreach(Point connection in foundConnections)
            {
                connection.SetState('o');
                //find lowest scoring prev point
                shortestDistance = 0;
                Point bestPrevPoint = null;
                firstPoint = true;
                foreach (Point prevPoints in connection.GetPotentialPrevPoints())
                {
                    if (firstPoint)
                    {
                        shortestDistance = prevPoints.GetScore();
                        bestPrevPoint = prevPoints;
                        firstPoint = false;
                    }
                    else
                    {
                        if(prevPoints.GetScore() < shortestDistance)
                        {
                            shortestDistance = prevPoints.GetScore();
                            bestPrevPoint = prevPoints;
                        }
                    }
                }
                connection.SetPrevPoint(bestPrevPoint);
            }
        }

        if (foundEnd)
        {
            //trace back to find the shortest route
            List<Point> shortestRoute = null;
            float lowestScore = 0;
            bool firstRoute = true;


            foreach (Point point in endPoint.GetConnectedPoints())
            {
                float score = 0;
                bool tracing = true;
                Point currPoint = point;
                List<Point> route = new List<Point>();
                route.Add(endPoint);
                while (tracing)
                {
                    route.Add(currPoint);
                    if (currPoint.GetState() == 's')
                    {
                        if (firstRoute)
                        {
                            shortestRoute = route;
                            lowestScore = score;
                            firstRoute = false;
                        }
                        else
                        {
                            if (score < lowestScore)
                            {
                                shortestRoute = route;
                                lowestScore = score;
                            }
                        }
                        tracing = false;
                        break;
                    }
                    score += currPoint.GetScore();
                    currPoint = currPoint.GetPrevPoint();
                }
            }

            shortestRoute.Reverse();
            List<Vector3> path = new List<Vector3>();
            foreach (Point point in shortestRoute)
            {
                path.Add(point.GetPos());
            }
            return path;
        }
        else
            return null;
    }

    public Vector3 NodeNearestPlayer()
    {
        float shortestDistance = 0;
        Point prevPoint = new Point(new Vector3(0, 0));
        bool firstPoint = true;
        foreach (Point point in points)
        {
            if (point.GetState() != 's')
            {
                float distance = Vector3.Distance(Player.transform.position, point.GetPos());
                if (!Physics.Raycast(Player.transform.position, point.GetPos() - Player.transform.position, distance))
                {
                    if (firstPoint)
                    {
                        shortestDistance = distance;
                        prevPoint = point;
                        firstPoint = false;
                    }
                    else if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        prevPoint = point;
                    }
                }
            }
        }

        return prevPoint.GetPos();
    }

}
