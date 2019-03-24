using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    public PlayerMovement player;

    public void Step(int _index)
    {
        player.FootStep(_index);
    }
}
