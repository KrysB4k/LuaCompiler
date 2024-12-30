using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;

namespace LuaCompiler
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        const string dllName = "luac547.dll";
        const string missingDll = dllName + " not found. Please make sure this file is in the working directory of the compiler tool.";

        [DllImport(dllName, EntryPoint = "CompileFile", CallingConvention = CallingConvention.Cdecl)]
        public static extern void CompileFile(string input, string output);

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length >= 1)
            {
                foreach (string filepath in e.Args)
                {
                    if (filepath.ToLower().EndsWith(".lua"))
                    {
                        try
                        {
                            string output = $"{filepath}c"; // append "c" to form .luac extension
                            CompileFile(filepath, output);
                        }
                        catch (EntryPointNotFoundException)
                        {
                            MessageBox.Show(missingDll, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            Current.Shutdown();
                        }
                        catch (DllNotFoundException)
                        {
                            MessageBox.Show(missingDll, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            Current.Shutdown();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            Current.Shutdown();
                        }
                    }
                    else
                    {
                        string filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);
                        string msg = $"Invalid file type of file '{filename}'";
                        MessageBox.Show(msg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                Current.Shutdown();
            }
            else
            {
                MainWindow wnd = new MainWindow();
                wnd.Show();
            }
        }
    }
}
