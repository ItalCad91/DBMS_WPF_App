   M S B A M L   `   `   `  ����F  BWPFTutorial, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WPFTutorialWPFTutorial  5   	       WPFTutorial.Database   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:WPFTutorial     5   	     � Title$  Database��5   	   $	��600��6   $	��800��6'   
����d� � SizeToContent$ Manual=�6O    � WindowState$ 	Maximized=�6f   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��Center=�6,   $��0��6I   .+�5      �� $��	/db 2.png��6   $	��400��6'   $	��300��63   $
��0,10q�6@   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��	Inventory��5      $��1��6$   $��2��61   $��10,50,10,10q�6B   $
��Left=�6W   5      �� 5   
   $��2��6   $��Center=�6"   $��20q�6?   .+�5      �� $��Welcome To your Database!��6   $��30(�68   
���� �$��0,0,0,10q�6_   �� 5      $���}To Edit any data from this database, please select the item that you would like to modify. Then click on Update to modify it.��6   $��20(�6�   
�������$
��Wrap=�6�   $	��304��6�   �� 5      -   5        $��Edit Database��5      $	��120��6@   
�������$��0,10,0,0q�6_   5      5       5!      