using UnityEngine;

namespace Scriptables.Ammo
{
    public interface IDefaultAmmo
    {
        GameObject AmmoPrefab { get; }
        float FiringForce { get; }

        public string ToString();
    }
}
