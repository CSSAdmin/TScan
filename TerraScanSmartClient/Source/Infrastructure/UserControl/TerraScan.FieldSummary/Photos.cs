using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TerraScan.FieldSummary;
using TerraScan.Common;


namespace TerraScan.FieldSummary
{
    public partial class Photos : UserControl
    {
        private DataTable photosDataSet;

        private DataTable fileDataSet;

        #region properities
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable PhotosDataSet
        {
            get
            {
                return this.photosDataSet;
            }
            set
            {
                this.photosDataSet = value;
            }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable FilesDataSet
        {
            get
            {
                return this.fileDataSet;
            }
            set
            {
                this.fileDataSet = value;
            }
        }
        #endregion properities


        public Photos()
        {
            InitializeComponent();
        }

        private void PopulatePhotosPanel()
        {
            try
            {
                this.panel1.Controls.Clear();
                int Xaxis = 0;
                if (this.fileDataSet.Rows.Count >= 3)
                {
                    if (this.fileDataSet.Rows.Count.Equals(this.photosDataSet.Rows.Count))
                    {
                        int scrollwidth = (this.fileDataSet.Rows.Count * 256) - (this.fileDataSet.Rows.Count * 3);

                        if (this.fileDataSet.Rows.Count <= 1500)
                        {
                            this.HScrollBar.Visible = false;
                            for (int i = 0; i < this.fileDataSet.Rows.Count; i++)
                            { //if (i < 3)
                                //{
                                PhotoPanel photo = new PhotoPanel();
                                string file = this.fileDataSet.Rows[i][0].ToString();
                                photo.FilePath = file;
                                string dateValue = this.photosDataSet.Rows[i][0].ToString();
                                DateTime dateTimeValue;
                                if (DateTime.TryParse(dateValue, out dateTimeValue))
                                {
                                    photo.EventDate = dateTimeValue.ToShortDateString();
                                }
                                photo.FunctionName = this.photosDataSet.Rows[i][1].ToString();
                                photo.Description = this.photosDataSet.Rows[i][2].ToString();
                                this.panel1.Controls.Add(photo);
                                photo.Location = new System.Drawing.Point(Xaxis - 1, 0);
                                Xaxis = Xaxis + 256 - 3;
                            }
                            //this.HScrollBar.Visible = false;
                            if (this.fileDataSet.Rows.Count <= 3)
                            {
                                this.HScrollBar.Enabled = false;
                                this.HScrollBar.Visible = true;  
                            }

                        }
                        else
                        {
                            MessageBox.Show("The user can view only 1500 photos.");
                            this.HScrollBar.Visible = false;
                            for (int i = 0; i < 1500; i++)
                            { //if (i < 3)
                                //{
                                PhotoPanel photo = new PhotoPanel();
                                string file = this.fileDataSet.Rows[i][0].ToString();
                                photo.FilePath = file;
                                string dateValue = this.photosDataSet.Rows[i][0].ToString();
                                DateTime dateTimeValue;
                                if (DateTime.TryParse(dateValue, out dateTimeValue))
                                {
                                    photo.EventDate = dateTimeValue.ToShortDateString();
                                }
                                photo.FunctionName = this.photosDataSet.Rows[i][1].ToString();
                                photo.Description = this.photosDataSet.Rows[i][2].ToString();
                                this.panel1.Controls.Add(photo);
                                photo.Location = new System.Drawing.Point(Xaxis - 1, 0);
                                Xaxis = Xaxis + 256 - 3;
                            }


                        }



                    }

                }
                else if (this.fileDataSet.Rows.Count > 0 && this.fileDataSet.Rows.Count < 3)
                {
                    if (this.fileDataSet.Rows.Count.Equals(this.photosDataSet.Rows.Count))
                    {
                        int count = this.fileDataSet.Rows.Count;
                        for (int i = 0; i < this.fileDataSet.Rows.Count; i++)
                        {
                            PhotoPanel photo = new PhotoPanel();
                            photo.FilePath = this.fileDataSet.Rows[i][0].ToString();
                            string dateValue = this.photosDataSet.Rows[i][0].ToString();
                            DateTime dateTimeValue;
                            if (DateTime.TryParse(dateValue, out dateTimeValue))
                            {
                                photo.EventDate = dateTimeValue.ToShortDateString();
                            }
                            photo.FunctionName = this.photosDataSet.Rows[i][1].ToString();
                            photo.Description = this.photosDataSet.Rows[i][2].ToString();
                            this.panel1.Controls.Add(photo);
                            photo.Location = new System.Drawing.Point(Xaxis - 1, 0);
                            Xaxis = Xaxis + 256 - 3;
                            this.HScrollBar.Enabled = false;
                        }

                        for (int i = 0; i < 3 - count; i++)
                        {
                            PhotoPanel photo1 = new PhotoPanel();
                            photo1.FilePath = string.Empty;
                            photo1.EventDate = string.Empty;
                            photo1.FunctionName = string.Empty;
                            photo1.Description = string.Empty;
                            this.panel1.Controls.Add(photo1);
                            photo1.Location = new System.Drawing.Point(Xaxis - 1, 0);
                            Xaxis = Xaxis + 256 - 3;
                            this.HScrollBar.Enabled = false;
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        PhotoPanel photo = new PhotoPanel();
                        photo.FilePath = string.Empty;
                        photo.EventDate = string.Empty;
                        photo.FunctionName = string.Empty;
                        photo.Description = string.Empty;
                        this.panel1.Controls.Add(photo);
                        photo.Location = new System.Drawing.Point(Xaxis - 1, 0);
                        Xaxis = Xaxis + 256 - 3;
                        this.HScrollBar.Enabled = false;
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            //photo.ImagePath =
            //getPhotos.FilesDataSet = this.FileDataTable;
            //this.UserControlPanel.Controls.Add(getPhotos);
            //this.F25090Panel.Controls.Clear();
            //this.F25090Panel.Controls.Add(this.UserControlPanel);
            //this.F25090Panel.Controls.Add(this.BottomPanel);
            //this.F25090PictureBox.SendToBack();
            //getPhotos.Location = new System.Drawing.Point(0, 0);
        }
        private void Photos_Load(object sender, EventArgs e)
        {
            if (this.fileDataSet != null)
            {
                this.PopulatePhotosPanel();
                
            }
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {

            if (e.NewValue == 1)
            {
                //PhotoPanel photo = new PhotoPanel();
                //photo.FilePath = this.fileDataSet.Rows[3][0].ToString();
                //photo.EventDate = this.photosDataSet.Rows[3][0].ToString();
                //photo.FunctionName = this.photosDataSet.Rows[3][1].ToString();
                //photo.Description = this.photosDataSet.Rows[3][2].ToString();
                //this.panel1.Controls.Add(photo);
                //photo.Location = new System.Drawing.Point(Xaxis - 1, 0);
                //Xaxis = Xaxis + 256 - 3;
            }
        }

        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //if (this.fileDataSet.Rows.Count > e.NewValue)
            //{
            //    if (e.NewValue > 0)
            //    {
            //        this.panel1.Controls.RemoveAt(e.OldValue);
            //        int j = e.NewValue;
            //        PhotoPanel photo = new PhotoPanel();
            //        photo.FilePath = this.fileDataSet.Rows[this.panel1.Controls.Count][0].ToString();
            //        photo.EventDate = this.photosDataSet.Rows[this.panel1.Controls.Count][0].ToString();
            //        photo.FunctionName = this.photosDataSet.Rows[this.panel1.Controls.Count][1].ToString();
            //        photo.Description = this.photosDataSet.Rows[this.panel1.Controls.Count][2].ToString();
            //        this.panel1.Controls.Add(photo);


            //    }
            //}
        }
    }
}
