using System;
using Player.Config;
using UnityEngine;

namespace Player.States
{
    internal struct PlayerComponents
    {
        public Transform PlayerTransform { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public CharacterController PlayerCharacterController { get; private set; }
        public PlayerInput PlayerInputComponent { get; private set; }
        public InputObserver PlayerInputObserver { get; private set; }
        public PlayerMovementStateMachine MovementStateMachine { get; private set; }
        public StatesStorage StorageStates { get; private set; }
        
        internal PlayerComponents(GameObject stateMachineGameObject, StatesStorage statesStorage)
        {
            PlayerTransform = stateMachineGameObject.GetComponent<Transform>();
            PlayerAnimator = stateMachineGameObject.GetComponent<Animator>();
            PlayerCharacterController = stateMachineGameObject.GetComponent<CharacterController>();
            PlayerInputComponent = stateMachineGameObject.GetComponent<PlayerInput>();
            PlayerInputObserver = stateMachineGameObject.GetComponent<InputObserver>();
            MovementStateMachine = stateMachineGameObject.GetComponent<PlayerMovementStateMachine>();
            StorageStates = statesStorage;
        }
    }
    
    [RequireComponent(typeof(PlayerConfigComponent))]
    internal class StatesStorage: MonoBehaviour
    {
        public SpawnState Spawn { get; private set; }
        public DeathState Death { get; private set; }
        public RunState Run { get; private set; }
        public JumpState Jump { get; private set; }
        public IdleState Idle { get; private set; }
        public TurnLeftState TurnLeft { get; private set; }
        public TurnRightState TurnRight { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            PlayerComponents components = new PlayerComponents(gameObject, this);

            PlayerConfig playerConfig = GetComponent<PlayerConfigComponent>().PlayerCfg;
            
            Spawn = new SpawnState(playerConfig, components);
            Death = new DeathState(playerConfig, components);
            Run = new RunState(playerConfig, components);
            Jump = new JumpState(playerConfig, components);
            Idle = new IdleState(playerConfig, components);
            TurnLeft = new TurnLeftState(playerConfig, components);
            TurnRight = new TurnRightState(playerConfig, components);
        }
    }
}

