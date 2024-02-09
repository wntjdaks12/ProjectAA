using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityObject : PoolableObject
{
    [field : SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    [field: SerializeField] public MotionHandler MotionHandler { get; private set; }

    public Entity Entity { get; private set; }

    public Dictionary<StateMachine.UpperBodyStateTypes, IState> UpperBodyStates { get; protected set; }
    public Dictionary<StateMachine.LowerBodyStateTypes, IState> LowerBodyStates { get; protected set; }
    public EntityObjectClickedEvent onClick { get; set; }

    public ActorObject Caster { get; set; }

    public class EntityObjectClickedEvent
    {
        public delegate void OnClickAction();
        public event OnClickAction onClickEvent;

        public void AddListener(OnClickAction call)
        {
            onClickEvent += call;
        }

        public void RemoveListener(OnClickAction call)
        {
            onClickEvent -= call;
        }

        public void OnClick()
        {
            onClickEvent?.Invoke();
        }
    }

    public virtual void Init(Entity entity)
    {
        Entity = entity;

        MotionHandler?.Init();

        onClick = new EntityObjectClickedEvent();

        if (Entity.Lifetime != 0 && gameObject.activeInHierarchy) StartCoroutine(Entity.StartLifeTime());
    }

    public void OnRemoveEntity()
    {
        ReturnPoolableObject();
    }
    /*
    public void Move(Vector3 normalDirection, float speed)
    {
        transform.position += normalDirection * Time.deltaTime * speed;
    }

    public void Rotate(Quaternion eulerAngles)
    {
        transform.rotation = eulerAngles;
    }*/

    public void OnMouseDown()
    {
    }

    public void OnMouseUp()
    {
        onClick?.OnClick();
    }
}
