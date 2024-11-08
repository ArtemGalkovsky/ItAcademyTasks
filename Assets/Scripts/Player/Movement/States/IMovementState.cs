using UnityEngine;

namespace Player.States
{
    public interface IMovementState
    {
        public void Enter();
        public void Update(float deltaTime);
        public void Exit();
    }
}
