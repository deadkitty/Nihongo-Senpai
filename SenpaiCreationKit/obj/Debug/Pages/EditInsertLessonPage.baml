   M S B A M L   `   `   `  ����L  HSenpaiCreationKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SenpaiCreationKit.PagesSenpaiCreationKit.Pages  5      2    ,SenpaiCreationKit.Pages.EditInsertLessonPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:SenpaiCreationKit.Pages     5      -   5           AppWidth#��[  5        	AppHeight#��[ 5	        AppBackground  N� 
Background#  [ 5
      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $��* �6    .+�5      �� $��0��6     LabelTextblockSmall#��[ 6%   ��6R   �� *Resources.Sentence+  Strings �� Source# [ �� 5       -   5         ��Name$ sentenceTextbox��5    M   $��1��6     ContentTextbox#��[ 6%   6f   �� 5"      $��2��6   #��[ 6%   ��6R   �� *Resources.InsertParts+# [ �� 5$      -   5         ��Name$ selectInsertsListbox��5$   #   $��3��6    �� SelectionMode$ Extended=�6A   $��Disabled=�6Z   $��10, 0, 10, 10q�6�   $	��100��6�   ��5%      �� 5&      .R�5'      �  K� IsItemsHost . 6$   5(      5*      � 54      $��4��6    55      �� 56      �� 57      .+�5:      �� -   5         ��Name$ createCloze��5:   )   $��0��6   $
��Left=�6>     ActionButton#��[ 6Y     SelectButtonColor#��[ 6�   ��6   �� *Resources.Create+# [ 6�   �� 5<      $��1��6   	 �� Orientation$	 
Horizontal=�6-   $��Right=�6F   .+�5=      �� -   5        $ chooseInsertParts��5=      #��[ 68   #��[ 6�   ��6^   �� *Resources.Add+# [ 6�   �� 5>      -   5        $ removeInsertParts��5>      #��[ 68     CancelButtonColor#��[ 6�   ��6^   �� *Resources.Remove+# [ 6�   5?      5@      � 5B      $��5��6   $��Bottom=�6    $
��Left=�6;    5C      �� 5D      $
��auto �6$   �� 5E      $
��auto �6$   .+�5H      �� -   5        $ 
editLesson��5H   &   $��0��6   #��[ 6:    	 ConfirmButtonColor#��[	 6�   ��6`   �� *Resources.EditLesson+# [ 6�   �� 5I      -   5        $ 
cancelEdit��5I   &   $��1��6   #��[ 6:   #��[ 6�   ��6`   �� *Resources.CancelEdit+# [ 6�   5K      5L      � 5N   
   $��1��6    5O      �� 5P      $
��auto �6    �� 5Q      $
��auto �6    �� 5R      $��* �6    �� 5S      $
��auto �6    .+�5V      �� $��0��6   #��[ 6%   ��6R   �� *Resources.LessonName+# [ �� 5W      -	   5        $ lessonNameTextbox��5W   %   $��1��6   #��[ 6@   $�� ��6h   $
��Left=�6p   $	��300��6�   �� 5Y      -
   5        $ clozesListbox��5Y   #   $��2��6   $ Extended=�6:   
���   �$��2q�6g   $��10q�6{   $��Disabled=�6�   6�   � 5[      $��3��6   $��Right=�6     5\      �� 5]      $
��auto �6'   �� 5^      $��* �6'   .+�5a      �� -   5        $ delete��5a      $��0��6,   #��[ 6<   #��[ 6b   ��6�   �� *Resources.Delete+# [ 6�   �� 5b      -   5        $ 	chooseAll��5b      $��1��6,   #��[ 6<   #��[ 6b   ��6�   �� *Resources.ChooseAll+# [ 6�   5d      5e      5f      5g      