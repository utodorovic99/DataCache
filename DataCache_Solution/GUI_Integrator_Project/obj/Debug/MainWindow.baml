   M S B A M L   `   `   `  ����Q  MGUI_Integrator_Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:GUI_Integrator_ProjectGUI_Integrator_Project  5   	   '    !GUI_Integrator_Project.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:GUI_Integrator_Project     5   	     � Title$  User Control Window��5   	   $	��455��6%   $	��800��62   .��5
      ��  �� Padding$ 10q�6   ._�5   
   �� .+�5      � $��47��6   $��
0,0,-0.4,0q�6    � ColumnDefinitions 5      �� 5      $
��151* �6'   �� 5      $
��151* �6'   �� 5      $
��471* �6'   .+�5      �� $
��Bold"�6   $
��Left=�6.   $��	10,14,0,0q�6I   $
��Wrap=�6\   $��FROM:��6p   $	��Top=�6}   $��21��6�   $��43��6�   �� 5      $
��Bold"�6   $
��Left=�6.   $��11.8,16,0,0q�6I   $
��Wrap=�6^   $	��TO:��6r   $	��Top=�6}   $��16��6�   $��22��6�   $��1��6�   (  "System.Windows.Controls.DatePicker  5      -   5        $��fromDate��5      $
��Left=�6-   $��	53,12,0,0q�6H   $	��Top=�6[   $��23��6s   $	��102��6   $��2��6�     5      -   5        $��toDate��5      $
��Left=�6+   $��38.8,12,0,0q�6F   $	��Top=�6[   $��23��6s   $	��102��6   $��1��6�   �� 5      $
��Bold"�6   $��1��6.   $
��Left=�6>   $��150.8,16,0,0q�6Y   $
��Wrap=�6o   $��Geographical field:��6�   $	��Top=�6�   $��2��6�   �� 5      -   5        $��geoComboBox��5      $��2��6.   $
��Left=�6>   $��111.6,13,0,0q�6Y   $	��Top=�6o   $��88��6�   �� 5      -   5        $��btnGeoSearch��5      $��Show search results��6-   $��2��6K   $
��Left=�6[   $��314.6,14,0,0q�6v   $	��Top=�6�   $	��135��6�   $��21��6�   6�   5      � 5      $	��354��6   $��
0,0,-0.4,0q�6    .+�5      &   System.Windows.Controls.DataGrid  -   5        $��dataGrid��5      $��	0,2,181,5q�6+      
IsReadOnly .    AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6E      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6Q   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *GID+6Q     5!      $ Consumption��6E   $ 5* 6Z    6-   �� *MWh+6Z     5"      $ 	Timestamp��6K   $ 5* 6^    6-   �� *	TimeStamp+6^   5$      �� 5&      -   5        $��	btnAddGeo��5&      $��627,88,41.6,235.8q�6*   $��	Add field��6E   6Y   �� 5'      -   5        $��id��5'      $
��Left=�6B   $��23��6]   $��
618,60,0,0q�6i   	 �� TextWrapping$
	 Wrap=�6}   $��ID��6�   $	��Top=�6�   $	��134��6�   �� 5(      -   5        $
��name��5(      $
��Left=�6B   $��23��6]   $��
618,32,0,0q�6i   $
	 Wrap=�6}   $
��Name��6�   $	��Top=�6�   $	��134��6�   �� 5)      $
��Left=�6   $��	636,6,0,0q�67   $
��Wrap=�6J   $��Geographical field��6^   $	��Top=�6x   �� 5*      -	   5        $��btnLoad��5*      $��Load data into database��6(   $
��Left=�6J   $��598,246,0,0q�6e   $	��Top=�6z   $	��166��6�   $��32��6�   6�   �� 5+      -
   5        $��statusLabel��5+      $��Database status��6+   $
��Left=�6E   $��668,323,0,-0.2q�6`   $	��Top=�6x   �� 5,      -   5        $��btnReconnect��5,      $��	Reconnect��6-   $
��Left=�6A   $��598,327,0,0q�6\   $	��Top=�6q   $��65��6�   6�   �� 5-      -   5        $��browseButton��5-      $��Browse��6-   $
��Left=�6>   $��598,218,0,0q�6Y   $	��Top=�6n   $	��166��6�   6�   �� 5.      -   5        $��btnShowAudit��5.      $��Show audit content��6-   $
��Left=�6J   $	��Top=�6e   $	��166��6}   $��598,283,0,0q�6�   $��35��6�   6�   �� 5/      -   5        $��	fileLabel��5/      $��Chosen file��6-   $
��Left=�6@   $��598,191,0,0q�6[   $	��Top=�6p   $	��166��6�   $��22��6�   50      51      52      53      