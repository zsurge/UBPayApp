﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace UBPayApp
{
    class HotKey
    {

        //调用WIN32的API

        [DllImport("user32.dll", SetLastError = true)]

        //声明注册快捷键方法，方法实体dll中。参数为窗口句柄，快捷键自定义ID，Ctrl,Shift等功能键，其他按键。

        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll", SetLastError = true)]

        //注销快捷键方法的声明。

        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    }



}
