// -------------------------------------------------------------------------------------------------
// <copyright file="F9006.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// This Form is to Scan the Attachment File
// Zoom in ,zoom out, rotate left and Rigth the Attachment image if not clear
// </summary>
// -------------------------------------------------------------------------------------------------
namespace D9005
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
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
    /// AttachmentScan form for Scanning
    /// </summary>
    public partial class F9006 : Form, IMessageFilter
    {
        #region Variables

        /// <summary>
        /// Background thread
        /// </summary>
        private static BackgroundWorker backGroundWork;

        /// <summary>
        /// imageFileName
        /// </summary>
        private string imageFileName = string.Empty;

        /// <summary>
        /// successfullyScanned
        /// </summary>
        private bool successfullyScanned;

        /// <summary>
        /// imageFileName
        /// </summary>
        private string filePathText = string.Empty;

        /// <summary>
        /// tifFilePath
        /// </summary>
        private string tifFilePath = string.Empty;

        /// <summary>
        /// Modi Document Object Initialization
        /// </summary>
        private MODI.Document midoc;

        /// <summary>
        /// Counter
        /// </summary>
        private int count;

        /// <summary>
        /// Picture Box Image
        /// </summary>
        private System.Drawing.Image tstimage;

        /// <summary>
        /// picture box array value
        /// </summary>
        private int test;

        /// <summary>
        /// dontpaint
        /// </summary>
        private bool dontpaint;

        /////// <summary>
        /////// WiaClass Object created
        /////// </summary>
        ////private WiaClass wiaManager;

        /////// <summary>
        /////// CollectionClass Object created 
        /////// </summary>
        ////private CollectionClass wiaDevs;

        /////// <summary>
        /////// ItemClass Object created
        /////// </summary>
        ////private ItemClass wiaRoot;

        /////// <summary>
        /////// CollectionClass Object created
        /////// </summary>
        ////private CollectionClass wiaPics;

        /////// <summary>
        /////// ItemClass Object created
        /////// </summary>
        ////private ItemClass wiaItem;

        /// <summary>
        /// ThreadStart object
        /// </summary>
        private ThreadStart job;

        /// <summary>
        /// Thread Object
        /// </summary>
        private Thread thread;

        /// <summary>
        /// selecttext
        /// </summary>
        private bool selecttext;

        /// <summary>
        /// sx 
        /// </summary>
        private double sx;

        /// <summary>
        /// sy
        /// </summary>
        private double sy;

        /// <summary>
        /// MODI.Document
        /// </summary>
        private MODI.Document printDoc;


        private bool msgfilter;
        private Twain tw;

        private int picnumber = 0;

       // private TiffManager tiffmanager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AttachmentScanForm"/> class.
        /// </summary>
        public F9006()
        {
            this.InitializeComponent();
            this.PicBoxes = new System.Windows.Forms.PictureBox[200];
            tw = new Twain();
            tw.Init(this.Handle);
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets a value indicating whether [set button color].
        /// </summary>
        /// <value><c>true</c> if [set button color]; otherwise, <c>false</c>.</value>
        public bool SuccesfullyScanned
        {
            get { return this.successfullyScanned; }
            set { this.successfullyScanned = value; }
        }

        /// <summary>
        /// Gets or sets the scan file path.
        /// </summary>
        /// <value>The scan file path.</value>
        public string ScanFilePath
        {
            get { return this.tifFilePath; }
            set { this.tifFilePath = value; }
        }

        #endregion

        #region User Events

        /// <summary>
        /// Calccombovals this instance.
        /// </summary>
        public void Calccomboval()
        {
            try
            {
                this.AxDocViewer.GetScale(ref this.sx, ref this.sy);
                this.ZoomComboBox.Text = "";
                this.ZoomComboBox.Text = Convert.ToString(Convert.ToInt32((this.sx * 100))) + " %";
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
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
                this.thread.Abort();
                ////this.Show();
                backGroundWork.Dispose();

                ////Scanning scanImageForm = new Scanning(this.imageFileName);
                Scanning scanImageForm = new Scanning();
               //// scanImageForm.AddOwnedForm(scanImageForm);
 
                switch (scanImageForm.ShowDialog())
                {
                    case DialogResult.OK:
                        {
                            this.filePathText = scanImageForm.ScanTextFilePath;
                            if (!string.IsNullOrEmpty(this.filePathText))
                            {
                            StreamReader sr = new StreamReader(this.filePathText);
                            this.tifFilePath = sr.ReadLine();
                            sr.Dispose();

                            DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "FinalMODI");
                            if (!dirInfo.Exists)
                            {
                                Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "FinalMODI");
                            }
                            string newfilepath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "FinalMODI" + "\\" + "FinalImage" + DateTime.Now.Ticks + ".tif";
                            if (File.Exists(newfilepath))
                            {
                                ////File.Delete(newfilepath);
                                File.Copy(this.tifFilePath, newfilepath);
                                this.tifFilePath = newfilepath;
                                this.FormLoad(newfilepath);
                                this.Show();
                                this.ScanButton.Enabled = false;
                            }
                            else
                            {
                                if (File.Exists(this.tifFilePath))
                                {
                                    File.Copy(this.tifFilePath, newfilepath);
                                    this.tifFilePath = newfilepath;
                                    this.FormLoad(newfilepath);
                                    this.Show();
                                    this.ScanButton.Enabled = false;
                                }
                            }
                        }
                            ////this.Visible = true;
                            this.DeleteButton.Enabled = true;
                            break;
                       }

                    case DialogResult.Cancel:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Threads the job.
        /// </summary>
        public void ThreadJob()
        {
            Progressform prgfrm = new Progressform();
            prgfrm.ShowDialog();
        }

        /// <summary>
        /// Clicks the handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ClickHandler(Object sender, System.EventArgs e)
        {
            try
            {
                this.splitContainer1.Panel1.Refresh();
                this.test = (int)((System.Windows.Forms.PictureBox)sender).Tag;

                if (this.midoc.Images.Count == 1)
                {
                    this.PreviousButton.Enabled = false;
                    this.NextButton.Enabled = false;
                }
                else
                {
                    this.PreviousButton.Enabled = true;
                    this.NextButton.Enabled = true;
                }

                if (this.test == 0)
                {
                    this.PreviousButton.Enabled = false;
                }
                else if ((this.test+1) == this.midoc.Images.Count)
                {
                    this.NextButton.Enabled = false;
                }
                else
                {
                    this.PreviousButton.Enabled = true;
                    this.NextButton.Enabled = true;
                }
                
                this.PageNoTextBox.Text = (this.test + 1) + " of " + " " + this.midoc.Images.Count;

                System.Drawing.Graphics gr = ((System.Windows.Forms.PictureBox)sender).CreateGraphics();
                System.Drawing.Rectangle rect = ((System.Windows.Forms.PictureBox)sender).ClientRectangle;

                System.Drawing.Pen pen = new Pen(Color.Blue);
                pen.Width = 7;

                gr.DrawRectangle(pen, rect);

                this.AxDocViewer.PageNum = this.test;
                this.splitContainer1.Panel1.Focus();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Show the Menuon Right Click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Popup(object sender, EventArgs e)
        {
            try
            {
                this.splitContainer1.Panel1.Invalidate();
                MenuItem menuClicked = (MenuItem)sender;
                string item = menuClicked.Text;
                if (this.test < 0)
                {
                }
                else if (this.test >= 0)
                {
                    if (item == "Delete Page")
                    {
                        this.DeletePage();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the ScanButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScanButton_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1.Controls.Clear();
            try
            {
                if (!msgfilter)
                {
                    msgfilter = true;
                    Application.AddMessageFilter(this);
                }
                tw.Acquire();
             }
            catch (Exception ex)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ErrorOnScanning"), SharedFunctions.GetResourceString("ScanInterface"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveFile();

                this.successfullyScanned = true;
                ////tifFilePath = "C:\\Scan\\aa.tif";
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
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
                            string[] deleteImageFiles = null;
                            string dire = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI";
                            deleteImageFiles = Directory.GetFiles(dire);
                            foreach (string filePath in deleteImageFiles)
                                if (System.IO.File.Exists(filePath))
                                {
                                    try
                                    {
                                        File.Delete(filePath);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }

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

                            ////string[] mergeImageFilesArray = null;
                            //////string outFile = @"C:\tiff\Sample.tiff";
                            ////string outFile = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI" + "\\" + "Final_scanned_page_" + DateTime.Now.Ticks + ".tiff";
                            ////// mergeImageFilesArray = Directory.GetFiles(@"C:\tiff\");
                            ////string directory = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + "TempMODI";
                            ////mergeImageFilesArray = Directory.GetFiles(directory);
                            ////tw.JoinTiffImages(mergeImageFilesArray, outFile, EncoderValue.MultiFrame);

                            ////this.imageFileName = outFile;
                            backGroundWork = new BackgroundWorker();
                            //backGroundWork.DoWork += new DoWorkEventHandler(this.BackGroundWorkDoWork);
                            //backGroundWork.ProgressChanged += new ProgressChangedEventHandler(this.BackGroundWorkProgressChanged);
                            backGroundWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackGroundWorkRunWorkerCompleted);
                            backGroundWork.RunWorkerAsync();

                            this.job = new ThreadStart(this.ThreadJob);
                            this.thread = new Thread(this.job);
                            this.thread.IsBackground = false;
                            this.thread.Start();
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
        /// Handles the Load event of the F9006 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9006_Load(object sender, EventArgs e)
        {
            this.dontpaint = true;
            this.DeleteButton.Enabled = false;
            ////this.PicBoxes = new System.Windows.Forms.PictureBox[100];
            ////this.ControlStatus(false);
        }

        /// <summary>
        /// Handles the CurPageNumChanged event of the AxDocViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AxDocViewer_CurPageNumChanged(object sender, EventArgs e)
        {
            try
            {
                ////Condition Checked the issue 4933
                if (this.AxDocViewer.NumPages > 0)
                {
                    this.PicBoxes[this.AxDocViewer.PageNum].Focus();
                    this.PicBoxes[this.AxDocViewer.PageNum].Show();
                    this.test = this.AxDocViewer.PageNum;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DeletePage();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the NextButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NextButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.midoc != null)
                {
                    this.PreviousButton.Enabled = true;
                    if (this.test == (this.midoc.Images.Count - 1))
                    {
                        this.NextButton.Enabled = false;
                        this.PageNoTextBox.Text = (this.test + 1) + " of " + " " + this.midoc.Images.Count;
                    }
                    else if (this.test < (this.midoc.Images.Count - 1))
                    {
                        this.test = this.test + 1;
                        this.PageNoTextBox.Text = (this.test + 1) + " of " + " " + this.midoc.Images.Count;
                        if ((this.test + 1) == this.midoc.Images.Count)
                        {
                            this.NextButton.Enabled = false;
                        }
                    }


                    System.Drawing.Graphics gr1 = this.PicBoxes[test].CreateGraphics();
                    System.Drawing.Rectangle rectangle = this.PicBoxes[test].ClientRectangle;
                    System.Drawing.Pen npen = new Pen(Color.Blue);
                    npen.Width = 7;
                    gr1.DrawRectangle(npen, rectangle);

                    if (test > 0)
                    {
                        System.Drawing.Graphics gr2 = this.PicBoxes[test - 1].CreateGraphics();
                        System.Drawing.Rectangle rectangle1 = this.PicBoxes[test - 1].ClientRectangle;
                        System.Drawing.Pen npen1 = new Pen(Color.Gray);
                        npen1.Width = 7;
                        gr2.DrawRectangle(npen1, rectangle1);
                    }

                    this.AxDocViewer.PageNum = this.test;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviousButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviousButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.midoc != null)
                {
                    this.NextButton.Enabled = true;

                    if (this.test > 0)
                    {
                        this.test = this.test - 1;
                    }

                    if (this.test == 0)
                    {
                        this.PreviousButton.Enabled = false;
                        this.PageNoTextBox.Text = (this.test + 1) + " of " + " " + this.midoc.Images.Count;
                    }
                    else
                    {
                        this.PageNoTextBox.Text = (this.test + 1) + " of " + " " + this.midoc.Images.Count;
                    }

                    System.Drawing.Graphics gr1 = this.PicBoxes[test].CreateGraphics();
                    System.Drawing.Rectangle rectangle = this.PicBoxes[test].ClientRectangle;
                    System.Drawing.Pen npen = new Pen(Color.Blue);
                    npen.Width = 7;
                    gr1.DrawRectangle(npen, rectangle);

                    if (this.midoc.Images.Count > 1)
                    {
                        System.Drawing.Graphics gr2 = this.PicBoxes[test + 1].CreateGraphics();
                        System.Drawing.Rectangle rectangle1 = this.PicBoxes[test + 1].ClientRectangle;
                        System.Drawing.Pen npen1 = new Pen(Color.Gray);
                        npen1.Width = 7;
                        gr2.DrawRectangle(npen1, rectangle1);
                    }

                    this.AxDocViewer.PageNum = this.test;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Paint event of the splitContainer1_Panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void SplitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (this.dontpaint == true)
                {
                    ////Dont do anyhting
                }
                else if (this.dontpaint == false)
                {
                    System.Drawing.Graphics gr1 = this.PicBoxes[test].CreateGraphics();
                    System.Drawing.Rectangle rectangle = this.PicBoxes[test].ClientRectangle;
                    System.Drawing.Pen npen = new Pen(Color.Blue);
                    npen.Width = 7;
                    gr1.DrawRectangle(npen, rectangle);
                    ////this.splitContainer1.Panel1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the RotateRightButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RotateRightButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.midoc != null)
                {
                    ////MODI.Image rotateImage = (MODI.Image)this.midoc.Images[this.test];
                    ////rotateImage.Rotate(90);
                    ////this.PicBoxes[this.test].Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    ////this.PicBoxes[this.test].SizeMode = PictureBoxSizeMode.CenterImage;
                    ////this.splitContainer1.Panel1.Refresh();

                    MODI.Image rotateImage = (MODI.Image)this.midoc.Images[this.test];
                    rotateImage.Rotate(90);
                    this.PicBoxes[test].Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    this.PicBoxes[test].SizeMode = PictureBoxSizeMode.CenterImage;
                    this.splitContainer1.Panel1.Refresh();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the RotateLeftButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RotateLeftButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.midoc != null)
                {
                    ////MODI.Image rtanticlockwise = (MODI.Image)this.midoc.Images[this.test];
                    ////rtanticlockwise.Rotate(-90);
                    ////this.PicBoxes[this.test].Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                    ////this.PicBoxes[this.test].SizeMode = PictureBoxSizeMode.CenterImage;
                    ////this.splitContainer1.Panel1.Refresh();

                    MODI.Image rtanticlockwise = (MODI.Image)this.midoc.Images[this.test];
                    ////Correct the rotation part
                    rtanticlockwise.Rotate(-90);
                    this.PicBoxes[test].Image.RotateFlip(RotateFlipType.Rotate90FlipXY);
                    this.PicBoxes[test].SizeMode = PictureBoxSizeMode.CenterImage;
                    this.splitContainer1.Panel1.Refresh();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the Panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Panel1_Click(object sender, System.EventArgs e)
        {
            this.splitContainer1.Panel1.Focus();
        }

        /// <summary>
        /// Handles the MouseWheel event of the Panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Panel1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                System.Drawing.Graphics gr1 = this.PicBoxes[test].CreateGraphics();
                System.Drawing.Rectangle rectangle = this.PicBoxes[test].ClientRectangle;
                System.Drawing.Pen npen = new Pen(Color.Blue);
                npen.Width = 7;
                gr1.DrawRectangle(npen, rectangle);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                // MODI.Document printDoc1 = new MODI.Document();
                // printDoc1 = this.midoc;
                Printform frm = new Printform(this.count, this.midoc);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the PlusZoomButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PlusZoomButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.midoc != null)
                {
                    ////AxDocViewer.FitMode = MODI.MiFITMODE.miFree;
                    ////double sx = 0; double sy = 0;
                    ////AxDocViewer.GetScale(ref sx, ref sy);
                    ////AxDocViewer.SetScale(sx * 1.1, sx * 1.1);

                    ////zoomvaluecombo.Items.Remove(Convert.ToInt32(sx * 100));
                    this.AxDocViewer.FitMode = MiFITMODE.miFree;
                    ////double sx = 0; double sy = 0;
                    this.AxDocViewer.GetScale(ref this.sx, ref this.sy);
                    if (this.sx >= 10.0)
                    {
                        this.ZoomComboBox.Text = "";
                        this.ZoomComboBox.Text = Convert.ToString(Convert.ToInt32((this.sx * 100))) + " %";
                        MessageBox.Show("The zoom factor should be a number between 15 and 1000", "Zoom Factor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (this.sx <= 10.0)
                    {
                        this.AxDocViewer.SetScale(this.sx * 1.5, this.sx * 1.5);
                        this.Calccomboval();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the MinusZoomButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MinusZoomButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.midoc != null)
                {
                    ////AxDocViewer.FitMode = MODI.MiFITMODE.miFree;
                    ////double sx = 0; double sy = 0;
                    ////AxDocViewer.GetScale(ref sx, ref sy);
                    ////AxDocViewer.SetScale(sx * 0.9, sx * 0.9);

                    this.ZoomComboBox.Items.Remove(Convert.ToInt32(this.sx * 100));
                    this.AxDocViewer.FitMode = MiFITMODE.miFree;
                    ////double sx = 0; double sy = 0;
                    this.AxDocViewer.GetScale(ref this.sx, ref this.sy);
                    if (this.sx <= 0.15)
                    {
                        this.ZoomComboBox.Text = "";
                        this.ZoomComboBox.Text = Convert.ToString(Convert.ToInt32((this.sx * 100))) + " %";
                        MessageBox.Show("The zoom factor should be a number between 15 and 1000", "Zoom Factor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (this.sx >= 0.15)
                    {
                        this.AxDocViewer.SetScale(this.sx * 0.5, this.sy * 0.5);
                        this.Calccomboval();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the HandButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HandButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.midoc != null)
                {
                    this.AxDocViewer.ActionState = MODI.MiActionState.miASTATE_PAN;
                    this.HandButton.Enabled = false;
                    this.NormalHandButton.Enabled = true;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the NormalHandButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NormalHandButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.midoc != null)
                {
                    this.AxDocViewer.ActionState = MODI.MiActionState.miASTATE_NONE;
                    this.HandButton.Enabled = true;
                    this.NormalHandButton.Enabled = false;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the CloseAttachmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseAttachmentButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Disables the controls.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void ControlStatus(bool status)
        {
            this.SaveButton.Enabled = status;
            ////this.PrintButton.Enabled = status;
            this.NormalHandButton.Enabled = status;
            this.HandButton.Enabled = status;
            this.ZoomComboBox.Enabled = status;
            this.PlusZoomButton.Enabled = status;
            this.MinusZoomButton.Enabled = status;
            this.RotateLeftButton.Enabled = status;
            this.RotateRightButton.Enabled = status;
            this.DeleteButton.Enabled = status;
            this.ScanButton.Enabled = true;
        }

        /// <summary>
        /// News the document.
        /// </summary>
        private void NewDocument()
        {
            if (this.midoc != null)
            {
                this.midoc.Create("");
                this.AxDocViewer.Document = this.midoc;
                this.AxDocViewer.Refresh();
            }
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        private void SaveFile()
        {
            if (this.midoc != null)
            {
                this.midoc.SaveAs(this.tifFilePath, MODI.MiFILE_FORMAT.miFILE_FORMAT_TIFF_LOSSLESS, MODI.MiCOMP_LEVEL.miCOMP_LEVEL_HIGH);
            }
        }

        /// <summary>
        /// Forms the load.
        /// </summary>
        /// <param name="donefilepath">The donefilepath.</param>
        private void FormLoad(string donefilepath)
        {
            this.dontpaint = false;
            this.PreviousButton.Enabled = false;
            this.NextButton.Enabled = true;
            this.midoc = new MODI.Document();
            this.printDoc = new MODI.Document();
            this.midoc.Create(donefilepath);
            this.printDoc.Create(donefilepath);
            this.ControlStatus(true);
            ////this.filename = donefilepath;

            ////initialise each picture box 
            for (this.count = 0; this.count < this.midoc.Images.Count; this.count++)
            {
                MODI.Image modiImage;
                this.PicBoxes[this.count] = new PictureBox();
                this.PicBoxes[this.count].Width = 125;
                this.PicBoxes[this.count].Height = 175;
                if (this.count == 0)
                {
                    this.PicBoxes[this.count].Top = (this.count + 2) * 10;
                }
                else if (this.count > 0)
                {
                    this.PicBoxes[count].Top = this.PicBoxes[count - 1].Top + this.PicBoxes[count].Height + 20;
                }

                this.PicBoxes[count].Left = 50;
                stdole.IPictureDisp pic;
                modiImage = (MODI.Image)this.midoc.Images[this.count];
                pic = modiImage.get_Thumbnail(MiTHUMBNAIL_SIZE.miTHUMB_SIZE_MEDIUM);
                this.tstimage = PictureToImageConverter.Convert(pic);
                this.PicBoxes[count].Image = this.tstimage;
                this.PicBoxes[count].Tag = this.count;
                this.PicBoxes[count].Click += new System.EventHandler(this.ClickHandler);
                this.PicBoxes[count].BorderStyle = BorderStyle.FixedSingle;
            }

            this.splitContainer1.Panel1.Controls.AddRange(this.PicBoxes);
            this.splitContainer1.Panel1.AutoScroll = true;
            ////this.CreatePopupMenu();
            this.AxDocViewer.Document = this.midoc;

            ////this.AxDocViewer.FitMode = MiFITMODE.miFree;
            ////double sx = 0; double sy = 0;
            ////this.AxDocViewer.GetScale(ref this.sx, ref this.sy);
            ////this.AxDocViewer.SetScale(this.sx * 1.5, this.sx * 1.5);
            this.Calccomboval();

            if (this.midoc.Images.Count == 1)
            {
                this.PageNoTextBox.Text = "1" + " of" + " " + this.midoc.Images.Count;
                this.NextButton.Enabled = false;
                this.PreviousButton.Enabled = false;
            }
            else if (this.midoc.Images.Count > 1)
            {
                this.PageNoTextBox.Text = "1" + " of" + " " + this.midoc.Images.Count;
                this.NextButton.Enabled = true;
                this.PreviousButton.Enabled = true;
            }

            this.DeleteButton.Enabled = false;
            this.splitContainer1.Panel1.Invalidate();
        }

        /// <summary>
        /// Creates the popup menu.
        /// </summary>
        private void CreatePopupMenu()
        {
            ContextMenu popUpMenu = new ContextMenu();
            popUpMenu.MenuItems.Add("Delete Page", new EventHandler(this.Popup));
            this.splitContainer1.Panel1.ContextMenu = popUpMenu;
        }

        /// <summary>
        /// Deletes the page.
        /// </summary>
        private void DeletePage()
        {
            ////Condition Checked the issue 4933
            if (this.AxDocViewer.NumPages > 0)
            {
                System.Windows.Forms.DialogResult rslt = new DialogResult();
                rslt = MessageBox.Show("Are you sure you want to remove the selected page?", "TerraScan T2 - Delete Scan Page", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rslt == DialogResult.Yes)
                {
                    ////delflag = true;
                    MODI.Image removeImage = (MODI.Image)this.midoc.Images[this.test];
                    this.midoc.Images.Remove(removeImage);
                    this.splitContainer1.Panel1.Controls.Clear();
                    this.splitContainer1.Panel1.Refresh();
                    int oldval = this.test;
                    this.RepopulateControls();

                    if (this.midoc.Images.Count > 0)
                    {
                        this.PageNoTextBox.Text = (oldval + 1) + " of" + " " + this.midoc.Images.Count;
                    }
                    else
                    {
                        this.PageNoTextBox.Text = string.Empty;
                    }

                    if (this.midoc.Images.Count == 1)
                    {
                        this.PreviousButton.Enabled = false;
                        this.NextButton.Enabled = false;
                    }
                    else
                    {
                        this.PreviousButton.Enabled = true;
                        this.NextButton.Enabled = true;
                    }
                    
                    this.test = oldval;
                }
                else
                {
                    ////do nothing 
                }
            }
        }

        /// <summary>
        /// Repopulates the controls.
        /// </summary>
        private void RepopulateControls()
        {
            if (this.midoc.Images.Count > 0)
            {
                this.count = 0;
                ////coding added for the issue 4933
                this.PicBoxes = new System.Windows.Forms.PictureBox[200];
                ////code end here
                for (this.count = 0; this.count < this.midoc.Images.Count; this.count++)
                {
                    MODI.Image modiImage;

                    this.PicBoxes[this.count] = new PictureBox();

                    this.PicBoxes[this.count].Width = 125;
                    this.PicBoxes[this.count].Height = 175;
                    if (this.count == 0)
                    {
                        this.PicBoxes[this.count].Top = (this.count + 2) * 30;
                    }
                    else if (this.count > 0)
                    {
                        this.PicBoxes[count].Top = this.PicBoxes[count - 1].Top + this.PicBoxes[count].Height + 20;
                    }

                    this.PicBoxes[count].Left = 20;
                    stdole.IPictureDisp pic;
                    modiImage = (MODI.Image)this.midoc.Images[this.count];
                    pic = modiImage.get_Thumbnail(MiTHUMBNAIL_SIZE.miTHUMB_SIZE_MEDIUM);
                    this.tstimage = PictureToImageConverter.Convert(pic);
                    this.PicBoxes[count].Image = this.tstimage;
                    this.PicBoxes[count].Tag = this.count;
                    this.PicBoxes[count].Click += new System.EventHandler(this.ClickHandler);
                    this.PicBoxes[count].BorderStyle = BorderStyle.FixedSingle;
                    this.PicBoxes[count].BackColor = Color.Gray;
                }

                this.splitContainer1.Panel1.Controls.AddRange(this.PicBoxes);
                ////this.CreatePopupMenu();
                this.splitContainer1.Panel1.AutoScroll = true;
                this.AxDocViewer.Document = this.midoc;
            }
            else
            {
                this.ControlStatus(false);
            }
        }

        #endregion

        #region ComboZoom

        /// <summary>
        /// Handles the DropDown event of the ZoomComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ZoomComboBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                this.ZoomComboBox.Items.Clear();
                this.Populate();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Populates this instance.
        /// </summary>
        private void Populate()
        {
            this.ZoomComboBox.Items.Add("Entire Page");
            this.ZoomComboBox.Items.Add("Text Width");
            this.ZoomComboBox.Items.Add("Page Width");
            this.ZoomComboBox.Items.Add("Page Height");
            this.ZoomComboBox.Items.Add("25 %");
            this.ZoomComboBox.Items.Add("50 %");
            this.ZoomComboBox.Items.Add("75 %");
            this.ZoomComboBox.Items.Add("100 %");
            this.ZoomComboBox.Items.Add("150 %");
            this.ZoomComboBox.Items.Add("200 %");
            this.ZoomComboBox.Items.Add("500 %");
        }

        /// <summary>
        /// Handles the KeyDown event of the ZoomComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ZoomComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int i = e.KeyValue;
                string zoomValue = this.ZoomComboBox.Text.Trim();
                if (i == 13 && !string.IsNullOrEmpty(zoomValue))
                {
                    ////if (test != "")
                    ////{
                    try
                    {
                        if ((Convert.ToDouble(zoomValue) / 100 < 0.15) || (Convert.ToDouble(zoomValue) / 100 > 10.0))
                        {
                            MessageBox.Show("The zoom factor should be a number between 15 and 1000", "Zoom Factor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (Convert.ToDouble(zoomValue) > 1000)
                            {
                                this.ZoomComboBox.Text = "";
                                this.ZoomComboBox.Text = "1000 %";
                                this.selecttext = true;
                            }
                            else if (Convert.ToDouble(zoomValue) < 15)
                            {
                                this.ZoomComboBox.Text = "";
                                this.ZoomComboBox.Text = "15 %";
                                this.selecttext = true;
                            }
                        }

                        if (this.selecttext == true)
                        {
                            double isx = 0;
                            double isy = 0;
                            isx = (Convert.ToDouble(zoomValue) / 100);
                            isy = (Convert.ToDouble(zoomValue) / 100);
                            this.AxDocViewer.SetScale(isx, isy);
                            this.selecttext = false;
                            this.ZoomComboBox.SelectAll();
                        }
                        else
                        {
                            double isx = 0;
                            double isy = 0;
                            isx = (Convert.ToDouble(zoomValue) / 100);
                            isy = (Convert.ToDouble(zoomValue) / 100);
                            this.AxDocViewer.SetScale(isx, isy);
                            this.ZoomComboBox.Text = "";
                            this.ZoomComboBox.Text = Convert.ToInt32(Convert.ToDouble(zoomValue)) + " %";
                            this.ZoomComboBox.SelectAll();
                            this.ZoomComboBox.Focus();
                        }
                        ////selecttext = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Invalid Input Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ZoomComboBox.Text = "";
                    }
                    ////}
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ZoomComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ZoomComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetValueCombo();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Sets the value combo.
        /// </summary>
        private void SetValueCombo()
        {
            try
            {
                string zoomval = this.ZoomComboBox.SelectedItem.ToString();

                if (zoomval == "Entire Page")
                {
                    this.AxDocViewer.FitMode = MiFITMODE.miFree;
                }
                else if (zoomval == "Text Width")
                {
                    this.AxDocViewer.FitMode = MiFITMODE.miByTextWidth;
                }
                else if (zoomval == "Page Width")
                {
                    this.AxDocViewer.FitMode = MiFITMODE.miByWidth;
                }
                else if (zoomval == "Page Height")
                {
                    this.AxDocViewer.FitMode = MiFITMODE.miByHeight;
                }
                else if (zoomval == "25 %")
                {
                    this.AxDocViewer.SetScale(0.25, 0.25);
                }
                else if (zoomval == "50 %")
                {
                    this.AxDocViewer.SetScale(0.5, 0.5);
                }
                else if (zoomval == "75 %")
                {
                    this.AxDocViewer.SetScale(0.75, 0.75);
                }
                else if (zoomval == "100 %")
                {
                    this.AxDocViewer.SetScale(1, 1);
                }
                else if (zoomval == "150 %")
                {
                    this.AxDocViewer.SetScale(1.5, 1.5);
                }
                else if (zoomval == "200 %")
                {
                    this.AxDocViewer.SetScale(2, 2);
                }
                else if (zoomval == "500 %")
                {
                    this.AxDocViewer.SetScale(5, 5);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        #endregion

        #region PictuerConvertClass

        /// <summary>
        /// Sealed Class Picture to Image Converter
        /// </summary>
        sealed private class PictureToImageConverter : System.Windows.Forms.AxHost
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="PictureToImageConverter"/> class.
            /// </summary>
            private PictureToImageConverter()
                : base(null)
            {
            }

            /// <summary>
            /// Converts the specified picture.
            /// </summary>
            /// <param name="picture">The picture.</param>
            /// <returns>Image</returns>
            public static System.Drawing.Image Convert(stdole.IPictureDisp picture)
            {
                return (System.Drawing.Image)System.Windows.Forms.AxHost.GetPictureFromIPicture(picture);
            }
        }

        #endregion
    }
}