// -------------------------------------------------------------------------------------------------
// <copyright file="Scanning.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// This Form is to Scan the Attachment File
// 
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D9005
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    
    using System.Windows.Forms;
    using System.IO;
    using System.Configuration;
    using System.Collections;
    using TerraScan.Common;
    using System.Web.Services.Protocols;
    using System.Runtime.InteropServices;
    ////using Interop.WIALib;
    using MODI;
    using stdole;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Security.Permissions;
    using System.Threading;
    using TerraScan.Utilities;
    using TwainLib;
   
    /// <summary>
    /// Scanning class
    /// </summary>
    public partial class Scanning : Form, IMessageFilter
    {
        #region Variables

        /// <summary>
        /// Background thread
        /// </summary>
        private static BackgroundWorker backGroundWork1;

        /// <summary>
        /// firstImagePath
        /// </summary>
        private string firstImagePath = string.Empty;

        /// <summary>
        /// imageFileName
        /// </summary>
        private string imageFileName = string.Empty;

        /// <summary>
        /// MODI.Document
        /// </summary>
        private MODI.Document document = new MODI.Document();

        ///// <summary>
        ///// finalFilePath
        ///// </summary>
        //private string finalFilePath = string.Empty;

        /// <summary>
        /// imageFileName
        /// </summary>
        private string filePathText = string.Empty;

        /////// <summary>
        /////// WiaClass Object created
        /////// </summary>
        ////private WiaClass wiaManager1 = new WiaClass();

        /////// <summary>
        /////// CollectionClass Object created 
        /////// </summary>
        ////private CollectionClass wiaDevs1;

        /////// <summary>
        /////// ItemClass Object created
        /////// </summary>
        ////private ItemClass wiaRoot1;

        /////// <summary>
        /////// CollectionClass Object created
        /////// </summary>
        ////private CollectionClass wiaPics1;

        /////// <summary>
        /////// ItemClass Object created
        /////// </summary>
        ////private ItemClass wiaItem;       

        /// <summary>
        /// ThreadStart object
        /// </summary>
        private ThreadStart job1;

        /// <summary>
        /// Thread Object
        /// </summary>
        private Thread thread1;

        /// <summary>
        /// F9006
        /// </summary>
        private F9006 form9006 = new F9006();

        private bool msgfilter;
        private Twain tw;
        private int picnumber = 0;

        /// <summary>
        /// Background thread
        /// </summary>
        private static BackgroundWorker backGroundWork;

        /// <summary>
        /// ThreadStart object
        /// </summary>
        private ThreadStart job;

        /// <summary>
        /// Thread Object
        /// </summary>
        private Thread thread;

        private string outFile;

        private int scannedcount;

        #endregion 

        #region Construtor

        public Scanning()
        {
            this.InitializeComponent();
            //// this.firstImagePath = tempPath;
            tw = new Twain();
            tw.Init(this.Handle);
        }

        /////// <summary>
        /////// Initializes a new instance of the <see cref="T:Scanning"/> class.
        /////// </summary>
        /////// <param name="tempPath">The temp path.</param>
        ////public Scanning(string tempPath)
        ////{
        ////    this.InitializeComponent();
        ////   //// this.firstImagePath = tempPath;
        ////    tw = new Twain();
        ////    tw.Init(this.Handle);
        ////}

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the scan text file path.
        /// </summary>
        /// <value>The scan text file path.</value>
        public string ScanTextFilePath
        {
            get { return this.filePathText; }
            set { this.filePathText = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Threads the job.
        /// </summary>
        public void ThreadJob()
        {
            Progressform prgfrm = new Progressform();
            prgfrm.ShowDialog();
        }

        /// <summary>
        /// Handles the ProgressChanged event of the BackGroundWork control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ////this.percent = e.ProgressPercentage;
        }

        /// <summary>
        /// Handles the DoWork event of the bw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ////this.wiaItem.Transfer(this.imageFileName, false);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        public void BackGroundWorkRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ////status = true;
                this.thread1.Abort();
                backGroundWork1.Dispose();
                this.AppendImage();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the ContinueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ContinueButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CheckOfficeScanner())
                {
                    try
                    {
                        if (!msgfilter)
                        {
                            // this.Enabled = false;
                            msgfilter = true;
                            Application.AddMessageFilter(this);
                        }
                        tw.Acquire();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("ErrorOnScanning"), SharedFunctions.GetResourceString("ScanInterface"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ErrorOnScanning"), SharedFunctions.GetResourceString("ScanInterface"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Checks the office scanner.
        /// </summary>
        /// <returns>bool</returns>
        private bool CheckOfficeScanner()
        {

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + SharedFunctions.GetResourceString("MSOfficeFilePath");
            string[] modiFolder = Directory.GetDirectories(path);

            if (modiFolder.Length > 0)
            {
                foreach (string folder in modiFolder)
                {
                    string modiPath = folder + "\\" + SharedFunctions.GetResourceString("MSOFFiceExe");

                    if (System.IO.File.Exists(modiPath))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
                   

        #region Scanning-Twain

        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            TwainCommand cmd = tw.PassMessage(ref m);
            if (cmd == TwainCommand.Not)
                return false;
            switch (cmd)
            {
                case TwainCommand.CloseRequest:
                    {
                        EndingScan();
                        tw.CloseSrc();
                        break;
                    }
                case TwainCommand.CloseOk:
                    {
                        EndingScan();
                        tw.CloseSrc();
                        break;
                    }
                case TwainCommand.DeviceEvent:
                    {
                        break;
                    }
                case TwainCommand.TransferReady:
                    {
                        ArrayList pics = tw.TransferPictures();
                        EndingScan();
                        tw.CloseSrc();
                        picnumber++;
                        for (int i = 0; i < pics.Count; i++)
                        {
                            IntPtr img = (IntPtr)pics[i];
                            bmpptr = GlobalLock(img);
                            pixptr = GetPixelInfo(bmpptr);
                            tw.SaveDIBAs(bmpptr, pixptr, i + 1);
                        }
                        scannedcount = 0;
                        scannedcount = this.NoofScannedfileCount();
                        this.HeaderLabel.Text = scannedcount + " page scanned.";
                        break;
                    }
            }
            return true;
         }

        /// <summary>
        /// Endings the scan.
        /// </summary>
        private void EndingScan()
        {
            if (msgfilter)
            {
                Application.RemoveMessageFilter(this);
                msgfilter = false;
                this.Enabled = true;
                this.Activate();
            }
        }
        
        #endregion

        #region For Saving Scanning Document - Twain

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);


        BITMAPINFOHEADER bmi;
        Rectangle bmprect;
        IntPtr dibhand;
        IntPtr bmpptr;
        IntPtr pixptr;
        protected IntPtr GetPixelInfo(IntPtr bmpptr)
        {
            bmi = new BITMAPINFOHEADER();
            Marshal.PtrToStructure(bmpptr, bmi);

            bmprect.X = bmprect.Y = 0;
            bmprect.Width = bmi.biWidth;
            bmprect.Height = bmi.biHeight;

            if (bmi.biSizeImage == 0)
                bmi.biSizeImage = ((((bmi.biWidth * bmi.biBitCount) + 31) & ~31) >> 3) * bmi.biHeight;

            int p = bmi.biClrUsed;
            if ((p == 0) && (bmi.biBitCount <= 8))
                p = 1 << bmi.biBitCount;
            p = (p * 4) + bmi.biSize + (int)bmpptr;
            return (IntPtr)p;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        internal class BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }

        #endregion
        

        /// <summary>
        /// Appends the image.
        /// </summary>
        private void AppendImage()
        {
            try
            {
                if (this.AxViewer.PageNum < 0)
                {
                    this.document.Create(this.imageFileName);
                    this.AxViewer.Document = this.document;
                    this.AxViewer.Refresh();
                }
                else
                {
                    MODI.Document tempDoc = new MODI.Document();
                    tempDoc.Create(this.imageFileName);

                    for (int j = 0; j < tempDoc.Images.Count; j++)
                    {
                        this.document.Images.Add(tempDoc.Images[j], null);
                    }
                }

                this.HeaderLabel.Text = this.document.Images.Count + " page scanned.";
                this.AxViewer.Visible = false;
                this.Show();
                ////this.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the DoneButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DoneButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (scannedcount > 0)
                {
                    try
                    {
                        string[] mergeImageFilesArray = null;
                        outFile = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "Final_scanned_page_" + DateTime.Now.Ticks + ".tiff";
                        string directory = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI";
                        mergeImageFilesArray = Directory.GetFiles(directory);
                        tw.JoinTiffImages(mergeImageFilesArray, outFile, EncoderValue.MultiFrame);
                        this.filePathText = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "PathTextFile" + DateTime.Now.Ticks + ".txt";

                        if (!string.IsNullOrEmpty(outFile))
                        {
                            this.document.Create(outFile);
                            this.AxViewer.Document = this.document;
                            this.AxViewer.Refresh();
                        }

                        ////this.HeaderLabel.Text = this.document.Images.Count + " page scanned.";
                    }
                    catch (COMException comex)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                    }
                    this.SaveFile();

                    StreamWriter sr = new StreamWriter(this.filePathText);
                    sr.Write(this.outFile);
                    sr.Dispose();

                    this.NewDocument();

                    this.document.Close(false);

                    this.Dispose();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
           //// this.form9006.ShowDialog();
        }

        /// <summary>
        /// Handles the Load event of the Scanning control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Scanning_Load(object sender, EventArgs e)
        {
            try
            {
                scannedcount = 0;
                scannedcount = this.NoofScannedfileCount();
                this.HeaderLabel.Text = scannedcount + " page scanned.";
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }



        #endregion

        #region Methods

        private int NoofScannedfileCount()
        {
            int filecount = 0;
            string[] tempImageFiles = null;
            string dir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI";
            tempImageFiles = Directory.GetFiles(dir);
            foreach (string filePath in tempImageFiles)
                if (System.IO.File.Exists(filePath))
                {
                    try
                    {
                        filecount++;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            return filecount;
        }


        /// <summary>
        /// Saves the file.
        /// </summary>
        private void SaveFile()
        {
            try
            {
                if (this.document != null && System.IO.File.Exists(this.firstImagePath))
                {
                    this.document.SaveAs(this.firstImagePath, MODI.MiFILE_FORMAT.miFILE_FORMAT_TIFF_LOSSLESS, MODI.MiCOMP_LEVEL.miCOMP_LEVEL_HIGH);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// News the document.
        /// </summary>
        private void NewDocument()
        {
            if (this.document != null)
            {
                this.document.Create("");
                this.AxViewer.Document = this.document;
                this.AxViewer.Refresh();
            }
        }

        #endregion
    }
}