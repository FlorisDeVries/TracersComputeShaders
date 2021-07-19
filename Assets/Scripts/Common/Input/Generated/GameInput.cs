// GENERATED AUTOMATICALLY FROM 'Assets/Settings/GameInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""WorldInteraction"",
            ""id"": ""8d120590-4ab2-42a0-84e7-e8605ec19e71"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMove"",
                    ""type"": ""Value"",
                    ""id"": ""54f16783-2d1d-4f98-8ba3-9cd3bfe274df"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VerticalMove"",
                    ""type"": ""Value"",
                    ""id"": ""ea553c03-23ec-41b4-9b56-a492c7877247"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""63f0d097-7a90-4f66-868a-a72c9061b471"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseMove"",
                    ""type"": ""Value"",
                    ""id"": ""c81a3c0c-02f4-4790-ab58-561e990d17ae"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""77f260d6-1f65-4e76-be58-13eebc53d02b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0bf107bd-20a4-40ed-b332-5d672e8a1d75"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""65287a1e-8a36-4ee2-bc7c-1759581425ea"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c13872d3-77ec-46d9-9b49-cbeff97455c8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""df531577-2ad5-46c9-a79f-9c1e6033f253"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a3648e08-3784-4e18-982c-48490cf0555c"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""7a245749-cfc3-4502-b8b6-05dd224f6d6c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""979fc0c8-ee1f-43fd-9138-6e7875660304"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""24186ed3-4207-44cf-b886-8217a75ebcc4"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ff700491-11d5-44af-9639-389c1c819f4b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // WorldInteraction
        m_WorldInteraction = asset.FindActionMap("WorldInteraction", throwIfNotFound: true);
        m_WorldInteraction_HorizontalMove = m_WorldInteraction.FindAction("HorizontalMove", throwIfNotFound: true);
        m_WorldInteraction_VerticalMove = m_WorldInteraction.FindAction("VerticalMove", throwIfNotFound: true);
        m_WorldInteraction_Sprint = m_WorldInteraction.FindAction("Sprint", throwIfNotFound: true);
        m_WorldInteraction_MouseMove = m_WorldInteraction.FindAction("MouseMove", throwIfNotFound: true);
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

    // WorldInteraction
    private readonly InputActionMap m_WorldInteraction;
    private IWorldInteractionActions m_WorldInteractionActionsCallbackInterface;
    private readonly InputAction m_WorldInteraction_HorizontalMove;
    private readonly InputAction m_WorldInteraction_VerticalMove;
    private readonly InputAction m_WorldInteraction_Sprint;
    private readonly InputAction m_WorldInteraction_MouseMove;
    public struct WorldInteractionActions
    {
        private @GameInput m_Wrapper;
        public WorldInteractionActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMove => m_Wrapper.m_WorldInteraction_HorizontalMove;
        public InputAction @VerticalMove => m_Wrapper.m_WorldInteraction_VerticalMove;
        public InputAction @Sprint => m_Wrapper.m_WorldInteraction_Sprint;
        public InputAction @MouseMove => m_Wrapper.m_WorldInteraction_MouseMove;
        public InputActionMap Get() { return m_Wrapper.m_WorldInteraction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WorldInteractionActions set) { return set.Get(); }
        public void SetCallbacks(IWorldInteractionActions instance)
        {
            if (m_Wrapper.m_WorldInteractionActionsCallbackInterface != null)
            {
                @HorizontalMove.started -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnHorizontalMove;
                @HorizontalMove.performed -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnHorizontalMove;
                @HorizontalMove.canceled -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnHorizontalMove;
                @VerticalMove.started -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnVerticalMove;
                @VerticalMove.performed -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnVerticalMove;
                @VerticalMove.canceled -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnVerticalMove;
                @Sprint.started -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnSprint;
                @MouseMove.started -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnMouseMove;
                @MouseMove.performed -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnMouseMove;
                @MouseMove.canceled -= m_Wrapper.m_WorldInteractionActionsCallbackInterface.OnMouseMove;
            }
            m_Wrapper.m_WorldInteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HorizontalMove.started += instance.OnHorizontalMove;
                @HorizontalMove.performed += instance.OnHorizontalMove;
                @HorizontalMove.canceled += instance.OnHorizontalMove;
                @VerticalMove.started += instance.OnVerticalMove;
                @VerticalMove.performed += instance.OnVerticalMove;
                @VerticalMove.canceled += instance.OnVerticalMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @MouseMove.started += instance.OnMouseMove;
                @MouseMove.performed += instance.OnMouseMove;
                @MouseMove.canceled += instance.OnMouseMove;
            }
        }
    }
    public WorldInteractionActions @WorldInteraction => new WorldInteractionActions(this);
    public interface IWorldInteractionActions
    {
        void OnHorizontalMove(InputAction.CallbackContext context);
        void OnVerticalMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnMouseMove(InputAction.CallbackContext context);
    }
}
