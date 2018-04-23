Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Grid.Hierarchy
Imports DevExpress.Xpf.Bars
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Data
Imports DevExpress.Xpf.Editors
Imports DevExpress.Mvvm.UI.Interactivity

Namespace FixedRowExample
	Public Class FixedRow
		Inherits Behavior(Of UIElement)
		Private _Panel As HierarchyPanel
		Private _Scroll As ScrollViewer
		Private _ScrollContent As ScrollContentPresenter
		Private _RootBorder As Border
		Private _FixedRowPanel As StackPanel
		Private _RowHeight As Double
		Private _ExtendedWidth As Double = 0
		Private _Grid As GridControl
		Private _TableView As TableView
		Private _Element As FrameworkElement
		Private _RowFixed As Boolean
		Private _RowHandle As Integer

		Protected Overrides Sub OnAttached()
			MyBase.OnAttached()
			_Grid = TryCast(Me.AssociatedObject, GridControl)
			AddHandler _Grid.Loaded, AddressOf grid_Loaded

			_Grid.UseLayoutRounding = True
			_Grid.SnapsToDevicePixels = True
		End Sub

		Private Sub grid_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			_TableView = TryCast(_Grid.View, TableView)

            _Panel = CType(LayoutHelper.FindElement(_TableView, Function(element) TypeOf element Is HierarchyPanel), HierarchyPanel)
            _Scroll = CType(LayoutHelper.FindElement(_TableView, Function(element) TypeOf element Is ScrollViewer), ScrollViewer)
            _ScrollContent = CType(LayoutHelper.FindElement(_TableView, Function(element) TypeOf element Is ScrollContentPresenter), ScrollContentPresenter)

			AddHandler _Scroll.ScrollChanged, AddressOf _Scroll_ScrollChanged
		End Sub

		Private Sub _Scroll_ScrollChanged(ByVal sender As Object, ByVal e As ScrollChangedEventArgs)
			If _RowFixed = True Then
				ChangeFixedRow(sender, e)
			End If
		End Sub

		Public Sub CreateFixedRow(ByVal rowHandle As Integer)

			Dim j As Integer = 0
			Do While j < _Panel.Children.Count
				If _Panel.Children(j).GetType() Is GetType(Border) Then
					_Panel.Children.RemoveAt(j)
				End If
				j += 1
			Loop

			_Element = _TableView.GetRowElementByRowHandle(rowHandle)
			_RowHandle = rowHandle

			_RowHeight = _Element.ActualHeight - 1

			_RootBorder = New Border()
			_RootBorder.Name = "RootBorder"

			_FixedRowPanel = New StackPanel()
			_FixedRowPanel.Name = "FixedRowPanel"
			_FixedRowPanel.Orientation = Orientation.Horizontal
			_FixedRowPanel.Background = Brushes.White

			For i As Integer = 0 To _Grid.Columns.Count - 1
				_FixedRowPanel.DataContext = _Grid.GetRow(rowHandle)

				Dim binding As New Binding()

				binding.Path = New PropertyPath(_Grid.Columns(i).FieldName)
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged

				Dim editor = TryCast(_Grid.Columns(i).ActualEditSettings.CreateEditor(), BaseEdit)

				editor.SetBinding(BaseEdit.EditValueProperty, binding)
				If editor.GetType() IsNot GetType(CheckEdit) Then
					editor.HorizontalContentAlignment = _Grid.Columns(i).ActualHorizontalContentAlignment
				Else
					editor.HorizontalAlignment = HorizontalAlignment.Center
				End If

				editor.ShowBorder = False

				Dim brushColor As Color = CType(ColorConverter.ConvertFromString("#FFC3CEDC"), Color)

				_FixedRowPanel.Children.Add(New Border() With {.Width = _Grid.Columns(i).ActualHeaderWidth, .BorderThickness = New Thickness(0, 0, 1, 0), .BorderBrush = New SolidColorBrush(brushColor), .Child = TryCast(editor, UIElement)})
			Next i

			_RootBorder.Child = _FixedRowPanel
			_RootBorder.ClipToBounds = True

			_Panel.Children.Add(_RootBorder)
			_RootBorder.Arrange(New Rect(_TableView.IndicatorWidth, 0, _Panel.ActualWidth, _RowHeight))
			_RootBorder.SetValue(Panel.ZIndexProperty, 100)

			_RootBorder.Visibility = Visibility.Hidden

			_RowFixed = True
			_Grid.UpdateLayout()
		End Sub

		Public Sub ChangeFixedRow(ByVal obj As Object, ByVal args As ScrollChangedEventArgs)
			Dim row = _TableView.GetRowElementByRowHandle(_RowHandle)

			If row Is Nothing Then
				_RootBorder.Visibility = Visibility.Visible
			Else
				Dim position As Point = row.TransformToAncestor(_ScrollContent).Transform(New Point(0, 0))
				If position.Y < 0 Then
					_RootBorder.Visibility = Visibility.Visible
				Else
					_RootBorder.Visibility = Visibility.Hidden
				End If
			End If

			Dim source = TryCast(args.OriginalSource, ScrollViewer)

			If source.Content Is Nothing Then
				Dim scroll = TryCast(obj, ScrollViewer)

				For i As Integer = 0 To _FixedRowPanel.Children.Count - 1
					Dim element = TryCast(_FixedRowPanel.Children(i), FrameworkElement)
					If i = _FixedRowPanel.Children.Count - 1 Then
						element.Width = _Grid.Columns(i).ActualDataWidth + 1
					Else
						element.Width = _Grid.Columns(i).ActualDataWidth
					End If

					If _ExtendedWidth <> 0 Then
						Dim border = TryCast(element, Border)

						If _ExtendedWidth < scroll.ExtentWidth Then
							If scroll.ExtentWidth < _Panel.ActualWidth - _TableView.IndicatorWidth Then
								border.BorderThickness = New Thickness(0, 0, 1, 0)
							ElseIf scroll.ExtentWidth > _Panel.ActualWidth - _TableView.IndicatorWidth AndAlso i <> 0 Then
								border.BorderThickness = New Thickness(1, 0, 0, 0)
							End If

						ElseIf _ExtendedWidth > scroll.ExtentWidth Then
							border.BorderThickness = New Thickness(0, 0, 1, 0)
							If scroll.ExtentWidth > _Panel.ActualWidth Then
								border.Width -= 1
							End If

						ElseIf args.HorizontalOffset > 0 Then
							border.BorderThickness = New Thickness(0, 0, 1, 0)
						End If

					End If
				Next i

				_ExtendedWidth = scroll.ExtentWidth

				If scroll.ExtentWidth > _Panel.ActualWidth Then
					_RootBorder.Arrange(New Rect(_TableView.IndicatorWidth, 0, scroll.ExtentWidth, _RowHeight))
					_FixedRowPanel.Arrange(New Rect(-args.HorizontalOffset, 0, scroll.ExtentWidth, _RowHeight))
				Else
					_RootBorder.Arrange(New Rect(_TableView.IndicatorWidth, 0, _Panel.ActualWidth, _RowHeight))
					_FixedRowPanel.Arrange(New Rect(-args.HorizontalOffset, 0, _Panel.ActualWidth, _RowHeight))
				End If
			End If
		End Sub

		Protected Overrides Sub OnDetaching()
			MyBase.OnDetaching()
		End Sub
	End Class
End Namespace
