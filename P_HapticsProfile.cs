// Copyright © 2021 Pokeyi - https://pokeyi.dev - pokeyi@pm.me - This work is licensed under the MIT License.

// using System;
using UdonSharp;
using UnityEngine;
// using UnityEngine.UI;
using VRC.SDKBase;
// using VRC.SDK3.Components;
// using VRC.Udon.Common.Interfaces;

namespace Pokeyi.UdonSharp
{
    [AddComponentMenu("Pokeyi.VRChat/P.VRC Haptics Profile")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.NoVariableSync)] // No variables are serialized over network.

    public class P_HapticsProfile : UdonSharpBehaviour
    {   // Controller haptics profile for VRChat:
        [Header(":: VRC Haptics Profile by Pokeyi ::")]

        [Header("Event: \"_TriggerHaptics\"")]
        [Space] // SendCustomEvent("_TriggerHaptics")
        [Tooltip("Enable right hand haptic event.")]
        [SerializeField] private bool rightHand = true;
        [Tooltip("Enable left hand haptic event.")]
        [SerializeField] private bool leftHand = true;
        [Tooltip("Duration of haptic event.")]
        [SerializeField] [Range(0F, 1F)] private float hapticDuration = 0.25F;
        [Tooltip("Amplitude/strength of haptic event.")]
        [SerializeField] [Range(0F, 1F)] private float hapticAmplitude = 0.5F;
        [Tooltip("Frequency of haptic event.")]
        [SerializeField] [Range(0F, 1F)] private float hapticFrequency = 0.5F;
        [Tooltip("Amount of distance falloff for amplitude/strength of haptic event.")]
        [SerializeField] [Range(0F, 1F)] private float distanceFalloff = 0F;
        [Tooltip("Max distance within which haptic event can be received.")]
        [SerializeField] private float maxDistance = 0F;

        private VRCPlayerApi playerLocal; // Reference to local player.
        private VRC_Pickup.PickupHand pickupHandRight; // Reference to right pickup hand.
        private VRC_Pickup.PickupHand pickupHandLeft; // Reference to left pickup hand.

        public void Start()
        {   // Assign references:
            playerLocal = Networking.LocalPlayer;
            pickupHandRight = VRC_Pickup.PickupHand.Right;
            pickupHandLeft = VRC_Pickup.PickupHand.Left;
        }

        public void _TriggerHaptics() // *Public/Protected*
        {   // Calculate local player distance to determine amplitude falloff if enabled, play haptic events:
            float distance = Vector3.Distance(transform.position, playerLocal.GetPosition());
            if ((maxDistance > 0F) && (distance > maxDistance)) return;
            float falloff = distance * distanceFalloff;
            if (falloff < 1F) falloff = 1F;
            if (rightHand) playerLocal.PlayHapticEventInHand(pickupHandRight, hapticDuration, hapticAmplitude / falloff, hapticFrequency);
            if (leftHand) playerLocal.PlayHapticEventInHand(pickupHandLeft, hapticDuration, hapticAmplitude / falloff, hapticFrequency);
        }
    }
}

/* MIT License

Copyright (c) 2021 Pokeyi - https://pokeyi.dev - pokeyi@pm.me

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. */