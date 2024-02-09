using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EntityController : MonoBehaviour
{
    /// <summary>
    /// 엔티티를 생성합니다.
    /// </summary>
    /// <typeparam name="T">엔티티</typeparam>
    /// <param name="id">아이디</param>
    /// <returns>엔티티를 리턴합니다.</returns>
    public T Spawn<T>(int id) where T : Entity
    {
        var entity = GameApplication.Instance.GameModel.RunTimeData.AddData(typeof(T).ToString(), id) as T;
        entity.Init();

        return entity;
    }

    /// <summary>
    /// 엔티티와 엔티티 오브젝트를 생성합니다.
    /// </summary>
    /// <typeparam name="T">엔티티</typeparam>
    /// <typeparam name="K">엔티티 오브젝트</typeparam>
    /// <param name="id">아이디</param>
    /// <param name="position">생성 위치</param>
    /// <param name="rotation">생성 회전</param>
    /// <param name="parent">부모에 생성 여부</param>
    /// <param name="value1">임시 데이터1</param>
    /// <returns>엔티티 오브젝트를 리턴합니다.</returns>
    public K Spawn<T, K>(int id, Vector3 position, Quaternion rotation, Transform parent = null, int value1 = 0) where T : Entity where K : EntityObject
    {
        var runTimeData = GameApplication.Instance.GameModel.RunTimeData;

        var entity = runTimeData.AddData(typeof(T).ToString(), id) as T;

        var prefabInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<PrefabInfo>(nameof(PrefabInfo), id);

        var entityObj = PoolObjectContainer.CreatePoolObject<K>(prefabInfo.Path);
        entityObj.gameObject.SetActive(false);

        if (rotation != Quaternion.identity) entityObj.transform.rotation = rotation;
        entityObj.transform.position = position;

        if (parent != null) entityObj.transform.SetParent(parent, false);

        entityObj.gameObject.SetActive(true);

        // 나중에 따로 분리시켜야 됨
        if (entity is DamageFont)
        {
            var damageFont = entity as DamageFont;
            damageFont.Value = value1;
        }        // 나중에 따로 분리시켜야 됨
        if (entity is DropItem)
        {
            var dropItem = entity as DropItem;
            dropItem.Value = value1;
        }

        entity.Init(entityObj.transform);
        entityObj.Init(entity);

        entity.OnDataRemove += (data) =>
        {
            RemoveEntity(data);
        };

        runTimeData.AddData($"{typeof(T).Name}Object", entity.InstanceId, entityObj);

        // 나중에 따로 분리시켜야 됨
        if (entity is Character)
        {
            var character = entity as Character;
            var characterObj = entityObj as CharacterObject;
            
            var headBarObj = GameApplication.Instance.EntityController.Spawn<HeadBar, HeadBarObject>(50001, characterObj.HeadBarNode.position, Quaternion.identity, GameObject.Find("DynamicOverlayCanvas").transform);
            var headBar = headBarObj.Entity as HeadBar;
            headBarObj.Target = characterObj;

            character.DeathEvent += (x) =>
            {
                RemoveEntity(entity);

                headBar.OnRemoveData();

                var dropDataInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<DropDataInfo>(nameof(DropDataInfo), character.Id);

                dropDataInfo.DropGoodsInfos?.ForEach(x =>
                {
                    if (x.Probability >= Random.Range(0, 101)) 
                    {
                        GameApplication.Instance.EntityController.Spawn<DropItem, DropItemObject>(100, character.Transform.position, Quaternion.identity, null, x.Id);
                    }
                });

                dropDataInfo.DropItemInfos?.ForEach(x =>
                {
                    if (x.Probability >= Random.Range(0, 101))
                    {
                        GameApplication.Instance.EntityController.Spawn<DropItem, DropItemObject>(100, character.Transform.position, Quaternion.identity, null, x.Id);
                    }
                });
            };

            if (character is Hero)
            {
                var equipWeaponId = GameApplication.Instance.PlayerManager.GetDBHeroInfoById(entity.Id).equipWeaponId;
                if(equipWeaponId != 0) GameApplication.Instance.PlayerManager.EquipWeapon(characterObj, equipWeaponId);
            }
        }

        if (parent != null && parent.GetComponent<CharacterObject>())
        {
            var character = parent.GetComponent<CharacterObject>().Entity as Character;

            if (entity is VFX)
            {
                character.DeathEvent += (x) =>
                {
                    entity.Transform.parent = null;

                    RemoveEntity(entity);
                };
             }
        }

        return entityObj;
    }

    /// <summary>
    /// 엔티티와 엔티티 오브젝트를 생성합니다.
    /// </summary>
    /// <typeparam name="T">엔티티</typeparam>
    /// <typeparam name="K">엔티티 오브젝트</typeparam>
    /// <param name="id">아이디</param>
    /// <param name="position">생성 위치</param>
    /// <param name="rotation">생성 회전</param>
    /// <param name="caster">시전자</param>
    /// <param name="parent">부모에 생성 여부</param>
    /// <param name="value1">임시 데이터1</param>
    /// <returns>엔티티 오브젝트를 리턴합니다.</returns>
    public K Spawn<T, K>(int id, Vector3 position, Quaternion rotation, ActorObject caster, Transform parent = null, int value1 = 0) where T : Entity where K : EntityObject
    {
        var runTimeData = GameApplication.Instance.GameModel.RunTimeData;

        var entity = runTimeData.AddData(typeof(T).ToString(), id) as T;

        var prefabInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<PrefabInfo>(nameof(PrefabInfo), id);

        var entityObj = PoolObjectContainer.CreatePoolObject<K>(prefabInfo.Path);
        entityObj.gameObject.SetActive(false);

        if (rotation != Quaternion.identity) entityObj.transform.rotation = rotation;
        entityObj.transform.position = position;

        if (parent != null) entityObj.transform.SetParent(parent, false);

        entityObj.Caster = caster;

        entityObj.gameObject.SetActive(true);

        // 나중에 따로 분리시켜야 됨
        if (entity is DamageFont)
        {
            var damageFont = entity as DamageFont;
            damageFont.Value = value1;
        }        // 나중에 따로 분리시켜야 됨
        if (entity is DropItem)
        {
            var dropItem = entity as DropItem;
            dropItem.Value = value1;
        }

        entity.Init(entityObj.transform);
        entityObj.Init(entity);

        entity.OnDataRemove += (data) =>
        {
            RemoveEntity(data);
        };

        runTimeData.AddData($"{typeof(T).Name}Object", entity.InstanceId, entityObj);

        // 나중에 따로 분리시켜야 됨
        if (entity is Character)
        {
            var character = entity as Character;
            var characterObj = entityObj as CharacterObject;

            var headBarObj = GameApplication.Instance.EntityController.Spawn<HeadBar, HeadBarObject>(50001, characterObj.HeadBarNode.position, Quaternion.identity, GameObject.Find("DynamicOverlayCanvas").transform);
            var headBar = headBarObj.Entity as HeadBar;
            headBarObj.Target = characterObj;

            character.DeathEvent += (x) =>
            {
                RemoveEntity(entity);

                headBar.OnRemoveData();

                var dropDataInfo = GameApplication.Instance.GameModel.PresetData.ReturnData<DropDataInfo>(nameof(DropDataInfo), character.Id);

                dropDataInfo.DropGoodsInfos?.ForEach(x =>
                {
                    Debug.Log(x.Probability);
                    if (x.Probability >= Random.Range(0, 101))
                    {
                        GameApplication.Instance.EntityController.Spawn<DropItem, DropItemObject>(100, character.Transform.position, Quaternion.identity, null, x.Id);
                    }
                });

                dropDataInfo.DropItemInfos?.ForEach(x =>
                {
                    if (x.Probability >= Random.Range(0, 101))
                    {
                        GameApplication.Instance.EntityController.Spawn<DropItem, DropItemObject>(100, character.Transform.position, Quaternion.identity, null, x.Id);
                    }
                });
            };
        }

        return entityObj;
    }

    public void RemoveEntity(IData data)
    {
        var runTimeData = GameApplication.Instance.GameModel.RunTimeData;

        var entityObjectTableName = $"{data.TableModel.TableName}Object";
        var entityObject = runTimeData.ReturnData<EntityObject>(entityObjectTableName, data.InstanceId);

        if (entityObject != null)
        {
            entityObject.OnRemoveEntity();

            runTimeData.RemoveData(data.TableModel.TableName, data.InstanceId);
            runTimeData.RemoveData(entityObjectTableName, data.InstanceId);

            if (entityObject.transform.parent != null) entityObject.transform.SetParent(null, false);
        }
    }
}
