using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntitySpawner))]
public class MonsterSpawnerEditor : Editor
{
    private void OnSceneGUI()
    {
        var monsterSpawner = (EntitySpawner)target;

        Handles.color = new Color(1, 0, 0, 0.2f);
        Handles.DrawSolidDisc(monsterSpawner.transform.position, new Vector3(0, 1, 0), monsterSpawner.spawnRadius);
    }
}