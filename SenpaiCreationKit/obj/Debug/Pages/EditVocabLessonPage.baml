   M S B A M L   `   `   `  ����L  HSenpaiCreationKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SenpaiCreationKit.ControlsSenpaiCreationKit.Controls  5      1    +SenpaiCreationKit.Pages.EditVocabLessonPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =toolkit)http://schemas.xceed.com/wpf/xaml/toolkit     5      =controls(clr-namespace:SenpaiCreationKit.Controls     5      -   5           AppWidth#��[  5	        	AppHeight#��[ 5
        AppBackground  N� 
Background#  [ 5      .��5      � -   5         �Name$ 
layoutRoot��5       � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $��* �6    .+�5!      �� $��0��6     LabelTextblockSmall#��[ 6%   ��6R   �� *Resources.Kana+  Strings �� Source# [ �� 5"      $��2��6   #��[ 6%   ��6R   �� *Resources.Kanji+# [ �� 5#      $��4��6   #��[ 6%   ��6R   �� *Resources.Translation+# [ �� 5$      $��6��6   #��[ 6%   ��6R   �� *Resources.Description+# [ �� 5&      -   5         ��Name$ kanaTextbox��5&   #   $��1��6     ContentTextbox#��[ 6?   6�   �� 5'      -   5        $ kanjiTextbox��5'   #   $��3��6   #��[ 6?   6�   �� 5(      -   5        $ translationTextbox��5(   #   $��5��6   #��[ 6?   6�   �� 5)      -   5        $ descriptionTextbox��5)   #   $��7��6   #��[ 6?   6�   � 5+      $��8��6   $��Bottom=�6    $
��Left=�6;    5,      �� 5-      $
��auto �6$   �� 5.      $
��auto �6$   .+�51      �� -   5         ��Name$ 
editLesson��51   &   $��0��6     ActionButton#��[ 6:     ConfirmButtonColor#��[ 6�   ��6`   �� *Resources.EditLesson+# [ 6�   �� 52      -   5        $ 
editCancel��52   &   $��1��6   #��[ 6:     CancelButtonColor#��[ 6�   ��6`   �� *Resources.CancelEdit+# [ 6�   54      56      � 58   
   $��1��6    59      �� 5:      $
��auto �6    �� 5;      $
��auto �6    �� 5<      $��* �6    �� 5=      $
��auto �6    .+�5@      � $��0��6    5A      �� 5B      $��* �6'   �� 5C      $��* �6'   .+�5F      �� $��0��6   #��[ 6,   ��6Y   �� *Resources.LessonName+# [ �� 5G      $��1��6   #��[ 6,   ��6Y   �� *Resources.FilterWords+# [ 5H      � 5I      $��1��6    5K      �� 5L      $��* �6'   �� 5M      $��* �6'   .+�5P      �� -	   5        $ lessonNameTextbox��5P      $��0��65   #��[ 6E   $�� ��6m   �� 5Q      -
   5         ��Name$ typeComboBox��5Q      $��1��61   $��5q�6A    �� SelectedIndex$ 0��6L   5S      �� 5U      -   5        	 ��Name$	 wordsListbox��5U   #   $��2��6   
 �� SelectionMode$
 Extended=�69   
���   �$��2q�6f   $��10q�6z   $��Disabled=�6�   6�   � 5W      $��3��6    5X      �� 5Y      $
��auto �6'   �� 5Z      $
��auto �6'   �� 5[      $��* �6'   .+�5^      �� -   5        $ setType��5^      $��0��6,   #��[ 6<    	 SelectButtonColor#��[	 6b   ��6�   �� *Resources.SetType+# [ 6�   �� 5_      -   5        $ delete��5_      $��1��6,   #��[ 6<   #��[	 6b   ��6�   �� *Resources.Delete+# [ 6�   �� 5`      -   5        $ 	chooseAll��5`      $��2��6,   #��[ 6<   #��[	 6b   $��Right=�6�   ��6�   �� *Resources.ChooseAll+# [ 6�   5b      5d      2   ,SenpaiCreationKit.Controls.SelectTypeControl  5f   
   -   5          Name$ selectTypeCtrl��5f   %   $��2��6=   $y�	Collapsed=�6Q   5g      5h      