using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.InputNew {
    public class VRHandsPlayerInput : MonoBehaviour {
        // Should this player handle request assignment of an input device as soon as the component awakes?
        [FormerlySerializedAs("autoSinglePlayerAssign")]
        public bool autoAssignGlobal = true;

        public ActionMapSlot leftHandActionMapSlot, rightHandActionMapSlot;

        public PlayerHandle handle { get; set; }

        void Awake() {
            if (autoAssignGlobal) {
                handle = PlayerHandleManager.GetNewPlayerHandle();
                handle.global = true;

                ActionMapInput leftHandActionMap = ActionMapInput.Create(leftHandActionMapSlot.actionMap);
                List<InputDevice> leftInputDevice = new List<InputDevice>();
                leftInputDevice.Add(InputSystem.LookupDevice(typeof(VRInputDevice), 3));
                leftHandActionMap.TryInitializeWithDevices(leftInputDevice);
                leftHandActionMap.active = true;
                leftHandActionMap.blockSubsequent = false;
                // TODO: Handle blocking, etc.
                handle.maps.Add(leftHandActionMap);

                ActionMapInput rightHandActionMap = ActionMapInput.Create(rightHandActionMapSlot.actionMap);
                List<InputDevice> rightInputDevice = new List<InputDevice>();
                rightInputDevice.Add(InputSystem.LookupDevice(typeof(VRInputDevice), 4));
                rightHandActionMap.TryInitializeWithDevices(rightInputDevice);
                rightHandActionMap.active = true;
                // TODO: Handle blocking, etc.
                handle.maps.Add(rightHandActionMap);

                /*foreach (ActionMapSlot actionMapSlot in actionMaps)
				{
					ActionMapInput actionMapInput = ActionMapInput.Create(actionMapSlot.actionMap);
					actionMapInput.TryInitializeWithDevices(handle.GetApplicableDevices());
					actionMapInput.active = actionMapSlot.active;
					actionMapInput.blockSubsequent = actionMapSlot.blockSubsequent;
					handle.maps.Add(actionMapInput);
				}*/
            }
        }

        public T GetActions<T>() where T : ActionMapInput {
            if (handle == null)
                return null;
            return handle.GetActions<T>();
        }
    }
}