using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Hierarchy;
using DevExpress.Xpf.Bars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Data;
using DevExpress.Xpf.Editors;
using DevExpress.Mvvm.UI.Interactivity;

namespace FixedRowExample
{
    public class FixedRow : Behavior<UIElement>
    {
        private HierarchyPanel _Panel;
        private ScrollViewer _Scroll;
        private ScrollContentPresenter _ScrollContent;
        private Border _RootBorder;
        private StackPanel _FixedRowPanel;
        private Double _RowHeight;
        private Double _ExtendedWidth = 0;
        private GridControl _Grid;
        private TableView _TableView;
        private FrameworkElement _Element;
        private bool _RowFixed;
        private int _RowHandle;

        protected override void OnAttached()
        {
            base.OnAttached();
            _Grid = this.AssociatedObject as GridControl;
            _Grid.Loaded += grid_Loaded;

            _Grid.UseLayoutRounding = true;
            _Grid.SnapsToDevicePixels = true;
        }

        void grid_Loaded(object sender, RoutedEventArgs e)
        {
            _TableView = _Grid.View as TableView;

            _Panel = (HierarchyPanel)LayoutHelper.FindElement(_TableView, (element) => { return element is HierarchyPanel; });
            _Scroll = (ScrollViewer)LayoutHelper.FindElement(_TableView, (element) => { return element is ScrollViewer; });
            _ScrollContent = (ScrollContentPresenter)LayoutHelper.FindElement(_TableView, (element) => { return element is ScrollContentPresenter; });

            _Scroll.ScrollChanged += _Scroll_ScrollChanged;
        }

        void _Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_RowFixed == true)
            {
                ChangeFixedRow(sender, e);
            }
        }

        public void CreateFixedRow(int rowHandle)
        {

            for (int j = 0; j < _Panel.Children.Count; j++)
            {
                if (_Panel.Children[j].GetType() == typeof(Border))
                {
                    _Panel.Children.RemoveAt(j);
                }
            }

            _Element = _TableView.GetRowElementByRowHandle(rowHandle);
            _RowHandle = rowHandle;

            _RowHeight = _Element.ActualHeight - 1;

            _RootBorder = new Border();
            _RootBorder.Name = "RootBorder";

            _FixedRowPanel = new StackPanel();
            _FixedRowPanel.Name = "FixedRowPanel";
            _FixedRowPanel.Orientation = Orientation.Horizontal;
            _FixedRowPanel.Background = Brushes.White;

            for (int i = 0; i < _Grid.Columns.Count; i++)
            {
                _FixedRowPanel.DataContext = _Grid.GetRow(rowHandle);

                Binding binding = new Binding();

                binding.Path = new PropertyPath(_Grid.Columns[i].FieldName);
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

                var editor = _Grid.Columns[i].ActualEditSettings.CreateEditor() as BaseEdit;

                editor.SetBinding(BaseEdit.EditValueProperty, binding);
                if (editor.GetType() != typeof(CheckEdit))
                {
                    editor.HorizontalContentAlignment = _Grid.Columns[i].ActualHorizontalContentAlignment;
                }
                else
                {
                    editor.HorizontalAlignment = HorizontalAlignment.Center;
                    editor.Loaded += (o, args) =>
                        {
                            var check = LayoutHelper.FindElement(editor.EditCore, element => element.Name == "checkbox");
                            if (check != null && check.Parent != null)
                                ((FrameworkElement)check.Parent).Margin = new Thickness(0);
                        };
                }

                editor.ShowBorder = false;

                Color brushColor = (Color)ColorConverter.ConvertFromString("#FFC3CEDC");

                _FixedRowPanel.Children.Add(new Border() { Width = _Grid.Columns[i].ActualHeaderWidth, BorderThickness = new Thickness(0, 0, 1, 0), BorderBrush = new SolidColorBrush(brushColor), Child = editor as UIElement });
            }

            _RootBorder.Child = _FixedRowPanel;
            _RootBorder.ClipToBounds = true;

            _Panel.Children.Add(_RootBorder);
            _RootBorder.Arrange(new Rect(_TableView.IndicatorWidth, 0, _Panel.ActualWidth, _RowHeight));
            _RootBorder.SetValue(Panel.ZIndexProperty, 100);

            _RootBorder.Visibility = Visibility.Hidden;

            _RowFixed = true;
            _Grid.UpdateLayout();
        }

        public void ChangeFixedRow(object obj, ScrollChangedEventArgs args)
        {
            var row = _TableView.GetRowElementByRowHandle(_RowHandle);

            if (row == null)
            {
                _RootBorder.Visibility = Visibility.Visible;
            }
            else
            {
                Point position = row.TransformToAncestor(_ScrollContent).Transform(new Point(0, 0));
                if (position.Y < 0)
                    _RootBorder.Visibility = Visibility.Visible;
                else _RootBorder.Visibility = Visibility.Hidden;
            }

            var source = args.OriginalSource as ScrollViewer;

            if (source.Content == null)
            {
                var scroll = obj as ScrollViewer;

                for (int i = 0; i < _FixedRowPanel.Children.Count; i++)
                {
                    var element = _FixedRowPanel.Children[i] as FrameworkElement;
                    if (i == _FixedRowPanel.Children.Count - 1)
                        element.Width = _Grid.Columns[i].ActualDataWidth + 1;
                    else element.Width = _Grid.Columns[i].ActualDataWidth;

                    if (_ExtendedWidth != 0)
                    {
                        var border = element as Border;

                        if (_ExtendedWidth < scroll.ExtentWidth)
                        {
                            if (scroll.ExtentWidth < _Panel.ActualWidth - _TableView.IndicatorWidth)
                                border.BorderThickness = new Thickness(0, 0, 1, 0);
                            else if (scroll.ExtentWidth > _Panel.ActualWidth - _TableView.IndicatorWidth && i != 0)
                                border.BorderThickness = new Thickness(1, 0, 0, 0);
                        }

                        else if (_ExtendedWidth > scroll.ExtentWidth)
                        {
                            border.BorderThickness = new Thickness(0, 0, 1, 0);
                            if (scroll.ExtentWidth > _Panel.ActualWidth)
                                border.Width -= 1;
                        }

                        else if (args.HorizontalOffset > 0)
                        {
                            border.BorderThickness = new Thickness(0, 0, 1, 0);
                        }

                    }
                }

                _ExtendedWidth = scroll.ExtentWidth;

                if (scroll.ExtentWidth > _Panel.ActualWidth)
                {
                    _RootBorder.Arrange(new Rect(_TableView.IndicatorWidth, 0, scroll.ExtentWidth, _RowHeight));
                    _FixedRowPanel.Arrange(new Rect(-args.HorizontalOffset, 0, scroll.ExtentWidth, _RowHeight));
                }
                else
                {
                    _RootBorder.Arrange(new Rect(_TableView.IndicatorWidth, 0, _Panel.ActualWidth, _RowHeight));
                    _FixedRowPanel.Arrange(new Rect(-args.HorizontalOffset, 0, _Panel.ActualWidth, _RowHeight));
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}
