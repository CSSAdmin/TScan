namespace D8054
{
    partial class F8054
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F8054));
            this.PointEventToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PointEventPanel = new System.Windows.Forms.Panel();
            this.CommentPanel = new System.Windows.Forms.Panel();
            this.CommentTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.CommentLabel = new System.Windows.Forms.Label();
            this.StartPointPanel = new System.Windows.Forms.Panel();
            this.StartLabelTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.StartPonitLabel = new System.Windows.Forms.Label();
            this.SummaryPanel = new System.Windows.Forms.Panel();
            this.RightEndLabel = new System.Windows.Forms.Label();
            this.LeftEndLabel = new System.Windows.Forms.Label();
            this.StartComLabel = new System.Windows.Forms.Label();
            this.StartDistLabel = new System.Windows.Forms.Label();
            this.PointPictureBox = new System.Windows.Forms.PictureBox();
            this.SummaryLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Line1Panel = new System.Windows.Forms.Panel();
            this.DistrictInfoSecIndicatorPictureBox = new System.Windows.Forms.PictureBox();
            this.OffsetPanel = new System.Windows.Forms.Panel();
            this.OffsetTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.OffsetLabel = new System.Windows.Forms.Label();
            this.StartPanel = new System.Windows.Forms.Panel();
            this.StartTextBox = new TerraScan.UI.Controls.TerraScanTextBox();
            this.StartLabel = new System.Windows.Forms.Label();
            this.PointMenuStrip = new System.Windows.Forms.MenuStrip();
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PointEventPanel.SuspendLayout();
            this.CommentPanel.SuspendLayout();
            this.StartPointPanel.SuspendLayout();
            this.SummaryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PointPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistrictInfoSecIndicatorPictureBox)).BeginInit();
            this.OffsetPanel.SuspendLayout();
            this.StartPanel.SuspendLayout();
            this.PointMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PointEventPanel
            // 
            this.PointEventPanel.Controls.Add(this.CommentPanel);
            this.PointEventPanel.Controls.Add(this.StartPointPanel);
            this.PointEventPanel.Controls.Add(this.SummaryPanel);
            this.PointEventPanel.Controls.Add(this.DistrictInfoSecIndicatorPictureBox);
            this.PointEventPanel.Controls.Add(this.OffsetPanel);
            this.PointEventPanel.Controls.Add(this.StartPanel);
            this.PointEventPanel.Location = new System.Drawing.Point(0, 0);
            this.PointEventPanel.Name = "PointEventPanel";
            this.PointEventPanel.Size = new System.Drawing.Size(801, 260);
            this.PointEventPanel.TabIndex = 0;
            // 
            // CommentPanel
            // 
            this.CommentPanel.BackColor = System.Drawing.Color.Transparent;
            this.CommentPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CommentPanel.Controls.Add(this.CommentTextBox);
            this.CommentPanel.Controls.Add(this.CommentLabel);
            this.CommentPanel.Location = new System.Drawing.Point(0, 182);
            this.CommentPanel.Name = "CommentPanel";
            this.CommentPanel.Size = new System.Drawing.Size(768, 78);
            this.CommentPanel.TabIndex = 19;
            // 
            // CommentTextBox
            // 
            this.CommentTextBox.AllowClick = true;
            this.CommentTextBox.AllowNegativeSign = false;
            this.CommentTextBox.ApplyCFGFormat = false;
            this.CommentTextBox.ApplyCurrencyFormat = false;
            this.CommentTextBox.ApplyFocusColor = true;
            this.CommentTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.CommentTextBox.ApplyNegativeStandard = true;
            this.CommentTextBox.ApplyParentFocusColor = true;
            this.CommentTextBox.ApplyTimeFormat = false;
            this.CommentTextBox.BackColor = System.Drawing.Color.White;
            this.CommentTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CommentTextBox.CFromatWihoutSymbol = false;
            this.CommentTextBox.CheckForEmpty = false;
            this.CommentTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CommentTextBox.Digits = -1;
            this.CommentTextBox.EmptyDecimalValue = false;
            this.CommentTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CommentTextBox.IsEditable = false;
            this.CommentTextBox.IsQueryableFileld = false;
            this.CommentTextBox.Location = new System.Drawing.Point(13, 18);
            this.CommentTextBox.LockKeyPress = false;
            this.CommentTextBox.MaxLength = 200;
            this.CommentTextBox.Multiline = true;
            this.CommentTextBox.Name = "CommentTextBox";
            this.CommentTextBox.PersistDefaultColor = false;
            this.CommentTextBox.Precision = 2;
            this.CommentTextBox.QueryingFileldName = "";
            this.CommentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CommentTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.CommentTextBox.Size = new System.Drawing.Size(752, 57);
            this.CommentTextBox.SpecialCharacter = "%";
            this.CommentTextBox.TabIndex = 4;
            this.CommentTextBox.TextCustomFormat = "$#,##0.00";
            this.CommentTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.CommentTextBox.WholeInteger = false;
            this.CommentTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartTextBox_KeyPress);
            this.CommentTextBox.TextChanged += new System.EventHandler(this.StartLabelTextBox_TextChanged);
            this.CommentTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartTextBox_KeyDown);
            // 
            // CommentLabel
            // 
            this.CommentLabel.AutoSize = true;
            this.CommentLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.CommentLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.CommentLabel.Location = new System.Drawing.Point(1, 1);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(65, 14);
            this.CommentLabel.TabIndex = 4;
            this.CommentLabel.Text = "Comment:";
            this.CommentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StartPointPanel
            // 
            this.StartPointPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartPointPanel.Controls.Add(this.StartLabelTextBox);
            this.StartPointPanel.Controls.Add(this.StartPonitLabel);
            this.StartPointPanel.Location = new System.Drawing.Point(268, 149);
            this.StartPointPanel.Name = "StartPointPanel";
            this.StartPointPanel.Size = new System.Drawing.Size(500, 34);
            this.StartPointPanel.TabIndex = 18;
            // 
            // StartLabelTextBox
            // 
            this.StartLabelTextBox.AllowClick = true;
            this.StartLabelTextBox.AllowNegativeSign = false;
            this.StartLabelTextBox.ApplyCFGFormat = false;
            this.StartLabelTextBox.ApplyCurrencyFormat = false;
            this.StartLabelTextBox.ApplyFocusColor = true;
            this.StartLabelTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.StartLabelTextBox.ApplyNegativeStandard = true;
            this.StartLabelTextBox.ApplyParentFocusColor = true;
            this.StartLabelTextBox.ApplyTimeFormat = false;
            this.StartLabelTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.StartLabelTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StartLabelTextBox.CFromatWihoutSymbol = false;
            this.StartLabelTextBox.CheckForEmpty = false;
            this.StartLabelTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StartLabelTextBox.Digits = -1;
            this.StartLabelTextBox.EmptyDecimalValue = false;
            this.StartLabelTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.StartLabelTextBox.ForeColor = System.Drawing.Color.Black;
            this.StartLabelTextBox.IsEditable = true;
            this.StartLabelTextBox.IsQueryableFileld = true;
            this.StartLabelTextBox.Location = new System.Drawing.Point(44, 15);
            this.StartLabelTextBox.LockKeyPress = false;
            this.StartLabelTextBox.MaxLength = 20;
            this.StartLabelTextBox.Name = "StartLabelTextBox";
            this.StartLabelTextBox.PersistDefaultColor = false;
            this.StartLabelTextBox.Precision = 2;
            this.StartLabelTextBox.QueryingFileldName = "";
            this.StartLabelTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.StartLabelTextBox.Size = new System.Drawing.Size(157, 16);
            this.StartLabelTextBox.SpecialCharacter = "%";
            this.StartLabelTextBox.TabIndex = 3;
            this.StartLabelTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.StartLabelTextBox.TextCustomFormat = "0.0";
            this.StartLabelTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Text;
            this.StartLabelTextBox.WholeInteger = false;
            this.StartLabelTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartTextBox_KeyPress);
            this.StartLabelTextBox.TextChanged += new System.EventHandler(this.StartLabelTextBox_TextChanged);
            this.StartLabelTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartTextBox_KeyDown);
            // 
            // StartPonitLabel
            // 
            this.StartPonitLabel.AutoSize = true;
            this.StartPonitLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.StartPonitLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StartPonitLabel.Location = new System.Drawing.Point(1, 2);
            this.StartPonitLabel.Name = "StartPonitLabel";
            this.StartPonitLabel.Size = new System.Drawing.Size(66, 14);
            this.StartPonitLabel.TabIndex = 3;
            this.StartPonitLabel.Text = "StartLabel:";
            this.StartPonitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SummaryPanel
            // 
            this.SummaryPanel.BackColor = System.Drawing.Color.White;
            this.SummaryPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SummaryPanel.Controls.Add(this.RightEndLabel);
            this.SummaryPanel.Controls.Add(this.LeftEndLabel);
            this.SummaryPanel.Controls.Add(this.StartComLabel);
            this.SummaryPanel.Controls.Add(this.StartDistLabel);
            this.SummaryPanel.Controls.Add(this.PointPictureBox);
            this.SummaryPanel.Controls.Add(this.SummaryLabel);
            this.SummaryPanel.Controls.Add(this.panel3);
            this.SummaryPanel.Controls.Add(this.panel2);
            this.SummaryPanel.Controls.Add(this.Line1Panel);
            this.SummaryPanel.Location = new System.Drawing.Point(0, 0);
            this.SummaryPanel.Name = "SummaryPanel";
            this.SummaryPanel.Size = new System.Drawing.Size(768, 150);
            this.SummaryPanel.TabIndex = 12;
            // 
            // RightEndLabel
            // 
            this.RightEndLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RightEndLabel.ForeColor = System.Drawing.Color.Black;
            this.RightEndLabel.Location = new System.Drawing.Point(648, 79);
            this.RightEndLabel.Name = "RightEndLabel";
            this.RightEndLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RightEndLabel.Size = new System.Drawing.Size(95, 15);
            this.RightEndLabel.TabIndex = 34;
            this.RightEndLabel.Text = "0";
            this.RightEndLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RightEndLabel.MouseEnter += new System.EventHandler(this.RightEndLabel_MouseEnter);
            // 
            // LeftEndLabel
            // 
            this.LeftEndLabel.AutoSize = true;
            this.LeftEndLabel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LeftEndLabel.ForeColor = System.Drawing.Color.Black;
            this.LeftEndLabel.Location = new System.Drawing.Point(17, 79);
            this.LeftEndLabel.Name = "LeftEndLabel";
            this.LeftEndLabel.Size = new System.Drawing.Size(14, 15);
            this.LeftEndLabel.TabIndex = 33;
            this.LeftEndLabel.Text = "0";
            this.LeftEndLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StartComLabel
            // 
            this.StartComLabel.AutoSize = true;
            this.StartComLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartComLabel.ForeColor = System.Drawing.Color.Black;
            this.StartComLabel.Location = new System.Drawing.Point(195, 125);
            this.StartComLabel.Name = "StartComLabel";
            this.StartComLabel.Size = new System.Drawing.Size(0, 16);
            this.StartComLabel.TabIndex = 32;
            this.StartComLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.StartComLabel.MouseEnter += new System.EventHandler(this.StartComLabel_MouseEnter);
            // 
            // StartDistLabel
            // 
            this.StartDistLabel.AutoSize = true;
            this.StartDistLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartDistLabel.ForeColor = System.Drawing.Color.Black;
            this.StartDistLabel.Location = new System.Drawing.Point(195, 107);
            this.StartDistLabel.Name = "StartDistLabel";
            this.StartDistLabel.Size = new System.Drawing.Size(0, 16);
            this.StartDistLabel.TabIndex = 31;
            this.StartDistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.StartDistLabel.MouseEnter += new System.EventHandler(this.StartDistLabel_MouseEnter);
            // 
            // PointPictureBox
            // 
            this.PointPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("PointPictureBox.Image")));
            this.PointPictureBox.Location = new System.Drawing.Point(192, 88);
            this.PointPictureBox.Name = "PointPictureBox";
            this.PointPictureBox.Size = new System.Drawing.Size(14, 14);
            this.PointPictureBox.TabIndex = 30;
            this.PointPictureBox.TabStop = false;
            // 
            // SummaryLabel
            // 
            this.SummaryLabel.AutoSize = true;
            this.SummaryLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.SummaryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.SummaryLabel.Location = new System.Drawing.Point(3, 3);
            this.SummaryLabel.Name = "SummaryLabel";
            this.SummaryLabel.Size = new System.Drawing.Size(63, 14);
            this.SummaryLabel.TabIndex = 18;
            this.SummaryLabel.Text = "Summary:";
            this.SummaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Location = new System.Drawing.Point(736, 66);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 10);
            this.panel3.TabIndex = 17;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(18, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 10);
            this.panel2.TabIndex = 16;
            // 
            // Line1Panel
            // 
            this.Line1Panel.BackColor = System.Drawing.Color.Gray;
            this.Line1Panel.Location = new System.Drawing.Point(18, 69);
            this.Line1Panel.Name = "Line1Panel";
            this.Line1Panel.Size = new System.Drawing.Size(718, 4);
            this.Line1Panel.TabIndex = 15;
            // 
            // DistrictInfoSecIndicatorPictureBox
            // 
            this.DistrictInfoSecIndicatorPictureBox.Location = new System.Drawing.Point(761, 0);
            this.DistrictInfoSecIndicatorPictureBox.Name = "DistrictInfoSecIndicatorPictureBox";
            this.DistrictInfoSecIndicatorPictureBox.Size = new System.Drawing.Size(42, 260);
            this.DistrictInfoSecIndicatorPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DistrictInfoSecIndicatorPictureBox.TabIndex = 29;
            this.DistrictInfoSecIndicatorPictureBox.TabStop = false;
            this.DistrictInfoSecIndicatorPictureBox.Click += new System.EventHandler(this.DistrictInfoSecIndicatorPictureBox_Click);
            this.DistrictInfoSecIndicatorPictureBox.MouseEnter += new System.EventHandler(this.DistrictInfoSecIndicatorPictureBox_MouseEnter);
            // 
            // OffsetPanel
            // 
            this.OffsetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OffsetPanel.Controls.Add(this.OffsetTextBox);
            this.OffsetPanel.Controls.Add(this.OffsetLabel);
            this.OffsetPanel.Location = new System.Drawing.Point(128, 149);
            this.OffsetPanel.Name = "OffsetPanel";
            this.OffsetPanel.Size = new System.Drawing.Size(141, 34);
            this.OffsetPanel.TabIndex = 17;
            // 
            // OffsetTextBox
            // 
            this.OffsetTextBox.AllowClick = true;
            this.OffsetTextBox.AllowNegativeSign = false;
            this.OffsetTextBox.ApplyCFGFormat = false;
            this.OffsetTextBox.ApplyCurrencyFormat = false;
            this.OffsetTextBox.ApplyFocusColor = true;
            this.OffsetTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.OffsetTextBox.ApplyNegativeStandard = true;
            this.OffsetTextBox.ApplyParentFocusColor = true;
            this.OffsetTextBox.ApplyTimeFormat = false;
            this.OffsetTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.OffsetTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OffsetTextBox.CFromatWihoutSymbol = false;
            this.OffsetTextBox.CheckForEmpty = false;
            this.OffsetTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.OffsetTextBox.Digits = -1;
            this.OffsetTextBox.EmptyDecimalValue = false;
            this.OffsetTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.OffsetTextBox.ForeColor = System.Drawing.Color.Black;
            this.OffsetTextBox.IsEditable = true;
            this.OffsetTextBox.IsQueryableFileld = true;
            this.OffsetTextBox.Location = new System.Drawing.Point(41, 15);
            this.OffsetTextBox.LockKeyPress = false;
            this.OffsetTextBox.MaxLength = 10;
            this.OffsetTextBox.Name = "OffsetTextBox";
            this.OffsetTextBox.PersistDefaultColor = false;
            this.OffsetTextBox.Precision = 2;
            this.OffsetTextBox.QueryingFileldName = "";
            this.OffsetTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.OffsetTextBox.Size = new System.Drawing.Size(92, 16);
            this.OffsetTextBox.SpecialCharacter = "%";
            this.OffsetTextBox.TabIndex = 2;
            this.OffsetTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.OffsetTextBox.TextCustomFormat = "0.0";
            this.OffsetTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.OffsetTextBox.WholeInteger = false;
            this.OffsetTextBox.Leave += new System.EventHandler(this.OffsetTextBox_Leave);
            this.OffsetTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OffsetTextBox_KeyPress);
            this.OffsetTextBox.TextChanged += new System.EventHandler(this.StartLabelTextBox_TextChanged);
            this.OffsetTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OffsetTextBox_KeyDown);
            // 
            // OffsetLabel
            // 
            this.OffsetLabel.AutoSize = true;
            this.OffsetLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.OffsetLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.OffsetLabel.Location = new System.Drawing.Point(1, 2);
            this.OffsetLabel.Name = "OffsetLabel";
            this.OffsetLabel.Size = new System.Drawing.Size(44, 14);
            this.OffsetLabel.TabIndex = 1;
            this.OffsetLabel.Text = "Offset:";
            this.OffsetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StartPanel
            // 
            this.StartPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartPanel.Controls.Add(this.StartTextBox);
            this.StartPanel.Controls.Add(this.StartLabel);
            this.StartPanel.Location = new System.Drawing.Point(0, 149);
            this.StartPanel.Name = "StartPanel";
            this.StartPanel.Size = new System.Drawing.Size(129, 34);
            this.StartPanel.TabIndex = 16;
            // 
            // StartTextBox
            // 
            this.StartTextBox.AllowClick = true;
            this.StartTextBox.AllowNegativeSign = false;
            this.StartTextBox.ApplyCFGFormat = false;
            this.StartTextBox.ApplyCurrencyFormat = false;
            this.StartTextBox.ApplyFocusColor = true;
            this.StartTextBox.ApplyNegativeForeColor = System.Drawing.Color.Empty;
            this.StartTextBox.ApplyNegativeStandard = true;
            this.StartTextBox.ApplyParentFocusColor = true;
            this.StartTextBox.ApplyTimeFormat = false;
            this.StartTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.StartTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StartTextBox.CFromatWihoutSymbol = false;
            this.StartTextBox.CheckForEmpty = false;
            this.StartTextBox.DecimalTextBoxValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.StartTextBox.Digits = -1;
            this.StartTextBox.EmptyDecimalValue = false;
            this.StartTextBox.Font = new System.Drawing.Font("Arial", 9.85F, System.Drawing.FontStyle.Bold);
            this.StartTextBox.ForeColor = System.Drawing.Color.Black;
            this.StartTextBox.IsEditable = true;
            this.StartTextBox.IsQueryableFileld = true;
            this.StartTextBox.Location = new System.Drawing.Point(29, 15);
            this.StartTextBox.LockKeyPress = false;
            this.StartTextBox.MaxLength = 9;
            this.StartTextBox.Name = "StartTextBox";
            this.StartTextBox.PersistDefaultColor = false;
            this.StartTextBox.Precision = 2;
            this.StartTextBox.QueryingFileldName = "";
            this.StartTextBox.SetFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(121)))));
            this.StartTextBox.Size = new System.Drawing.Size(92, 16);
            this.StartTextBox.SpecialCharacter = "%";
            this.StartTextBox.TabIndex = 1;
            this.StartTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.StartTextBox.TextCustomFormat = "0.0";
            this.StartTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Decimal;
            this.StartTextBox.WholeInteger = false;
            this.StartTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartTextBox_KeyPress);
            this.StartTextBox.TextChanged += new System.EventHandler(this.StartLabelTextBox_TextChanged);
            this.StartTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StartTextBox_KeyDown);
            // 
            // StartLabel
            // 
            this.StartLabel.AutoSize = true;
            this.StartLabel.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.StartLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.StartLabel.Location = new System.Drawing.Point(1, 2);
            this.StartLabel.Name = "StartLabel";
            this.StartLabel.Size = new System.Drawing.Size(36, 14);
            this.StartLabel.TabIndex = 2;
            this.StartLabel.Text = "Start:";
            this.StartLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PointMenuStrip
            // 
            this.PointMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditMenu});
            this.PointMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.PointMenuStrip.Name = "PointMenuStrip";
            this.PointMenuStrip.Size = new System.Drawing.Size(804, 24);
            this.PointMenuStrip.TabIndex = 175;
            this.PointMenuStrip.Text = "menuStrip1";
            this.PointMenuStrip.Visible = false;
            // 
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(37, 20);
            this.EditMenu.Text = "Edit";
            // 
            // undoMenuItem
            // 
            this.undoMenuItem.Name = "undoMenuItem";
            this.undoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoMenuItem.Size = new System.Drawing.Size(148, 22);
            this.undoMenuItem.Text = "Undo";
            this.undoMenuItem.Click += new System.EventHandler(this.UndoMenuItem_Click);
            // 
            // F8054
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PointMenuStrip);
            this.Controls.Add(this.PointEventPanel);
            this.Name = "F8054";
            this.ParentFormId = 8054;
            this.Size = new System.Drawing.Size(804, 260);
            this.Tag = "8054";
            this.Load += new System.EventHandler(this.F8054_Load);
            this.PointEventPanel.ResumeLayout(false);
            this.CommentPanel.ResumeLayout(false);
            this.CommentPanel.PerformLayout();
            this.StartPointPanel.ResumeLayout(false);
            this.StartPointPanel.PerformLayout();
            this.SummaryPanel.ResumeLayout(false);
            this.SummaryPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PointPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DistrictInfoSecIndicatorPictureBox)).EndInit();
            this.OffsetPanel.ResumeLayout(false);
            this.OffsetPanel.PerformLayout();
            this.StartPanel.ResumeLayout(false);
            this.StartPanel.PerformLayout();
            this.PointMenuStrip.ResumeLayout(false);
            this.PointMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip PointEventToolTip;
        private System.Windows.Forms.Panel PointEventPanel;
        private System.Windows.Forms.Panel SummaryPanel;
        private System.Windows.Forms.Panel StartPointPanel;
        private System.Windows.Forms.Label StartPonitLabel;
        private System.Windows.Forms.Panel OffsetPanel;
        private System.Windows.Forms.Label OffsetLabel;
        private System.Windows.Forms.Panel StartPanel;
        private System.Windows.Forms.Label StartLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel Line1Panel;
        private System.Windows.Forms.Label SummaryLabel;
        private System.Windows.Forms.PictureBox PointPictureBox;
        private System.Windows.Forms.Label StartDistLabel;
        private System.Windows.Forms.Label StartComLabel;
        private System.Windows.Forms.Label LeftEndLabel;
        private System.Windows.Forms.Panel CommentPanel;
        private System.Windows.Forms.Label CommentLabel;
        private System.Windows.Forms.Label RightEndLabel;
        private System.Windows.Forms.PictureBox DistrictInfoSecIndicatorPictureBox;
        private TerraScan.UI.Controls.TerraScanTextBox StartTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox StartLabelTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox CommentTextBox;
        private TerraScan.UI.Controls.TerraScanTextBox OffsetTextBox;
        private System.Windows.Forms.MenuStrip PointMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem EditMenu;
        private System.Windows.Forms.ToolStripMenuItem undoMenuItem;
    }
}
