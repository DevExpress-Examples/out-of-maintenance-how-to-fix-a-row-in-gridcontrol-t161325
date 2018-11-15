<!-- default file list -->
*Files to look at*:

* [FixedRow.cs](./CS/FixedRowExample/Behavior/FixedRow.cs) (VB: [FixedRow.vb](./VB/FixedRowExample/Behavior/FixedRow.vb))
* [MainWindow.xaml](./CS/FixedRowExample/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/FixedRowExample/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/FixedRowExample/MainWindow.xaml.cs) (VB: [MainWindow.xaml](./VB/FixedRowExample/MainWindow.xaml))
* [TaskViewModel.cs](./CS/FixedRowExample/ViewModel/TaskViewModel.cs) (VB: [TaskViewModel.vb](./VB/FixedRowExample/ViewModel/TaskViewModel.vb))
<!-- default file list end -->
# How to fix a row in GridControl


<p>In this example, it's demonstrated how to fix a row in GridControl so that its position is preserved even when the grid is being scrolled. To show a button that fixes a row, use the <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfGridTableView_ShowFixRowButtontopic">ShowFixRowButton</a> property. If you wish to add fixed rows from your view model, bind to the <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfGridTableView_FixedTopRowstopic">FixedTopRows</a> or <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfGridTableView_FixedBottomRowstopic">FixedBottomRows</a> collections. <br>TableView provides commands for fixing rows: FixRowToTop and FixRowToBottom. Please note that it's necessary to pass the row that you wish to fix to the command parameter.</p>

<br/>


