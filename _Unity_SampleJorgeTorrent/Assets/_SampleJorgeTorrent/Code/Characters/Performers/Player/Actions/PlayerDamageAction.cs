using _SampleJorgeTorrent.Code.Characters.Performers.Enemies.Skeleton.Services;
using _SampleJorgeTorrent.Code.HealthSystem;
using _SampleJorgeTorrent.Code.Utilities.DesignPatterns.ServiceLocatorPattern;
using DG.Tweening;
using UnityEngine;

namespace _SampleJorgeTorrent.Code.Characters.Performers.Player.Actions
{
    public class PlayerDamageAction : PerformerAction
    {
        [SerializeField] private float _impulseVelocity = 10f;
        [SerializeField] private float _invulnerabilityDuration = 2.5f;
        [SerializeField] private float _invulnerabilityBlinkTime = 0.05f;

        private Health _playerHealth;
        private Transform _playerTransform;
        private Rigidbody _playerRigidbody;
        private Animator _playerAnimator;
        private AnimationEventsDispatcher _playerAnimationEventsDispatcher;
        private Renderer _playerRenderer;
        private Material[] _playerMaterials;
        private Transform _enemyTransform;

        protected override void StorePerformerServices(ServiceLocator performerServiceLocator)
        {
            _playerHealth = performerServiceLocator.GetService<Health>();
            _playerTransform = performerServiceLocator.GetService<Transform>();
            _playerRigidbody = performerServiceLocator.GetService<Rigidbody>();
            _playerAnimator = performerServiceLocator.GetService<Animator>();
            _playerAnimationEventsDispatcher = performerServiceLocator.GetService<AnimationEventsDispatcher>();
            _playerRenderer = performerServiceLocator.GetService<Renderer>();
            _playerMaterials = _playerRenderer.materials;
            _enemyTransform = performerServiceLocator.GetService<EnemyTransformWrapper>().Value;
        }

        protected override void DefinePerformanceConditions()
        {
            _playerHealth.OnDamaged += PerformIfAllowed;
            _playerAnimationEventsDispatcher.SubscribeEventCallbackToAnimation("OnRecoveredFromDamage", CancelIfActive);
        }

        protected override void Perform()
        {
            StartInvulnerability();
            SetImpulseAndDirection();
            SetDamageGraphicalResponse();
        }

        private void StartInvulnerability()
        {
            _playerHealth.SetVulnerability(false);
        }

        private void SetImpulseAndDirection()
        {
            Vector3 damageImpulseForward = Vector3.Normalize(_playerTransform.position - _enemyTransform.position);
            damageImpulseForward.y = 0;
            Vector2 damageImpulse = new Vector2(damageImpulseForward.x, damageImpulseForward.z) * _impulseVelocity;
            Tween damageVelocityChangeTween = DOTween.To(() => damageImpulse, x => damageImpulse = x, Vector2.zero, 0.5f);
            damageVelocityChangeTween.onUpdate = delegate ()
            {
                _playerRigidbody.velocity = new Vector3(damageImpulse.x, _playerRigidbody.velocity.y, damageImpulse.y);
            };
            Vector3 impulseForwardEulerAngles = Quaternion.LookRotation(damageImpulseForward).eulerAngles;
            _playerTransform.DORotate(impulseForwardEulerAngles, 0.3f);
        }

        private void SetDamageGraphicalResponse()
        {
            _playerAnimator.SetTrigger("Damaged");
            foreach (Material playerMaterial in _playerMaterials)
            {
                playerMaterial.DOColor(Color.red, 0.25f).SetLoops(2, LoopType.Yoyo);
            }
        }

        protected override void Cancel()
        {
            EndInvulnerability();
        }

        private void EndInvulnerability()
        {
            float invulnerabilityDuration = _invulnerabilityDuration;
            float momentWhenVisibilityToggled = invulnerabilityDuration - _invulnerabilityBlinkTime;
            Tween invulnerabilityTween = DOTween.To(() => invulnerabilityDuration, x => invulnerabilityDuration = x, 0, invulnerabilityDuration).SetEase(Ease.Linear);
            invulnerabilityTween.onUpdate = delegate ()
            {
                if (invulnerabilityDuration <= momentWhenVisibilityToggled)
                {
                    momentWhenVisibilityToggled -= _invulnerabilityBlinkTime;
                    ToggleRendererVisibility();
                }
            };
            invulnerabilityTween.onComplete = delegate ()
            {
                _playerRenderer.enabled = true;
                _playerHealth.SetVulnerability(true);
            };
        }

        private void ToggleRendererVisibility()
        {
            _playerRenderer.enabled = !_playerRenderer.enabled;
        }
    }
}