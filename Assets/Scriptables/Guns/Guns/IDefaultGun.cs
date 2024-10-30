using UnityEngine;
using UnityEngine.Events;

namespace Scriptables.Guns
{
    public interface IDefaultGun
    { 
        public DefaultMagazine[] MagazineTypes { get; }
        public float PreviousFireTime { get; }

        public void SetupGun();
        public MagazinesStates Fire(DefaultMagazine magazine, Transform gunTransform);
        public string ToString();
    }
}

