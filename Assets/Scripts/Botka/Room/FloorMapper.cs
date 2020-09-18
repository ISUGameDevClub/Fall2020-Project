using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomSet;

public class FloorMapper : MonoBehaviour
{
    public int[] _DefaultShapeMatrix = { 4, 4 };
    public int[] _ShapeMatrix;

    [Header("DO NOT SET")]
    public Room[,] _FloorRoomMatrix;
    public BranchEndPoint[] _BranchEndpoints;
    public GameObject[] _X;
    // Start is called before the first frame update

    private void Awake()
    {
        _ShapeMatrix = _ShapeMatrix != null ? _ShapeMatrix : _DefaultShapeMatrix;
        _X = new GameObject[4];
    }
    void Start()
    {

        _FloorRoomMatrix = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_FloorRoomMatrix != null)
        {
            if (_BranchEndpoints != null)
            {
                int i = 0;
                foreach (BranchEndPoint b in _BranchEndpoints)
                {
                    _X[i] = b._EndPoint;
                    i++;
                }
            }
        }
    }
    public void Init(GameObject origin)
    {

        _FloorRoomMatrix = new Room[_ShapeMatrix[0], _ShapeMatrix[1]];
        int rowS = _ShapeMatrix[0] % 2 == 0 ? _ShapeMatrix[0] / 2 : (_ShapeMatrix[0] - 1) / 2; // finding middle or near middle
        int colS = _ShapeMatrix[1] % 2 == 0 ? _ShapeMatrix[1] / 2 : (_ShapeMatrix[1] - 1) / 2; // finding middle or near middle if odd number.
        _FloorRoomMatrix[rowS, colS] = origin.GetComponentInChildren<Room>();
        GameObject[] x = { origin, origin, origin, origin };
        SetEndpoints(x);
        foreach (BranchEndPoint e in _BranchEndpoints)
        {
            int[] z = { rowS, colS };
            e._EndPointIndeces = z;
        }



    }
    public void SetEndpoints(GameObject[] Endpoints)
    {
        if (Endpoints != null)
        {
            int index = 0;
            _BranchEndpoints = new BranchEndPoint[Endpoints.Length];
            foreach (GameObject obj in Endpoints)
            {
                int[] d = { -1, -1 };
                _BranchEndpoints[index] = new BranchEndPoint(obj, d, 0, false);
                index++;
            }



        }
    }
    public bool IsBLocking(int row, int col)
    {
        if (row < 0 || row > _ShapeMatrix[0] - 1 || col < 0 || col > _ShapeMatrix[1] - 1)
        {
            Debug.Log("Indes out of range : " + row + "," + col);
            return true;
        }
        Debug.Log("Indes in rnage : " + row + "," + col);
        return _FloorRoomMatrix[row, col] != null;
    }

    public void Add(BranchEndPoint endPoint, GameObject obj, RoomSet.Direction dir)
    {
        int[] indeces = null;
        indeces = GetIndecies(endPoint._EndPointIndeces, dir);
       // indeces = endPoint._EndPointIndeces;
        Debug.Log(indeces[0] + "," + indeces[1]);
        _FloorRoomMatrix[indeces[0], indeces[1]] = endPoint._EndPoint.GetComponentInChildren<Room>();

        endPoint._EndPoint = obj;
        endPoint._EndPointIndeces = indeces;
        endPoint._EndpointCount++;

    }

    public int[] GetIndecies(int[] location, RoomSet.Direction dir)
    {
        int[] l = new int[2];
        l[0] = location[0];
        l[1] = location[1];
        switch (dir) // switch direction
        {
            case Direction.Null:

                break;
            case Direction.Left:
                l[1] -= 1;
                break;
            case Direction.Right:
                l[1] += 1;
                break;
            case Direction.Up:
                l[0] -= 1;
                break;
            case Direction.Down:
                l[0] += 1;
                break;
        }
        return l;
    }

    [ContextMenu("Print matrix")]
    public void Print()
    {
        string log = "";

        for (int i = 0; i < _ShapeMatrix[0]; i++)
        {
            for (int z = 0; z < _ShapeMatrix[1]; z++)
            {
                if (_FloorRoomMatrix[i, z] != null)
                {
                    log += "1";
                }
                else
                {
                    log += "0";
                }

                if (z == _ShapeMatrix[0] - 1)
                {
                    log += "\n";
                }
                else
                {
                    log += ",";
                }
            }
            
        }
        Debug.Log(log);
    }
}

