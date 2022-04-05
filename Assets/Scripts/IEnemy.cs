using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy 
{
    public abstract byte CheckPlayerPosition();
    public abstract void Move();
    public abstract void Shut();
    public abstract void ChangeSpeed(byte speed);
    public abstract void Break();

}
