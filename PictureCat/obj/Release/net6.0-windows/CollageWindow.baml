   M S B A M L   `   `   `  ���� E  APictureCat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PictureCat
PictureCat      PictureCat.CollageWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:PictureCat     -     � Title$  Collage��$	��760��$
��1050��$��Center=� � WindowStartupLocation$ CenterOwner=� � SizeToContent$ WidthAndHeight=�   WindowBackgroundBrush#��[  $��19(� � 	Resources %   PictureCat.DoubleToIntConverter  DoubleToIntConverter��%   &	         .��� $��Center=�$��Center=�$��10q� � ColumnDefinitions �� $	��100��$	��310���� $��* �.+��� $��Center=�$��0��$��Center=� �� Orientation$ Vertical=�$	��700��$��
0, 0, 5, 0q�.+��� -    ��Name$ ImageListBox��$	��300��$	��500������ .R��� $ 
Horizontal=�$��50��.+���  ��Name$ ListBoxItemImage��$��10q����� *path+��  ��Name$ ListBoxItemPath��$��10q����� *title+�� -   	 ��Name$	 DeleteButton��$��0, 10, 0, 0q�$��Delete Selected��  DleteButtonStyle#��[ $	��200���� -   $	 
SaveButton��$��Save Collage��$��0, 10q�  AcceptButtonStyle#��[ $	��200���� -   
 ��Name$
 GridCeckBox��$��Markup��$��Center=�$��Center=�
�������$��Medium"��� $��Choose collage style:��$��Center=�$��0, 20, 0, 0q�
�������$��Medium"��� $	��200��$ 
Horizontal=�$��Center=�.+��� -    ��Name$ DelayBar�� �� Orientation$ 
Horizontal=�$	��148��$��30�� � Maximum$ 6Q� � Minimum$ 2Q���Z� �� -   $ DelayValueTextBox��
�������$��Medium"�$��10��$��20q�$��Center=�����  �� ElementName$ DelayBar�� �� Path$ Value� �� 	Converter# [ �� -    ��Name$ CollageCanvas�����	#FFE8E7E7$	��700��$	��700��$��1�� J� 	AllowDrop$
 True��.+�� -	    �Name$ 
CanvasGrid��$
 True������ $ CollageCanvas��$ Width����� $ CollageCanvas��$ Height�