   M S B A M L   `   `   `  ����  SenpaiCreationKit1    +SenpaiCreationKit.Pages.EditFlashLessonPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5           AppWidth#��[  5        	AppHeight#��[ 5        AppBackground  N� 
Background#  [ 5	      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5       $
��auto �6    �� 5!      $
��auto �6    �� 5"      $��* �6    .+�5%      �� $��0��6     LabelTextblockSmall#��[ 6&   ��6S   �� *Resources.Kanji+  Strings �� Source# [ �� 5&      $��2��6   #��[ 6&   ��6S   �� *Resources.Meaning+# [ �� 5'      $��4��6   #��[ 6&   ��6S   �� *Resources.Onyomi+# [ �� 5(      $��6��6   #��[ 6&   ��6S   �� *Resources.Kunyomi+# [ �� 5)      $��8��6   #��[ 6&   ��6S   �� *Resources.Example1+# [ �� 5*      $��10��6   #��[ 6&   ��6S   �� *Resources.Example2+# [ �� 5+      $��12��6   #��[ 6&   ��6S   �� *Resources.Example3+# [ �� 5-      -   5         ��Name$ kanjiTextbox��5-   $   $��1��6     ContentTextbox#��[ 6=   6�   �� 5.      -   5        $ meaningTextbox��5.   $   $��3��6   #��[ 6=   6�   �� 5/      -   5        $ onyomiTextbox��5/   $   $��5��6   #��[ 6=   6�   �� 50      -   5        $ kunyomiTextbox��50   $   $��7��6   #��[ 6=   6�   �� 51      -   5        $ exampleTextbox1��51   $   $��9��6   #��[ 6=   6�   �� 52      -   5        $ exampleTextbox2��52   $   $��11��6   #��[ 6=   6�   �� 53      -   5        $ exampleTextbox3��53   $   $��13��6   #��[ 6=   6�   � 55      $��14��6   $��Bottom=�6!   $
��Left=�6<    56      �� 57      $
��auto �6$   �� 58      $
��auto �6$   .+�5;      �� -	   5         ��Name$ 
editLesson��5;   &   $��0��6     ActionButton#��[ 6:     ConfirmButtonColor#��[ 6�   ��6`   �� *Resources.EditLesson+# [ 6�   �� 5<      -
   5        $ 
cancelEdit��5<   &   $��1��6   #��[ 6:     CancelButtonColor#��[ 6�   ��6`   �� *Resources.CancelEdit+# [ 6�   5>      5@      � 5B   
   $��1��6    5C      �� 5D      $
��auto �6    �� 5E      $
��auto �6    �� 5F      $��* �6    �� 5G      $
��auto �6    .+�5J      �� $��0��6   #��[ 6%   ��6R   �� *Resources.LessonName+# [ �� 5K      -   5        $ lessonNameTextbox��5K   %   $��1��6   #��[ 6@   $�� ��6h   $
��Left=�6p   $	��300��6�   �� 5M      -   5         ��Name$ kanjisListbox��5M   #   $��2��6    �� SelectionMode$ Extended=�6:   
���   �$��2q�6g   $��10q�6{   $��Disabled=�6�   6�   � 5O      $��3��6   $��Right=�6     5P      �� 5Q      $
��auto �6'   �� 5R      $��* �6'   .+�5U      �� -   5        $ delete��5U      $��0��6,   #��[ 6<    	 SelectButtonColor#��[	 6b   ��6�   �� *Resources.Delete+# [ 6�   �� 5V      -   5        $ 	chooseAll��5V      $��1��6,   #��[ 6<   #��[	 6b   ��6�   �� *Resources.ChooseAll+# [ 6�   5X      5Y      5Z      5[      