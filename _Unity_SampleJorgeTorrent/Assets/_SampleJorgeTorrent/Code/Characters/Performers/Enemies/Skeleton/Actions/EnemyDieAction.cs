using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using DG.Tweening;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Actions
{
    public class EnemyDieAction : PerformerAction
    {
        private Material[] _enemyMaterials;
        private Animator _enemyAnimator;
        private Health _enemyHealth;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _enemyMaterials = performerServiceLocator.GetService<Renderer>().materials;
            _enemyAnimator = performerServiceLocator.GetService<Animator>();
            _enemyHealth = performerServiceLocator.GetService<Health>();
        }

        protected override void DefinePerformanceConditions()
        {
            _enemyHealth.OnKilled += PerformIfAllowed;
        }

        protected override void Perform()
        {
            SetDamageGraphicalResponse();
        }

        private void SetDamageGraphicalResponse()
        {
            _enemyAnimator.SetBool("IsDead", true);
            foreach (Material enemyMaterial in _enemyMaterials)
            {
                enemyMaterial.DOColor(Color.red, 0.25f).SetLoops(2, LoopType.Yoyo);
            }
        }

        protected override void Cancel()
        {
            _enemyAnimator.SetBool("IsDead", false);
        }
    }
}