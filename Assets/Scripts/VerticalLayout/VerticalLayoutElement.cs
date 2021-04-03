using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameCore.UI.Layout
{
    
    public class VerticalLayoutElement : MonoBehaviour
    {
        [FormerlySerializedAs("SizeWidget")]
        [FormerlySerializedAs("container")]       
        [SerializeField]
        private UIWidget _sizeWidget;
        public UIWidget SizeWidget
        {
            get
            {
                if (_sizeWidget == null) _sizeWidget = GetComponent<UIWidget>();
                return _sizeWidget;
            }
            set
            {
                _sizeWidget = value;
            }
        }

        /// <summary>
        /// Позволяет элементу участвовать в выравнивании, даже если он неактивен.
        /// </summary>
        public bool DontShrinkIfInactiveInHierarchy = false;
        public bool ShrinkIfInactiveInHierarchy => !DontShrinkIfInactiveInHierarchy;

        public event Action OnSizeChanged;

        public void CheckIfSizeChangedAndNotify()
        {
            // TODO проверить реальное изменение размера
            NotifySizeChanged();
        }

        public void NotifySizeChanged()
        {
            OnSizeChanged?.Invoke();
        }

        protected virtual void OnEnable()
        {
            if (ShrinkIfInactiveInHierarchy)
            {
                NotifySizeChanged();
            }
        }

        void OnDisable()
        {
            if (ShrinkIfInactiveInHierarchy)
            {
                NotifySizeChanged();
            }
        }
    }
}