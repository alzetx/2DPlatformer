using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameTickable 
{
    void Tick(float deltaTime); 
}
public interface IGameFixedTickable
{
    void FixedTick(float deltaTime); //fixedDeltaTime
}

public interface IGameLateTickable
{
    void LateTick(float deltaTime);
}