   M S B A M L   `   `   `  ����   UBPayApp    UBPayApp.Login   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  Login�� � 	Resources     LinearGradientBlueBackground �� EndPoint �� 
StartPoint � Color � Offset  MyBtnBorderColor �� Color  MyBtnHoverBackgroundColor  MyWpfButton �� 
TargetType �� Property	 �� Value
 �� 
TargetType ��Name J� SnapsToDevicePixels �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 Z     &	 o     &	 �     7   7  7  �� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF377FED��$ 0Q�� $ 	#FF074CC0��$ 1Q��� $ 	#FF2D78F4���� $ 	#FF317EF3���� " ��.���  � ��8	   ��  � ��
	 �������  � ��8	  ��  � ��	 �� "
 ��.R��� $ border��#��z��#��z��#��z�� . $ 10,10,10,10��._��� $ contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ��� $ border��8	   � ��.��� $	��870������  �� ImageSource$6 0/UBPayApp;component/bin/Release/pic/login_bg.jpg��.+��� -   $��image1��$��70��$
��Left=�$��287,118,0,0q�$	��Top=�$	��291��$8��2/UBPayApp;component/bin/Release/pic/login_head.png��$
��Fill=�� -   $��	gridlogin��$	��220��$
��Left=�$��287,186,0,0q�$	��Top=�$	��291��
�������.+��� -   $��lb__IDLogin��$��账号登录��$��31��$
��Left=�$��	49,17,0,0q�$	��Top=�$��SimHei)�$��16(����	#FF050000�� -   $��lb__BarCodeLogin��$��扫描登录��$��SimHei)�$��16(�$��31��$
��Left=�$��
165,17,0,0q�$	��Top=����	#FFB1ACAC� -   $��gridIDLogin��$	��178��$
��Left=�$��288,228,0,0q�$	��Top=�$	��290��$y�Visible=�.+��� -   $��image3��$��28��$
��Left=�$��	52,24,0,0q�$
��Fill=�$	��Top=�$��30��$=��7/UBPayApp;component/bin/Release/pic/fuwushangguanli.png���� -   $��image4��$��28��$
��Left=�$��	52,68,0,0q�$6��0/UBPayApp;component/bin/Release/pic/password.png��$
��Fill=�$	��Top=�$��30���� -   $��img_Login__Inputframe1��$��32��$
��Left=�$
��Fill=�$	��Top=�$	��149��$��
104,24,0,0q�$<��6/UBPayApp;component/bin/Release/pic/inputframe_hot.png���� -	   $��img_Login__Inputframe2��$��32��$��104,65,37,0q�$
��Fill=�$	��Top=��� -
   $��	lbInputID��$��请输入账号��$��31��$
��Left=�$	��Top=����	#FFC8C5C5$��11(�$��Microsoft YaHei,Arial)�$��
115,25,0,0q��� -   $	��jm1��$ 5��$��106,27,40,126q�$��0q����	#FF737070._��� -   $��tBoxInputID��$��25��$	��138��$��0q�$��15(�$��user_1_1��
���   � �� 	MaxLength$ 30��$��Center=���Z� �� -   $��	lbInputPW��$��请输入密码��$��31��$
��Left=�$	��Top=����	#FFC8C5C5$��11(�$��Microsoft YaHei,Arial)�$��
115,67,0,0q��� -   $	��jm2��$ 5��$��106,66,40,82q�$��0q����	#FF737070._�G� -   $��tBoxInputPW��$��23��$	��142��$��0q�$��15(� G� Password$ 1111111��
���   � G� 	MaxLength$ 30��$��Center=���Z� �� -   $��	bt__login��$��登录��$��31��$
��Left=�$��100,114,0,0q�$	��Top=�$	��100��#��[ $��11(�� -   $��gridLoginInfo��$��85��$
��Left=�$��0,12,0,0q�$	��Top=�$	��291�����	#FFEBEBEB$y�Visible=�.+��� -   $��label1��$��正在登录...��$��41��$
��Left=�$��0,27,0,0q�$	��Top=�$	��291��$��Center=�$��11(�$y�Visible=�� -   $��gridCoeLogin��$	��166��$
��Left=�$��287,240,0,0q�$	��Top=�$	��291��$y�Visible=�.+��� -   $��lb__Info��$$��正在获取登录二维码...��$��41��$
��Left=�$	��Top=����	#FF241818$��11(�$��Microsoft YaHei,Arial)�$��0,26,0,0q�$	��291��$��Center=��� -   $��	img__Code��$��96��$
��Left=�$��	101,8,0,0q�$
��Fill=�$	��Top=�$��96���� -   $��lb_Ver��$��Label��$��28��$
��Left=�$��	25,35,0,0q�$	��Top=�