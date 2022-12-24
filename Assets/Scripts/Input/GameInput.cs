//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Settings/Input/GameInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace BeamUp.Input
{
    public partial class @GameInput : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputActions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""694bccfc-20f0-4945-b364-5eb365905582"",
            ""actions"": [
                {
                    ""name"": ""PlaceSteroid"",
                    ""type"": ""Button"",
                    ""id"": ""8a2df5c1-ea0c-4276-9618-0c101b9b3534"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PointerPosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2a222778-6071-4b08-b28b-4493e09968d1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ShootDrag"",
                    ""type"": ""Button"",
                    ""id"": ""b94a8e59-09b2-4d39-8cbb-4629ff3cfb0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""24ae4712-a9fe-4568-bb79-1d6c51dcf6eb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""PlaceSteroid"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""926c8819-ff20-4334-acdd-d6035ae546a5"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""PointerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad312dad-f1c5-4823-a023-6b316dbe00b1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gameplay"",
                    ""action"": ""ShootDrag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gameplay"",
            ""bindingGroup"": ""Gameplay"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_PlaceSteroid = m_Gameplay.FindAction("PlaceSteroid", throwIfNotFound: true);
            m_Gameplay_PointerPosition = m_Gameplay.FindAction("PointerPosition", throwIfNotFound: true);
            m_Gameplay_ShootDrag = m_Gameplay.FindAction("ShootDrag", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_PlaceSteroid;
        private readonly InputAction m_Gameplay_PointerPosition;
        private readonly InputAction m_Gameplay_ShootDrag;
        public struct GameplayActions
        {
            private @GameInput m_Wrapper;
            public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @PlaceSteroid => m_Wrapper.m_Gameplay_PlaceSteroid;
            public InputAction @PointerPosition => m_Wrapper.m_Gameplay_PointerPosition;
            public InputAction @ShootDrag => m_Wrapper.m_Gameplay_ShootDrag;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @PlaceSteroid.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlaceSteroid;
                    @PlaceSteroid.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlaceSteroid;
                    @PlaceSteroid.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlaceSteroid;
                    @PointerPosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPointerPosition;
                    @PointerPosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPointerPosition;
                    @PointerPosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPointerPosition;
                    @ShootDrag.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootDrag;
                    @ShootDrag.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootDrag;
                    @ShootDrag.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShootDrag;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @PlaceSteroid.started += instance.OnPlaceSteroid;
                    @PlaceSteroid.performed += instance.OnPlaceSteroid;
                    @PlaceSteroid.canceled += instance.OnPlaceSteroid;
                    @PointerPosition.started += instance.OnPointerPosition;
                    @PointerPosition.performed += instance.OnPointerPosition;
                    @PointerPosition.canceled += instance.OnPointerPosition;
                    @ShootDrag.started += instance.OnShootDrag;
                    @ShootDrag.performed += instance.OnShootDrag;
                    @ShootDrag.canceled += instance.OnShootDrag;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        private int m_GameplaySchemeIndex = -1;
        public InputControlScheme GameplayScheme
        {
            get
            {
                if (m_GameplaySchemeIndex == -1) m_GameplaySchemeIndex = asset.FindControlSchemeIndex("Gameplay");
                return asset.controlSchemes[m_GameplaySchemeIndex];
            }
        }
        public interface IGameplayActions
        {
            void OnPlaceSteroid(InputAction.CallbackContext context);
            void OnPointerPosition(InputAction.CallbackContext context);
            void OnShootDrag(InputAction.CallbackContext context);
        }
    }
}
