   M S B A M L   `   `   `  ����C  ?Expenses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ExpensesExpenses  5   	       Expenses.AddRecord   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Expenses     5   	     � Title$  	AddRecord��5   	   $	��225��6   $	��500��6(   $	��500��64   $	��225��6C   .��5	      � .+�5
   
   �� $��Info:��6   $
��Left=�6    $��	10,10,0,0q�6;   $	��Top=�6N   �� 5   
   -   5         ��Name$ textBox1��5      $
��Left=�6$   $��	10,36,0,0q�6?    �� TextWrapping$
 Wrap=�6R   $	��Top=�6f   $	��120��6~   �� 5   
   $��Amount��6   $
��Left=�6!   $��	10,59,0,0q�6<   $	��Top=�6O   �� 5   
   -   5        $ textbox2��5      $
��Left=�6$   $��	10,85,0,0q�6?   $
 Wrap=�6R   $	��Top=�6f   $	��120��6~   �� 5   
   $��Category��6   $
��Left=�6#   $��
10,108,0,0q�6>   $	��Top=�6R   �� 5   
   -   5         ��Name$ 	combobox1��5      $
��Left=�6&   $��
10,139,0,0q�6A   $	��Top=�6U   $	��120��6m   �� 5   
   -   5        $
��Save��5      $��
0,108,28,0q�6    $	��Top=�64   $��Right=�6_   $��29��6{   �� 5   
   -   5        $��Cancel��5      $��
0,139,18,0q�6"   $	��Top=�66    �� IsCancel$
 True��6N   $��Right=�6^   $��39��6z   6�   �� 5   
   -   5         ��Name$ 	checkbox1��5      $��Today?��6&   $��	0,38,17,0q�67   $	��Top=�6J   $��Right=�6�   $��56��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
datePicker��5      $��388,68,10,0q�6)   $	��Top=�6>   �� 5   
   -   5        $ income��5      $��Income��6#   $��	0,15,13,0q�64   $	��Top=�6G   $��Right=�6_   $��59��6{   �� 5   
   -	   5         ��Name$ 
addListBut��5      $��Add List��6%   $
��Left=�68   $��
153,0,0,23q�6S   $��20��6w   $��Bottom=�6�   &   System.Windows.Controls.DataGrid  5   
   -
   5          Name$ dataGrid��5      $��143,23,127,50q�6P   ${�False��6g   5      5      