   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	   C ?Expenses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ExpensesExpenses 5	   	      Expenses.UserUI   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5   	   "localclr-namespace:Expenses  5	   	   -   5           MaterialDesignWindow#��[  5        � Title$  UserUI��5   	   $	��739��6   $
��1177��6%    � WindowStartupLocation$ CenterScreen=�62   .��5      � .+�5   
   �� -   5         ��Name$	 msp��5      $	��110��6"   $	��Top=�6/   5      �� 5   
    �� Orientation$ Vertical=�6   $��	0,122,0,0q�6,   $��Right=�6?   $	��306��6[   .+�5         LiveCharts.Wpf.PieChart  -   5        $��SumPie��5      $	��200��6*      InnerRadius$ 50Q�67     5      -   5        $	��Pie��5      $	��400��6&   $��0,50,0,0q�66   &    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�6H   5      #   LiveCharts.Wpf.CartesianChart  5   
   -   5        $
��Line��5      $ Right=�6)   $��0,122,561,0q�6@   $	��157��6U   $	��Top=�6b   � 5   
   $��0,0,0,-5q�6   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5         
IsReadOnly$
 True��6)      AutoGenerateColumns$ False��6;   $��43,288,342,151q�6W   $
��True��6p   	   AlternatingRowBackground	 �	#FFEBEBEB
   Columns
 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Info��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *Info+  5      $ Value��6)    68   �� *Value+  5      $ Category��6)    6;   �� *
Category+  5      $
 Date��6)    67   �� *Date+ �� StringFormat$ 
yyyy-MM-dd��  5      $ Transaction��6)    6>   �� *Transaction+  5         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6)   $ Shoping List��63    6I   �� *GroceryList+5      �� 5      -   5        $��0,220,453,0q�5      $	��Top=�6*    �� 	IsDefault$
 True��6B   $��Right=�6e   $��48��6�   .��5       '	  !MaterialDesignThemes.Wpf.PackIcon	   	  Kind$ CartPlus=�6*   5!      �� 5"      -   5        $��0,220,356,0q�5"      $	��Top=�6*   $��Right=�6U   $��46��6q   .��5#      	  $
 Edit=�6*   5$      �� 5%      -	   5        $��0,220,403,0q�5%      $	��Top=�6*   $��Right=�6W   $��49��6s   .��5&      	  $ 
CartRemove=�6*   5'      � 5(      -
   5        $��footer��5(      $��10,0,342,10q�6&   $	��136��6;   $��Bottom=�6H   6e   �� 5)      -   5        $��0,183,356,0q�5)      $	��Top=�6*   $
 True��6C   $��Right=�6j   $	��145��6�   .��5*      	  $ ExportVariant=�6*   .
  (MaterialDesignThemes.Wpf.ElevationAssist 
  	Elevation$	 Dp0=�6?   5+      5-      5/      50      