   M S B A M L   `   `   `  ����C  ?Expenses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ExpensesExpenses  5   	       Expenses.AddRecord   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Expenses     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignWindow#��[  5   	     � Title$  	AddRecord��5
   	   $	��428��6   $	��788��6(   $	��500��64   $	��225��6C   .��5      � .+�5   
   �� -   5         ��Name$ textBox1��5      $	��Top=�6%   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Info��6>    % !MaterialDesignFloatingHintTextBox#��[ 6e   $��	10,10,0,0q�6�   $
��Left=�6�   $	��120��6�   �� 5   
   -   5        $ textbox2��5      $	��Top=�6%   $ Amount��6>   #��[ 6g   $��	10,64,0,0q�6�   $
��Left=�6�   $	��120��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DPicker��5      $��	0,95,10,0q�6&   $	��Top=�69   $��Right=�6Q   $��86��6m   �� 5   
   -   5        $��
0,146,32,0q�5       �� 	IsDefault$
 True��68   .  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp0=�6I    * &MaterialDesignFloatingActionMiniButton#��[ 6x   $��40��6�   $	��Top=�6�   $��Right=�6�   .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ ContentSave=�6&   5      �� 5   
   -   5         �� IsCancel$
 True��5      $��
0,201,32,0q�6!   $	 Dp0=�6J   #��[ 6y   $��40��6�   $	��Top=�6�   $��Right=�6�   .��5        $ 
CancelBold=�6&   5      �� 5   
   -   5         ��Name$ income��5      $��Income��6#   	 d� 	IsChecked$	 FalseY�64    + 'MaterialDesignFilterChipPrimaryCheckBox#��[ 6F   $��	0,23,10,0q�6�   $��32��6�   $	��Top=�6�   $��Right=�6�   �� 5   
   -   5        
 ��Name$
 
addListBut��5      $��
227,0,0,19q�6%   $	 Dp0=�6I   #��[ 6x   $��40��6�   $��Bottom=�6�   $
��Left=�6�   $��40��6�   .��5        -   5        $��addListButIco��5   &   $
 Cart=�6;   5      &   System.Windows.Controls.DataGrid  5   
   -	   5          Name$ dataGrid��5         
IsReadOnly$
 True��6%   $��214,23,127,64q�6b   �� 5   
   -
   5        $��	10,0,0,19q�5      $	 Dp0=�6;   #��[ 6j   $��40��6�   $��Bottom=�6�   $
��Left=�6�   $��40��6�   .��5        $ PlusBold=�6&   5      R� 5   
   -   5         R�Name$ CatTree��5      $	��150��6$   $��
0,133,0,64q�63   $
��Left=�6G   $	��209��6b   ���	#FFF0EDED6n   �� 5   
   $��Category��6   $
��Left=�6#   $��
10,103,0,0q�6>   $	��Top=�6R   5       5!      