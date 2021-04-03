using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

namespace GameCore.UI.Layout
{
    /// <summary>
    /// Компонент для вертикальной укладки элементов в контейнере в стопку от верхнего края контейнера.
    /// Удобно, когда элементы контейнера могут отключаться и нужно пересчитывать координаты оставшихся.
    /// Элементами контейнера считаются его непосредственные потомки с компонентом VerticalLayoutElement.
    /// </summary>
    public class VerticalLayout : VerticalLayoutElement
    {
        public enum LayoutMode { Vertical, Horizontal }

        public UIWidget container => SizeWidget;
        private int _savedContainerSize;
        private LayoutMode _savedContainerLayout;
        
        public LayoutMode LayoutType;

        /// <summary>
        /// Изменяет размер контейнера по размеру содержимого.
        /// </summary>
        public bool ResizeContainerToFit = false;

        private bool _initialized = false;

        private ObservableCollection<VerticalLayoutElement> _elements;
        private Dictionary<VerticalLayoutElement, Vector3> _savedState = new Dictionary<VerticalLayoutElement, Vector3>();

        protected override void OnEnable()
        {
            base.OnEnable();
            Align();
        }

        /// <summary>
        /// Возвращает элементы контейнера и его размер в изначальное положение (в момент вызова InitLayout).
        /// </summary>
        public void ResetInitialState()
        {
            if (container != null)
            {
                SetContainerSize(_savedContainerSize);
            }

            LayoutType = _savedContainerLayout;

            foreach (var kvp in _savedState)
            {
                kvp.Key.transform.localPosition = kvp.Value;
            }
        }

        private IEnumerable<VerticalLayoutElement> ScanForLayoutElements()
        {
            var immediateChildren = Enumerable.Range(0, transform.childCount).Select(i => transform.GetChild(i)).Where(c => c.parent == transform);
            var elements = immediateChildren.Select(c => c.GetComponent<VerticalLayoutElement>()).Where(vle => vle != null);
            return elements;
        }

        // Инициализировать контейнер с нуля. Находит дочерние VerticalLayoutElement у текущего GameObject'а и выравнивает их.
        public void InitLayoutFromScene()
        {
            InitLayout(new ObservableCollection<VerticalLayoutElement>(ScanForLayoutElements()));
        }

        /// <summary>
        /// Обновляет список элементов контейнера и выполняет выравнивание.
        /// </summary>
        public void UpdateLayoutFromScene()
        {
            var elements = ScanForLayoutElements();

            throw new NotImplementedException("TODO compare new elements with _elements and generate Observable collection changed events ");

            foreach (var element in elements)
            {
                if (!_savedState.ContainsKey(element)) _savedState.Add(element, element.transform.localPosition);
            }

            Align();
        }

        /// <summary>
        /// Инициализировать контейнер с нуля.
        /// Запоминает элементы контейнера и их изначальное относительное расположение.
        /// Это необходимо для отмены выравнивания и для того, чтобы корректно добавлять элементы в рантайме в уже выровненный контейнер.
        /// Например, если есть элемент а1 с позицией 1 и элемент а3 с позицией 3. В рантайме элемент а3 смещен в позицию 2 для уплотнения.
        /// При добавлении элемента а2 с позицией 2 он должен оказаться между а1 и а3, для этого нужно помнить их изначальное расположение.
        /// </summary>
        public void InitLayout(ObservableCollection<VerticalLayoutElement> elements)
        {
            if (container == null) throw new InvalidOperationException("UIWidget component not found.");
            _savedContainerSize = GetContainerSize();
            _savedContainerLayout = LayoutType;
            
            // Cleanup previous state
            Clean();
            if (_elements != null)
            {
                _elements.CollectionChanged -= OnObservableCollectionChanged;
            }

            _elements = elements;
            _elements.CollectionChanged += OnObservableCollectionChanged;

            // Запомнить стартовые позиции элементов
            foreach (var element in elements)
            {
                AddElement(element);
            }
           
            _initialized = true;
        }

        private void Clean()
        {
            while (_savedState.Any()) { RemoveElement(_savedState.Last().Key); }
        }

        private void AddElement(VerticalLayoutElement element)
        {
            element.OnSizeChanged += OnElementSizeChanged;
            _savedState.Add(element, element.transform.localPosition);
        }

        private void RemoveElement(VerticalLayoutElement element)
        {
            element.OnSizeChanged -= OnElementSizeChanged;
            _savedState.Remove(element);
        }

        private void OnObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    foreach(var newElement in e.NewItems.Cast<VerticalLayoutElement>())
                    {
                        AddElement(newElement);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    foreach(var oldElement in e.OldItems.Cast<VerticalLayoutElement>())
                    {
                        RemoveElement(oldElement);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    for(var i = 0; i < e.OldItems.Count; i++)
                    {
                        var oldElement = e.OldItems[i] as VerticalLayoutElement;
                        var newElement = e.NewItems[i] as VerticalLayoutElement;
                        RemoveElement(oldElement);
                        AddElement(newElement);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
					Clean();
                    foreach (var element in _elements)
                    {
                        AddElement(element);
                    }
					break;
                default:
                    break;
            }
            AlignElements();
        }

        private void OnElementSizeChanged()
        {
            AlignElements();
        }

        /// <summary>
        /// Выравнивает элементы контейнера в стопку от края контейнера согласно выбранной ориентации.
        /// </summary>
        public void Align()
        {
            if (!_initialized) InitLayoutFromScene();
            
            AlignElements();
        }

        /// <summary>
        /// Выравнивает элементы контейнера в стопку от края контейнера согласно выбранной ориентации.
        /// </summary>
        private void AlignElements()
        {
            if (!_initialized) throw new InvalidOperationException("Trying to align elements but layout is not initialized");

            // container.pivot = UIWidget.Pivot.TopLeft;
            var elements = _savedState.Keys;

            bool IsConsideredInLayout(VerticalLayoutElement element) 
            {
                if (element.DontShrinkIfInactiveInHierarchy) return true;

                // Sometimes layout is updated for inactive objects. In this case consider element's activeSelf
                if (container.gameObject.activeInHierarchy) return element.gameObject.activeInHierarchy;
                else return element.gameObject.activeSelf;
            }

            if (ResizeContainerToFit) 
            {
                var totalSpaceForElements = elements.Where(e => IsConsideredInLayout(e)).Aggregate(0, (acc, e) => { acc += GetElementSize(e); return acc; });
                SetContainerSize(totalSpaceForElements);
            }

            // Sort by position in starting state
            // Stack from the start of the container
            var spaceInUseFromStart = 0;
            foreach (var element in SortElements())
            {
                if (!IsConsideredInLayout(element)) continue;

                var oldPos = element.SizeWidget.transform.localPosition;
                
                var containerStartLocalCoord = GetContainerStartInLocalSpace();
                var elementPivotOffset = GetElementPivotOffset(element);
                switch(LayoutType)
                {
                    case LayoutMode.Horizontal:
                        SetElementPosition(element, (int)(containerStartLocalCoord + spaceInUseFromStart + elementPivotOffset));
                        break;
                    case LayoutMode.Vertical:
                        SetElementPosition(element, (int)(containerStartLocalCoord - spaceInUseFromStart - elementPivotOffset));
                        break;
                    default:
                        throw new NotImplementedException();
                }
                spaceInUseFromStart += GetElementSize(element);
            }
        }

        /// <summary>
        /// Возвращает координату, соответствующую началу контейнера в текущей ориентации в пространстве хозяина Layout'а.
        /// Учитывается то, что рамка контейнера и элементы не вложены друг в друга.
        /// </summary>
        private float GetContainerStartInLocalSpace()
        {
            switch (LayoutType)
            {
                case LayoutMode.Horizontal: return transform.InverseTransformPoint(container.transform.position).x - container.pivotOffset.x * container.width;
                case LayoutMode.Vertical: return transform.InverseTransformPoint(container.transform.position).y + (1 - container.pivotOffset.y) * container.height;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Возвращает смещение Pivot'а элемента в текущей ориентации в пикселях (насколько начало координат элемента смещено от его левого или верхнего края)
        /// </summary>
        private float GetElementPivotOffset(VerticalLayoutElement element)
        {
            switch (LayoutType)
            {
                case LayoutMode.Horizontal: return (1f - element.SizeWidget.pivotOffset.x) * element.SizeWidget.width;
                case LayoutMode.Vertical: return (1f - element.SizeWidget.pivotOffset.y) * element.SizeWidget.height;
                default: throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Возвращает размер элемента в текущей ориентации (ширину или высоту)
        /// </summary>
        private int GetElementSize(VerticalLayoutElement element)
        {
            switch (LayoutType)
            {
                case LayoutMode.Horizontal: return element.SizeWidget.width;
                case LayoutMode.Vertical: return element.SizeWidget.height;
                default: throw new NotImplementedException();
            }
        }

        private void SetElementPosition(VerticalLayoutElement element, int position)
        {
            var oldPos = element.SizeWidget.transform.localPosition;
            switch (LayoutType)
            {
                case LayoutMode.Horizontal: element.transform.localPosition = new Vector3(position, oldPos.y, oldPos.z); break;
                case LayoutMode.Vertical: element.transform.localPosition = new Vector3(oldPos.x, position, oldPos.z); break;
                default: throw new NotImplementedException(); 
            }
        }

        private void SetContainerSize(int size)
        {
            switch (LayoutType)
            {
                case LayoutMode.Horizontal: container.width = size; break;
                case LayoutMode.Vertical: container.height = size; break;
                default: throw new NotImplementedException(); 
            }
            CheckIfSizeChangedAndNotify();
        }

        private int GetContainerSize()
        {
            switch (LayoutType)
            {
                case LayoutMode.Horizontal: return container.width;
                case LayoutMode.Vertical: return container.height;
                default: throw new NotImplementedException(); 
            }
        }

        private IEnumerable<VerticalLayoutElement> SortElements()
        {
            switch (LayoutType)
            {
                case LayoutMode.Horizontal:
                    return _savedState.Keys.OrderBy(e => _savedState[e].x);
                case LayoutMode.Vertical:
                    return _savedState.Keys.OrderByDescending(e => _savedState[e].y);
                default: throw new NotImplementedException();
            }
        }
    }
}