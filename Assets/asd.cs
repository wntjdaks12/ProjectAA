using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class asd : MonoBehaviour
{
    [SerializeField] private Button btn;

    public EntityController entityController;

    public void Start()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() =>
        {
        //    GameObject.FindObjectOfType<HeroObject>().WeaponObject = entityController.Spawn<Weapon, WeaponObject>(20001);
        });
    }
}
