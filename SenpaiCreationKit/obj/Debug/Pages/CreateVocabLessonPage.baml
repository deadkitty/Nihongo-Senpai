   M S B A M L   `   `   `  ����L  HSenpaiCreationKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SenpaiCreationKit.ControlsSenpaiCreationKit.Controls  5      3    -SenpaiCreationKit.Pages.CreateVocabLessonPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =toolkit)http://schemas.xceed.com/wpf/xaml/toolkit     5      =controls(clr-namespace:SenpaiCreationKit.Controls     5      -   5           AppWidth#��[  5	        	AppHeight#��[ 5
        AppBackground  N� 
Background#  [ 5      .��5      � -   5         �Name$ 
layoutRoot��5       � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $��* �6    .+�5"      �� $��0��6     LabelTextblockSmall#��[ 6%   ��6R   �� *Resources.Kana+  Strings �� Source# [ �� 5#      $��2��6   #��[ 6%   ��6R   �� *Resources.Kanji+# [ �� 5$      $��4��6   #��[ 6%   ��6R   �� *Resources.Translation+# [ �� 5%      $��6��6   #��[ 6%   ��6R   �� *Resources.Description+# [ �� 5'      -   5         ��Name$ kanaTextbox��5'   #   $��1��6     ContentTextbox#��[ 6?   6�   �� 5(      -   5        $ kanjiTextbox��5(   #   $��3��6   #��[ 6?   6�   �� 5)      -   5        $ translationTextbox��5)   #   $��5��6   #��[ 6?   6�   �� 5*      -   5        $ descriptionTextbox��5*   #   $��7��6   #��[ 6?   6�   � 5,      $��8��6   $��Bottom=�6    $
��Left=�6;    5-      �� 5.      $
��auto �6$   �� 5/      $
��auto �6$   .+�52      �� -   5         ��Name$ createLesson��52   &   $��0��6     ActionButton#��[ 6>     ConfirmButtonColor#��[ 6�   ��6d   �� *Resources.CreateLesson+# [ 6�   �� 53      -   5        $ cancelCreation��53   &   $��1��6   #��[ 6>     CancelButtonColor#��[ 6�   ��6d   �� *Resources.CancelCreation+# [ 6�   55      57      � 59   
   $��1��6    5:      �� 5;      $
��auto �6    �� 5<      $
��auto �6    �� 5=      $��* �6    �� 5>      $
��auto �6    .+�5A      � $��0��6    5B      �� 5C      $��* �6'   �� 5D      $��* �6'   .+�5G      �� $��0��6   #��[ 6,   ��6Y   �� *Resources.LessonName+# [ �� 5H      $��1��6   #��[ 6,   ��6Y   �� *Resources.FilterWords+# [ 5I      � 5J      $��1��6    5L      �� 5M      $��* �6'   �� 5N      $��* �6'   .+�5Q      �� -	   5        $ lessonNameTextbox��5Q      $��0��65   #��[ 6E   $�� ��6m   �� 5R      -
   5         ��Name$ typeComboBox��5R      $��1��61   $��5q�6A    �� SelectedIndex$ 0��6L   6^   5T      �� 5V      -   5        	 ��Name$	 wordsListbox��5V   #   $��2��6   
 �� SelectionMode$
 Extended=�69   
���   �$��2q�6f   $��10q�6z   $��Disabled=�6�   6�   � 5X      $��3��6    5Y      �� 5Z      $
��auto �6'   �� 5[      $
��auto �6'   �� 5\      $��* �6'   .+�5_      �� -   5        $ setType��5_      $��0��6,   #��[ 6<    	 SelectButtonColor#��[	 6b   ��6�   �� *Resources.SetType+# [ 6�   �� 5`      -   5        $ delete��5`      $��1��6,   #��[ 6<   #��[	 6b   ��6�   �� *Resources.Delete+# [ 6�   �� 5a      -   5        $ 	chooseAll��5a      $��2��6,   #��[ 6<   #��[	 6b   $��Right=�6�   ��6�   �� *Resources.ChooseAll+# [ 6�   5c      5e      2   ,SenpaiCreationKit.Controls.SelectTypeControl  5g   
   -   5          Name$ selectTypeCtrl��5g   %   $��2��6=   $y�	Collapsed=�6Q   5h      5i      