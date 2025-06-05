# Unity Extensions Repository

这是一个为Unity开发者提供的功能扩展库，包含大量实用的扩展方法和工具类，旨在简化日常开发工作流程。代码经过模块化组织，可方便地集成到现有项目中。

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
- `ColorLibrary` - 颜色处理相关功能
- `RegexLibrary` - 正则表达式工具

### 3. 特殊功能 (Extensions)
- `EventTimer` - 带标签参数的事件计时器系统

## 安装使用
1. 通过Unity Package Manager添加仓库
2. 或直接导入`UnityExtensions.asmdef`到您的项目

## 适用平台
兼容Unity 2019.4 LTS及以上版本