namespace FpgaTraceAnalysis
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.demeanGroupBox = new System.Windows.Forms.GroupBox();
            this.demeanButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.demeanEntireRange = new System.Windows.Forms.RadioButton();
            this.demeanCurrentRange = new System.Windows.Forms.RadioButton();
            this.integrationGroupBox = new System.Windows.Forms.GroupBox();
            this.integrationIntervalText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.integrateButton = new System.Windows.Forms.Button();
            this.integrateSumOfSquares = new System.Windows.Forms.RadioButton();
            this.integrateAbsolute = new System.Windows.Forms.RadioButton();
            this.integrateRaw = new System.Windows.Forms.RadioButton();
            this.snrGroupBox = new System.Windows.Forms.GroupBox();
            this.snrVoltage = new System.Windows.Forms.RadioButton();
            this.snrDB = new System.Windows.Forms.RadioButton();
            this.snrButton = new System.Windows.Forms.Button();
            this.clampGroupBox = new System.Windows.Forms.GroupBox();
            this.clampButton = new System.Windows.Forms.Button();
            this.clampMinimum = new System.Windows.Forms.RadioButton();
            this.clampMaximum = new System.Windows.Forms.RadioButton();
            this.clampValueText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.subGroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.subValueText = new System.Windows.Forms.TextBox();
            this.substituteButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.subLess = new System.Windows.Forms.RadioButton();
            this.subGreater = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highContrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lowConstrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalMaxMinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTrace = new System.Windows.Forms.OpenFileDialog();
            this.snrOpenTraces = new System.Windows.Forms.OpenFileDialog();
            this.subOpenTrace = new System.Windows.Forms.OpenFileDialog();
            this.saveToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveCsvDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveViewToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.demeanGroupBox.SuspendLayout();
            this.integrationGroupBox.SuspendLayout();
            this.snrGroupBox.SuspendLayout();
            this.clampGroupBox.SuspendLayout();
            this.subGroupBox.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.BitDepth = 24;
            this.tableLayoutPanel1.SetColumnSpan(this.openGLControl1, 2);
            this.openGLControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.FrameRate = 20;
            this.openGLControl1.Location = new System.Drawing.Point(3, 3);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.tableLayoutPanel1.SetRowSpan(this.openGLControl1, 2);
            this.openGLControl1.Size = new System.Drawing.Size(522, 534);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.openGLControl1_OpenGLDraw);
            this.openGLControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseDown);
            this.openGLControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseMove);
            this.openGLControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl1_MouseUp);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 236F));
            this.tableLayoutPanel1.Controls.Add(this.openGLControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.hScrollBar1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.hScrollBar2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(764, 561);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar1.Location = new System.Drawing.Point(264, 540);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(264, 21);
            this.hScrollBar1.TabIndex = 4;
            this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hScrollBar2.Location = new System.Drawing.Point(0, 540);
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(264, 21);
            this.hScrollBar2.TabIndex = 5;
            this.hScrollBar2.ValueChanged += new System.EventHandler(this.hScrollBar2_ValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.textBox1);
            this.flowLayoutPanel1.Controls.Add(this.demeanGroupBox);
            this.flowLayoutPanel1.Controls.Add(this.integrationGroupBox);
            this.flowLayoutPanel1.Controls.Add(this.snrGroupBox);
            this.flowLayoutPanel1.Controls.Add(this.clampGroupBox);
            this.flowLayoutPanel1.Controls.Add(this.subGroupBox);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(531, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.tableLayoutPanel1.SetRowSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(230, 555);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(159, 55);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Power Trace Analysis Toolbox\r\nCreated for CSE 8393\r\nAuthored by Adam Critchley";
            // 
            // demeanGroupBox
            // 
            this.demeanGroupBox.Controls.Add(this.demeanButton);
            this.demeanGroupBox.Controls.Add(this.label1);
            this.demeanGroupBox.Controls.Add(this.demeanEntireRange);
            this.demeanGroupBox.Controls.Add(this.demeanCurrentRange);
            this.demeanGroupBox.Location = new System.Drawing.Point(3, 64);
            this.demeanGroupBox.Name = "demeanGroupBox";
            this.demeanGroupBox.Size = new System.Drawing.Size(200, 116);
            this.demeanGroupBox.TabIndex = 4;
            this.demeanGroupBox.TabStop = false;
            this.demeanGroupBox.Text = "Demean";
            // 
            // demeanButton
            // 
            this.demeanButton.Location = new System.Drawing.Point(7, 87);
            this.demeanButton.Name = "demeanButton";
            this.demeanButton.Size = new System.Drawing.Size(75, 23);
            this.demeanButton.TabIndex = 3;
            this.demeanButton.Text = "Calculate";
            this.demeanButton.UseVisualStyleBackColor = true;
            this.demeanButton.Click += new System.EventHandler(this.demeanButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Calculate mean from...";
            // 
            // demeanEntireRange
            // 
            this.demeanEntireRange.AutoSize = true;
            this.demeanEntireRange.Checked = true;
            this.demeanEntireRange.Location = new System.Drawing.Point(7, 66);
            this.demeanEntireRange.Name = "demeanEntireRange";
            this.demeanEntireRange.Size = new System.Drawing.Size(87, 17);
            this.demeanEntireRange.TabIndex = 1;
            this.demeanEntireRange.TabStop = true;
            this.demeanEntireRange.Text = "Entire Range";
            this.demeanEntireRange.UseVisualStyleBackColor = true;
            // 
            // demeanCurrentRange
            // 
            this.demeanCurrentRange.AutoSize = true;
            this.demeanCurrentRange.Location = new System.Drawing.Point(7, 42);
            this.demeanCurrentRange.Name = "demeanCurrentRange";
            this.demeanCurrentRange.Size = new System.Drawing.Size(85, 17);
            this.demeanCurrentRange.TabIndex = 0;
            this.demeanCurrentRange.Text = "Current View";
            this.demeanCurrentRange.UseVisualStyleBackColor = true;
            // 
            // integrationGroupBox
            // 
            this.integrationGroupBox.Controls.Add(this.integrationIntervalText);
            this.integrationGroupBox.Controls.Add(this.label2);
            this.integrationGroupBox.Controls.Add(this.integrateButton);
            this.integrationGroupBox.Controls.Add(this.integrateSumOfSquares);
            this.integrationGroupBox.Controls.Add(this.integrateAbsolute);
            this.integrationGroupBox.Controls.Add(this.integrateRaw);
            this.integrationGroupBox.Location = new System.Drawing.Point(3, 186);
            this.integrationGroupBox.Name = "integrationGroupBox";
            this.integrationGroupBox.Size = new System.Drawing.Size(200, 141);
            this.integrationGroupBox.TabIndex = 5;
            this.integrationGroupBox.TabStop = false;
            this.integrationGroupBox.Text = "Integration";
            // 
            // integrationIntervalText
            // 
            this.integrationIntervalText.Location = new System.Drawing.Point(122, 18);
            this.integrationIntervalText.Name = "integrationIntervalText";
            this.integrationIntervalText.Size = new System.Drawing.Size(70, 20);
            this.integrationIntervalText.TabIndex = 5;
            this.integrationIntervalText.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Interval for integration:";
            // 
            // integrateButton
            // 
            this.integrateButton.Location = new System.Drawing.Point(6, 112);
            this.integrateButton.Name = "integrateButton";
            this.integrateButton.Size = new System.Drawing.Size(75, 23);
            this.integrateButton.TabIndex = 3;
            this.integrateButton.Text = "Calculate";
            this.integrateButton.UseVisualStyleBackColor = true;
            this.integrateButton.Click += new System.EventHandler(this.integrateButton_Click);
            // 
            // integrateSumOfSquares
            // 
            this.integrateSumOfSquares.AutoSize = true;
            this.integrateSumOfSquares.Checked = true;
            this.integrateSumOfSquares.Location = new System.Drawing.Point(6, 88);
            this.integrateSumOfSquares.Name = "integrateSumOfSquares";
            this.integrateSumOfSquares.Size = new System.Drawing.Size(100, 17);
            this.integrateSumOfSquares.TabIndex = 2;
            this.integrateSumOfSquares.TabStop = true;
            this.integrateSumOfSquares.Text = "Sum of Squares";
            this.integrateSumOfSquares.UseVisualStyleBackColor = true;
            // 
            // integrateAbsolute
            // 
            this.integrateAbsolute.AutoSize = true;
            this.integrateAbsolute.Location = new System.Drawing.Point(6, 64);
            this.integrateAbsolute.Name = "integrateAbsolute";
            this.integrateAbsolute.Size = new System.Drawing.Size(66, 17);
            this.integrateAbsolute.TabIndex = 1;
            this.integrateAbsolute.Text = "Absolute";
            this.integrateAbsolute.UseVisualStyleBackColor = true;
            // 
            // integrateRaw
            // 
            this.integrateRaw.AutoSize = true;
            this.integrateRaw.Location = new System.Drawing.Point(6, 40);
            this.integrateRaw.Name = "integrateRaw";
            this.integrateRaw.Size = new System.Drawing.Size(47, 17);
            this.integrateRaw.TabIndex = 0;
            this.integrateRaw.Text = "Raw";
            this.integrateRaw.UseVisualStyleBackColor = true;
            // 
            // snrGroupBox
            // 
            this.snrGroupBox.Controls.Add(this.snrVoltage);
            this.snrGroupBox.Controls.Add(this.snrDB);
            this.snrGroupBox.Controls.Add(this.snrButton);
            this.snrGroupBox.Location = new System.Drawing.Point(3, 333);
            this.snrGroupBox.Name = "snrGroupBox";
            this.snrGroupBox.Size = new System.Drawing.Size(200, 97);
            this.snrGroupBox.TabIndex = 6;
            this.snrGroupBox.TabStop = false;
            this.snrGroupBox.Text = "SNR";
            // 
            // snrVoltage
            // 
            this.snrVoltage.AutoSize = true;
            this.snrVoltage.Checked = true;
            this.snrVoltage.Location = new System.Drawing.Point(6, 44);
            this.snrVoltage.Name = "snrVoltage";
            this.snrVoltage.Size = new System.Drawing.Size(61, 17);
            this.snrVoltage.TabIndex = 2;
            this.snrVoltage.TabStop = true;
            this.snrVoltage.Text = "Voltage";
            this.snrVoltage.UseVisualStyleBackColor = true;
            // 
            // snrDB
            // 
            this.snrDB.AutoSize = true;
            this.snrDB.Location = new System.Drawing.Point(6, 20);
            this.snrDB.Name = "snrDB";
            this.snrDB.Size = new System.Drawing.Size(38, 17);
            this.snrDB.TabIndex = 1;
            this.snrDB.Text = "dB";
            this.snrDB.UseVisualStyleBackColor = true;
            // 
            // snrButton
            // 
            this.snrButton.Location = new System.Drawing.Point(6, 67);
            this.snrButton.Name = "snrButton";
            this.snrButton.Size = new System.Drawing.Size(75, 23);
            this.snrButton.TabIndex = 0;
            this.snrButton.Text = "Calculate";
            this.snrButton.UseVisualStyleBackColor = true;
            this.snrButton.Click += new System.EventHandler(this.snrButton_Click);
            // 
            // clampGroupBox
            // 
            this.clampGroupBox.Controls.Add(this.clampButton);
            this.clampGroupBox.Controls.Add(this.clampMinimum);
            this.clampGroupBox.Controls.Add(this.clampMaximum);
            this.clampGroupBox.Controls.Add(this.clampValueText);
            this.clampGroupBox.Controls.Add(this.label3);
            this.clampGroupBox.Location = new System.Drawing.Point(3, 436);
            this.clampGroupBox.Name = "clampGroupBox";
            this.clampGroupBox.Size = new System.Drawing.Size(200, 110);
            this.clampGroupBox.TabIndex = 7;
            this.clampGroupBox.TabStop = false;
            this.clampGroupBox.Text = "Clamp";
            // 
            // clampButton
            // 
            this.clampButton.Location = new System.Drawing.Point(6, 83);
            this.clampButton.Name = "clampButton";
            this.clampButton.Size = new System.Drawing.Size(75, 23);
            this.clampButton.TabIndex = 4;
            this.clampButton.Text = "Calculate";
            this.clampButton.UseVisualStyleBackColor = true;
            this.clampButton.Click += new System.EventHandler(this.clampButton_Click);
            // 
            // clampMinimum
            // 
            this.clampMinimum.AutoSize = true;
            this.clampMinimum.Checked = true;
            this.clampMinimum.Location = new System.Drawing.Point(6, 60);
            this.clampMinimum.Name = "clampMinimum";
            this.clampMinimum.Size = new System.Drawing.Size(66, 17);
            this.clampMinimum.TabIndex = 3;
            this.clampMinimum.TabStop = true;
            this.clampMinimum.Text = "Minimum";
            this.clampMinimum.UseVisualStyleBackColor = true;
            // 
            // clampMaximum
            // 
            this.clampMaximum.AutoSize = true;
            this.clampMaximum.Location = new System.Drawing.Point(6, 37);
            this.clampMaximum.Name = "clampMaximum";
            this.clampMaximum.Size = new System.Drawing.Size(69, 17);
            this.clampMaximum.TabIndex = 2;
            this.clampMaximum.Text = "Maximum";
            this.clampMaximum.UseVisualStyleBackColor = true;
            // 
            // clampValueText
            // 
            this.clampValueText.Location = new System.Drawing.Point(104, 17);
            this.clampValueText.Name = "clampValueText";
            this.clampValueText.Size = new System.Drawing.Size(88, 20);
            this.clampValueText.TabIndex = 1;
            this.clampValueText.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Value to clamp to:";
            // 
            // subGroupBox
            // 
            this.subGroupBox.Controls.Add(this.label6);
            this.subGroupBox.Controls.Add(this.subValueText);
            this.subGroupBox.Controls.Add(this.substituteButton);
            this.subGroupBox.Controls.Add(this.label5);
            this.subGroupBox.Controls.Add(this.subLess);
            this.subGroupBox.Controls.Add(this.subGreater);
            this.subGroupBox.Controls.Add(this.label4);
            this.subGroupBox.Location = new System.Drawing.Point(3, 552);
            this.subGroupBox.Name = "subGroupBox";
            this.subGroupBox.Size = new System.Drawing.Size(200, 149);
            this.subGroupBox.TabIndex = 8;
            this.subGroupBox.TabStop = false;
            this.subGroupBox.Text = "Substitution";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "the value";
            // 
            // subValueText
            // 
            this.subValueText.Location = new System.Drawing.Point(63, 82);
            this.subValueText.Name = "subValueText";
            this.subValueText.Size = new System.Drawing.Size(100, 20);
            this.subValueText.TabIndex = 5;
            this.subValueText.Text = "0";
            // 
            // substituteButton
            // 
            this.substituteButton.Location = new System.Drawing.Point(6, 121);
            this.substituteButton.Name = "substituteButton";
            this.substituteButton.Size = new System.Drawing.Size(75, 23);
            this.substituteButton.TabIndex = 4;
            this.substituteButton.Text = "Calculate";
            this.substituteButton.UseVisualStyleBackColor = true;
            this.substituteButton.Click += new System.EventHandler(this.substituteButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "and others substitute NaN.";
            // 
            // subLess
            // 
            this.subLess.AutoSize = true;
            this.subLess.Checked = true;
            this.subLess.Location = new System.Drawing.Point(6, 59);
            this.subLess.Name = "subLess";
            this.subLess.Size = new System.Drawing.Size(75, 17);
            this.subLess.TabIndex = 2;
            this.subLess.TabStop = true;
            this.subLess.Text = "Less Than";
            this.subLess.UseVisualStyleBackColor = true;
            // 
            // subGreater
            // 
            this.subGreater.AutoSize = true;
            this.subGreater.Location = new System.Drawing.Point(7, 36);
            this.subGreater.Name = "subGreater";
            this.subGreater.Size = new System.Drawing.Size(88, 17);
            this.subGreater.TabIndex = 1;
            this.subGreater.Text = "Greater Than";
            this.subGreater.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Substitute points...";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.colorsToolStripMenuItem,
            this.calculateToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(764, 24);
            this.menuMain.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTraceToolStripMenuItem,
            this.saveToCSVToolStripMenuItem,
            this.saveViewToCSVToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openTraceToolStripMenuItem
            // 
            this.openTraceToolStripMenuItem.Name = "openTraceToolStripMenuItem";
            this.openTraceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openTraceToolStripMenuItem.Text = "Open Trace";
            this.openTraceToolStripMenuItem.Click += new System.EventHandler(this.openTraceToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // colorsToolStripMenuItem
            // 
            this.colorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.highContrastToolStripMenuItem,
            this.lowConstrastToolStripMenuItem});
            this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            this.colorsToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.colorsToolStripMenuItem.Text = "Graph";
            // 
            // highContrastToolStripMenuItem
            // 
            this.highContrastToolStripMenuItem.Name = "highContrastToolStripMenuItem";
            this.highContrastToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.highContrastToolStripMenuItem.Text = "High Contrast";
            this.highContrastToolStripMenuItem.Click += new System.EventHandler(this.highContrastToolStripMenuItem_Click);
            // 
            // lowConstrastToolStripMenuItem
            // 
            this.lowConstrastToolStripMenuItem.Name = "lowConstrastToolStripMenuItem";
            this.lowConstrastToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lowConstrastToolStripMenuItem.Text = "Low Constrast";
            this.lowConstrastToolStripMenuItem.Click += new System.EventHandler(this.lowConstrastToolStripMenuItem_Click);
            // 
            // calculateToolStripMenuItem
            // 
            this.calculateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.globalMaxMinToolStripMenuItem});
            this.calculateToolStripMenuItem.Name = "calculateToolStripMenuItem";
            this.calculateToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.calculateToolStripMenuItem.Text = "Calculate";
            // 
            // globalMaxMinToolStripMenuItem
            // 
            this.globalMaxMinToolStripMenuItem.Name = "globalMaxMinToolStripMenuItem";
            this.globalMaxMinToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.globalMaxMinToolStripMenuItem.Text = "Global Max/Min";
            this.globalMaxMinToolStripMenuItem.Click += new System.EventHandler(this.globalMaxMinToolStripMenuItem_Click);
            // 
            // openTrace
            // 
            this.openTrace.Filter = "Agilent Trace|*.bin|Mini DSO Buffer|*.buf";
            this.openTrace.Title = "Open a trace...";
            // 
            // snrOpenTraces
            // 
            this.snrOpenTraces.Filter = "Agilent Trace|*.bin|Mini DSO Buffer|*.buf";
            this.snrOpenTraces.Multiselect = true;
            this.snrOpenTraces.Title = "Select traces for reference signal...";
            // 
            // subOpenTrace
            // 
            this.subOpenTrace.Title = "Select the trace that provides substitution values...";
            // 
            // saveToCSVToolStripMenuItem
            // 
            this.saveToCSVToolStripMenuItem.Name = "saveToCSVToolStripMenuItem";
            this.saveToCSVToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToCSVToolStripMenuItem.Text = "Save to CSV";
            this.saveToCSVToolStripMenuItem.Click += new System.EventHandler(this.saveToCSVToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // saveCsvDialog
            // 
            this.saveCsvDialog.DefaultExt = "*.csv";
            this.saveCsvDialog.Filter = "Comma Seperated Value|*.csv";
            this.saveCsvDialog.Title = "Save to CSV";
            // 
            // saveViewToCSVToolStripMenuItem
            // 
            this.saveViewToCSVToolStripMenuItem.Name = "saveViewToCSVToolStripMenuItem";
            this.saveViewToCSVToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.saveViewToCSVToolStripMenuItem.Text = "Save View to CSV";
            this.saveViewToCSVToolStripMenuItem.Click += new System.EventHandler(this.saveViewToCSVToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 585);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "MainForm";
            this.Text = "Power Trace Analysis Toolbox";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.demeanGroupBox.ResumeLayout(false);
            this.demeanGroupBox.PerformLayout();
            this.integrationGroupBox.ResumeLayout(false);
            this.integrationGroupBox.PerformLayout();
            this.snrGroupBox.ResumeLayout(false);
            this.snrGroupBox.PerformLayout();
            this.clampGroupBox.ResumeLayout(false);
            this.clampGroupBox.PerformLayout();
            this.subGroupBox.ResumeLayout(false);
            this.subGroupBox.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highContrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lowConstrastToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem openTraceToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openTrace;
        private System.Windows.Forms.ToolStripMenuItem calculateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalMaxMinToolStripMenuItem;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox demeanGroupBox;
        private System.Windows.Forms.RadioButton demeanEntireRange;
        private System.Windows.Forms.RadioButton demeanCurrentRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button demeanButton;
        private System.Windows.Forms.GroupBox integrationGroupBox;
        private System.Windows.Forms.RadioButton integrateRaw;
        private System.Windows.Forms.RadioButton integrateSumOfSquares;
        private System.Windows.Forms.RadioButton integrateAbsolute;
        private System.Windows.Forms.Button integrateButton;
        private System.Windows.Forms.TextBox integrationIntervalText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog snrOpenTraces;
        private System.Windows.Forms.GroupBox snrGroupBox;
        private System.Windows.Forms.Button snrButton;
        private System.Windows.Forms.RadioButton snrDB;
        private System.Windows.Forms.RadioButton snrVoltage;
        private System.Windows.Forms.GroupBox clampGroupBox;
        private System.Windows.Forms.TextBox clampValueText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button clampButton;
        private System.Windows.Forms.RadioButton clampMinimum;
        private System.Windows.Forms.RadioButton clampMaximum;
        private System.Windows.Forms.GroupBox subGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton subLess;
        private System.Windows.Forms.RadioButton subGreater;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button substituteButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox subValueText;
        private System.Windows.Forms.OpenFileDialog subOpenTrace;
        private System.Windows.Forms.ToolStripMenuItem saveToCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SaveFileDialog saveCsvDialog;
        private System.Windows.Forms.ToolStripMenuItem saveViewToCSVToolStripMenuItem;

    }
}

