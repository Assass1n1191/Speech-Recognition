using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LetterBuilder : MonoBehaviour
{
    public static LetterBuilder Instance;

    [SerializeField] private GameObject _letter;
    private List<Sprite> _letterSprites;

    private void Awake()
    {
        Instance = this;
    }

	private void Start () 
	{
        _letterSprites = Resources.LoadAll<Sprite>("Letters").ToList();
	    _letterSprites = _letterSprites.OrderBy(x => x.name).ToList();
    }

    public GameObject SpawnLetter(Vector3 pos)
    {
        Sprite randomLetter = _letterSprites[Random.Range(0, _letterSprites.Count)];

        GameObject newLetter = Instantiate(_letter, pos, _letter.transform.rotation);
        newLetter.GetComponent<SpriteRenderer>().sprite = randomLetter;
        newLetter.GetComponent<Letter>().letterName = randomLetter.name;
        newLetter.transform.parent = transform;

        return newLetter;
    }
}
