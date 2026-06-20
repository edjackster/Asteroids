using Core.Signals;
using Core.Spawns;
using Gameplay.Player.PlayerState;
using Gameplay.Player.Shooting;
using Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Bullet _bulletPrefab;

        [SerializeField] private Transform _bulletParent;
        [SerializeField] private Transform _spawnPosition;

        [SerializeField] private Player _player;

        public override void InstallBindings()
        {
            BindBulletPool();
            BindServices();
            BindPlayer();
            DeclareSignals();
            BindPlayerStates();
        }

        private void BindPlayerStates()
        {
            Container
                .BindInterfacesAndSelfTo<DeadState>()
                .AsTransient();
            Container
                .BindInterfacesAndSelfTo<ConsciousState>()
                .AsTransient();
            Container
                .Bind<UnconsciousState>()
                .AsTransient();

            Container
                .BindInterfacesAndSelfTo<PlayerStateChanger>()
                .AsSingle()
                .NonLazy();
        }

        private void BindServices()
        {
            Container
                .BindInterfacesAndSelfTo<BulletSpawner>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayer()
        {
            Container
                .BindInterfacesAndSelfTo<HealthSystem>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<LaserAmmunition>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ScoreCounter>()
                .AsSingle();

            Container
                .BindInstance(_player)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerCollisionHandler>()
                .AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<PlayerHitSignal>();
            Container.DeclareSignal<DespawnSignal<Bullet>>();
        }

        private void BindBulletPool()
        {
            BindPoolTool.Bind<Bullet>(Container, _bulletPrefab, _spawnPosition, _bulletParent);
        }
    }
}