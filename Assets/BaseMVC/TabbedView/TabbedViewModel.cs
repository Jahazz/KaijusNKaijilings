using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.RuntimeTabbedView
{
    public class TabbedViewModel<EnumType, CreatedTabType, CreatedButtonType, TabbedViewViewType> : BaseModel<TabbedViewViewType>
        where EnumType : struct, IConvertible
        where CreatedTabType : TabbedViewTab<CreatedButtonType, EnumType>
        where CreatedButtonType : TabbedViewButton<EnumType>
        where TabbedViewViewType : TabbedViewView<EnumType, CreatedTabType, CreatedButtonType>
    {
        public delegate void TabSpawnedArguments (TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType> spawnedTab);
        public event TabSpawnedArguments OnTabSpawned;

        [field: SerializeField]
        private bool AutoInitialize { get; set; }

        protected override void Start ()
        {
            base.Start();

            if (AutoInitialize == true)
            {
                InitializeFromEnum();
            }
        }

        public void InitializeFromEnum ()
        {
            foreach (EnumType enumValue in Enum.GetValues(typeof(EnumType)))
            {
                SpawnTab(enumValue);
            }

            CurrentView.ShowFirstTab();
        }

        public virtual void SpawnTab (EnumType tabData)
        {
            TabbedViewButtonTabPair<CreatedButtonType, CreatedTabType, EnumType> spawnedTab = CurrentView.CreateTabAndButton(tabData);
            OnTabSpawned?.Invoke(spawnedTab);
        }
    }
}
