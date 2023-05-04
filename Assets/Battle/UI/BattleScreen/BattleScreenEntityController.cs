using BattleCore.UI;
using System;
using System.Collections;
using UnityEngine;

namespace BattleCore.ScreenEntity
{
    public class BattleScreenEntityController : MonoBehaviour
    {
        [field: SerializeField]
        private Animator Animator { get; set; }
        [field: SerializeField]
        private Transform OnHitEffectTransform { get; set; }
        [field: SerializeField]
        private Transform DamageIndicatorTransform { get; set; }

        private Entity BoundEntity { get; set; }

        private bool IsAttackAnimationInContact = false;
        private Canvas DamageIndicatorCanvas { get; set; }
        private DamageIndicator DamageIndicatorPrefab { get; set; }
        private EntityStats EntityStats { get; set; }

        public void Initialize (Entity entity, Canvas damageIndicatorCanvas, DamageIndicator damageIndicatorPrefab, EntityStats entityStats)
        {
            BoundEntity = entity;
            DamageIndicatorCanvas = damageIndicatorCanvas;
            DamageIndicatorPrefab = damageIndicatorPrefab;
            EntityStats = entityStats;
            BoundEntity.IsAlive.OnVariableChange += HandleOnAliveStateChange;
            BoundEntity.OnDamaged += HandleOnEntityDamaged;
        }

        public void DestroyEntityStats ()
        {
            Destroy(EntityStats.gameObject);
        }

        public void AttackAnimationCallback ()
        {
            IsAttackAnimationInContact = false;
        }

        public IEnumerator PlayAnimation (AnimationType animationToPlay)
        {
            IsAttackAnimationInContact = true;
            Animator.SetTrigger(Enum.GetName(typeof(AnimationType), animationToPlay));
            yield return new WaitUntil(() => IsAttackAnimationInContact == false);
        }

        public bool IsAnimatorIdle ()
        {
            return Animator.GetCurrentAnimatorStateInfo(0).IsName(Enum.GetName(typeof(AnimationType), AnimationType.IDLE));
        }

        private void HandleOnEntityDamaged (EntityDamageData damage)
        {
            StartCoroutine(PlayAnimation(AnimationType.GET_HIT));

            DamageIndicator spawnedIndicator = Instantiate(DamageIndicatorPrefab, DamageIndicatorCanvas.transform);
            spawnedIndicator.transform.position = DamageIndicatorTransform.position;
            spawnedIndicator.Initialize(damage.TotalDamage);

            if (damage.AttackEffect != null)
            {
                Instantiate(damage.AttackEffect, OnHitEffectTransform);
            }
        }

        private void HandleOnAliveStateChange (bool newValue)
        {
            StartCoroutine(PlayAnimation(AnimationType.DIE));
        }

        protected virtual void OnDestroy ()
        {
            BoundEntity.IsAlive.OnVariableChange -= HandleOnAliveStateChange;
            BoundEntity.OnDamaged -= HandleOnEntityDamaged;
        }
    }
}
