   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  F BLoya.Dameer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:Loya.Dameer;assembly=Loya.DameerLoya.Dameer P LUtility.Tool.Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullfGclr-namespace:Utility.Tool.Controls.View;assembly=Utility.Tool.ControlsUtility.Tool.Controls.View  UBPayApp   UBPayApp.OrderQuery   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6sys&clr-namespace:System;assembly=mscorlib     CTimeTool.clr-namespace:Loya.Dameer;assembly=Loya.Dameer     XViewGclr-namespace:Utility.Tool.Controls.View;assembly=Utility.Tool.Controls     -     N� Title$  
OrderQuery�� � 	Resources    CheckRadioFillNormal � Color � Offset  CheckRadioStrokeNormal  CheckRadioFocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  RadioButtonColorStyle �� 
TargetType	 �� 
TargetType
 ��Name J� Opacity �� RadiusX �� RadiusY ��Name �� Triggers P� Property P� Value �� 
TargetName(  "System.Windows.Controls.DatePicker  DatePickerCalendar'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard g� Duration	 W� To �� TargetProperty �� 
TargetName �Name  Key �� Color   Transitions%  System.Windows.VisualTransition   GeneratedDuration	   To	 �� To  g� 	BeginTime! �� KeyTime" �� Value# O� KeyTime$ O� Value% � ColumnDefinitions& � RowDefinitions' ��Name( �� CornerRadius) �� EndPoint* �� 
StartPoint+ J� RenderTransformOrigin, ��Name:  4System.Windows.Controls.Primitives.DatePickerTextBox-  Name
. � Tag/ J� IsHitTestVisible0 �Name1 � AllowsTransparency2 � 	StaysOpen3 � PlacementTarget4 �� ElementName5 �� Value6 �� Binding7 �� Source8 �� RelativeSource " MyScrollViewerControlTemplate29 ��Name: �� CanContentScroll; �� CanHorizontallyScroll< �� CanVerticallyScroll= ��Name4  .System.Windows.Automation.AutomationProperties>   AutomationId? � Cursor@ �� ScrollableHeightA � MaximumB � Minimum*C �� #ComputedVerticalScrollBarVisibilityD �� ViewportHeightE �� ViewportSize  MyScrollBarStyleF � ValueG �� ModeH �� ScrollableWidthI �� Orientation,J �� %ComputedHorizontalScrollBarVisibilityK �� ViewportWidth   LinearGradientBlueBackground 	 MyBtnBorderColor 
 MyBtnHoverBackgroundColor  MyWpfButtonL ��Name��%�  &	        &	 B     &	 �     &	 �     7   7  7  '  (    &	 �    &	 `    &		 �    &	
 �    &	 �    7  7 	 7 
 �� .��� $ 	#FFD2D4D2��$ 0Q�� $ 	#FFFFFFFF��$ 1Q��� .��� $ 	#FF004C94��$ 0Q�� $ 	#FF003C74��$ 1Q��� .���  � �� �� .R��� $��14,0,0,0q� . #��� ��$��1��$	 1 2R��� " 	�.���  � ��# � ����  � ��8   ��  � ��8  ��  � ��$ 1q���  � �� �� "	 	�.R�� .+��� $
 	rectangle��
�������
���   �$ 1Q�$ 3��$ 3��$��1���� $ 	textBlock��$	��8,0q�$
��Wrap=�#��z��$��Center=�$	��Top=�$��27.137��$��11(�
����  � P�  �@	�	GroupName$  ��P�  �@	�	GroupName$  ��P�  �@d�	IsChecked$
 TrueY�. ��� $ 	rectangle�� � �� �	#FF1094FF�� $ 	rectangle�� �@J�Opacity$ 1Q��� $ 	textBlock�� � �� �� .��� $ #FFFFFF��$ 0Q�� $ #FFFFFF��$ 1Q�P�  �@J�IsMouseOver . . ��� $ 	rectangle�� Z�  �@J�Effect�� $ 	rectangle�� � �� �#AAAAAAP�  � �� . . ��� 8   �@�FocusVisualStyle��  � ��$ 2,0,0,0q�P�  � {� .  . ��� # � �� � ���� "  .���  � ��
 �   ���  � ��$	 335����  � ��$ 73����  � |� .  ��  � ��$ Center=���  �@ IsTodayHighlighted . ��  �@ SelectedDateFormat$ Short=���  � ��
 �������  � ��$ 1q���  � �� �#D2D6D9��  �@ CalendarStyle# �  ��  � �� �� "	  .R��� #��z��#��z��#��z��   CommonStates��.   
Normal��  Disabled��. �� .��W� $ 0D�$ 1X�$ Opacity�$ PART_DisabledVisual��._�� $ 	PART_Root��#��z��#��z�� ���� DisabledBrush $ 	#A5FFFFFF���� DropDownButtonTemplate "	 ��.R��    CommonStates��   $ 0D�  $ 0:0:0.1D�$ 	MouseOver��  $ 0:0:0.1D�$ Pressed��.   
Normal��  	MouseOver��. �� .���� $ 0D�$ 	#FF448DCAX�$1 +(Border.Background).(SolidColorBrush.Color)�$ 
Background���� $  0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[3].(GradientStop.Color)�$ BackgroundGradient��.g��� $! 0��$" 	#7FFFFFFF���� $  0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�$ BackgroundGradient��.g��� $! 0��$" 	#CCFFFFFF���� $  0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�$ BackgroundGradient��.g��� $! 0��$" 	#F2FFFFFF��  Pressed��. �� .���� $  0X�$ 00:00:00.001D�$1 +(Border.Background).(SolidColorBrush.Color)�$ 
Background��.g��� $! 0��$" 	#FF448DCA��U� $  0X�$ 00:00:00.001D�$ (UIElement.Opacity)�$ 	Highlight��.\��� $# 0��$$ 1Q��� $  0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�$ BackgroundGradient��.g��� $! 0��$" 	#EAFFFFFF���� $  0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�$ BackgroundGradient��.g��� $! 0��$" 	#C6FFFFFF���� $  0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[3].(GradientStop.Color)�$ BackgroundGradient��.g��� $! 0��$" 	#6BFFFFFF���� $  0X�$ 00:00:00.001D�$O I(Border.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�$ BackgroundGradient��.g��� $! 0��$" 	#F4FFFFFF��  Disabled��.+�� 
������ $��LeftToRight=�$��Center=�$��18��$��0q�$��Center=�$��19��% �� $	��20* ��� $	��20* ��� $	��20* ��� $	��20* �& �� $	��23* ��� $	��19* ��� $	��19* ��� $	��19* �.+��� $' 	Highlight��$y�Hidden=����	#FF45D6FA$��0q�$��4��$( 0,0,1,1��$��-1q�$ 0Q�$��0��$��4���� $' 
Background��$y�Hidden=����	#FFFFFFFF$��0q����	#FF1F3B53$��4��$( .5��$��0,-1,0,0q�$ 1Q�$��1��$��3���� $' BackgroundGradient��$y�Hidden=����	#BF000000$��0q�$��4��$( .5��$��0,-1,0,0q�$ 1Q�$��1��$��3������ $
) .7,1&�$
* .7,0&�.��� $ 	#FFFFFFFF��$ 0Q�� $ 	#F9FFFFFF��$ 0.375Q�� $ 	#E5FFFFFF��$ 0.625Q�� $ 	#C6FFFFFF��$ 1Q��� $y�Hidden=�$��4��$��1��$��1������ $) 0.3,-1.1&�$* 0.46,1.6&�.��� $ 	#FF4084BD��� $ 	#FFAFCFEA��$ 1Q����� $) 0.48,-1&�$* 	0.48,1.25&�.��� $ 	#FF494949��� $ 	#FF9F9F9F��$ 1Q�F� $��4��$��0��"����        � 	= ��[ � z     	#��z��#��z��$��Center=�$��4,3,4,3q�$��1��$��3��$+ 0.5,0.5&�$��Center=�A� $y�Hidden=�$��4�����	#FFFFFFFF$��Center=�$��3��$��0��$��Center=�$��3���� $' DisabledVisual��$y�Hidden=����	#B2FFFFFF$��1q�$��4��$( 	0,0,.5,.5��$ 0Q�$��0��$��4��% �� $��* ��� $
��Auto �.+��� $, PART_Button��$��1��#��z��|�.  $
��Left=�$��3,0,3,0q�$��0��$��Center=�$��28��$��28��
������ .���� $2��,/UBPayApp;component/bin/Release/pic/rili.png��$��18��  $- PART_TextBox��#��z. $��0��/ .  |�.  $��Center=�$��0��$��Stretch=�$��Normal$�$��11(����#484C50$��73��$��0,0,2,0q����#F0F0F0
������ � $ PART_DisabledVisual��$��2��$��0��/ .  $ 0Q�$��0��$��73��% �� $��* ��� $
��Auto �.+�� $0 
PART_Popup��1 . $��Bottom=�2 .  3 �� $4 PART_TextBox�� �� $5 false��6 �� #7 Zr�.a��� $ PART_TextBox�� �� *
Foreground+8 �� *TemplatedParent+ � ��P�  �@J�IsMouseOver . . ���  �@J�Opacity$	 0.8Q�P�  �@ IsDropDownOpen . . ���  �@J�Opacity$ 1Q��� "	 ��.R�� $
 Grid��#��z��% �� $��* ��� $
��Auto �& �� $��* ��� $
��Auto �.+��� $
 Corner��$��1��
�������$��1���� $!9 PART_ScrollContentPresenter��#: z��; .  < .  #��z��#��z��$��0��#��z��$��0���� $= PART_VerticalScrollBar��$> VerticalScrollBar��$? Arrow��$��1��#A z@ $B 0Q�$��0��#y�zC #E zD #���  F �� *VerticalOffset+$G OneWay=�8 �� *TemplatedParent+�� $= PART_HorizontalScrollBar��$> HorizontalScrollBar��$? Arrow��$��0��#A zH $B 0Q�$I 
Horizontal=�$��1��#y�zJ #E zK #���  F �� *HorizontalOffset+$G OneWay=�8 �� *TemplatedParent+�� $) 0.5,1&�$* 0.5,0&�.��� $ 	#FF377FED��$ 0Q�� $ 	#FF074CC0��$ 1Q��� $ 	#FF2D78F4���� $ 	#FF317EF3���� " ��.���  � ��8   ��  � ��
 �������  � ��8  ��  � �� �� "	 ��.R��� $' border��#��z��#��z��#��z�� . $( 10,10,10,10��._��� $L contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ��� $ border��8   � ��.��� 
�������.+��� -   M ��Name$M lb__OrderQuery��$��订单查询��$��13(�$��30��$
��Left=�$��37,0,0,0q�$	��Top=�$
��Bold"�� -   $ grid1��$��20��$
��Left=�$��29,2,0,0q�$	��Top=�$��4�����	#FF1970F8�� -   $M label1��$��订单查询��$��13(�$��30��$
��Left=�$��
46,146,0,0q�$	��Top=�$
��Bold"�� -   $ grid2�����	#FF1970F8$��20��$
��Left=�$��
30,151,0,0q�$	��Top=�$��4��� -   $ displayData��.+�&   System.Windows.Controls.DataGrid  -   N  Name$N OrderQueryPage��O   AutoGenerateColumnsO .  $��32,178,12,30q�$��Visible=�P   CanUserAddRowsP .  ���� Q   ColumnsQ 0	  *System.Windows.Controls.DataGridTextColumn	  ,
  &System.Windows.Controls.DataGridColumnR 
  Header$R id��S 
  Width5  /System.Windows.Controls.DataGridLengthConverter$	S 100 1  +System.Windows.Controls.DataGridBoundColumnT   BindingT �� *id+$G TwoWay=�U �� UpdateSourceTrigger$U PropertyChanged=�	  $R 订单编号��$	S 220 T �� *
order_id+$G TwoWay=�$U PropertyChanged=�	  $R 门店id��$	S 120 T �� *
store_id+$G TwoWay=�$U PropertyChanged=�	  $R 门店名称��$	S 200 T �� *
store_name+$G TwoWay=�$U PropertyChanged=�	  $R 操作员id��$	S 120 T �� *operator_id+$G TwoWay=�$U PropertyChanged=�	  $R 操作员名称��$	S 100 T �� *operator_name+$G TwoWay=�$U PropertyChanged=�	  $R 设备id��$	S 120 T �� *	device_id+$G TwoWay=�$U PropertyChanged=�	  $R 设备名称��$	S 100 T �� *device_name+$G TwoWay=�$U PropertyChanged=�	  $R 商户ID��$	S 100 T �� *merchant_id+$G TwoWay=�$U PropertyChanged=�	  $R 支付方式��$	S 150 T �� *channel_name+$G TwoWay=�$U PropertyChanged=�	  $R 交易类型��$	S 150 T �� *payment_name+$G TwoWay=�$U PropertyChanged=�	  $R 订单金额��$	S 130 T �� *amount+$G TwoWay=�$U PropertyChanged=�	  $R 实际交易金额��$	S 130 T �� *actual_amount+$G TwoWay=�$U PropertyChanged=�	  $R 退款金额��$	S 130 T �� *refund_amount+$G TwoWay=�$U PropertyChanged=�	  $R 订单标题��$	S 150 T �� *	subject+$G TwoWay=�$U PropertyChanged=�	  $R 商品信息��$	S 200 T �� *body+$G TwoWay=�$U PropertyChanged=�	  $R 订单状态��$	S 100 T �� *	_status+$G TwoWay=�$U PropertyChanged=�	  $R 订单备注��$	S 150 T �� *remark+$G TwoWay=�$U PropertyChanged=�	  $R 订单发起时间��$	S 200 T �� *time_create+$G TwoWay=�$U PropertyChanged=�4  .System.Windows.Controls.DataGridTemplateColumn  $R 操作��$S 80 V   CellTemplateV �� .R��� W �� Orientation$W 
Horizontal=�$��Center=�.+��� $, DetailedInformation��-   $��详细��$��40��$��Center=��� $, updateInformation��-	   $��更新��$��40��$��Center=�� $��29,30,12,254q�.+��� -
   $
 
rectangle1��$
��Left=�$��1,0,0,0q����	#FFC7B6B6$	��828��	� -   X 	�Name$X today��$��
32,12,0,74q�$
? Hand��#���   . $��今日��$
��Left=�$��47��	� -   $X 	yesterday��$��
96,12,0,74q�$
? Hand��#���   . $��昨日��$
��Left=�$��47��	� -   $X Nearly7days��$��158,12,0,74q�$
? Hand��#���   . $��本周��$
��Left=�$��47��	� -   $X Nearly30days��$��219,12,0,74q�$
? Hand��#���   . $��本月��$
��Left=�$��60��  Loya.Dameer.Dameer  -   $��datePicker1��$
��Left=�$��21��$��
505,10,0,0q�$	��Top=�$	��131��Y   Format$Y Custom=�Z   CustomFormat$Z yyyy-MM-dd HH:mm:ss��[   ShowCheckBox[ .  �� -   $M label9��$	��至��$��11(�$��27��$
��Left=�$��	475,8,0,0q�$	��Top=�  -   $��datePicker2��$
��Left=�$��21��$��
338,10,0,0q�$	��Top=�$	��131��$Y Custom=�$Z yyyy-MM-dd HH:mm:ss��[ .  �� -   $M label10��$��订单号：��$��11(�$��24��$
��Left=�$��	32,39,0,0q�$	��Top=�$��58���� -   $	' jm1��$( 5��$��95,40,551,39q�$��1q����	#FF737070._��� -   $M 	lbInputID��$��请输入订单号�����	#FFC8C5C5$��11(�$��Microsoft YaHei,Arial)�$��
-1,-1,9,-1q��� -   $M label11��$��门店��$��11(�$��24��$
��Left=�$��
295,32,0,0q�$	��Top=��� -   \ ��Name$\ cmb_store_id��$��20��$
��Left=�$��
349,34,0,0q�$	��Top=�$	��111��$��11(��� -   $, bt__OrderQuery��$��查询��$��19��$��Right=�$��
0,73,701,0q�$	��Top=�$��96��#��[ $��11(��� -   $, bt__exPortExcel��$��导出��$��19��$��Right=�$��
0,73,551,0q�$	��Top=�$��96��#��[ $��11(��� -   ] ��Name$] tBoxInputID��$��0q�$��11(�$�� ��
���   �^ �� 	MaxLength$^ 30��$��Center=�$��95,40,551,39q���Z� �� -   $M label11_Copy��$��	操作员��$��11(�$��24��$
��Left=�$��
465,32,0,0q�$	��Top=��� -   $\ cmb_operator_id��$��20��$
��Left=�$��
519,34,0,0q�$	��Top=�$	��111��$��11(��� -   $M label11_Copy1��$��设备��$��11(�$��24��$
��Left=�$��
635,32,0,0q�$	��Top=��� -   $\ cmb_device_id��$��20��$
��Left=�$��
689,32,0,0q�$	��Top=�$	��111��$��11(��� -   $M label11_Copy2��$��支付方式��$��11(�$��24��$
��Left=�$��
295,60,0,0q�$	��Top=��� -   $\ cmbPaymentMode��$��20��$
��Left=�$��
349,61,0,0q�$	��Top=�$	��111��$��11(��� -    $M label11_Copy3��$��交易类型��$��11(�$��24��$
��Left=�$��
465,60,0,0q�$	��Top=��� -!   $\ cmb_Payment_Type��$��20��$
��Left=�$��
519,59,0,0q�$	��Top=�$	��111��$��11(��� -"   $M label11_Copy4��$��订单状态��$��11(�$��24��$
��Left=�$��
635,56,0,0q�$	��Top=��� -#   $\ cmb_Order_Status��$��20��$
��Left=�$��
689,57,0,0q�$	��Top=�$	��111��$��11(��� -$   $, btPreviousPage��$��	上一页��$��11(�$��19��$��Right=�$��0,362,396,0q�#��[ $	��Top=�$��86���� -%   $, 
btNextPage��$��	下一页��$��11(�$��19��$��Right=�$��	0,362,7,0q�#��[ $	��Top=�$��86���� -&   $] txTargetPage��$��0��_ �� TextAlignment$_ Center=�$��30��$��Right=�$��Center=�$��0,358,220,0q��� -'   $M label12��$	��页��$��11(�$��24��$��Right=�$��0,360,200,0q�$	��Top=��� -(   $, btTargetPage��$��GO��$��11(�$��19��$��Right=�$��0,362,100,0q�#��[ $	��Top=�$��86��� -)   $ DisplayQueryPage��$��484,362,259,5q�$��19��.+��� $��Center=���`� ` �� StringFormat$#` 第{0}页/共{1}页 转到第��.1��� a �� Path$a CurrentPage��� $a 	TotalPage��� $a 
TargetPage�