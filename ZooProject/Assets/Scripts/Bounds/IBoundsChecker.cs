using UnityEngine;

public interface IBoundsChecker
{
    public bool IsInBounds(Vector3 worldCoords);
    public Vector3 ReturnDirection(Vector3 worldCoords);
}
