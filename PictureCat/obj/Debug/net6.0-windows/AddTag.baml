   M S B A M L   `   `   `  ����E  APictureCat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PictureCat
PictureCat  5   	       PictureCat.AddTag   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PictureCat     5   	   -   5          �Name$  AddForm��5   	    � Title$	 Add��5   	    � SizeToContent$ WidthAndHeight=�5	   	   $��16(�5
   	    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�5   	    � Icon$ /ApplictaionIcon.ico��5   	   .��5      ��  �� Orientation$ Vertical=�6   .+�5   
   �� -   5         ��Name$ CatTagComboBox��5       �� SelectedIndex$ 0��6+   .@�5      �� $��
Categories��6   �� 5      $
��Tags��6   5      �� 5   
   -   5        	 ��Name$	 AddStackPanel��5      $��Center=�6,   $��Center=�6I   $
��Auto��6d   $
��Auto��6q   .+�5      �� -   5        
 ��Name$
 NewTagTextBox��5      $	��200��6-   $��16(�69    �� 	MaxLength$ 30��6G    �� TextWrapping$
 Wrap=�6V   $
��Auto=�6j   " �� VerticalScrollBarVisibility$ Disabled=�6�   6�   �� 5      -   5         ��Name$ SubmitButton��5      $��Submit��6+   $��30��6<   6H   5      �� 5   
   -   5        $	 DeleteStackPanel��5      .+�5      �� -   5         ��Name$ DeletionListBox��5      $	��117��6/   $	��341��6<   $
��Auto=�6H   $��Disabled=�5       %� IsVirtualizing$
 True��5       %� CacheLength@  :System.Windows.Controls.VirtualizationCacheLengthConverter$ 1 5       %� IsContainerVirtualizable$
 True��5       � 	Resources 5       �� 
TargetType   ListCheckBoxItemStyle �� BasedOn��%0   '��       7   �� 5       " ��6   8   6>   5!      5"      �� 5#      -   5        $��Delete��5#      6&   5$      5%      5&      