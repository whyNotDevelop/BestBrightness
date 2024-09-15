using System;
using System.Runtime.Loader;

public class CustomAssemblyLoadContext : AssemblyLoadContext
{
    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        return LoadUnmanagedDllFromPath(unmanagedDllName);
    }

    public IntPtr LoadUnmanagedLibrary(string absolutePath)
    {
        return LoadUnmanagedDllFromPath(absolutePath);
    }
}
