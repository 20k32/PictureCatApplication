   M S B A M L   `   `   `  ����E  APictureCat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PictureCat
PictureCat  5   	   %    PictureCat.SelecedPictureWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PictureCat     5   	   -   5          �Name$  selectedWindow��5   	   $	��620��5	   	    � SizeToContent$ Height=�5
   	    � 
ResizeMode$ NoResize=�5   	      WindowBackgroundBrush#��[  5   	   $��19(�5   	    � WindowStartupLocation$ CenterOwner=�5   	   $��Visible=�5   	    � Icon$ /ApplictaionIcon.ico��5   	    � Title 5   	   �� *Title+.��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��501 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ DeleteButton��5      $��2��5      $	��Top=�5      $��Center=�5      $��
0, 5, 5, 0q�5        DleteButtonStyle#��[ 5       $��64��5!      .��5"      �� $��Images/delete.png��6   5#      �� 5$   
   $��1��6    �� Orientation$ Vertical=�6%   $��Center=�6<   .+�5%      �� -   5        	 ��Name$	 PictureBorder��5%      $��0, 10, 0, 0q�6,   
 �� CornerRadius$
 
3, 3, 7, 7��6A   ���	#FF3A3A3A$��5q�6s   $��Center=�6�   $��Center=�6�   
�������._�5&      �� -   5         ��Name$ ImageStackPanel��5&      $ Vertical=�66   $��Center=�6M   $��Center=�6h   .+�5'      �� -   5         ��Name$ SelectedImage��5'      $��1��63   $��0��6C   $	��257��6P   $	��400��6`   ��5(      �� 5)       �� 
TargetType" ��6$    �� Triggers 5*   "   P� 5+   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5,   *   ��  �� Property �@�Cursor �� Value$
 Hand��6C   5-   '   5/      51      �� 52      ���	#FF7D7770$��Center=�65   $��Center=�6P   $��
0, 5, 0, 0q�6m   ._�53      �� -   5         ��Name$ TitleTextBox��53   &   $	��150��55   "    �� 	MaxLength$	 100��56   "     TitleTextBoxStyle#��[ 57   "   ��53   <   �� *Title+��54   "   ��  �� ElementName$ SelectedImage�� �� Path$ ActualWidth�58   "   59      5:      5;      �� 5<      $ 
Horizontal=�6   $��Center=�62   .+�5=      �� 
�������$
 10��6-   $��40��6?   ._�5>      (  "System.Windows.Controls.DatePicker  -   5          Name$ datePicker1��5>   E    J� 	AllowDrop$ False��6Z      IsDropDownOpen$ false��6l      SelectedDateFormat$
 Long=�6�   $��Center=�6�   $	��254��6�    =� ErrorTemplate 6!   Z�    SelectedDate 6�   �� *ReleaseDate+ � 	Resources 5?      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType  Key �� Color'  !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup!   Transitions%  System.Windows.VisualTransition"   GeneratedDuration	#   To$   States   System.Windows.VisualState%   
Storyboard& g� Duration	' W� To( �� TargetProperty) �� 
TargetName* J� Opacity+ �� Padding, �Name- ��Name. J� IsHitTestVisible/ ��Name��%�  '        '  J     �� 5@      "  6$   .�5A   "   ��  � ��
 ���� 6@   5B      �� 5C      "  6$   .�5D   "   ��  � �� 5E   &   �� 5F   *   "  6:   .R�5G   .   �  5H   2   ���� 5I   6   WatermarkBrush $ Transparent��6]   5J   3     5K   2     5L   6   CommonStates��! 5M   :     5N   >   $" 0D�6O     5O   >   $" 0:0:0.1D�6O   $# 	MouseOver��6k   .$ 5Q   :     
Normal��6F     5R   :   	MouseOver��6F   5S   7     5T   6   WatermarkStates��! 5U   :     5V   >   $" 0D�6O   .$ 5X   :     Unwatermarked��6F     5Y   :   Watermarked��.% 5Z   >   �� .��5[   B   W� $& 0D�6R   ' 06_   $( Opacity�6f   $) ContentElement��6�   W� 5\   B   $& 0D�6R   ' 16_   $( Opacity�6f   $) PART_Watermark��6�   5]   ?   5^   ;   5_   7     5`   6   FocusStates��! 5a   :     5b   >   $" 0D�6O   .$ 5d   :     	Unfocused��6F     5e   :   Focused��6F   5f   7   .+�5h   2   �� $	 Border��69   
������ #��z��6c   #��z��6�   $
 5��6�   $* 1Q�6�   #+ z��6�   ._�5i   6   � $, WatermarkContent��6;   #��z��6U   #��z��6�   .+�5j   :   �� $	 ContentElement��6A   
������ $��1q�6s   �� 5k   :   $	 watermark_decorator��6A   
������ $��1q�6x   ._�5l   >   �� $- PART_Watermark��6M   $|�False��6e   $. False��6w   $* 0Q�6�   $��2q�6�   5m   ;   �� 5n   :   $/ PART_ContentHost��6G   #��z��6a   $��0q�6�   #��z��6�   5o   7   5p   3   5q   /   5r   +   5t   #    5u   "   P� 5v   &    � ��$  ��6>   . �5w   *   ��  � ��
 �����6N   5x   '   P� 5y   &    � �� 6>   Z� . �5z   *   ��  � ��
 �����6N   5{   '   5}      5~      5      5�      �� 5�      -   5        $ 
LikeButton��5�      $��Center=�5�        LikeButtonStyleFirst#��[ 5�      $��25, 10, 0, 10q�5�      $��41��5�      $��57��6%   .��5�      �� $��Images/like.png��6   $��37��65   $��32��6@   5�      5�      �� 5�      $ 
Horizontal=�6   $��Center=�62   $	��336��6O   .+�5�      �� $ Vertical=�6   $��Center=�64   $	��254��6O   .+�5�      �� $��2q�6   $
��Auto=�61   $	��200��6a   ._�5�      �� -	   5        0 ��Name$0 CheckListBox��5�   "   $
��Auto=�68   $��Disabled=�5�      $	��117��5�      1 %� IsVirtualizing$
1 True��6'   2 %� CacheLength@  :System.Windows.Controls.VirtualizationCacheLengthConverter$2 1 5�      3 %� IsContainerVirtualizable$
3 True��5�       5�        ListCheckBoxItemStyle4 �� BasedOn��%�   '��       '�� T     7  �� 5�   "   " ��6(   .�5�   &   ��  �@��CornerRadius$ 8��6E   5�   #   �� 5�   "   " ��6(   84   6J   5�      5�      5�      �� 5�      $��2q�6   $
��Auto=�61   $	��200��6a   ._�5�      �� -
   5        $0 
HashTagBox��5�   "   5 �� SelectionMode$5 Single=�5�      $
��Auto=�5�      $��Disabled=�5�      $	��117��5�      $
1 True��6'   $2 1 5�      $
3 True��5�       5�      ��%�   '��       '�� T     7  �� 5�   "   " ��6(   .�5�   &   ��  �@��CornerRadius$ 8��6E   5�   #   �� 5�   "   " ��6(   84   6J   5�      5�      5�      5�      �� 5�      -   5        $ AddDeleteCategoryTagPanel��5�      $ Vertical=�6@   $��Center=�6W   $
��Auto��6r   $��25, 0, 0, 0q�6�   .+�5�      �� -   5        $ AddTagButton��5�       # AddDeleteTagCategoryButtonStyle#��[ 63   .��5�      �� $��Images/add.png��6    5�      �� 5�      -   5        $ DeleteTagButton��5�      #��[ 66   $��
0, 5, 0, 5q�6�   .��5�      �� $��Images/remove.png��6    5�      �� 5�      -   5        $ AddToCollageButton��5�      #��[ 69   .��5�      �� $ ��Images/collageMainMenu.png��6    $��40��6D   5�      5�      5�      �� 5�      $��Additional information:��6   $��
0, 5, 0, 3q�67   $��Center=�6K   
�������$��Medium"�6|   �� 5�      -   5        $&  AdditionalInformationRichTextBox��5�       % !AdditionalInformationTextboxStyle#��[ 5�      $��75��5�      $��0, 0, 0, 10q�6)   $	��340��5�      $	 300��5�      6 �� AcceptsReturn$
6 True��5�      7 �� TextWrapping$
7 Wrap=�5�      $
��Auto=�5�      ��5�      �� *Description+5�      �� 5�      $ 
Horizontal=�6   $��Center=�62   $��Center=�6O   $
��Auto��6j   $��0, 0, 0, 10q�6x   .+�5�      �� -   5        $ AcceptButton��5�      $��40��5�        AcceptButtonStyle#��[ 5�      $��0��5�      $	��250��6)   ��5�      �� *SaveChangesLocal+��5�      �� $ selectedWindow��.��5�      �� $��Images/confirm.png��6   5�      �� 5�      -   5        $ UploadButton��5�      $��40��5�      $��1��5�      $��
5, 0, 0, 0q�6)   $	��246��5�       	 UploadButtonStyle#��[	 6%   ��5�      �� *AddToDbCommand+.��5�      �� $��Images/upload.png��6   5�      5�      5�      5�      5�      