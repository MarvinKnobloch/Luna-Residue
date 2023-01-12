using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[SaveDuringPlay]
[AddComponentMenu("")]
public class Lockoncamclamp : CinemachineExtension

{
    [Tooltip("Camera will clamp relative to this transform")]
    public Transform refOrientation;
    [Tooltip("Max X/Y angle the camera can turn")]
    public Vector2 angleBounds;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Aim)
        {
            Quaternion xCamOnly = Quaternion.Euler(state.RawOrientation.eulerAngles.x, 0, 0);
            Quaternion xRefOnly = Quaternion.Euler(refOrientation.rotation.eulerAngles.x, 0, 0);
            float xAngle = Quaternion.Angle(xCamOnly, xRefOnly);
            if (xAngle > angleBounds.x)
            {
                xCamOnly = Quaternion.Euler(state.RawOrientation.eulerAngles.x + 1, 0, 0);
                if (xAngle < Quaternion.Angle(xCamOnly, xRefOnly))
                {
                    state.RawOrientation = Quaternion.Euler(state.RawOrientation.eulerAngles.x - (xAngle - angleBounds.x), state.RawOrientation.eulerAngles.y, state.RawOrientation.eulerAngles.z);
                }
                else
                {
                    state.RawOrientation = Quaternion.Euler(state.RawOrientation.eulerAngles.x + (xAngle - angleBounds.x), state.RawOrientation.eulerAngles.y, state.RawOrientation.eulerAngles.z);
                }
            }
        }
    }
}
