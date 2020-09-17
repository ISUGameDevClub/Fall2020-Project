using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchEndPoint 
{
    public GameObject _EndPoint;
    public bool _Blocked;
    public int _EndpointCount;
    public int[] _EndPointIndeces;
    public BranchEndPoint(GameObject endPoint, int[] location, int count, bool blocked)
    {
        this._EndPoint = endPoint;
        this._EndPointIndeces = location;
        this._EndpointCount = count;
        _Blocked = blocked;
    }
}
