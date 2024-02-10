using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterObject : ActorObject, IHeadBarSubject
{
    public Quaternion NormalDirection { get; set; }

    [field: SerializeField] public Transform HeadBarNode { get; set; }
    [field: SerializeField] public Transform leftHandNode { get; private set; }
    [field: SerializeField] public Transform RightHandNode { get; private set; }

    private WeaponObject weaponObject;
    public WeaponObject WeaponObject
    {
        get
        {
            return weaponObject;
        }
        set
        {
            weaponObject = value;

            if (weaponObject != null)
            {
                animator.SetInteger("WeaponId", weaponObject.Entity.Id);
                animator.SetTrigger("OnSwapWeapon");
            }
            else
            {
                animator.SetInteger("WeaponId", 0);
                animator.SetTrigger("OnSwapWeapon");
            }
        }
    }

    public StateMachine StateMachine { get; private set; }

    public override void Init(Entity entity)
    {
        base.Init(entity);

        UpperBodyStates = new Dictionary<StateMachine.UpperBodyStateTypes, IState>();
        LowerBodyStates = new Dictionary<StateMachine.LowerBodyStateTypes, IState>();

        var upperBodyIdle = new IdleState();
        var upperBodyMove = new WalkState();
        var upperBodyRun = new RunState();
        var upperBodyAttack = new AttackState();
        var upperBodyDeath = new DeathState();

        UpperBodyStates.Add(StateMachine.UpperBodyStateTypes.Idle, upperBodyIdle);
        UpperBodyStates.Add(StateMachine.UpperBodyStateTypes.Walk, upperBodyMove);
        UpperBodyStates.Add(StateMachine.UpperBodyStateTypes.Run, upperBodyRun);
        UpperBodyStates.Add(StateMachine.UpperBodyStateTypes.Attack, upperBodyAttack);
        UpperBodyStates.Add(StateMachine.UpperBodyStateTypes.Death, upperBodyDeath);

        var lowerBodyIdle = new IdleState();
        var lowerBodyMove = new WalkState();
        var lowerBodyRun = new RunState();
        var lowerBodyAttack = new AttackState();
        var lowerBodyDeath = new DeathState();

        LowerBodyStates.Add(StateMachine.LowerBodyStateTypes.Idle, lowerBodyIdle);
        LowerBodyStates.Add(StateMachine.LowerBodyStateTypes.Walk, lowerBodyMove);
        LowerBodyStates.Add(StateMachine.LowerBodyStateTypes.Run, lowerBodyRun);
        LowerBodyStates.Add(StateMachine.LowerBodyStateTypes.Attack, lowerBodyAttack);
        LowerBodyStates.Add(StateMachine.LowerBodyStateTypes.Death, lowerBodyDeath);

        StateMachine = new StateMachine(upperBodyIdle, upperBodyMove);

        var character = Entity as Character;
        /*
        LowerBodyStates[StateMachine.LowerBodyStateTypes.Idle].enterEvent += () =>
        {
            Rotate(NormalDirection);
        };*/

        LowerBodyStates[StateMachine.LowerBodyStateTypes.Walk].enterEvent += () =>
        {
            character.CurrentMoveSpeed = 10;
            NavMeshAgent.speed = character.CurrentMoveSpeed;

            animator.SetFloat("speed", character.CurrentMoveSpeed);
        };/*
        LowerBodyStates[StateMachine.LowerBodyStateTypes.Walk].updateEvent += () =>
        {
            Rotate(NormalDirection);
            Move(transform.forward, character.CurrentMoveSpeed);
        };*/
        LowerBodyStates[StateMachine.LowerBodyStateTypes.Walk].exitEvent += () =>
        {
            character.CurrentMoveSpeed = 0;
            NavMeshAgent.speed = character.CurrentMoveSpeed;

            animator.SetFloat("speed", character.CurrentMoveSpeed);
        };

        LowerBodyStates[StateMachine.LowerBodyStateTypes.Run].enterEvent += () =>
        {
            character.CurrentMoveSpeed = 14;
            NavMeshAgent.speed = character.CurrentMoveSpeed;

            animator.SetFloat("speed", character.CurrentMoveSpeed);
        };/*
        LowerBodyStates[StateMachine.LowerBodyStateTypes.Run].updateEvent += () =>
        {
            Rotate(NormalDirection);
            Move(transform.forward, character.CurrentMoveSpeed);
        };*/
        LowerBodyStates[StateMachine.LowerBodyStateTypes.Run].exitEvent += () =>
        {
            character.CurrentMoveSpeed = 0;
            NavMeshAgent.speed = character.CurrentMoveSpeed;

            animator.SetFloat("speed", character.CurrentMoveSpeed);
        };

        UpperBodyStates[StateMachine.UpperBodyStateTypes.Attack].enterEvent += () =>
        {
            animator.SetInteger("AttackIndex", Random.Range(0, MotionHandler.AttackStateCount));
            animator.SetTrigger("onAttack");
        };
        LowerBodyStates[StateMachine.LowerBodyStateTypes.Attack].enterEvent += () =>
        {
          //  animator.SetTrigger("onAttack");
        };

        UpperBodyStates[StateMachine.UpperBodyStateTypes.Death].enterEvent += () =>
        {
        };

        StateMachine.setMachine(UpperBodyStates[StateMachine.UpperBodyStateTypes.Idle], LowerBodyStates[StateMachine.LowerBodyStateTypes.Idle]);

        character.DeathEvent += (x) =>
        {
            OnDeath();
        };
    }

    public void OnHit(int damage)
    {
        var character = Entity as Character;
        character.OnHit(damage);
    }

    public void OnHeal(int heal)
    {
        var character = Entity as Character;
        character.OnHeal(heal);
    }

    public void OnDeath()
    {
        StateMachine.setMachine(UpperBodyStates[StateMachine.UpperBodyStateTypes.Death], LowerBodyStates[StateMachine.LowerBodyStateTypes.Death]);
    }

    public IEnumerator OnSkillUseAsync(SkillTool skillTool, ActorObject[] targets1, ActorObject[] targets2 = null)
    {
        var character = Entity as Character;

        skillTool.IsCooldownTime = true;

        for (int i = 0; i < skillTool.Strategys.Length; i++)
        {
            yield return new WaitForSeconds(skillTool.Skill.DamageDelays[i]);

            ActorObject[] resTargets;
            if (targets2 != null && i == 1)
            {
                resTargets = targets2;
            }
            else
            {
                resTargets = targets1;
            }

            if (skillTool.Skill.VFXIds[0][0] == 70002)
            {
                for (int j = 0; j < resTargets.Length; j++)
                {
                    GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(skillTool.Skill.VFXIds[0][0], resTargets[j].HitNode.position, transform.rotation, this);
                }
            }
            else if (skillTool.Skill.VFXIds[0][0] == 70003)
            {
                for (int j = 0; j < resTargets.Length; j++)
                {
                    GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(skillTool.Skill.VFXIds[0][0], resTargets[j].HitNode.localPosition, transform.rotation, resTargets[j].transform);
                }
            }
            else
            {
                GameApplication.Instance.EntityController.Spawn<VFX, VFXObject>(skillTool.Skill.VFXIds[0][animator.GetInteger("AttackIndex")], HitNode.position, transform.rotation);
            }

            var count = 0;
            switch (skillTool.Skill.RepeatCounts[i])
            {
                case -1: count = targets1.Length; break;
                default: count = skillTool.Skill.RepeatCounts[i]; break;
            }

            for (int j = 0; j < count; j++)
            {
                for (int k = 0; k < skillTool.Skill.StatAbility.SkillDPSCount; k++)
                {
                    skillTool.Strategys[i].OnUseAsync(resTargets, new IStat[2] { skillTool.Skill, Entity }.Concat(character.SubItems).ToArray());

                    if (skillTool.Skill.Type != Skill.Types.AttackPassive)
                    {
                        switch (skillTool.Skill.StrategyType)   
                        {
                            case Skill.StrategyTypes.Attack:
                                (Entity as Character).SkillTools.ForEach(x =>
                                {
                                    if (x.Skill.Type == Skill.Types.AttackPassive)
                                    {
                                        skillTool.Strategys[i].OnUseAsync(resTargets, new IStat[2] { skillTool.Skill, Entity }.Concat(character.SubItems).ToArray());

                                        StartCoroutine(OnSkillUseAsync(x, resTargets));
                                    }
                                });

                                break;

                            case Skill.StrategyTypes.AttackHeal:
                                (Entity as Character).SkillTools.ForEach(x =>
                                {
                                    if (x.Skill.Type == Skill.Types.AttackPassive)
                                    {
                                        skillTool.Strategys[i].OnUseAsync(resTargets, new IStat[2] { skillTool.Skill, Entity }.Concat(character.SubItems).ToArray());

                                        StartCoroutine(OnSkillUseAsync(x, resTargets));
                                    }
                                });

                                break;
                        }
                    }

                    yield return new WaitForSeconds(skillTool.Skill.StatAbility.SkillDPSSecond);
                }
            }
        }

        yield return new WaitForSeconds(skillTool.Skill.StatAbility.CooldownTime);

        skillTool.IsCooldownTime = false;
    }
}
 