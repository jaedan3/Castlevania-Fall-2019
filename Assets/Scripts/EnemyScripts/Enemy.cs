using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    public abstract void movement();
    public abstract void tracking();
    public abstract void attack();
    public abstract void damaged();
}
