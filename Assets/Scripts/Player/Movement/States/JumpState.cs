namespace Player.States
{
    internal class JumpState : DefaultMovementState
    {
        private bool _isJumping = false;
        private bool _jumpPeakGotten = false;
        private float _currentYVelocity;
        private float _configJumpVelocity;
        private float _configGravityY;
        
        public JumpState(Config.PlayerConfig playerConfig, PlayerComponents playerComponents) : base(playerConfig, playerComponents)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            _jumpPeakGotten = false;
            _currentYVelocity = 0f;
            
            _configJumpVelocity = Config.JumpVelocity;
            _configGravityY = Config.GravityY;
            
            StartJump();
        }

        public override void Update(float deltaTime)
        {
            _currentYVelocity = Components.PlayerCharacterController.velocity.y;
            
            if (_currentYVelocity >= _configJumpVelocity)
            {
                _jumpPeakGotten = true;
            } else if (_jumpPeakGotten && _currentYVelocity > _configGravityY)
            {
                _currentYVelocity -= _configGravityY * deltaTime;
            }
            else if (Components.PlayerCharacterController.isGrounded)
            {
                _isJumping = false;
                Components.MovementStateMachine.TransitionToState(Components.StorageStates.Idle);
            }

            _currentYVelocity += _configJumpVelocity - (_configJumpVelocity * deltaTime);
        }

        public override void Exit()
        {
            base.Exit();
        }

        private void StartJump()
        {
            if (Components.PlayerCharacterController.isGrounded)
            {
                Components.PlayerAnimator.SetTrigger(Config.JumpTriggerName);
                _isJumping = true;
            }
        }
        
        protected override void TryChangeState(IMovementState nextState)
        {
            if (!_isJumping)
            {
                Components.MovementStateMachine.TransitionToState(nextState);
            }
        }
    }
}

