using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{

    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;
    [Space]
    [SerializeField] private bool canAttack;

    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOndashOver;
    [SerializeField] private bool canCreateCloneOnCounterAttack;

    [Header("Clone can duplicate")]
    [SerializeField] private bool canDuplicateClone;
    [SerializeField] private float chanceToDupicate;
    [Header("Crystal instead of clone")]
    public bool crystalInseadOfClone;


    public void CreateClone(Transform _clonePosition, Vector3 _offset)
    {
        if (crystalInseadOfClone)
        {
            SkillManager.instance.crystal.CreateCrystal();
            return;
        }

        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<Clone_Skill_Controller>().
            SetupClone(_clonePosition, cloneDuration, canAttack, _offset, FindClosestEnemy(newClone.transform), canDuplicateClone, chanceToDupicate);
    }

    public void CreateCloneOnDashStart()
    {
        if (createCloneOnDashStart)
        {
            CreateClone(player.transform, Vector3.zero);
        }
    }

    public void CreateCloneOnDashOver()
    {
        if (createCloneOndashOver)
        {
            CreateClone(player.transform, Vector3.zero);
        }
    }

    public void CreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (canCreateCloneOnCounterAttack)
        {
            StartCoroutine(CreateCloneWithDelay(_enemyTransform, new Vector3(1.5f * player.facingDir, 0)));
        }
    }

    private IEnumerator CreateCloneWithDelay(Transform _transform,Vector3 _offset)
    {
        yield return new WaitForSeconds(0.4f);
        CreateClone(_transform, _offset);
    }
}
