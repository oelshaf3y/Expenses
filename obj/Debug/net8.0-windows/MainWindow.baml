   M S B A M L   `   `   `  ����C  ?Expenses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ExpensesExpenses  5   	       Expenses.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Expenses     5   	     � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)    � 
ResizeMode$ NoResize=�65   .��5	      � .+�5
   
   �� -   5        $��Add Category��5
      $��	0,47,30,0q�6(   $	��Top=�6;   $��Right=�6S   $��83��6o   6z   �� 5   
   -   5        $��Add Sub Category��5      $��	0,84,32,0q�6,   $	��Top=�6?   $��Right=�6n   $	��110��6�   �� 5   
   -   5        $��
Add Record��5      $��
0,88,345,0q�6&   $	��Top=�6:   $��Right=�6d   $��75��6�   �� 5   
   -   5        $��Add User��5      $��
0,169,30,0q�6$   $	��Top=�68   $��Right=�6`   $	��112��6|   �� 5   
   $
��User��6   $
��Left=�6   $��	43,11,0,0q�6:   $	��Top=�6M   �� 5   
   -   5         ��Name$ 	combobox1��5      $
��Left=�6&   $��	43,41,0,0q�6A   $	��Top=�6T   $	��120��6l   6x   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ datagrid��5      $��43,115,253,31q�6%   $
��True��6<      AlternatingRowBackground �	#FFEBEBEB6a   �� 5   
   -   5         ��Name$ currentLabel��5      $��Label��6&   $
��Left=�66   $��
276,21,0,0q�6Q   $	��Top=�6e   �� 5   
   -   5        $ incomeLabel��5      $��Label��6%   $
��Left=�65   $��
276,49,0,0q�6P   $	��Top=�6d   �� 5   
   -	   5        $ 	paidLable��5      $��Label��6#   $
��Left=�63   $��
276,75,0,0q�6N   $	��Top=�6b   �� 5   
   -
   5        $��Edit Record��5      $��
0,88,260,0q�6'   $	��Top=�6;   $��Right=�6S   $��64��6o   6z   �� 5   
   -   5        $ monthPicker��5      $
��Left=�6(   $��
109,77,0,0q�6C   $	��Top=�6W   $��54��6o   6z   �� 5   
   -   5        $ 
yearPicker��5      $
��Left=�6'   $��	43,76,0,0q�6B   $	��Top=�6U   $��50��6m   6x   �� 5   
   $��CheckBox��6   $
��Left=�6&   $��1144,361,0,0q�6A   $	��Top=�6W   5      5      