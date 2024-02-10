using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<SkillInfo> skillInfos;
    [SerializeField] private List<SubItem> subItems;

    [field: SerializeField] public List<DBHeroInfo> DBHeroInfos { get; private set; }
    [field:SerializeField] public List<DBGoodsInfo> DBGoodsInfos { get; private set; }
    [field:SerializeField] public List<DBItemInfo> DBItemInfos { get; private set; }
    [SerializeField] private List<DBRuneInfo> DBRuneInfos;
    [SerializeField] private List<DBWeaponInfo> DBWeaponInfos;
    [SerializeField] private List<DBSubItemInfo> DBSubItemInfos;

    private void Awake()
    {
        DBHeroInfos = new List<DBHeroInfo>();
        DBGoodsInfos = new List<DBGoodsInfo>();
        DBItemInfos = new List<DBItemInfo>();
        DBRuneInfos = new List<DBRuneInfo>();
        DBWeaponInfos = new List<DBWeaponInfo>();
        DBSubItemInfos = new List<DBSubItemInfo>();

        DBSubItemInfos.Add(new DBSubItemInfo() { id = 100001, count = 1 });

        var heroInfo = new DBHeroInfo();
        heroInfo.id = 10001;
        heroInfo.skillIds = new int[1];
        heroInfo.skillIds[0] = 60001;
        heroInfo.equipWeaponId = 0;
        heroInfo.equipSubItemIds = new List<int>();
        DBHeroInfos.Add(heroInfo);

        var heroInfo2 = new DBHeroInfo();
        heroInfo2.id = 10002;
        heroInfo2.skillIds = new int[1];
        heroInfo2.skillIds[0] = 60002;
        heroInfo2.equipWeaponId = 20001;
        heroInfo2.equipSubItemIds = new List<int>();
        DBHeroInfos.Add(heroInfo2);

        var heroInfo3 = new DBHeroInfo();
        heroInfo3.id = 10003;
        heroInfo3.skillIds = new int[1];
        heroInfo3.skillIds[0] = 60004;
        heroInfo3.equipWeaponId = 20002;
        heroInfo3.equipSubItemIds = new List<int>();
        DBHeroInfos.Add(heroInfo3);

        for (int i = 0; i < DBHeroInfos.Count; i++)
        {
            DBHeroInfos[i].equipSubItemIds.Add(DBSubItemInfos[0].id);
        }

        SetDBWeaponInfo(new DBWeaponInfo() { id = 20001, count = 1 });
        SetDBWeaponInfo(new DBWeaponInfo() { id = 20002, count = 1 });
    }

    private void Start()
    {
        skillInfos = new List<SkillInfo>();
        for (int i = 0; i < DBHeroInfos.Count; i++)
        {
            skillInfos.Add(new SkillInfo());

            skillInfos[i].heroId = DBHeroInfos[i].id;

            skillInfos[i].skillTools = new List<SkillTool>();

            for (int j = 0; j < DBHeroInfos[i].skillIds.Length; j++)
            {
                skillInfos[i].skillTools.Add(new SkillTool(GameApplication.Instance.EntityController.Spawn<Skill>(DBHeroInfos[i].skillIds[j])));
            }
        }

        subItems = new List<SubItem>();

        for(int i = 0; i < DBSubItemInfos.Count; i++)
        {
            var subItem = GameApplication.Instance.GameModel.PresetData.ReturnData<SubItem>(nameof(SubItem), DBSubItemInfos[i].id);
            subItem.Init();
            subItems.Add(subItem);
        }
    }

    public DBHeroInfo GetDBHeroInfoById(int id)
    {
        return DBHeroInfos.Find(x => x.id == id);
    }

    public SubItem GetSubItemById(int id)
    {
        return subItems.Find(x => x.Id == id);
    }

    public List<SkillTool> GetSkillTools(int heroId)
    {
        return skillInfos.Where(x => x.heroId == heroId).Select(x => x.skillTools).FirstOrDefault();
    }
    public SubItem[] GetSubItems(int heroId)
    {
        var heroInfo = GetDBHeroInfoById(heroId);

        var subItems = new SubItem[heroInfo.equipSubItemIds.Count];
        for (int i = 0; i < subItems.Length; i++)
        {
            subItems[i] = this.subItems.Find(x => x.Id == heroInfo.equipSubItemIds[i]);
        }

        return subItems;
    }


    public void SetGoodsInfo(DBGoodsInfo goodsInfo)
    {
        var resGoodsInfo = DBGoodsInfos.Find(x => x.id == goodsInfo.id);

        if (resGoodsInfo == null)
        {
            DBGoodsInfos.Add(goodsInfo);
        }
        else
        {
            resGoodsInfo.value += goodsInfo.value;
        }
    }

    public void SetItemInfo(DBItemInfo itemInfo)
    {
        var resItemInfo = DBItemInfos.Find(x => x.id == itemInfo.id);

        if (resItemInfo == null)
        {
            DBItemInfos.Add(itemInfo);
        }
        else
        {
            resItemInfo.value += itemInfo.value;
        }
    }

    // 룬 DB 관련
    public void EquipRune(int heroId, int runeId)
    {
        var dbHeroId = GetDBHeroInfoById(heroId);

        if (dbHeroId.equipRuneId != runeId)
        {
            dbHeroId.equipRuneId = runeId;

            skillInfos.Find(x => x.heroId == heroId).skillTools.Add(new SkillTool(GameApplication.Instance.EntityController.Spawn<Skill>(runeId)));

            Debug.Log("룬 장착");
        }
    }
    public void DivestRune(int heroId, int runeId)
    {
        var dbHeroId = GetDBHeroInfoById(heroId);

        if (dbHeroId.equipRuneId == runeId && dbHeroId.equipRuneId != 0)
        {
            dbHeroId.equipRuneId = 0;

            var skillTool = skillInfos.Find(x => x.heroId == heroId).skillTools.Find(x => x.Skill.Id == runeId);

            skillInfos.Find(x => x.heroId == heroId).skillTools.Remove(skillTool);
        }
    }
    public int GetDBRuneInfoCount()
    {
        return DBRuneInfos.Count;
    }
    public DBRuneInfo GetDBRuneInfoByIndex(int index)
    {
        return DBRuneInfos[index];
    }
    public DBRuneInfo GetDBRuneInfoById(int id)
    {
        return DBRuneInfos.Find(x => x.id == id);
    }
    public void SetDBRuneInfo(DBRuneInfo dbRuneInfo)
    {
        var resDBRuneInfo = DBRuneInfos.Find(x => x.id == dbRuneInfo.id);

        if (resDBRuneInfo == null)
        {
            DBRuneInfos.Add(dbRuneInfo);
        }
        else
        {
            resDBRuneInfo.count += dbRuneInfo.count;
        }
    }

    // 무기 DB 관련
    public void EquipWeapon(int heroId, int weaponId)
    {
        var dbHeroId = GetDBHeroInfoById(heroId);

        var heroObjects = GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<HeroObject>(nameof(HeroObject)).Where((x => x.Entity.Id == heroId)).ToArray();

        if (dbHeroId.equipWeaponId != weaponId)
        {
            dbHeroId.equipWeaponId = weaponId;

            for (int i = 0; i < heroObjects.Length; i++)
            {
                var heroObject = heroObjects[i];

                if (heroObject.MotionHandler.isAttack)
                {
                    GameApplication.Instance.EntityController.Spawn<NotifyPopUp, NotifyPopUpObject>(71, Vector3.zero, Quaternion.identity, GameObject.Find("DynamicOverlayCanvas").transform);

                    heroObject.MotionHandler.OnOnetimeEnd += () =>
                    {
                        heroObject.WeaponObject = GameApplication.Instance.EntityController.Spawn<Weapon, WeaponObject>(weaponId, Vector3.zero, Quaternion.identity, heroObject.RightHandNode);
                    };
                }
                else
                {
                    heroObjects[i].WeaponObject = GameApplication.Instance.EntityController.Spawn<Weapon, WeaponObject>(weaponId, Vector3.zero, Quaternion.identity, heroObjects[i].RightHandNode);
                }
            }
        }
    }

    public void EquipWeapon(CharacterObject characterObject, int weaponId)
    {
        var dbHeroId = GetDBHeroInfoById(characterObject.Entity.Id);

        if (dbHeroId.equipWeaponId != weaponId)
        {
            dbHeroId.equipWeaponId = weaponId;
        }

        characterObject.WeaponObject = GameApplication.Instance.EntityController.Spawn<Weapon, WeaponObject>(weaponId, Vector3.zero, Quaternion.identity, characterObject.RightHandNode);
    }

    public void DivestWeapon(int heroId, int weaponId)
    {
        var dbHeroId = GetDBHeroInfoById(heroId);

        if (dbHeroId.equipWeaponId == weaponId && dbHeroId.equipWeaponId != 0)
        {
            dbHeroId.equipWeaponId = 0;

            var heroObjects = GameApplication.Instance.GameModel.RunTimeData.ReturnDatas<HeroObject>(nameof(HeroObject)).Where((x => x.Entity.Id == heroId)).ToArray();

            for (int i = 0; i < heroObjects.Length; i++)
            {
                var heroObject = heroObjects[i];

                if (heroObject.MotionHandler.isAttack)
                {
                    GameApplication.Instance.EntityController.Spawn<NotifyPopUp, NotifyPopUpObject>(71, Vector3.zero, Quaternion.identity, GameObject.Find("DynamicOverlayCanvas").transform);

                    heroObject.MotionHandler.OnOnetimeEnd += () =>
                    {
                        GameApplication.Instance.EntityController.RemoveEntity(heroObject.WeaponObject.Entity);

                        heroObject.WeaponObject = null;
                    };
                }
                else
                {
                    GameApplication.Instance.EntityController.RemoveEntity(heroObject.WeaponObject.Entity);

                    heroObject.WeaponObject = null;
                }
            }
        }
    }
    public int GetDBWeaponInfoCount()
    {
        return DBWeaponInfos.Count;
    }
    public DBWeaponInfo GetDBWeaponInfoByIndex(int index)
    {
        return DBWeaponInfos[index];
    }
    public void SetDBWeaponInfo(DBWeaponInfo dbWeaponInfo)
    {
        var resDBWeaponInfo = DBWeaponInfos.Find(x => x.id == dbWeaponInfo.id);

        if (resDBWeaponInfo == null)
        {
            DBWeaponInfos.Add(dbWeaponInfo);
        }
        else
        {
            resDBWeaponInfo.count += dbWeaponInfo.count;
        }
    }

    // 보조 아이템 DB 관련
    public void SetDBSubItemInfo(DBSubItemInfo dbSubItemInfo)
    {
        var resDBSubItemInfo = DBSubItemInfos.Find(x => x.id == dbSubItemInfo.id);

        if (resDBSubItemInfo == null)
        {
            DBSubItemInfos.Add(resDBSubItemInfo);
        }
        else
        {
            resDBSubItemInfo.count += resDBSubItemInfo.count;
        }
    }
}

public class SkillInfo
{
    public int heroId;
    public List<SkillTool> skillTools;
}

[System.Serializable]
public class DBHeroInfo
{
    public int id;
    public int[] skillIds;
    public int equipRuneId;
    public int equipWeaponId;
    public List<int> equipSubItemIds;
}

[System.Serializable]
public class DBRuneInfo
{
    public int id;
    public int count;
}

[System.Serializable]
public class DBGoodsInfo
{
    public int id;
    public int value;
}

[System.Serializable]
public class DBItemInfo
{
    public int id;
    public int value;
}

[System.Serializable]
public class DBWeaponInfo
{
    public int id;
    public int count;
}

[System.Serializable]
public class DBSubItemInfo
{
    public int id;
    public int count;
}