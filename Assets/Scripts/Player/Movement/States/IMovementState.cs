using UnityEngine;

namespace Player.States
{
    internal interface IMovementState
    {
        public void Enter();
        public void Update(float deltaTime);
        public void Exit();
    }
}
