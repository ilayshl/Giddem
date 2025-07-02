using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    /* static private Dictionary<MonoBehaviour, Coroutine> activeCoroutines = new();

    public static void StartDashParticles(MonoBehaviour source, Transform position, float spawnInterval, SkinnedMeshRenderer[] objectRenderer)
    {
        if (activeCoroutines.ContainsKey(source)) return;
        activeCoroutines[source] = 
        StartCoroutine(DashParticles(position, spawnInterval, objectRenderer));
    }

    public static void StopDashParticles(MonoBehaviour source)
    {
        activeCoroutines.Remove(source);
    }
 */

    public static void StopCourotine()
    {
        StopAllCoroutines();
    }

    public static IEnumerator DashParticles(Transform position, float spawnInterval, SkinnedMeshRenderer[] objectRenderer)
    {
        while (true)
        {
            for (int i = 0; i < objectRenderer.Length; i++)
            {
                GameObject spawnedObject = new();
                spawnedObject.transform.SetPositionAndRotation(position.position, position.rotation);
                MeshRenderer spawnedMeshRenderer = spawnedObject.AddComponent<MeshRenderer>();
                MeshFilter spawnedMeshFilter = spawnedObject.AddComponent<MeshFilter>();

                Mesh mesh = new();
                objectRenderer[i].BakeMesh(mesh);
                spawnedMeshFilter.mesh = mesh;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    } 
}
