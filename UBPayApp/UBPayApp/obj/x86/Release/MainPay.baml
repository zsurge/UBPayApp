   M S B A M L   `   `   `  ����   UBPayApp    UBPayApp.MainPay   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  MainPay�� � 	Resources    ButtonFocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  ButtonNormalBackground �� EndPoint �� 
StartPoint � Color	 � Offset  ButtonNormalBorder
 �� Color  ButtonClickShow �� 
TargetType �� ElementName �� 
TargetType ��Name  ButtonClickUp1 J� Opacity ��Name �� Triggers P� Property P� Value �� 
TargetName ! MyScrollViewerControlTemplate �Name � ColumnDefinitions � RowDefinitions ��Name �� CanContentScroll �� CanHorizontallyScroll �� CanVerticallyScroll ��Name4  .System.Windows.Automation.AutomationProperties   AutomationId � Cursor �� ScrollableHeight  � Maximum! � Minimum*" �� #ComputedVerticalScrollBarVisibility# �� ViewportHeight$ �� ViewportSize  MyScrollBarStyle% � Value& �� Mode' �� RelativeSource( �� ScrollableWidth) �� Orientation,* �� %ComputedHorizontalScrollBarVisibility+ �� ViewportWidth  ScrollBarDisabledBackground  VerticalScrollBarPageButton 	 ScrollBarThumb, �� RadiusX- �� RadiusY !
 HorizontalScrollBarPageButton. ]�Name/ J� IsMouseOver0 ]� DecreaseRepeatButton1 �� PageUpCommand2 ]� IncreaseRepeatButton3 �� PageDownCommand4 ]� Thumb5 �� Triggers6 �� PageLeftCommand7 �� PageRightCommand��%a  &	        &	 N     &	 �     &	 �     7   7  7  7  &	 �    &	 e    &	 x    &		 '    &	
 	    &	 �	    7  7  7 	 7  7 
 7 
 7 	 7  �� .���  � �� �� .R��� $��2q� . #��� ��$��1��$	 1 2R��� $	 0,1&�$	 0,0&�.��� $ #F3F3F3��$	 0Q�� $ #EBEBEB��$		 0.5Q�� $ #DDDDDD��$		 0.5Q�� $ #CDCDCD��$	 1Q��� $
 	#FF707070���� " ��.���  �@�FocusVisualStyle8   ��  � ��8  ��  � ��$ 0,0,0,0q���  � �� �#666666��  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� *OpenCommand+$ GH����  � �� �� " ��.R�� $��Stretch=�#��z��#��z��$��0q�.+��� #��z��#��z��$
��Left=�$��0��$	��Top=�$��0���� #��z��$��Stretch=�$
��Auto��$
��Auto��$��0���� $ HighlightBackground��8�� $��Stretch=�$
��Auto��$��Stretch=�$
��Auto��$ 0Q���Z� �� $ contentPresenter��#��z��$��0q���. # z #��z�� P�  � �� . . ��� $ HighlightBackground�� �@J�Opacity$	 0.8Q��� 8   � ����  � �� �#FC9153�� $ HighlightBackground�� � ��$ 0���� $ HighlightBackground�� � ��$ Stretch=��� $ HighlightBackground�� � ��$ Stretch=�P�  �@J�IsKeyboardFocused . P�  �@d�	IsChecked$
 trueY�P�  � {� .  . ���  � �� �#ADADAD�� " ��.R�� $
 Grid��#��z�� �� $��* ��� $
��Auto � �� $��* ��� $
��Auto �.+��� $ Corner��$��1��
�������$��1���� $! PART_ScrollContentPresenter��# z�� .   .  #��z��#��z��$��0��#��z��$��0���� $ PART_VerticalScrollBar��$ VerticalScrollBar��$ Arrow��$��1��#  z $! 0Q�$��0��#y�z" #$ z# #���  % �� *VerticalOffset+$& OneWay=�' �� *TemplatedParent+�� $ PART_HorizontalScrollBar��$ HorizontalScrollBar��$ Arrow��$��0��#  z( $! 0Q�$) 
Horizontal=�$��1��#y�z* #$ z+ #���  % �� *HorizontalOffset+$& OneWay=�' �� *TemplatedParent+�� $
 #E7E7E7���� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  � �� �� " ��.R��� #��z��#��z��#��z���� " n�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � �� �� " n�.R��� $��	thumbRect�����#FE6E0E$, 3��$- 3�� P�  �@J�IsMouseOver . . ��� $ 	thumbRect�� � �� �#C1C1C1�� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  � �� �� " ��.R��� #��z��#��z��#��z���� " ��.���  � �� �#07E7E7��  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��$ 6����  � ��$ 16����  � �� �� " ��.R�� $ Bg�����#E7E7E7 . $��5�� �� .+�]� $. 
PART_Track����. #{�z/ 0 �� #��Z1 8��  2 �� #��Z3 8�� 4 n� 8��  P�  � {� .  . ��� $ Bg��8   � ��5 P�  �@��Orientation$ 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 0����  � ��$ 6����  � ��$ 6����  � �� �#07E7E7��  � �� �� " ��.R�� $ Bg�����#00E7E7 .  �� .+�]� $. 
PART_Track��#{�z/ 0 �� #��Z6 8�� 2 �� #��Z7 8�� 4 n� 8��  P�  � {� .  . ��� $ Bg��8   � ��.��� ��Z� .+�]� .+�� -   .+� � -   $��frame1��$
��Left=�$��	0,126,0,0q�8  � NavigationUIVisibility$8 Hidden=�$	��870���� -   $��scrollViewer1��#��[ $��679,115,62,323q���. 9 �� PanningMode$9 HorizontalOnly=�$��Visible=�$��Disabled=�$y�Hidden=�.���� -   : ��Name$: inputCanvas��$
��Left=�$	��139��$	��Top=�$	��173��� -   $��grid1��$��59��$
��Left=�$	��Top=�$	��870�����	#FF000079.+��� -   $��	img__Logo��$��52��$
��Left=�$��26,4,0,0q�$
��Fill=�$	��Top=�$��52��$4��./UBPayApp;component/bin/Release/pic/ublogo.png���� -   $��label5��$��UB云支付收银台��$��32��$
��Left=�$��	98,14,0,0q�$	��Top=�
�������$��20(�o� $��Center=�$��32��$��724,14,10,0q�$	��Top=����	#FF000079.@�m� -   $��	menu_user��
�������$��32��$	��136��$��18(�.@�m� -	   $��CheckUpgrade��
���   �$��检查更新��m� -
   $
��exit��
���   �$��退出��� -   $��grid2��$��61��$
��Left=�$��0,59,0,0q�$	��Top=�$	��668��.+��� -   $��lb__OrderQuery��$��订单查询��$��29��$
��Left=�$��	56,10,0,0q�$	��Top=�$��15(�$
��Bold"��� -   $��lb__OrderSum��$��订单汇总��$��29��$
��Left=�$��
179,10,0,0q�$	��Top=�$��15(�$
��Bold"��� -   $��
lb__Refund��$��退款处理��$��29��$
��Left=�$��
318,10,0,0q�$	��Top=�$��15(�$
��Bold"��� -   $��
lb__Change��$��	交接班��$��29��$
��Left=�$��
441,10,0,0q�$	��Top=�$��15(�$
��Bold"��� -   $��lb__Set��$��设置��$��29��$
��Left=�$��
562,10,0,0q�$	��Top=�$��15(�$
��Bold"��� -   $��img__OrderQuery��$��25��$
��Left=�$��	37,12,0,0q�$
��Fill=�$	��Top=�$��23��$5��//UBPayApp;component/bin/Release/pic/dingdan.png���� -   $��img__OrderSum��$��25��$
��Left=�$��
158,12,0,0q�$
��Fill=�$	��Top=�$��23��$;��5/UBPayApp;component/bin/Release/pic/dingdanchaxun.png���� -   $��img__Refund��$��23��$
��Left=�$��
295,14,0,0q�$
��Fill=�$	��Top=�$��23��$4��./UBPayApp;component/bin/Release/pic/refund.png���� -   $��img__Charge��$��23��$
��Left=�$��
420,14,0,0q�$
��Fill=�$	��Top=�$��23��$8��2/UBPayApp;component/bin/Release/pic/jiaojieban.png���� -   $��img__Setting��$��23��$
��Left=�$��
539,13,0,0q�$
��Fill=�$	��Top=�$��23��$4��./UBPayApp;component/bin/Release/pic/shezhi.png��� -   $��g_line1��$��3��$
��Left=�$��	26,41,0,0q�$	��Top=�$	��120�����	#FF1094FF; J� OpacityMask; Z� � -   $��g_line2��$��3��$
��Left=�$��
154,41,0,0q�$	��Top=�$	��120�����	#FF1094FF; Z� � -   $��g_line3��$��3��$
��Left=�$��
284,41,0,0q�$	��Top=�$	��121�����	#FF1094FF; Z� � -   $��g_line4��$��3��$
��Left=�$��
412,41,0,0q�$	��Top=�$	��108�����	#FF1094FF; Z� � -   $��g_line5��$��3��$
��Left=�$��
531,41,0,0q�$	��Top=�$	��109�����	#FF1094FF; Z� 