using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TerraScan.FieldSummary
{
    public partial class PhotoPanel : UserControl
    {
        /// <summary>
        /// File path of the Attachment
        /// </summary>
        private string filePath;

        /// <summary>
        /// Event Date 
        /// </summary>
        private string eventDate;

        /// <summary>
        /// Function Name
        /// </summary>
        private string functionName;

        /// <summary>
        /// Description
        /// </summary>
        private string description;

        private DataTable photoDetail;
        
        
        #region properities

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable PhotoDetail
        {
            get
            {
                return this.photoDetail;
            }
            set
            {
                this.photoDetail = value;
            }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string EventDate
        {
            get
            {
                return this.eventDate;
            }
            set
            {
                this.eventDate = value;
            }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string FunctionName
        {
            get
            {
                return this.functionName;
            }
            set
            {
                this.functionName = value;
            }
        }
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        #endregion properities

        public PhotoPanel()
        {
            InitializeComponent();
        }

        private void PhotoPanel_Load(object sender, EventArgs e)
        {
            string file=this.filePath;
            if (this.eventDate != null)
            {
                this.EventDatelabel.Text = this.eventDate;
            }
            if (this.functionName != null)
            {
                this.Functionlabel.Text = this.functionName;
            }
            if (this.description != null)
            {
                this.Descriptionlabel.Text = this.description;
            }
            if (!string.IsNullOrEmpty(file))
            {
                if (System.IO.File.Exists(file))
                {
                    //byte[] data = File.ReadAllBytes(file); 
                    //MemoryStream ms = new MemoryStream();
                    //this.Imagepanel.BackgroundImage = Image.FromStream(ms,true,true); 
                    try
                    {
                        FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                        Byte[] Img = new Byte[stream.Length];
                        stream.Read(Img, 0, Convert.ToInt32(stream.Length));
                        this.Imagepanel.BackgroundImage = Image.FromStream(stream);
                        stream.Close();
                        stream.Dispose();  
                    }
                    catch(Exception ex)
                    {
                        this.Imagepanel.BackgroundImage = global::TerraScan.FieldSummary.Properties.Resources.preview_unavailable; 
                    }
 
                    //this.Imagepanel.BackgroundImage = Image.FromFile(file);
                }
                else
                {
                    this.Imagepanel.BackgroundImage = global::TerraScan.FieldSummary.Properties.Resources.preview_unavailable; 
                }
            }
            else
            {
                this.Imagepanel.BackgroundImage = global::TerraScan.FieldSummary.Properties.Resources.Noattachement;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
