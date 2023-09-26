using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace _SampleJorgeTorrent.Code.Characters.Performers
{
    public delegate void AnimationEventCallback();
    public class AnimationEventsDispatcher : MonoBehaviour
    {
        public Dictionary<string, List<AnimationEventCallback>> m_animationIdToAnimationEvent;

        public void Configure()
        {
            m_animationIdToAnimationEvent = new Dictionary<string, List<AnimationEventCallback>>();
        }

        public void SubscribeEventCallbackToAnimation(string eventId, AnimationEventCallback eventCallback)
        {
            AddAnimationIdIfNotAddedYet(eventId);
            m_animationIdToAnimationEvent[eventId].Add(eventCallback);
        }

        private void AddAnimationIdIfNotAddedYet(string eventId)
        {
            if (!m_animationIdToAnimationEvent.ContainsKey(eventId))
            {
                m_animationIdToAnimationEvent.Add(eventId, new List<AnimationEventCallback>());
            }
        }

        public void DispatchAnimationEvent(string eventId)
        {
            Assert.IsTrue(m_animationIdToAnimationEvent.ContainsKey(eventId));
            List<AnimationEventCallback> eventCallbacks = m_animationIdToAnimationEvent[eventId];
            foreach (AnimationEventCallback eventCallback in eventCallbacks)
            {
                eventCallback();
            }
        }
    }
}