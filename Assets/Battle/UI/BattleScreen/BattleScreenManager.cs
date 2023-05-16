using BattleCore.ScreenEntity;
using BattleCore.UI.SummaryScreen;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleCore.UI
{
    public class BattleScreenManager : MultiCameraOverworldLayoutSystem
    {
        [field: SerializeField]
        public BattleScreenController BattleScreenController { get; private set; }
        [field: SerializeField]
        private RectTransform Background { get; set; }
        [field: SerializeField]
        private RectTransform Foreground { get; set; }
        [field: SerializeField]
        private Image BackgroundImage { get; set; }
        [field: SerializeField]
        private BattlegroundPreparedUnityEvent OnBattlegroundPrepared { get; set; }

        private Material BackgroundMaterial { get; set; }
        private Action<BattleResultType> BattleFinishedCallback { get; set; }

        private float Duration { get; set; } = 1;
        private const string BLAND_FACTOR_VARIABLE_NAME = "_BlendFactor";

        public void Initialize (Player firstPlayer, Player secondPlayer, Action<BattleResultType> battleFinishedCallback)
        {
            TargetNearClipPlane = 0.0f;
            TargetFarClipPlane = 30.0f;
            TargetFOV = 3f;

            Initialize();

            TargetActorLayerName = "Dialogue";
            FirstActor = new Actor(firstPlayer, TargetActorLayerName);
            SecondActor = new Actor(secondPlayer, TargetActorLayerName);
            BattleFinishedCallback = battleFinishedCallback;

            BackgroundMaterial = BackgroundImage.material;
            BackgroundMaterial.SetFloat(BLAND_FACTOR_VARIABLE_NAME, 0);
            InitializeBackgroundEnter(TweenBackgroundBlendFactor(1, 2));
        }

        private Tween TweenBackgroundBlendFactor (float targetValue, float time)
        {
            return DOTween.To(() => BackgroundMaterial.GetFloat(BLAND_FACTOR_VARIABLE_NAME), (float variable) => BackgroundMaterial.SetFloat(BLAND_FACTOR_VARIABLE_NAME, variable), targetValue, time);
        }

        private Sequence MoveActor (Transform actorTransform, Vector3 position, Quaternion rotation)
        {
            return DOTween.Sequence()
                .Join(actorTransform.DOMove(position, Duration))
                .Join(actorTransform.DORotate(rotation.eulerAngles, Duration));
        }

        protected override void HandleOnBackgroundEntered ()
        {
            InitializeMovingCharacters(DOTween.Sequence()
                            .Join(MoveActor(FirstActor.Model.transform, FirstActorTargetTransform.position, FirstActorTargetTransform.rotation))
                            .Join(MoveActor(SecondActor.Model.transform, SecondActorTargetTransform.position, SecondActorTargetTransform.rotation)));
        }

        protected override void HandleOnCharactersMoved ()
        {
            InitializeZoomIn(SetCameraValues(Duration));
        }

        protected override void HandleOnZoomInCompleted ()
        {
            OnBattlegroundPrepared.Invoke(FirstActor.Player, SecondActor.Player);
        }

        public Tweener SpawnEntity (Transform TargetTransform, Entity entityToSpawn, out BattleScreenEntityController spawnedEntity)
        {
            spawnedEntity = Instantiate(entityToSpawn.BaseEntityType.ModelPrefab, TargetTransform);
            spawnedEntity.transform.localScale = Vector3.zero;
            FirstActor.SetLayerOfTransform(spawnedEntity.transform);

            return spawnedEntity.transform.DOScale(Vector3.one, 1);
        }

        public void Close (BattleResultType battleResult)
        {
            BattleFinishedCallback(battleResult);

            CharactersCamera.nearClipPlane = MainCamera.nearClipPlane;
            CharactersCamera.farClipPlane = MainCamera.farClipPlane;
            InitializeZoomOut(CharactersCamera.DOOrthoSize(MainCamera.orthographicSize, Duration));
        }

        protected override void HandleOnZoomOutCompleted ()
        {
            InitializeMovingCharactersBack(DOTween.Sequence()
                   .Join(MoveActor(FirstActor.Model.transform, FirstActor.InitialPosition, FirstActor.InitialRotation))
                   .Join(MoveActor(SecondActor.Model.transform, SecondActor.InitialPosition, SecondActor.InitialRotation)));
        }

        protected override void HandleOnCharactersMovedBack ()
        {
            InitializeBackgroundExit(TweenBackgroundBlendFactor(0, 2));
        }

        protected override void HandleOnBackgroundExited ()
        {
            base.HandleOnBackgroundExited();

            FirstActor.ResetLayer();
            SecondActor.ResetLayer();
        }
    }
}
