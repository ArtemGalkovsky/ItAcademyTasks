using System;
using System.Linq;
using UnityEngine;

namespace Scriptables.Guns
{
    [CreateAssetMenu(fileName = "SuperGun", menuName = "Guns/SuperGun")]
    public class SuperGun : DefaultGun
    {
        public override string ToString()
        {
            return "SuperGun";
        }
    }
}
