namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;

    public class OpenWith
    {
        /// <summary>
        /// set Normal Constant Datatype 
        /// </summary>        
        public const uint Normal = 1;

        #region structs

        ///// <summary>
        ///// This Method used to Open the Application.
        ///// </summary>
        //[Serializable]
        //public struct ShellExecuteInfo
        //{
        //    #region Variables
        //    /// <summary>
        //    /// create Size as Integer DataType
        //    /// </summary>
        //    private int size;

        //    /// <summary>
        //    /// create Verb as String DataType
        //    /// </summary>
        //    private string verb;

        //    /// <summary>
        //    /// create File as String DataType
        //    /// </summary>
        //    private string file;

        //    /// <summary>
        //    /// create Show as UnsignedInteger DataType
        //    /// </summary>
        //    private uint show;

        //    #endregion Variables

        //    #region Property

        //    /// <summary>
        //    /// Gets or sets the size.
        //    /// </summary>
        //    /// <value>The size.</value>
        //    public int Size
        //    {
        //        get { return this.size; }
        //        set { this.size = value; }
        //    }

        //    /// <summary>
        //    /// Gets or sets the verb.
        //    /// </summary>
        //    /// <value>The verb.</value>
        //    public string Verb
        //    {
        //        get { return this.verb; }
        //        set { this.verb = value; }
        //    }

        //    /// <summary>
        //    /// Gets or sets the file.
        //    /// </summary>
        //    /// <value>The file.</value>
        //    public string File
        //    {
        //        get { return this.file; }
        //        set { this.file = value; }
        //    }

        //    /// <summary>
        //    /// Gets or sets the show.
        //    /// </summary>
        //    /// <value>The show.</value>
        //    [CLSCompliant(false)]
        //    public uint Show
        //    {
        //        get { return this.show; }
        //        set { this.show = value; }
        //    }

        //    #endregion Property
        //}



        [Serializable]
        public struct ShellExecuteInfo
        {
            private int size;
            private uint mask;
            private IntPtr hwnd;
            private string verb;
            private string file;
            private string parameters;
            private string directory;
            private uint show;

            private IntPtr instApp;
            private IntPtr iDList;
            private string classOpen;
            private IntPtr hkeyClass;
            public uint hotKey;
            public IntPtr icon;
            public IntPtr monitor;
            public int Size
            {
                get { return this.size; }
                set { this.size = value; }
            }

            public uint Mask
            {
                get { return this.mask; }
                set { this.mask = value; }
            }

            public IntPtr Hwnd
            {
                get { return this.hwnd; }
                set { this.hwnd = value; }
            }
            public string Verb
            {
                get { return this.verb; }
                set { this.verb = value; }
            }

            public string File
            {
                get { return this.file; }
                set { this.file = value; }
            }

            public string Parameters
            {
                get { return this.parameters; }
                set { this.parameters = value; }
            }

            public string Directory
            {
                get { return this.directory; }
                set { this.directory = value; }
            }

            public uint Show
            {
                get { return this.show; }
                set { this.show = value; }
            }
            public IntPtr InstApp
            {
                get { return this.instApp; }
                set { this.instApp = value; }
            }

            public IntPtr IDList
            {
                get { return this.iDList; }
                set { this.iDList = value; }
            }

            public string ClassOpen
            {
                get { return this.classOpen; }
                set { this.classOpen = value; }
            }
            public IntPtr HkeyClass
            {
                get { return this.hkeyClass; }
                set { this.hkeyClass = value; }
            }
            public uint HotKey
            {
                get { return this.hotKey; }
                set { this.hotKey = value; }
            }
            public IntPtr Icon
            {
                get { return this.icon; }
                set { this.icon = value; }
            }

            public IntPtr Monitor
            {
                get { return this.monitor; }
                set { this.monitor = value; }
            }



        }
        #endregion structs
        /// <summary>
        /// Shells the execute ex.
        /// </summary>
        /// <param name="lpExecInfo">The lp exec info.</param>
        /// <returns> Returns the boolean result</returns>
        [DllImport("shell32.dll", SetLastError = true)]
        extern public static bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);

        /// <summary>
        /// Open the file with the corresponding Application.
        /// </summary>
        /// <param name="file">The file.</param>
        public static void OpenAs(string file)
        {
            /*8ShellExecuteInfo sei = new ShellExecuteInfo();
            sei.Size = Marshal.SizeOf(sei);
            sei.Verb = "openas";
            sei.File = file;
            sei.Show = Normal;

            if (!ShellExecuteEx(ref sei))
            {
                throw new System.ComponentModel.Win32Exception();
            }*/

            ShellExecuteInfo sei = new ShellExecuteInfo();
            sei.Size = Marshal.SizeOf(sei);
            sei.Verb = "openas";
            sei.File = file;
            sei.Show = Normal;
            if (!ShellExecuteEx(ref sei))
            {
                throw new System.ComponentModel.Win32Exception();
            }
        }

    }
}
