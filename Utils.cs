using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExampleAssembly
{
    public class Utils
    {
        public static void CreateConsole()
        {
            AllocConsole();

            IntPtr stdHandle = GetStdHandle(-11);
            IntPtr intPtr = CreateFile("CONOUT$", 3221225472u, FileShare.Write, IntPtr.Zero, FileMode.OpenOrCreate, (FileAttributes)0, IntPtr.Zero);

            if (intPtr != stdHandle)
            {
                SetStdHandle(-11, intPtr);

                Console.SetOut(new StreamWriter(Console.OpenStandardOutput(), Console.OutputEncoding)
                {
                    AutoFlush = true
                });
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetStdHandle(int nStdHandle, IntPtr hHandle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CreateFile([MarshalAs(UnmanagedType.LPTStr)] string filename, [MarshalAs(UnmanagedType.U4)] uint access, [MarshalAs(UnmanagedType.U4)] FileShare share, IntPtr securityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition, [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes, IntPtr templateFile);

        [DllImport("kernel32")]
        private static extern bool AllocConsole();
    }
}
