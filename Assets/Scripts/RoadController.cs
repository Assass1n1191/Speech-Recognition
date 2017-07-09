using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public static RoadController Instance;

    [SerializeField] private GameObject _ground;
    public List<Ground> _groundPool;
    private const int poolSize = 10;
    private float _groundStep = 7.5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
        _groundPool = new List<Ground>();
        InitGroundPool();
	}
	
    private void InitGroundPool()
    {
        GameObject tmpGround;
        for (int i = 0; i < poolSize; i++)
        {
            tmpGround = Instantiate(_ground, new Vector3(0f, 0f, _groundStep * i), Quaternion.identity);
            tmpGround.transform.parent = gameObject.transform;
            tmpGround.GetComponentInChildren<Ground>().myIndexInGroundPool = i;
            _groundPool.Add(tmpGround.GetComponentInChildren<Ground>());
        }
    }

    public void MoveGorundPart(int partIndex)
    {
        Ground partToMove = _groundPool[partIndex];
        partToMove.gameObject.transform.parent.position += new Vector3(0f, 0f, _groundStep * poolSize);

        _groundPool.Remove(partToMove);
        _groundPool.Add(partToMove);

        SetNewPartIndexes();
    }

    private void SetNewPartIndexes()
    {
        for(int i = 0; i < poolSize; i++)
        {
            _groundPool[i].GetComponent<Ground>().myIndexInGroundPool = i;
        }
    }
}
