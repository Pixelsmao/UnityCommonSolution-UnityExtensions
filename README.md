# UnityCommonSolution -  UnityExtensions

![GitHub](https://img.shields.io/badge/Unity-2021.3%2B-blue)
![GitHub](https://img.shields.io/badge/license-MIT-green)
![GitHub](https://img.shields.io/badge/Platform-Windows-red)

这是一个为Unity开发者提供的功能扩展库，包含大量实用的扩展方法和工具类，旨在简化日常开发工作流程。代码经过模块化组织，可方便地集成到现有项目中。

## 安装

1. **通过克隆仓库安装**

   将本仓库克隆到您的 Unity 项目的 `Assets` 目录下：

   ```bash
   git clone https://github.com/Pixelsmao/UnityCommonSolution-UnityExtensions.git
   ```

2. **使用UPM进行安装：**

   在 Unity 编辑器中，点击顶部菜单栏,打开 Package Manager 窗口.

   ```
   Window > Package Manager
   ```

   在 Package Manager 窗口的左上角，点击 **+** 按钮，然后选择 **Add package from git URL...**。
   在弹出的输入框中，粘贴本仓库的 Git URL：

   ```
   https://github.com/Pixelsmao/UnityCommonSolution-UnityExtensions.git
   ```

   然后点击 **Add**。

## 主要功能模块

### 1. 扩展方法 (Extension Methods)
系统化分类的扩展方法集合，涵盖：
- **数据结构**：Array、Dictionary、List、Queue、Stack等集合类型的扩展
- **数据类型**：Bool、Bytes、Enum、Float、Int、String等基础类型的扩展
- **反射处理**：Type、FieldInfo、PropertyInfo等反射相关扩展
- **系统操作**：文件处理、进程管理等系统级功能
- **Unity组件**：Transform、GameObject、Camera等组件扩展
- **UI系统**：RectTransform、Image、Text等UGUI组件的扩展

### 2. 功能库 (Extension Libraries)
提供特定领域的实用工具库：
- `Colors` - 颜色库
- `Regexs` - 正则表达式集合

### 3. 特殊功能 (Extensions)
- `EventTimer` - 带标签参数的事件计时器系统

## 安装使用
1. 通过Unity Package Manager添加仓库
2. 或直接导入`UnityExtensions.asmdef`到您的项目

## 适用平台
兼容Unity 2021.3 LTS及以上版本