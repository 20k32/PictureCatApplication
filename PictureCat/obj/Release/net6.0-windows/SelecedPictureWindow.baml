   M S B A M L   `   `   `  ���� E  APictureCat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PictureCat
PictureCat  %    PictureCat.SelecedPictureWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:PictureCat     -     �Name$  selectedWindow��$	��620�� � SizeToContent$ Height=� � 
ResizeMode$ NoResize=�   WindowBackgroundBrush#��[  $��19(� � WindowStartupLocation$ CenterOwner=�$��Visible=� � Icon$ /ApplictaionIcon.ico�� � Title �� *Title+.���  � ColumnDefinitions �� $��* ��� $	��501 ��� $��* �.+��� -    ��Name$ DeleteButton��$��2��$	��Top=�$��Center=�$��
0, 5, 5, 0q�  DleteButtonStyle#��[ $��64��.���� $��Images/delete.png���� $��1�� �� Orientation$ Vertical=�$��Center=�.+��� -   	 ��Name$	 PictureBorder��$��0, 10, 0, 0q�
 �� CornerRadius$
 
3, 3, 7, 7�����	#FF3A3A3A$��5q�$��Center=�$��Center=�
�������._��� -    ��Name$ ImageStackPanel��$ Vertical=�$��Center=�$��Center=�.+��� -    ��Name$ SelectedImage��$��1��$��0��$	��257��$	��400������  �� 
TargetType" �� �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  �� Property �@�Cursor �� Value$
 Hand���� ���	#FF7D7770$��Center=�$��Center=�$��
0, 5, 0, 0q�._��� -    ��Name$ TitleTextBox��$	��150�� �� 	MaxLength$	 100��  TitleTextBoxStyle#��[ ���� *Title+����  �� ElementName$ SelectedImage�� �� Path$ ActualWidth��� $ 
Horizontal=�$��Center=�.+��� 
�������$
 10��$��40��._�(  "System.Windows.Controls.DatePicker  -     Name$ datePicker1�� J� 	AllowDrop$ False��   IsDropDownOpen$ false��   SelectedDateFormat$
 Long=�$��Center=�$	��254�� =� ErrorTemplate Z�    SelectedDate �� *ReleaseDate+ � 	Resources :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType  Key �� Color'  !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup!   Transitions%  System.Windows.VisualTransition"   GeneratedDuration	#   To$   States   System.Windows.VisualState%   
Storyboard& g� Duration	' W� To( �� TargetProperty) �� 
TargetName* J� Opacity+ �� Padding, �Name- ��Name. J� IsHitTestVisible/ ��Name��%�  '        '  %     �� "  .���  � ��
 ���� �� "  .���  � �� �� "  .R��  ���� WatermarkBrush $ Transparent��    CommonStates��!   $" 0D�  $" 0:0:0.1D�$# 	MouseOver��.$   
Normal��  	MouseOver��  WatermarkStates��!   $" 0D�.$   Unwatermarked��  Watermarked��.% �� .��W� $& 0D�' 0$( Opacity�$) ContentElement��W� $& 0D�' 1$( Opacity�$) PART_Watermark��  FocusStates��!   $" 0D�.$   	Unfocused��  Focused��.+��� $	 Border��
������ #��z��#��z��$
 5��$* 1Q�#+ z��._�� $, WatermarkContent��#��z��#��z��.+��� $	 ContentElement��
������ $��1q��� $	 watermark_decorator��
������ $��1q�._��� $- PART_Watermark��$|�False��$. False��$* 0Q�$��2q��� $/ PART_ContentHost��#��z��$��0q�#��z�� P�  � ��$  ��. ���  � ��
 �����P�  � �� Z� . ���  � ��
 ������� -   $ 
LikeButton��$��Center=�  LikeButtonStyleFirst#��[ $��25, 10, 0, 10q�$��41��$��57��.���� $��Images/like.png��$��37��$��32���� $ 
Horizontal=�$��Center=�$	��336��.+��� $ Vertical=�$��Center=�$	��254��.+��� $��2q�$
��Auto=�$	��200��._��� -	   0 ��Name$0 CheckListBox��$
��Auto=�$��Disabled=�$	��117��1 %� IsVirtualizing$
1 True��2 %� CacheLength@  :System.Windows.Controls.VirtualizationCacheLengthConverter$2 1 3 %� IsContainerVirtualizable$
3 True��   ListCheckBoxItemStyle4 �� BasedOn��%V   '��       '�� /     7  �� " ��.���  �@��CornerRadius$ 8���� " ��84   �� $��2q�$
��Auto=�$	��200��._��� -
   $0 
HashTagBox��5 �� SelectionMode$5 Single=�$
��Auto=�$��Disabled=�$	��117��$
1 True��$2 1 $
3 True�� ��%V   '��       '�� /     7  �� " ��.���  �@��CornerRadius$ 8���� " ��84   �� -   $ AddDeleteCategoryTagPanel��$ Vertical=�$��Center=�$
��Auto��$��25, 0, 0, 0q�.+��� -   $ AddTagButton�� # AddDeleteTagCategoryButtonStyle#��[ .���� $��Images/add.png���� -   $ DeleteTagButton��#��[ $��
0, 5, 0, 5q�.���� $��Images/remove.png���� -   $ AddToCollageButton��#��[ .���� $ ��Images/collageMainMenu.png��$��40���� $��Additional information:��$��
0, 5, 0, 3q�$��Center=�
�������$��Medium"��� -   $&  AdditionalInformationRichTextBox�� % !AdditionalInformationTextboxStyle#��[ $��75��$��0, 0, 0, 10q�$	��340��$	 300��6 �� AcceptsReturn$
6 True��7 �� TextWrapping$
7 Wrap=�$
��Auto=����� *Description+�� $ 
Horizontal=�$��Center=�$��Center=�$
��Auto��$��0, 0, 0, 10q�.+��� -   $ AcceptButton��$��40��  AcceptButtonStyle#��[ $��0��$	��250������ *SaveChangesLocal+���� $ selectedWindow��.���� $��Images/confirm.png���� -   $ UploadButton��$��40��$��1��$��
5, 0, 0, 0q�$	��246�� 	 UploadButtonStyle#��[	 ���� *AddToDbCommand+.���� $��Images/upload.png��