using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevMonsterSpawnButton : MonoBehaviour
{
    [SerializeField] private Button btn;

    void Start()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => 
        {
            GameApplication.Instance.EntityController.Spawn<Monster, MonsterObject>(40001, new Vector3(-11, 0, 5), Quaternion.identity);
        });
    }
}
