using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class CharacterAI : EntityAI
{
    private IEnumerator startRandomAsync;

    protected override void Start()
    {
        var characterObject = EntityObject as CharacterObject;
        characterObject.NavMeshAgent.updateRotation = false;

        if (startRandomAsync == null)
        {
            startRandomAsync = StartRandom();

            StartCoroutine(startRandomAsync);
        }
    }

    protected override void Update()
    {
        var characterObject = EntityObject as CharacterObject;
        var character = characterObject.Entity as Character;

        if (startRandomAsync == null)
        {
            startRandomAsync = StartRandom();

            StartCoroutine(startRandomAsync);
        }

        if (characterObject.MotionHandler.isAttack) return;  // ��ų ��� ����� �ƴ� ��� 

        var targets = character.GetOrderByVisableObjects(EntityObject.transform);

        if (targets.Length > 0) // �þ߿� Ÿ���� ���� ���
        {
            var dist = Vector3.Distance(targets[0].transform.position, character.Transform.position);
            Vector3 dirVec3 = Vector3.zero;

            if (character.SkillTools == null) return;

            for (int i = 0; i < character.SkillTools.Count; i++)
            {
                if (character.SkillTools[i].Skill.Type == Skill.Types.Active)
                {
                    if (dist <= character.SkillTools[i].Skill.SkillAttackRange) // ��ų ���� ��
                    {
                        dirVec3 = (targets[0].transform.position - character.Transform.position).normalized * -1;

                        if (!character.SkillTools[i].IsCooldownTime) // ��ų ���� �ƴ� ���
                        {
                            dirVec3 = (targets[0].transform.position - character.Transform.position).normalized;

                            EntityObject.transform.rotation = Quaternion.LookRotation(dirVec3);

                            characterObject.StateMachine.setMachine(EntityObject.UpperBodyStates[StateMachine.UpperBodyStateTypes.Attack], EntityObject.LowerBodyStates[StateMachine.LowerBodyStateTypes.Attack]);

                            int count = 0;
                            if (character.SkillTools[i].Skill.TargetCounts[0] == -1)
                            {
                                count = targets.Length;
                            }
                            else if (character.SkillTools[i].Skill.TargetCounts[0] <= targets.Length)
                            {
                                count = character.SkillTools[i].Skill.TargetCounts[0];
                            }
                            else
                            {
                                count = targets.Length;
                            }

                            var resTargets = new Collider[count];
                            for (int j = 0; j < count; j++)
                            {
                                resTargets[j] = targets[j];
                            }

                            switch (character.SkillTools[i].Skill.StrategyType)
                            {
                                case Skill.StrategyTypes.Attack: StartCoroutine(characterObject.OnSkillUseAsync(character.SkillTools[i], resTargets.Select(x => x.GetComponent<ActorObject>()).ToArray())); break;
                                case Skill.StrategyTypes.AttackHeal: StartCoroutine(characterObject.OnSkillUseAsync(character.SkillTools[i], resTargets.Select(x => x.GetComponent<ActorObject>()).ToArray(), new ActorObject[1] { EntityObject.GetComponent<ActorObject>() })); break;
                            }
                            /*
                            character.SkillTools.ForEach(x =>
                            {
                                if (x.Skill.Type == Skill.Types.Passive)
                                {
                                    StartCoroutine(characterObject.OnSkillUseAsync(x, resTargets.Select(x => x.GetComponent<ActorObject>()).ToArray()));
                                }
                            });*/

                            if (startRandomAsync != null)
                            {
                                StopCoroutine(startRandomAsync);

                                startRandomAsync = null;
                            }
                            characterObject.NavMeshAgent.destination = transform.position;
                        }
                        else
                        {
                            var di = character.SkillTools[i].Skill.SkillAttackRange - dist;
                            if (-1f <= di && di <= 1f) // ��ų ��Ÿ� ���� ���� �� ���
                            {
                                characterObject.StateMachine.setMachine(EntityObject.UpperBodyStates[StateMachine.UpperBodyStateTypes.Idle], EntityObject.LowerBodyStates[StateMachine.LowerBodyStateTypes.Idle]);
                            }
                            else // ��ų ��Ÿ� ���� �������� ���� ���
                            {
                                EntityObject.transform.rotation = Quaternion.LookRotation(dirVec3);
       
                                characterObject.StateMachine.setMachine(EntityObject.UpperBodyStates[StateMachine.UpperBodyStateTypes.Walk], EntityObject.LowerBodyStates[StateMachine.LowerBodyStateTypes.Walk]);

                                characterObject.NavMeshAgent.destination = targets[0].transform.position + character.SkillTools[i].Skill.SkillAttackRange * dirVec3;
                            }
                        }
                    }
                    else // ��ų ���� �ƿ�
                    {
                        dirVec3 = (targets[0].transform.position - character.Transform.position).normalized;

                        EntityObject.transform.rotation = Quaternion.LookRotation(dirVec3);

                        characterObject.StateMachine.setMachine(EntityObject.UpperBodyStates[StateMachine.UpperBodyStateTypes.Walk], EntityObject.LowerBodyStates[StateMachine.LowerBodyStateTypes.Walk]);

                        characterObject.NavMeshAgent.destination = targets[0].transform.position;
                    }
                }
            }
        }
        else  // �þ߿� Ÿ���� ���� ���
        {
            characterObject.StateMachine.setMachine(EntityObject.UpperBodyStates[StateMachine.UpperBodyStateTypes.Walk], EntityObject.LowerBodyStates[StateMachine.LowerBodyStateTypes.Walk]);

            EntityObject.transform.rotation = Quaternion.LookRotation(characterObject.NavMeshAgent.desiredVelocity);

            if (startRandomAsync == null)
            {
                startRandomAsync = StartRandom();

                StartCoroutine(startRandomAsync);
            }
        }

        characterObject.StateMachine.OperateUpdate();
    }

    public IEnumerator StartRandom()
    {
        var characterObject = EntityObject as CharacterObject;

        while (true)
        {
            var point = RandomPoint();
            characterObject.NavMeshAgent.destination = point;

            yield return new WaitUntil(() => (transform.position - point).sqrMagnitude < 0.1f);
        }
    }

    public Vector3 RandomPoint()
    {
        NavMeshHit navMeshHit;

        while (true)
        {
            var randCirclePos = Random.insideUnitCircle * 10;
            var spawnPos = transform.position; spawnPos.x += randCirclePos.x; spawnPos.z += randCirclePos.y;
            if (NavMesh.SamplePosition(spawnPos, out navMeshHit, 1f, NavMesh.AllAreas))
            {
                return navMeshHit.position;
            }
        }
    }
}
