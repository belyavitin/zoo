using UnityEngine;
using Extensions;

public class BoundsControllerByCamera : MonoSingleton<BoundsControllerByCamera>, IBoundsChecker
{
    [SerializeField]
    Camera gameCamera;
    [SerializeField, Range(0, 0.5f)]
    float border;

    protected override void Awake()
    {
        base.Awake();
        if (gameCamera == null)
            gameCamera = GetComponent<Camera>();
    }

    public bool IsInBounds(Vector3 worldCoords)
    {
        Vector3 vp = gameCamera.WorldToViewportPoint(worldCoords);
        return Inside(vp.x) && Inside(vp.y);
    }

    private bool Inside(float viewportPos)
    {
        return viewportPos >= border && viewportPos <= 1 - border;
    }

    private float Clamp(float viewportPos)
    {
        return Mathf.Clamp(viewportPos, border, 1 - border);
    }

    public Vector3 ReturnDirection(Vector3 worldCoords)
    {
        if (IsInBounds(worldCoords))
            return Vector3.zero;

        Vector3 vp = gameCamera.WorldToViewportPoint(worldCoords);
        Vector3 clamped = new Vector3(Clamp(vp.x), Clamp(vp.y), vp.z);
        Vector3 worldClamped = gameCamera.ViewportToWorldPoint(clamped);
        var delta = worldClamped - worldCoords;
        delta.y = 0;
        var result = delta;
        return result;
    }
}