using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Platformer
{
   public class InputReader : ScriptableObject
    {

        public event UnityAction<Vector2> Move  = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction EnableMouseControlCamera = delegate { };
        public event UnityAction DisableMouseControlCamera = delegate { };

        // public Vector3 Direction 

        public void OnMove(InputAction.CallbackContext callbackContext)
        {

        }

        public void OnLook(InputAction.CallbackContext callbackContext)
        {

        }

        public void OnAttack(InputAction.CallbackContext callbackContext)
        {

        }

        public void OnMouseControlCamera(InputAction.CallbackContext callbackContext)
        {

        }

        public void OnRun(InputAction.CallbackContext callbackContext)
        {

        }
        public void OnJump(InputAction.CallbackContext callbackContext)
        {

        }
    }
}

