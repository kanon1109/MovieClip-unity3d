using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public MovieClip mc;
    public Image img;
	// Use this for initialization
	void Start ()
    {
        mc.loadPathRes("test");
        //mc.setScale(15f);
        mc.gotoAndPlay(1, -1);
	}
	
	// Update is called once per frame
	void Update () 
    {
        //img.sprite = mc.GetComponent<SpriteRenderer>().sprite;
	}
}
