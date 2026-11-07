using System.Diagnostics;
using AkiMacro.Interop;

namespace AkiMacro.Input;

public class Win32ButtonStateProvider : IButtonStateProvider
{
    // 国服 YuanShen.exe，国际服 GenshinImpact.exe
    private static readonly string[] GenshinProcesses = ["YuanShen", "GenshinImpact"];

    private static bool IsKeyPressed(int virtualKey)
    {
        if (!IsGenshinForeground()) return false;
        short state = NativeMethods.GetAsyncKeyState(virtualKey);
        return (state & 0x8000) != 0;
    }

    private static bool IsGenshinForeground()
    {
        var name = GetForegroundProcessName();
        if (name is null) return false;
        foreach (var p in GenshinProcesses)
            if (string.Equals(name, p, StringComparison.OrdinalIgnoreCase))
                return true;
        return false;
    }

    private static string? GetForegroundProcessName()
    {
        var hwnd = NativeMethods.GetForegroundWindow();
        if (hwnd == IntPtr.Zero) return null;
        NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);
        if (pid == 0) return null;
        try
        {
            return Process.GetProcessById((int)pid).ProcessName;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public bool IsXButton1Pressed() => IsKeyPressed((int)VirtualKey.XBUTTON1);
    public bool IsXButton2Pressed() => IsKeyPressed((int)VirtualKey.XBUTTON2);
}
