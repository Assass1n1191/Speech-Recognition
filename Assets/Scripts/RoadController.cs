using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public static RoadController Instance;

    [SerializeField] private GameObject _ground;
    public List<Ground> _groundPool;
    private const int poolSize = 10;
    private float _groundStep = 8f;

    private List<Letter> _letterPool;

    private int tmpCounter = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
        _groundPool = new List<Ground>();
        _letterPool = new List<Letter>();
        InitGroundPool();
	}
	
    private void InitGroundPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var tmpGround = Instantiate(_ground, new Vector3(0f, 0f, _groundStep * i), _ground.transform.rotation);
            tmpGround.transform.parent = gameObject.transform;
            tmpGround.GetComponent<Ground>().myIndexInGroundPool = i;
            _groundPool.Add(tmpGround.GetComponent<Ground>());

            if (i > 0 && i % 3 == 0)
            {
                LetterBuilder.Instance.SpawnLetter(tmpGround.transform.position);
                //GameObject newLetter = LetterBuilder.Instance.SpawnLetter();
                //newLetter.transform.position = partToMove.transform.position;


                //Instantiate(LetterBuilder.Instance.SpawnLetter(), partToMove.transform.position, Quaternion.identity);
            }

        }
    }

    public void MoveGorundPart(int partIndex)
    {
        Ground partToMove = _groundPool[partIndex];
        partToMove.gameObject.transform.position += new Vector3(0f, 0f, _groundStep * poolSize);

        _groundPool.Remove(partToMove);
        _groundPool.Add(partToMove);

        SetNewPartIndexes();

        tmpCounter++;
        if (tmpCounter % 2 == 0)
        {
            _letterPool.Add(LetterBuilder.Instance.SpawnLetter(partToMove.gameObject.transform.position).GetComponent<Letter>());
            //GameObject newLetter = LetterBuilder.Instance.SpawnLetter();
            //newLetter.transform.position = partToMove.transform.position;


            //Instantiate(LetterBuilder.Instance.SpawnLetter(), partToMove.transform.position, Quaternion.identity);
        }



    }

    private void SetNewPartIndexes()
    {
        for(int i = 0; i < poolSize; i++)
        {
            _groundPool[i].GetComponent<Ground>().myIndexInGroundPool = i;
        }
    }
}
