<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128650477/16.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T161325)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))**
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
* [ViewModel.cs](./CS/ViewModel/ViewModel.cs) (VB: [ViewModel.vb](./VB/ViewModel/ViewModel.vb))
<!-- default file list end -->
# How to fix a row in GridControl


<p>In this example, it's demonstrated how to fix a row in GridControl so that its position is preserved even when the grid is being scrolled. To show a button that fixes a row, use the <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfGridTableView_ShowFixRowButtontopic">ShowFixRowButton</a> property. If you wish to add fixed rows from your view model, bind to the <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfGridTableView_FixedTopRowstopic">FixedTopRows</a> or <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfGridTableView_FixedBottomRowstopic">FixedBottomRows</a> collections. <br>TableView provides commands for fixing rows: FixRowToTop and FixRowToBottom. Please note that it's necessary to pass the row that you wish to fix to the command parameter.</p>

<br/>


