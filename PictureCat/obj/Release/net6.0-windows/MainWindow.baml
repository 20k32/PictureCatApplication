   M S B A M L   `   `   `  ���� E  APictureCat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PictureCat
PictureCat      PictureCat.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  $localclr-namespace:PictureCat  -     �Name$
  Main�� � Title$ 
PicutreCat��$	��620��$	��950��   WindowBackgroundBrush#��[  $��19(� � WindowState$ 	Maximized=� � Icon$  Images/ApplictaionIcon.ico��.��� $
��Auto�� � RowDefinitions �� $��* �$	��310���� $��10 ��� $��* �.+��� -    ��Name$ LogPanel�� �� Orientation$ Vertical=�$��0, 10, 0, 0q�$��Center=�$��Center=�$��0��$	��350��$	��300��.+��� -    ��Name$ MainPageImage��$��Images/Logo.png��$��Uniform=� J� 	AllowDrop$
 True��$	��300��	 ^� InitialShowDelay$		 300��
 ^� PlacementTarget
 ��  �� ElementName$ MainPageImage�� � ToolTip _�   MainMenuToolTipStyle#��[ .��Drag image files here�� $��1�� �� ShowsPreview$ False��$	��100��$��4�� �� ResizeDirection$
 Rows=� �� ResizeBehavior$ PreviousAndNext=�$��Center=�� $��2�� �� $
��Auto ��� $
��Auto ��� $��* �.+�� $��0��$��Center=� � ColumnDefinitions �� $��* ��� $
��Auto ��� $��* �.+��� $��1�� �� CornerRadius$ 10�����	#FF1F76AD$��2q�$��0,0,0,10q�$��Center=�._��� $ 
Horizontal=�.+��� -    ��Name$ SearchTextBox��$	��400��  SearchTextBoxStyle#��[ �� -    ��Name$ SearchButton��$��3q�  SearchButtonStyleFirst#��[ $��Center=�$��30�� J� RenderTransformOrigin$ 0.5,0.5&�$��40��.���� $��Images/search.png��$
��Fill=�$��26���� -   $ NavigationPanel��$ 
Horizontal=�$��1��$��Center=�.+��� -   $ MainPageButton��  MainMenuButtonHeadStyle#��[ .���� $��Images/mainpage.png��$��55��$��Uniform=��� -   $ LikedPicturesButton��  BaseMainMenuButtonStyle#��[ .���� $��Images/LikeButton.png��$��Uniform=��� -	   $ CollagePicturesButton��#��[ .���� $ ��Images/collageMainMenu.png��$��Uniform=��� -
   $ AddedPicturesButton��  MainMenuButtonTailStyle#��[ .���� $��Images/addedpage.png��$��Uniform=��� -    ��Name$ MyScrollViewer��$
��Auto=�$��2��$��0, 10, 0, 0q� � FocusVisualStyle Z� .��   PictureCat.FlexWrapPanel  -     Name$ MyFlexWrapPanel��$��Center=�$��Center=�   RequestedItemWidth$	 400Q�$��50, 10, 50, 10q� %� IsVirtualizing$
 True�� %� VirtualizationMode$ 	Recycling=� %� 
ScrollUnit$ Pixel=�