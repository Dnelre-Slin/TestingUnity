using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CameraInfo
{
    public Camera realCamera;
    public Camera projCamera;
    public AudioListener realAudioListener;
    public AudioListener projAudioListener;
}

public static class IDSProjectionCreator
{
    public static IDSProjection CreateProjections(GameObject sourceObject, List<CameraInfo> cameraInfos, Transform parent = null)
    {
        GameObject projectionObject = new GameObject();

        projectionObject.transform.localPosition = sourceObject.transform.localPosition;
        projectionObject.transform.localRotation = sourceObject.transform.localRotation;
        projectionObject.transform.localScale = sourceObject.transform.localScale;

        IDSProjectionCreator.CopyAllCosmeticComponentsAndGetCameraInfo(sourceObject, projectionObject, cameraInfos);
        IDSProjection idsProjection = IDSProjectionCreator.AddProjectionComponent(sourceObject, projectionObject);

        projectionObject.name = sourceObject.name + "_proj";
        projectionObject.transform.parent = parent;

        IDSProjectionCreator.CreateChildProjections(sourceObject, projectionObject, cameraInfos);

        return idsProjection;
    }

    public static void CreateChildProjections(GameObject sourceObject, GameObject projectionObject, List<CameraInfo> cameraInfos)
    {
        foreach (Transform child in sourceObject.transform)
        {
            IDSProjectionCreator.CreateProjections(child.gameObject, cameraInfos, projectionObject.transform);
        }
    }

    public static IDSProjection AddProjectionComponent(GameObject sourceObject, GameObject projectionObject)
    {
        IDSProjection projectionComp = projectionObject.AddComponent<IDSProjection>();
        projectionComp.realTransform = sourceObject.transform;
        return projectionComp;
    }

    public static Transform MoveProjectionsEnter(IDSInternal idsInternal, IDSProjection idsProjection)
    {
        idsInternal.externalZone.parent = idsInternal.idsExternal.transform;
        GameObject projectionZone = new GameObject(idsInternal.externalZone.name + "_proj");
        projectionZone.transform.parent = idsProjection.transform;
        AddProjectionComponent(idsInternal.externalZone.gameObject, projectionZone);

        foreach (Transform child in idsInternal.externalZone)
        {
            child.parent = projectionZone.transform;
        }

        return projectionZone.transform;
    }

    public static void MoveProjectionsExit(IDSInternal idsInternal)
    {
        foreach (Transform child in idsInternal.projectionZone)
        {
            child.parent = idsInternal.externalZone;
        }
    }

    public static void CopyAllCosmeticComponentsAndGetCameraInfo(GameObject source, GameObject destination, List<CameraInfo> cameraInfos, bool onlyActive = true)
    {
        CameraInfo cameraInfo = new CameraInfo();
        cameraInfo.realCamera = null;
        cameraInfo.projCamera = null;
        cameraInfo.realAudioListener = null;
        cameraInfo.projAudioListener = null;

        bool added = false;

        foreach(var component in source.GetComponents<Component>())
        {
            var componentType = component.GetType();
            if (componentType != typeof(Transform)
                && componentType != typeof(Rigidbody)
                && componentType != typeof(MonoBehaviour)
                && !componentType.IsSubclassOf(typeof(Collider))
                && !componentType.IsSubclassOf(typeof(MonoBehaviour))
                && componentType != typeof(Camera) // Fix, as camera copy is not working properly
                && componentType != typeof(AudioListener) // So audio listener can be added to cameraInfos list
            )
            {
                RuntimeComponentCopier.AddComponent(destination, component);
            }
            else if (componentType == typeof(Camera)) // Fix, as camera copy is not working properly
            {
                Camera camCopy = destination.AddComponent<Camera>();
                camCopy.enabled = ((Camera)component).enabled;
                cameraInfo.realCamera = (Camera)component;
                cameraInfo.projCamera = camCopy;
                added = true;
            }
            else if (componentType == typeof(AudioListener)) // So audio listener can be added to cameraInfos list
            {
                AudioListener alCopy = (AudioListener)RuntimeComponentCopier.AddComponent(destination, component);
                cameraInfo.realAudioListener = (AudioListener)component;
                cameraInfo.projAudioListener = alCopy;
                added = true;
            }
        }

        if (added)
        {
            cameraInfos.Add(cameraInfo);
        }
    }

    public static void SwitchCameraInfosToProjection(List<CameraInfo> cameraInfos)
    {
        foreach (var camInfo in cameraInfos)
        {
            if (camInfo.realCamera != null && camInfo.projCamera != null)
            {
                camInfo.projCamera.enabled = camInfo.realCamera.enabled;
                camInfo.realCamera.enabled = false;
            }
            if (camInfo.realAudioListener != null && camInfo.projAudioListener != null)
            {
                camInfo.projAudioListener.enabled = camInfo.realAudioListener.enabled;
                camInfo.realAudioListener.enabled = false;
            }
        }
    }

    public static void SwitchCameraInfosToReal(List<CameraInfo> cameraInfos)
    {
        foreach (var camInfo in cameraInfos)
        {
            if (camInfo.realCamera != null && camInfo.projCamera != null)
            {
                camInfo.realCamera.enabled = camInfo.projCamera.enabled;
                camInfo.projCamera.enabled = false;
            }
            if (camInfo.realAudioListener != null && camInfo.projAudioListener != null)
            {
                camInfo.realAudioListener.enabled = camInfo.projAudioListener.enabled;
                camInfo.projAudioListener.enabled = false;
            }
        }
    }
    // public static void MoveProjectionsEnter(IDSInternal idsInternal, IDSProjection idsProjection)
    // {
    //     GameObject newSourceObject = new GameObject(idsInternal.externalZone.name);
    //     Transform newProjectedTransform = idsInternal.externalZone;
    //     newProjectedTransform.name = newProjectedTransform.name + "_proj";
    //     newProjectedTransform.parent = idsProjection.transform;
    //     newSourceObject.transform.parent = idsInternal.idsExternal.transform;
    //     idsInternal.externalZone = newSourceObject.transform;
    //     AddProjectionComponent(newSourceObject, newProjectedTransform.gameObject);
    // }

    // public static void MoveProjectionsExit(IDSInternal idsInternal)
    // {
    //     Transform source = idsInternal.projectionZone;
    //     Transform projection = idsInternal.externalZone;
    //     source.name = projection.name;
    //     idsInternal.externalZone = source;
    //     // GameObject.Destroy(projection);
    // }

    // public static List<Transform> ExtractChildTransforms(Transform parentTransform)
    // {
    //     List<Transform> childTransforms = new List<Transform>(10);

    //     foreach (Transform child in parentTransform)
    //     {
    //         childTransforms.Add(child);
    //         child.parent = null;
    //     }

    //     return childTransforms;
    // }

    // public static void InsertChildTransforms(Transform parentTransform, List<Transform> childTransforms)
    // {
    //     foreach (Transform child in childTransforms)
    //     {
    //         child.parent = parentTransform;
    //     }
    // }
}

public static class IDSTransformations
{
    public static void TransformEnter(Transform objectTransform, Rigidbody objectRgbd, Rigidbody externalParentRgbd, Transform internalZone, Transform externalZone)
    {
        // Position
        Vector3 deltaPosition = internalZone.position - externalZone.position;
        Vector3 localPosition = objectTransform.position - externalZone.position;
        localPosition = Quaternion.Inverse(externalZone.rotation) * localPosition;
        // localPosition = internalZone.rotation * localPosition; // Needs to be uncommented if the 'source' is ever to be rotated.
        objectTransform.position = localPosition + internalZone.position;

        // Rotation
        objectTransform.rotation = Quaternion.Inverse(externalZone.rotation) * objectTransform.rotation;
        // objectTransform.rotation = internalZone.rotation * objectTransform.rotation; // Needs to be uncommented if the 'source' is ever to be rotated.

        // Velocity
        Vector3 relativeVelocity = (externalParentRgbd != null) ? objectRgbd.velocity - externalParentRgbd.velocity : objectRgbd.velocity;
        objectRgbd.velocity = Quaternion.Inverse(externalZone.rotation) * relativeVelocity;
        // objectRgbd.velocity = internalZone.rotation * objectRgbd.velocity; // Needs to be uncommented if the 'source' is ever to be rotated.
    }

    public static void TransformExit(Transform objectTransform, Rigidbody objectRgbd, Rigidbody externalParentRgbd, Transform internalZone, Transform externalZone)
    {
        // Position
        Vector3 deltaPosition = externalZone.position - internalZone.position;
        Vector3 localPosition = objectTransform.position - internalZone.position;
        // localPosition = Quaternion.Inverse(internalZone.rotation) * localPosition; // Needs to be uncommented if the 'source' is ever to be rotated.
        localPosition = externalZone.rotation * localPosition;
        objectTransform.position = localPosition + externalZone.position;

        // Rotation
        // objectTransform.rotation = Quaternion.Inverse(internalZone.rotation) * objectTransform.rotation; // Needs to be uncommented if the 'source' is ever to be rotated.
        objectTransform.rotation = externalZone.rotation * objectTransform.rotation;

        // Velocity
        // objectRgbd.velocity = Quaternion.Inverse(internalZone.rotation) * objectRgbd.velocity; // Needs to be uncommented if the 'source' is ever to be rotated.
        Vector3 transformedVelocity = externalZone.rotation * objectRgbd.velocity;
        objectRgbd.velocity = (externalParentRgbd != null) ? transformedVelocity + externalParentRgbd.velocity : transformedVelocity;
    }
}

public static class IDSHandleSourceObject
{
    public static List<Camera> FindAndDisableCameras(GameObject sourceObject)
    {
        if (sourceObject.activeInHierarchy)
        {
            List<Camera> cameras = new List<Camera>();
            // Cameras
            Camera cam = sourceObject.GetComponent<Camera>();
            if (cam != null && cam.enabled)
            {
                cam.enabled = false;
                cameras.Add(cam);
            }
            foreach (Camera c in sourceObject.GetComponentsInChildren<Camera>(false))
            {
                if (c.enabled)
                {
                    c.enabled = false;
                    cameras.Add(c);
                }
            }
            if (cameras.Count > 0)
            {
                return cameras;
            }
        }
        return null;
    }

    public static List<AudioListener> FindAndDisableAudioListeners(GameObject sourceObject)
    {
        if (sourceObject.activeInHierarchy)
        {
            List<AudioListener> audioListeners = new List<AudioListener>();
            // Cameras
            AudioListener audioListener = sourceObject.GetComponent<AudioListener>();
            if (audioListener != null && audioListener.enabled)
            {
                audioListener.enabled = false;
                audioListeners.Add(audioListener);
            }
            foreach (AudioListener al in sourceObject.GetComponentsInChildren<AudioListener>(false))
            {
                if (al.enabled)
                {
                    al.enabled = false;
                    audioListeners.Add(al);
                }
            }
            if (audioListeners.Count > 0)
            {
                return audioListeners;
            }
        }
        return null;
    }

    public static void ReenableCameras(List<Camera> cameras)
    {
        if (cameras != null)
        {
            foreach (Camera cam in cameras)
            {
                cam.enabled = true;
            }
        }
    }

    public static void ReenableAudioListeners(List<AudioListener> audioListeners)
    {
        if (audioListeners != null)
        {
            foreach (AudioListener audioListener in audioListeners)
            {
                audioListener.enabled = true;
            }
        }
    }
}