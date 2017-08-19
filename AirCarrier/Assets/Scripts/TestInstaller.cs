using Zenject;
using UnityEngine;
using System.Collections;

public class TestInstaller : MonoInstaller
{
    public Object prefab;
    public override void InstallBindings()
    {
        //Container.Bind<IPanzer>().FromComponentInNewPrefab(prefab).AsSingle();
    }
}

public class Greeter
{
    public Greeter(string message)
    {
        Debug.Log(message);
    }
}


