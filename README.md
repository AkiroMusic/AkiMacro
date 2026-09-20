# AkiMacro — Windows Macro Automation Tool

General-purpose mouse macro tool | Author: AkiMacro | Version 1.2.0

## Features

| Side Button | Function | Description |
|------|------|------|
| **X1** | Auto Rotation | While held, moves the mouse smoothly to the right; 20ms polling, 20 micro-steps |
| **X2** | Two-Button Macro | Left button held → right button tap → release, repeated twice |

## System Requirements

- Windows 10/11 (64-bit)
- [.NET 10.0 runtime](https://dotnet.microsoft.com/download/dotnet/10.0)
- Administrator privileges (the program automatically requests UAC elevation)

## Download

Download the latest version from [Releases](https://github.com/AkiroMusic/AkiMacro/releases/latest)

## Build from Source

```bash
dotnet restore
dotnet build -c Release
dotnet test
dotnet publish AkiMacro.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

You can also run `run.bat` directly to build and launch quickly.

## Project Structure

```
AkiMacro.sln
├── AkiMacro.csproj          # WPF project configuration
├── App.xaml / App.xaml.cs       # Application entry point + automatic UAC elevation
├── MainWindow.xaml / .cs        # Main window (borderless, custom title bar)
├── AboutWindow.xaml / .cs       # About window
├── SettingsWindow.xaml / .cs    # Settings window (read-only display)
├── app.manifest                 # Administrator privilege manifest
├── ViewModels/
│   └── MainWindowViewModel.cs   # MVVM ViewModel
├── Interop/                     # Win32 P/Invoke
│   ├── Win32Input.cs            # SendInput / GetAsyncKeyState
│   └── Win32Structs.cs          # INPUT / MOUSEINPUT structs
├── Input/                       # Input abstraction layer
│   ├── IInputSimulator.cs       # Simulation interface
│   ├── IButtonStateProvider.cs  # Button state interface
│   ├── Win32InputSimulator.cs   # Win32 implementation
│   └── Win32ButtonStateProvider.cs
├── MacroEngine/                 # Macro engine
│   ├── MacroWorkerBase.cs       # Worker base class
│   ├── RotationWorker.cs        # Auto rotation macro
│   ├── DoubleClickWorker.cs     # Two-button macro
│   ├── MacroCoordinator.cs      # Coordinator
│   └── InputLock.cs             # Global input lock
├── Styles/Theme.xaml            # Dark theme resources
├── tests/                       # xUnit tests
├── app.ico                      # Application icon
├── logo.png                     # UI icon
└── run.bat                      # One-click build and launch
```

## Technical Architecture

| Module | Description |
|------|------|
| **Interop** | Wraps the Win32 API |
| **Input** | Input abstraction layer for easy unit testing |
| **MacroEngine** | Macro execution engine; manages Worker thread lifecycles |
| **ViewModels** | MVVM pattern, keeping UI and business logic separate |

**Design highlights:**
- **Automatic elevation**: Checks for administrator privileges at startup and triggers UAC when not running as administrator
- **Thread safety**: The `InputLock.SyncRoot` global lock ensures only one Worker simulates input at a time
- **Error handling**: Automatically stops and reports an error when `SendInput` fails
- **Interface abstraction**: `IInputSimulator` / `IButtonStateProvider` support dependency injection and testing

## Disclaimer

This tool is for learning and entertainment purposes only. Do not use it for any behavior that violates game rules, laws, or regulations.

---

**Author: AkiMacro**

---

# AkiMacro

通用鼠标宏工具 | 作者：AkiMacro | 版本 1.2.0

## 功能

| 侧键 | 功能 | 说明 |
|------|------|------|
| **X1** | 自动旋转 | 按住时鼠标平滑向右移动，20ms 轮询，20 个微步 |
| **X2** | 双键宏 | 左键按住 → 右键点按 → 松开，循环两次 |

## 系统要求

- Windows 10/11（64 位）
- [.NET 10.0 运行时](https://dotnet.microsoft.com/download/dotnet/10.0)
- 管理员权限（程序会自动请求 UAC 提权）

## 下载

从 [Releases](https://github.com/AkiroMusic/AkiMacro/releases/latest) 下载最新版本

## 从源码构建

```bash
dotnet restore
dotnet build -c Release
dotnet test
dotnet publish AkiMacro.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

也可直接运行 `run.bat` 快速构建并启动。

## 项目结构

```
AkiMacro.sln
├── AkiMacro.csproj          # WPF 项目配置
├── App.xaml / App.xaml.cs       # 应用入口 + UAC 自动提权
├── MainWindow.xaml / .cs        # 主界面（无边框、自定义标题栏）
├── AboutWindow.xaml / .cs       # 关于窗口
├── SettingsWindow.xaml / .cs    # 设置窗口（只读展示）
├── app.manifest                 # 管理员权限清单
├── ViewModels/
│   └── MainWindowViewModel.cs   # MVVM ViewModel
├── Interop/                     # Win32 P/Invoke
│   ├── Win32Input.cs            # SendInput / GetAsyncKeyState
│   └── Win32Structs.cs          # INPUT / MOUSEINPUT 结构体
├── Input/                       # 输入抽象层
│   ├── IInputSimulator.cs       # 模拟接口
│   ├── IButtonStateProvider.cs  # 按键状态接口
│   ├── Win32InputSimulator.cs   # Win32 实现
│   └── Win32ButtonStateProvider.cs
├── MacroEngine/                 # 宏引擎
│   ├── MacroWorkerBase.cs       # Worker 基类
│   ├── RotationWorker.cs        # 自动旋转宏
│   ├── DoubleClickWorker.cs     # 双键宏
│   ├── MacroCoordinator.cs      # 协调器
│   └── InputLock.cs             # 全局输入锁
├── Styles/Theme.xaml            # 深色主题资源
├── tests/                       # xUnit 测试
├── app.ico                      # 应用图标
├── logo.png                     # UI 图标
└── run.bat                      # 一键构建启动
```

## 技术架构

| 模块 | 说明 |
|------|------|
| **Interop** | 封装 Win32 API |
| **Input** | 输入抽象层，便于单元测试 |
| **MacroEngine** | 宏执行引擎，管理 Worker 线程生命周期 |
| **ViewModels** | MVVM 模式，UI 与业务逻辑分离 |

**设计要点：**
- **自动提权**：启动时检测管理员权限，非管理员触发 UAC
- **线程安全**：`InputLock.SyncRoot` 全局锁确保同一时间只有一个 Worker 模拟输入
- **错误处理**：`SendInput` 失败时自动停止并上报错误
- **接口抽象**：`IInputSimulator` / `IButtonStateProvider` 支持依赖注入和测试

## 免责声明

本工具仅供学习和娱乐使用，请勿用于任何违反游戏规则或法律法规的行为。

---

**作者：AkiMacro**
