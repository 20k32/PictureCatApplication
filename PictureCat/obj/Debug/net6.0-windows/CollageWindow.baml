   M S B A M L   `   `   `  ����E  APictureCat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PictureCat
PictureCat  5   	       PictureCat.CollageWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PictureCat     5   	   -   5          � Title$  Collage��5   	   $	��760��5	   	   $
��1050��6   $��Center=�5
   	    � WindowStartupLocation$ CenterOwner=�5   	    � SizeToContent$ WidthAndHeight=�5   	      WindowBackgroundBrush#��[  5   	   $��19(�5   	    � 	Resources 5      %   PictureCat.DoubleToIntConverter  DoubleToIntConverter��%   &	         5   
   6%   5      .��5      � $��Center=�6   $��Center=�6(   $��10q�6C    � ColumnDefinitions 5   
   �� 5      $	��100��6   $	��310��6.   �� 5      $��* �6   .+�5   
   �� $��Center=�6   $��0��62   $��Center=�6B    �� Orientation$ Vertical=�6]   $	��700��6t   $��
0, 0, 5, 0q�6�   .+�5      �� -   5         ��Name$ ImageListBox��5      $	��300��6,   $	��500��68   ��5      �� 5      .R�5      �� $ 
Horizontal=�6%   $��50��6>   .+�5      ��  ��Name$ ListBoxItemImage��6$   $��10q�6V   ��6>   �� *path+6V   �� 5        ��Name$ ListBoxItemPath��6(   $��10q�6X   ��6A   �� *title+6X   5!      5"      5$      �� 5%      -   5        	 ��Name$	 DeleteButton��5%      $��0, 10, 0, 0q�6+   $��Delete Selected��6@     DleteButtonStyle#��[ 6u   $	��200��6�   �� 5&      -   5        $	 
SaveButton��5&      $��Save Collage��6)   $��0, 10q�6Y     AcceptButtonStyle#��[ 6h   $	��200��6�   �� 5'      -   5        
 ��Name$
 GridCeckBox��5'      $��Markup��6,   $��Center=�6w   $��Center=�6�   
�������$��Medium"�6�   �� 5(      $��Choose collage style:��6   $��Center=�65   $��0, 20, 0, 0q�6R   
�������$��Medium"�6{   �� 5)      $	��200��6   $ 
Horizontal=�5*      $��Center=�62   .+�5,      �� -   5         ��Name$ DelayBar��5-       �� Orientation$ 
Horizontal=�5.      $	��148��5/      $��30��6!    � Maximum$ 6Q�50       � Minimum$ 2Q�6!   ��5,      Z� 51      �� 52      -   5        $ DelayValueTextBox��52      
�������$��Medium"�59      $��10��5:      $��20q�5;      $��Center=�5<      ��53      ��  �� ElementName$ DelayBar�� �� Path$ Value� �� 	Converter# [ 5<      5=      5>      �� 5?   
   -   5         ��Name$ CollageCanvas��5@      ���	#FFE8E7E7$	��700��5B      $	��700��5C      $��1��5D       J� 	AllowDrop$
 True��5E      .+�5G      � -	   5         �Name$ 
CanvasGrid��5H      $
 True��5I      ��5J      �� $ CollageCanvas��$ Width���5K      �� $ CollageCanvas��$ Height�5M      5N      5S      5T      