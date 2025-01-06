using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compass : MonoBehaviour
{

    float halfsize = 200;

    public Vector3 NorthDirection;
    public Transform player;

    public RectTransform Northlayer;
    public RectTransform north;
    public RectTransform east;
    public RectTransform west;
    public RectTransform south;


    // Update is called once per frame
    void Update()
    {
        ChangeNorthDirection();

        /*var northAngle = -Vector2.SignedAngle(player.ForwardZX, Vector2.up);
        var eastAngle = -Vector2.SignedAngle(player.ForwardZX, Vector2.right);
        var westAngle = -Vector2.SignedAngle(player.ForwardZX, Vector2.left);
        var southAngle = -Vector2.SignedAngle(player.ForwardZX, Vector2.down);

        north.anchoredPosition = new Vector2(AngleToCompassPos(northAngle), north.anchoredPosition.y);
        east.anchoredPosition = new Vector2(AngleToCompassPos(eastAngle), north.anchoredPosition.y);
        west.anchoredPosition = new Vector2(AngleToCompassPos(westAngle), north.anchoredPosition.y);
        south.anchoredPosition = new Vector2(AngleToCompassPos(southAngle), north.anchoredPosition.y);*/

    }

    public void ChangeNorthDirection()
    {
        NorthDirection.z = player.eulerAngles.y;
        Northlayer.localEulerAngles = NorthDirection;

    }

    /*float AngleToCompassPos(float angle)
    {
        return (halfsize / 180f) * angle;
    }*/

}
