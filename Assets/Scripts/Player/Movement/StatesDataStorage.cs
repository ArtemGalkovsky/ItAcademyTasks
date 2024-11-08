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
        public StatesDataStorage DataStorageStatesData { get; private set; }
        
        internal PlayerComponents(GameObject stateMachineGameObject, StatesDataStorage statesDataStorage)
        {
            PlayerTransform = stateMachineGameObject.GetComponent<Transform>();
            PlayerAnimator = stateMachineGameObject.GetComponent<Animator>();
            PlayerCharacterController = stateMachineGameObject.GetComponent<CharacterController>();
            PlayerInputComponent = stateMachineGameObject.GetComponent<PlayerInput>();
            PlayerInputObserver = stateMachineGameObject.GetComponent<InputObserver>();
            MovementStateMachine = stateMachineGameObject.GetComponent<PlayerMovementStateMachine>();
            DataStorageStatesData = statesDataStorage;
        }
    }

    internal struct States
    {
        public SpawnState Spawn { get; private set; }
        public DeathState Death { get; private set; }
        public RunState Run { get; private set; }
        public JumpState Jump { get; private set; }
        public IdleState Idle { get; private set; }
        public TurnLeftState TurnLeft { get; private set; }
        public TurnRightState TurnRight { get; private set; }

        public States(StatesDataStorage statesDataStorage)
        {
            Spawn = new SpawnState(statesDataStorage);
            Death = new DeathState(statesDataStorage);
            Run = new RunState(statesDataStorage);
            Jump = new JumpState(statesDataStorage);
            Idle = new IdleState(statesDataStorage);
            TurnLeft = new TurnLeftState(statesDataStorage);
            TurnRight = new TurnRightState(statesDataStorage);
        }
    }
    
    [RequireComponent(typeof(PlayerConfigComponent), typeof(CharacterController), typeof(Animator)),
    RequireComponent(typeof(CharacterController), typeof(PlayerInput), typeof(InputObserver)),
    RequireComponent(typeof(PlayerMovementStateMachine))]
    internal class StatesDataStorage: MonoBehaviour
    {
        public States PlayerMovementStates { get; private set; }
        public PlayerComponents Components { get; private set; }
        public PlayerConfig Config { get; private set; }
        
        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            PlayerComponents components = new PlayerComponents(gameObject, this);
            
            PlayerConfig playerConfig = GetComponent<PlayerConfigComponent>().PlayerCfg;

            Config = playerConfig;
            Components = components;
            PlayerMovementStates = new States(this);
        }
    }
}

