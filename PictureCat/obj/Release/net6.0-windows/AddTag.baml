   M S B A M L   `   `   `  ���� E  APictureCat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PictureCat
PictureCat      PictureCat.AddTag   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:PictureCat     -     �Name$  AddForm�� � Title$	 Add�� � SizeToContent$ WidthAndHeight=�$��16(� � WindowStartupLocation$ CenterOwner=� � 
ResizeMode$ NoResize=� � Icon$ /ApplictaionIcon.ico��.����  �� Orientation$ Vertical=�.+��� -    ��Name$ CatTagComboBox�� �� SelectedIndex$ 0��.@��� $��
Categories���� $
��Tags���� -   	 ��Name$	 AddStackPanel��$��Center=�$��Center=�$
��Auto��$
��Auto��.+��� -   
 ��Name$
 NewTagTextBox��$	��200��$��16(� �� 	MaxLength$ 30�� �� TextWrapping$
 Wrap=�$
��Auto=�" �� VerticalScrollBarVisibility$ Disabled=��� -    ��Name$ SubmitButton��$��Submit��$��30���� -   $	 DeleteStackPanel��.+��� -    ��Name$ DeletionListBox��$	��117��$	��341��$
��Auto=�$��Disabled=� %� IsVirtualizing$
 True�� %� CacheLength@  :System.Windows.Controls.VirtualizationCacheLengthConverter$ 1  %� IsContainerVirtualizable$
 True�� � 	Resources  �� 
TargetType   ListCheckBoxItemStyle �� BasedOn��%   '��       7   �� " ��8   �� -   $��Delete��