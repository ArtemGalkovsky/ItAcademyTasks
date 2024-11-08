// using System;
// using UnityEngine;
// using UnityEngine.InputSystem;
//
// namespace Player
// {
//     [RequireComponent(typeof(CharacterController), typeof(PlayerInput), typeof(PlayerRespawnAndDeath)),
//     RequireComponent(typeof(PlayerMovement), typeof(Animator))]
//     public class PlayerController : MonoBehaviour
//     {
//         private PlayerInput _playerInput;
//         private PlayerRespawnAndDeath _playerRespawnAndDeath;
//         private PlayerMovement _playerMovement;
//         
//         internal PlayerInputActions EnabledPlayerInputActions { get; private set; }
//     
//         private void Awake()
//         {
//             _playerInput = GetComponent<PlayerInput>();
//             _playerInput.Initialize();
//             EnabledPlayerInputActions = _playerInput.EnabledPlayerActions;
//             
//             _playerRespawnAndDeath = GetComponent<PlayerRespawnAndDeath>();
//             _playerRespawnAndDeath.Death.AddListener(OnPlayerDeath);
//             _playerRespawnAndDeath.Spawn.AddListener(() => OnPlayerRespawnState(isPlayerCompleteRespawn: false));
//             _playerRespawnAndDeath.SpawnEnds.AddListener(() => OnPlayerRespawnState(isPlayerCompleteRespawn: true));
//             
//             _playerMovement = GetComponent<PlayerMovement>();
//         }
//
//         private void OnPlayerDeath()
//         {
//             _playerMovement.ToggleMovement(canMoveNow: false);
//         }
//         
//         private void OnPlayerRespawnState(bool isPlayerCompleteRespawn)
//         {
//             _playerMovement.ToggleMovement(canMoveNow: isPlayerCompleteRespawn); // if respawn has been completed -> canMove = true, else -> false.
//         }
//     }
// }
//
