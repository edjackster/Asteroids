using System;
using UnityEngine;
using Zenject;

namespace Tools.Runtime
{
    public class ToolInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            var camera = Camera.main;

            if (camera is null)
                throw new NullReferenceException("Main camera not found");
            
            Container
                .Bind<Camera>()
                .FromInstance(Camera.main)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<ScreenEdgeTool>()
                .AsSingle()
                .NonLazy();
        }
    }
}