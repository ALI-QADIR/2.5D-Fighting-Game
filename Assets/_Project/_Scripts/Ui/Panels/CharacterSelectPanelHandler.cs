using System;
using System.Collections.Generic;
using Smash.StructsAndEnums;
using Smash.System;
using Smash.Ui.System;
using UnityEngine;

namespace Smash.Ui.Panels
{
    public class CharacterSelectPanelHandler : MonoBehaviour, IPanelHandler
    {
        [Header("Animation")]
        [SerializeField] private AnimationStrategy<Action> m_animationStrategy;
        [SerializeField] private PanelState m_panelState;
        
        [Header("View Port")]
        [SerializeField] private Transform m_viewportTransform;
        [SerializeField] private List<RectTransform> m_2PlayersRects; 
        [SerializeField] private List<RectTransform> m_3PlayersRects;
        [SerializeField] private List<RectTransform> m_4PlayersRects;
        [SerializeField] private List<RectTransform> m_5PlayersRects;
        [SerializeField] private List<RectTransform> m_6PlayersRects;
        
        private void Awake()
        {
            m_panelState = PanelState.SlidOutToRight;
            OpenPanel();
        }
        
        public Transform GetViewportTransform() => m_viewportTransform;

        public List<RectTransform> GetTransforms(int playerCount)
        {
            return playerCount switch
            {
                2 => m_2PlayersRects,
                3 => m_3PlayersRects,
                4 => m_4PlayersRects,
                5 => m_5PlayersRects,
                6 => m_6PlayersRects,
                _ => null
            };
        }

        public void OpenPanel()
        {
            if(m_panelState == PanelState.Open) return;
            m_animationStrategy.Activate(OnOpenComplete);
        }
		
        public void ClosePanel()
        {
            m_animationStrategy.Deactivate(OnCloseComplete);
        }
        
        private void OnOpenComplete()
        {
            m_panelState = PanelState.Open;
        }

        private void OnCloseComplete()
        {
            m_panelState = PanelState.SlidOutToRight;
            gameObject.SetActive(false);
        }
    }
}
