   M S B A M L   `   `   `  ����C  ?UBPayApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:UBPayAppUBPayApp  5   	       UBPayApp.Refund   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:UBPayApp     5   	   -   5          � Title$  退款��5   	   $	��338��6   $	��291��6!    � WindowStartupLocation$ CenterScreen=�6D   .��5	      � .+�5
   
   �� $��订单号：��6   $
��Left=�6   $��	10,10,0,0q�6:   $	��Top=�6M   �� 5   
   -   5        $��lab_order_id��5   �   $�� ��6   $
��Left=�6   $��	85,10,0,0q�66   $	��Top=�6I    J� RenderTransformOrigin$ 0.025,0.577&�6a   $	��188��6�   6�   �� 5   
   $��退款单号：��6   $
��Left=�6    $��	10,41,0,0q�6;   $	��Top=�6N   �� 5   
   -   5        $��lab_refund_order_id��5   m   $�� ��6   $
��Left=�6   $��	85,41,0,0q�66   $	��Top=�6I   $	��188��6a   6m   �� 5   
   $��总金额：��6   $
��Left=�6   $��	10,72,0,0q�6:   $	��Top=�6M   �� 5   
   -   5        $��lab_total_amount��5   �   $�� ��6   $
��Left=�6   $��	85,72,0,0q�66   $	��Top=�6I   $ 0.075,0.077&�6a   $	��188��6�   6�   �� 5   
   $��已退金额：��6   $
��Left=�6    $��
10,103,0,0q�6;   $	��Top=�6O   �� 5   
   -   5        $��lab_retired_amout��5   n   $�� ��6   $
��Left=�6   $��
85,103,0,0q�66   $	��Top=�6J   $	��188��6b   6n   �� 5   
   -   5        $��lab_refund_amout��5   g   $��退款金额：��6   $
��Left=�6    $��
10,134,0,0q�6;   $	��Top=�6O   6g   �� 5   
   -   5        $��tx_refund_amount��5   �   $
��Left=�6   $��23��6-   $��
85,134,0,0q�69    �� TextWrapping$
 Wrap=�6M   $�� ��6a   $	��Top=�6i   $	��165��6�   6�   �� 5   
   -   5        $��lab_refund_reason��5   �   $��退款原因：��6   $
��Left=�6    $��
10,165,0,0q�6;   $	��Top=�6O   $ 
0.85,0.615&�6g   6�   �� 5   
   -	   5        $��tx_refund_reason��5   �   $
��Left=�6   $��23��6-   $��
85,165,0,0q�69   $
 Wrap=�6M   $�� ��6a   $	��Top=�6i   $	��165��6�   6�   �� 5   
   $��退款结果：��6   $
��Left=�6    $��
10,196,0,0q�6;   $	��Top=�6O   �� 5   
   -
   5        $��lab_Refund_Status��5   n   $�� ��6   $
��Left=�6   $��
85,196,0,0q�66   $	��Top=�6J   $	��165��6b   6n   �� 5   
   -   5        $��
btn_refund��5      $��确认退款��6   $
��Left=�6    $��
85,239,0,0q�6;   $	��Top=�6O   $	��136��6g   $��35��6s   6�   5%      5&      