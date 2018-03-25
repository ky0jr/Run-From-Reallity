using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection
{
    None = 0,
    Left = 1,
    Right = 2,
    Up = 4,
    Down = 8,
}

public class SwipeManager : MonoBehaviour {

    private static SwipeManager instance;
    public static SwipeManager Instance {get {return instance;}}

    public SwipeDirection Direction { set; get; }

    private Vector3 touchPosition;
    private float swipeResistanceY = 50f;
    private float swipeResistanceX = 50f;

    void Start () {
        instance = this;
	}	

	void Update () {

        Direction = SwipeDirection.None;

        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
            //Debug.Log("I press");
        }

        if(Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = touchPosition - Input.mousePosition;
            //Debug.Log("I press");
            if (Mathf.Abs(deltaSwipe.x) > swipeResistanceX)
             {
                 Direction |= (deltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;
                 //Debug.Log("I swipe x");
             }
            if (Mathf.Abs(deltaSwipe.y) > swipeResistanceY)
            {
                Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Down;
               // Debug.Log("I swipe y");
            }
        }

	}

    public bool IsSwiping(SwipeDirection dir)
    {
        return (Direction & dir) == dir;
    }
}
