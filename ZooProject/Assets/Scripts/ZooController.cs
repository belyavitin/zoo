using System.Collections;
using UnityEngine;
using System;

public interface IBusEvent
{
}

public enum EZooAnimalDietaryType { Predator, Prey }

public class ZooAnimalDeathEvent : IBusEvent
{
    public readonly EZooAnimalDietaryType Dietary;

    public ZooAnimalDeathEvent(EZooAnimalDietaryType dietary)
    {
        Dietary = dietary;
    }

    public override string ToString()
    {
        return $"({base.ToString()}){{{nameof(Dietary)}: {Dietary}}}";
    }
}

public class ZooAnimalEatenEvent : IBusEvent
{
    public readonly Vector3 PredatorWorldPos;

    public ZooAnimalEatenEvent(Vector3 predatorWorldPos)
    {
        PredatorWorldPos = predatorWorldPos;
    }
}

public interface IZooBehaviour
{
}
