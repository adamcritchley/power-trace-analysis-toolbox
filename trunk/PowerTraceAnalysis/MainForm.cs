using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;
using System.IO;

namespace FpgaTraceAnalysis
{
    public partial class MainForm : Form
    {
        private DsoReader binFile = null;

        private double sampleSize;
        private double windowMaxY;
        private double windowMinY;

        private float lineRed;
        private float lineGreen;
        private float lineBlue;

        private float bgRed;
        private float bgGreen;
        private float bgBlue;
        private bool mouseDown;

        private int upMarkerX;
        private int upMarkerY;
        private double upGraphX;
        private double upGraphY;
        private bool upMarkerValid;

        private int downMarkerX;
        private int downMarkerY;
        private double downGraphX;
        private double downGraphY;
        private bool downMarkerValid;

        private double windowMaxX;
        private double windowMinX;

        private string filePath;

        public MainForm()
        {
            InitializeComponent();

            this.filePath = "";
            this.binFile = null;

            windowMaxY = 0.025;
            windowMinY = -0.025;
        }

        public MainForm(string demeanedFile)
        {
            InitializeComponent();

            this.filePath = demeanedFile;
            this.binFile = new AgilentBinReader(this.filePath);

            setAxisY();

            this.Text = "Power Trace Analysis Toolbox [" + Path.GetFileName(this.filePath) + "]";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            windowMinX = 0.0;
            windowMaxX = 1.0;

            lineRed = 0.0f;
            lineGreen = 1.0f;
            lineBlue = 0.0f;

            bgRed = 0.0f;
            bgGreen = 0.0f;
            bgBlue = 0.0f;

            mouseDown = false;
            upMarkerValid = false;
            downMarkerValid = false;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            upMarkerValid = false;
            downMarkerValid = false;
        }

        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            SharpGL.OpenGL gl = openGLControl1.OpenGL;

            gl.ClearColor(bgRed, bgGreen, bgBlue, 1.0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.MatrixMode(SharpGL.Enumerations.MatrixMode.Projection);
            gl.Ortho2D(0, 1, windowMinY, windowMaxY);

            if (binFile != null)
            {
                drawTrace(gl);
            }

            drawMarkers(gl);

            gl.LoadIdentity();

            drawScaleY(gl);
            drawScaleX(gl);

            gl.Flush();
        }

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                int width = openGLControl1.Width;
                int height = openGLControl1.Height;

                upMarkerX = e.X;
                upMarkerY = e.Y;

                upMarkerX = e.X;
                upMarkerY = e.Y;
                upGraphX = (e.X / (double)width);
                upGraphY = (1 - (e.Y / (double)height)) * (windowMaxY - windowMinY) + windowMinY;

                upMarkerValid = true;
            }
        }

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mouseDown = true;

                int width = openGLControl1.Width;
                int height = openGLControl1.Height;

                downMarkerX = e.X;
                downMarkerY = e.Y;
                downGraphX = (e.X / (double)width);
                downGraphY = (1 - (e.Y / (double)height)) * (windowMaxY - windowMinY) + windowMinY;

                downMarkerValid = true;
                upMarkerValid = false;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                downMarkerValid = false;
                upMarkerValid = false;
            }
        }

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void setAxisY()
        {
            bool firstValue = true;

            double max = 0;
            double min = 0;

            foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
            {
                int d = 0;

                DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                {
                    int size = waveform.getCount(d);

                    for (int i = 0; i < size; i++)
                    {
                        double value = waveform.getVoltage(i, d);
                        if (!Double.IsNaN(value))
                        {
                            if (firstValue)
                            {
                                max = value;
                                min = value;
                                firstValue = false;
                            }
                            else
                            {
                                max = Math.Max(value, max);
                                min = Math.Min(value, min);
                            }
                        }
                    }

                    sampleSize = size;

                    d++;
                    wavet = waveform.getDatasetType(d);
                }
            }

            double border = Math.Abs(max - min) / 10;
            windowMaxY = max + border;
            windowMinY = min - border;
        }

        private void drawMarkers(OpenGL gl)
        {
            int fontSize = 10;
            int width = openGLControl1.Width;
            int height = openGLControl1.Height;

            if (height > 400)
            {
                fontSize = 14;
            }

            gl.LineWidth(2);

            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1 - bgRed, 1 - bgGreen, 1 - bgBlue);

            if (downMarkerValid)
            {
                gl.Vertex(0, downGraphY);
                gl.Vertex(1, downGraphY);
                gl.Vertex(downGraphX, windowMinY);
                gl.Vertex(downGraphX, windowMaxY);
            }

            if (upMarkerValid)
            {
                gl.Vertex(0, upGraphY);
                gl.Vertex(1, upGraphY);
                gl.Vertex(upGraphX, windowMinY);
                gl.Vertex(upGraphX, windowMaxY);
            }

            gl.End();

            gl.LoadIdentity();

            if (downMarkerValid)
            {
                int sample = (int)Math.Floor(sampleSize * (windowMaxX * downGraphX + windowMinX));
                string markerText = "(" + sample.ToString() + "," + downGraphY.ToString() + ")";
                gl.DrawText(downMarkerX + 2, (height - downMarkerY) + 2, 1 - bgRed, 1 - bgGreen, 1 - bgBlue, "Arial Black", fontSize, markerText);
            }

            if (upMarkerValid)
            {                
                int sample = (int)Math.Floor(sampleSize * (windowMaxX * upGraphX + windowMinX));
                string markerText = "(" + sample.ToString() + "," + upGraphY.ToString() + ")";
                gl.DrawText(upMarkerX + 2, (height - upMarkerY) + 2, 1 - bgRed, 1 - bgGreen, 1 - bgBlue, "Arial Black", fontSize, markerText);
            }
        }

        private void drawScaleX(OpenGL gl)
        {
            int fontSize = 10;
            int width = openGLControl1.Width;
            const double stride = 0.10;

            if (width > 400)
            {
                fontSize = 14;
            }

            if (binFile != null)
            {
                foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
                {
                    int d = 0;

                    DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                    while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                    {
                        int size = waveform.getCount(d);

                        for (double j = 0; j < 1.0; j += stride)
                        {
                            // Where are we on the graph?
                            double graphx = Math.Floor(size * (windowMaxX * j + windowMinX));
                            // Where are we on the window?
                            int winx = (int)(j * width);

                            // Invert our text color from the background color
                            string formattedText = ((int)graphx).ToString();
                            gl.DrawText(winx, 10 + d*5, 1 - bgRed, 1 - bgGreen, 1 - bgBlue, "Arial Black", fontSize, formattedText);
                        }

                        d++;
                        wavet = waveform.getDatasetType(d);
                    }
                }
            }
        }

        private void drawScaleY(OpenGL gl)
        {
            int fontSize = 10;
            int height = openGLControl1.Height;
            const double stride = 0.10;

            if (height > 400)
            {
                fontSize = 14;
            }

            for (double j = 0; j < 1.0; j += stride)
            {
                // Where are we on the graph?
                double graphy = j * (windowMaxY - windowMinY) + windowMinY;
                // Where are we on the window?
                int winy = (int)(j * height);

                // Invert our text color from the background color
                string ytext = String.Format("{0:N6}", graphy);
                gl.DrawText(5, winy, 1 - bgRed, 1 - bgGreen, 1 - bgBlue, "Arial Black", fontSize, ytext);
            }
        }

        private void drawTrace(OpenGL gl)
        {
            bool isDrawing = false;

            gl.LineWidth(1);

            gl.Color(lineRed, lineGreen, lineBlue);

            foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
            {
                int d = 0;

                DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                {
                    int size = waveform.getCount(d);
                    double drawSize = size * windowMaxX;
                    int drawStart = (int)(Math.Min(windowMinX * size, size - drawSize));
                    int drawEnd = (int)(Math.Max(Math.Min(drawStart + drawSize - 1, size), 0));

                    for (int i = drawStart; i <= drawEnd; i++)
                    {
                        double y = waveform.getVoltage(i, d);
                        double x = (i - drawStart) / (double)drawSize;

                        if (Double.IsNaN(y) && isDrawing)
                        {
                            gl.End();
                            isDrawing = false;
                        }
                        else if( !isDrawing )
                        {
                            isDrawing = true;
                            gl.Begin(OpenGL.GL_LINE_STRIP);
                            gl.Vertex(x, 0);
                        }

                        gl.Vertex(x, y);
                    }

                    d++;
                    wavet = waveform.getDatasetType(d);
                }
            }

            gl.End();
        }

        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            windowMinX = (hScrollBar2.Value / 100.0);
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            windowMaxX = (hScrollBar1.Value / 100.0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openTraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openTrace.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (Path.GetExtension(openTrace.SafeFileName).ToLower() == ".bin")
                {
                    binFile = new AgilentBinReader(openTrace.FileName);
                }
                else if (Path.GetExtension(openTrace.SafeFileName).ToLower() == ".buf")
                {
                    MiniDsoBufReader.dsoOptions opts = new MiniDsoBufReader.dsoOptions();

                    opts.loadTrack1 = true;
                    opts.sampleTime = 0.1;

                    binFile = new MiniDsoBufReader(openTrace.FileName, opts);
                }

                filePath = openTrace.FileName;

                setAxisY();

                this.Text = "Power Trace Analysis [" + openTrace.SafeFileName + "]";
            }
        }

        private void saveToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (binFile == null)
                return;

            saveCsvDialog.FileName = Path.ChangeExtension(filePath, "csv");

            if (saveCsvDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            StreamWriter outCsv = new StreamWriter(saveCsvDialog.FileName);

            outCsv.WriteLine("Trace, Sample, Voltage");

            foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
            {
                int d = 0;

                DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                {
                    int size = waveform.getCount(d);

                    for (int i = 0; i < size; i++)
                    {
                        double y = waveform.getVoltage(i, d);
                        outCsv.WriteLine(d.ToString() + "," + i.ToString() + "," + y.ToString());
                    }

                    d++;
                    wavet = waveform.getDatasetType(d);
                }

                outCsv.Close();
            }
        }

        private void saveViewToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (binFile == null)
                return;

            saveCsvDialog.FileName = Path.ChangeExtension(filePath, "csv");

            if (saveCsvDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            StreamWriter outCsv = new StreamWriter(saveCsvDialog.FileName);

            outCsv.WriteLine("Trace, Sample, Voltage");

            foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
            {
                int d = 0;

                DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                {
                    int size = waveform.getCount(d);
                    double drawSize = size * windowMaxX;
                    int drawStart = (int)(Math.Min(windowMinX * size, size - drawSize));
                    int drawEnd = (int)(Math.Max(Math.Min(drawStart + drawSize - 1, size), 0));

                    for (int i = drawStart; i <= drawEnd; i++)
                    {
                        double y = waveform.getVoltage(i, d);
                        outCsv.WriteLine(d.ToString() + "," + i.ToString() + "," + y.ToString());
                    }

                    d++;
                    wavet = waveform.getDatasetType(d);
                }

                outCsv.Close();
            }
        }

        private void highContrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lineRed = 0.0f;
            lineGreen = 1.0f;
            lineBlue = 0.0f;

            bgRed = 0.0f;
            bgGreen = 0.0f;
            bgBlue = 0.0f;
        }

        private void lowConstrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lineRed = 1.0f;
            lineGreen = 0.0f;
            lineBlue = 0.0f;

            bgRed = 1.0f;
            bgGreen = 1.0f;
            bgBlue = 1.0f;
        }

        private void globalMaxMinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int width = openGLControl1.Width;
            int height = openGLControl1.Height;

            int minX = 0;
            int maxX = 0;

            double minY = 0;
            double maxY = 0;

            int size = 0;

            foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
            {
                int d = 0;

                DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                {
                    size = waveform.getCount(d);

                    minX = maxX = 0;
                    maxY = minY = waveform.getVoltage(0, d);

                    for (int i = 1; i < size; i++)
                    {
                        double y = waveform.getVoltage(i, d);
                        if (y > maxY)
                        {
                            maxX = i;
                            maxY = y;
                        }
                        else if (y < minY)
                        {
                            minX = i;
                            minY = y;
                        }
                    }

                    d++;
                    wavet = waveform.getDatasetType(d);
                }
            }

            if (minX < maxX)
            {
                upMarkerX = (int)((maxX / (double)size) * width);
                upMarkerY = height - (int)(1 + (maxY - windowMinY) / (windowMaxY - windowMinY) * height);
                upGraphX = (maxX / (double)size);
                upGraphY = maxY;

                downMarkerX = (int)((minX / (double)size) * width);
                downMarkerY = height - (int)(1 + (minY - windowMinY) / (windowMaxY - windowMinY) * height);
                downGraphX = (minX / (double)size);
                downGraphY = minY;
            }
            else
            {
                downMarkerX = (int)((maxX / (double)size) * width);
                downMarkerY = height - (int)(1 + (maxY - windowMinY) / (windowMaxY - windowMinY) * height);
                downGraphX = (maxX / (double)size);
                downGraphY = maxY;

                upMarkerX = (int)((minX / (double)size) * width);
                upMarkerY = height - (int)(1 + (minY - windowMinY) / (windowMaxY - windowMinY) * height);
                upGraphX = (minX / (double)size);
                upGraphY = minY;
            }

            downMarkerValid = true;
            upMarkerValid = true;
        }

        private void demeanButton_Click(object sender, EventArgs e)
        {
            if (binFile == null)
                return;

            double mean = 0;

            if (demeanCurrentRange.Checked)
            {
                foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
                {
                    int d = 0;

                    DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                    while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                    {
                        int size = waveform.getCount(d);
                        double drawSize = size * windowMaxX;
                        int drawStart = (int)(Math.Min(windowMinX * size, size - drawSize));
                        int drawEnd = (int)(Math.Max(Math.Min(drawStart + drawSize - 1, size), 0));

                        double y = waveform.getVoltage(drawStart, d);
                        mean += y;

                        for (int i = drawStart; i < drawEnd; i++)
                        {
                            y = waveform.getVoltage(i, d);
                            mean += y;
                        }

                        y = waveform.getVoltage(drawEnd, d);
                        mean += y;

                        mean /= size;

                        d++;
                        wavet = waveform.getDatasetType(d);
                    }
                }
            }
            else if (demeanEntireRange.Checked)
            {
                foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
                {
                    int d = 0;

                    DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                    while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                    {
                        int size = waveform.getCount(d);

                        for (int i = 0; i < size; i++)
                        {
                            mean += waveform.getVoltage(i, d);
                        }

                        mean /= size;

                        d++;
                        wavet = waveform.getDatasetType(d);
                    }
                }
            }

            string demeanedFile = Path.ChangeExtension(filePath, "Demean.bin");
            AgilentBinWriter writer = new AgilentBinWriter(demeanedFile);

            foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
            {
                AgilentBinWriter.tPBWaveformNormal normalWaveform = new AgilentBinWriter.tPBWaveformNormal();
                int d = 0;

                DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                while (wavet == DsoReader.ePBDataType.PB_DATA_NORMAL)
                {
                    int size = waveform.getCount(d);
                    normalWaveform.setCount(size, d);

                    for (int i = 0; i < size; i++)
                    {
                        float demeanVoltage = (float)(waveform.getVoltage(i, d) - mean);
                        normalWaveform.setVoltage( demeanVoltage, i, d);
                    }

                    writer.writeWaveform(normalWaveform);

                    d++;
                    wavet = waveform.getDatasetType(d);
                }
            }

            writer.close();

           (new MainForm(demeanedFile)).Show();
        }

        private void integrateButton_Click(object sender, EventArgs e)
        {
            if (binFile == null)
                return;

            int integrationSize = 1;

            try
            {
                integrationSize = int.Parse(integrationIntervalText.Text);
            }
            catch (Exception exception)
            {
                if( MessageBox.Show("Invalid interval specified! Continue using 1?", 
                    "Error", 
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No )
                {
                    return;
                }
            }

            string integrationFile = Path.ChangeExtension(filePath, "Integration.bin");
            AgilentBinWriter writer = new AgilentBinWriter(integrationFile);

            if (integrateRaw.Checked)
            {
                foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
                {
                    AgilentBinWriter.tPBWaveformNormal normalWaveform = new AgilentBinWriter.tPBWaveformNormal();
                    int d = 0;
                    float sum = 0.0f;
                    int integrationCount = 0;
                    int integrationCurrent = 0;

                    DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                    while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                    {
                        int size = waveform.getCount(d);

                        // Round up and set the size of the integrated waveform.
                        normalWaveform.setCount((int)Math.Ceiling(size / (float)integrationSize), d);

                        // Integrate the waveform.
                        for (int i = 0; i < size; i++)
                        {
                            sum += (float)waveform.getVoltage(i, d);

                            integrationCurrent++;
                            if (integrationSize == integrationCurrent)
                            {
                                normalWaveform.setVoltage(sum, integrationCount, d);
                                integrationCount++;
                                integrationCurrent = 0;
                                sum = 0.0f;
                            }
                        }

                        // Do we have a partial integration left?
                        if (integrationCurrent != 0)
                        {
                            normalWaveform.setVoltage(sum, integrationCount, d);
                        }

                        writer.writeWaveform(normalWaveform);

                        d++;
                        wavet = waveform.getDatasetType(d);
                    }
                }
            }
            else if (integrateAbsolute.Checked)
            {
                foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
                {
                    AgilentBinWriter.tPBWaveformNormal normalWaveform = new AgilentBinWriter.tPBWaveformNormal();
                    int d = 0;
                    float sum = 0.0f;
                    int integrationCount = 0;
                    int integrationCurrent = 0;

                    DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                    while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                    {
                        int size = waveform.getCount(d);

                        // Round up and set the size of the integrated waveform.
                        normalWaveform.setCount((int)Math.Ceiling(size / (float)integrationSize), d);

                        // Integrate the waveform.
                        for (int i = 0; i < size; i++)
                        {
                            sum += (float)Math.Abs(waveform.getVoltage(i, d));

                            integrationCurrent++;
                            if (integrationSize == integrationCurrent)
                            {
                                normalWaveform.setVoltage(sum, integrationCount, d);
                                integrationCount++;
                                integrationCurrent = 0;
                                sum = 0.0f;
                            }
                        }

                        // Do we have a partial integration left?
                        if (integrationCurrent != 0)
                        {
                            normalWaveform.setVoltage(sum, integrationCount, d);
                        }

                        writer.writeWaveform(normalWaveform);

                        d++;
                        wavet = waveform.getDatasetType(d);
                    }
                }
            }
            else if (integrateSumOfSquares.Checked)
            {
                foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
                {
                    AgilentBinWriter.tPBWaveformNormal normalWaveform = new AgilentBinWriter.tPBWaveformNormal();
                    int d = 0;
                    float sum = 0.0f;
                    int integrationCount = 0;
                    int integrationCurrent = 0;

                    DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                    while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                    {
                        int size = waveform.getCount(d);

                        // Round up and set the size of the integrated waveform.
                        normalWaveform.setCount( (int)Math.Ceiling(size/(float)integrationSize), d);

                        // Integrate the waveform.
                        for (int i = 0; i < size; i++)
                        {
                            sum += (float)Math.Pow(waveform.getVoltage(i, d), 2);

                            integrationCurrent++;
                            if (integrationSize == integrationCurrent)
                            {
                                normalWaveform.setVoltage(sum, integrationCount, d);
                                integrationCount++;
                                integrationCurrent = 0;
                                sum = 0.0f;
                            }
                        }

                        // Do we have a partial integration left?
                        if (integrationCurrent != 0)
                        {
                            normalWaveform.setVoltage(sum, integrationCount, d);
                        }

                        writer.writeWaveform(normalWaveform);

                        d++;
                        wavet = waveform.getDatasetType(d);
                    }
                }
            }

            writer.close();

            (new MainForm(integrationFile)).Show();
        }

        private void snrButton_Click(object sender, EventArgs e)
        {
            if (snrOpenTraces.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (binFile == null)
                return;

            string snrFilename = Path.ChangeExtension(snrOpenTraces.SafeFileNames[0], "SNR.bin");
            AgilentBinWriter snrFile = new AgilentBinWriter(snrFilename);
            AgilentBinWriter.tPBWaveformNormal snrWave = new AgilentBinWriter.tPBWaveformNormal();

            DsoReader traceFile = null;

            bool useBin = false;
            bool useBuf = false;

            if (Path.GetExtension(snrOpenTraces.SafeFileNames[0]).ToLower() == ".bin")
            {
                useBin = true;
            }
            else if (Path.GetExtension(snrOpenTraces.SafeFileNames[0]).ToLower() == ".buf")
            {
                useBuf = true;
            }

            if (useBin)
            {
                traceFile = new AgilentBinReader(snrOpenTraces.FileNames[0]);
            } 
            else if (useBuf)
            {
                MiniDsoBufReader.dsoOptions opts = new MiniDsoBufReader.dsoOptions();

                opts.loadTrack1 = true;
                opts.sampleTime = 0.1;

                traceFile = new MiniDsoBufReader(snrOpenTraces.FileNames[0], opts);
            }

            // Prepare our SNR waveform by setting it size and values to zero
            int size = traceFile.waveforms[0].getCount(0);
            int d = 0;
            double[] mean_signal = new double[size];
            double[] trace_diff = new double[size];
            double[] trace_value = new double[size];
            for (int i = 0; i < size; i++)
            {
                mean_signal[i] = 0.0;
            }

            double mean_trace = 0.0;
            double mean_diff = 0.0;

            double numTraces = 0.0;
            snrWave.setCount(traceFile.waveforms[0].getCount(0), 0);

            traceFile = null;

            // Calculate the signal point statistics from our traces
            foreach (string filename in snrOpenTraces.FileNames)
            {
                if (useBin)
                {
                    traceFile = new AgilentBinReader(filename);
                }
                else if (useBuf)
                {
                    MiniDsoBufReader.dsoOptions opts = new MiniDsoBufReader.dsoOptions();

                    opts.loadTrack1 = true;
                    opts.sampleTime = 0.1;

                    traceFile = new MiniDsoBufReader(filename, opts);
                }

                d = 0;

                foreach (DsoReader.DsoWaveform waveform in traceFile.waveforms)
                {
                    DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                    while (wavet == DsoReader.ePBDataType.PB_DATA_NORMAL)
                    {
                        // Integrate the waveform.
                        for (int i = 0; i < size; i++)
                        {
                            mean_signal[i] += waveform.getVoltage(i, d);
                        }

                        d++;
                        wavet = waveform.getDatasetType(d);
                    }

                    numTraces++;
                }
            }

            for (int i = 0; i < size; i++)
            {
                mean_signal[i] /= numTraces;
            }

            // Calculate the signal point statistics from our loaded trace
            d = 0;

            foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
            {
                DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                while (wavet == DsoReader.ePBDataType.PB_DATA_NORMAL)
                {
                    // Integrate the waveform.
                    for (int i = 0; i < size; i++)
                    {
                        trace_value[i] = waveform.getVoltage(i, d);
                        mean_trace += waveform.getVoltage(i, d);
                        trace_diff[i] = waveform.getVoltage(i, d) - mean_signal[i];
                        mean_diff += trace_diff[i];
                    }

                    d++;
                    wavet = waveform.getDatasetType(d);
                }
            }

            mean_trace /= size;
            mean_diff /= size;

            double signal = 0.0;
            double noise = 0.0;
            float snrValue = 0.0f;

            for (int i = 0; i < size; i++)
            {
                signal = Math.Abs(trace_value[i] - mean_trace);
                noise = Math.Abs(trace_diff[i] - mean_diff);

                if (snrDB.Checked)
                {
                    snrValue = (float)(10 * Math.Log10(signal) - 10 * Math.Log10(noise));
                }
                else if (snrVoltage.Checked)
                {
                    snrValue = (float)(signal/noise);
                }

                snrWave.setVoltage(snrValue, i, 0);
            }

            snrFile.writeWaveform(snrWave);

            snrFile.close();

            (new MainForm(snrFilename)).Show();
        }

        private void clampButton_Click(object sender, EventArgs e)
        {
            if (binFile == null)
                return;

            double clampValue = 0;

            try
            {
                clampValue = double.Parse(clampValueText.Text);
            }
            catch (Exception exception)
            {
                if (MessageBox.Show("Invalid clamp value specified! Continue using 0?",
                    "Error",
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            string clampFilename = Path.ChangeExtension(this.filePath, "Clamp.bin");
            AgilentBinWriter clampFile = new AgilentBinWriter(clampFilename);
            AgilentBinWriter.tPBWaveformNormal clampWave = new AgilentBinWriter.tPBWaveformNormal();

            if (clampMaximum.Checked)
            {
                foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
                {
                    int d = 0;

                    DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                    while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                    {
                        int size = waveform.getCount(d);
                        clampWave.setCount(size, 0);

                        for (int i = 0; i < size; i++)
                        {
                            float value = (float)Math.Min(waveform.getVoltage(i, d), clampValue);
                            clampWave.setVoltage(value, i, 0);
                        }

                        d++;
                        wavet = waveform.getDatasetType(d);
                    }
                }
            }
            else if (clampMinimum.Checked)
            {
                foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
                {
                    int d = 0;

                    DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                    while (wavet != DsoReader.ePBDataType.PB_DATA_UNKNOWN)
                    {
                        int size = waveform.getCount(d);
                        clampWave.setCount(size, 0);

                        for (int i = 0; i < size; i++)
                        {
                            float value = (float)Math.Max(waveform.getVoltage(i, d), clampValue);
                            clampWave.setVoltage(value, i, 0);
                        }

                        d++;
                        wavet = waveform.getDatasetType(d);
                    }
                }
            }

            clampFile.writeWaveform(clampWave);

            clampFile.close();

            (new MainForm(clampFilename)).Show();
        }

        private void substituteButton_Click(object sender, EventArgs e)
        {
            if (binFile == null)
                return;

            double subValue = 0;

            try
            {
                subValue = double.Parse(subValueText.Text);
            }
            catch (Exception exception)
            {
                if (MessageBox.Show("Invalid substitution threshold specified! Continue using 0?",
                    "Error",
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            if (subOpenTrace.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            string subFilename = Path.ChangeExtension(subOpenTrace.SafeFileName, "Substitution.bin");
            AgilentBinWriter subFile = new AgilentBinWriter(subFilename);
            AgilentBinWriter.tPBWaveformNormal subWave = new AgilentBinWriter.tPBWaveformNormal();

            DsoReader traceFile = null;

            if (Path.GetExtension(subOpenTrace.SafeFileName).ToLower() == ".bin")
            {
                traceFile = new AgilentBinReader(subOpenTrace.FileName);
            }
            else if (Path.GetExtension(subOpenTrace.SafeFileName).ToLower() == ".buf")
            {
                MiniDsoBufReader.dsoOptions opts = new MiniDsoBufReader.dsoOptions();

                opts.loadTrack1 = true;
                opts.sampleTime = 0.1;

                traceFile = new MiniDsoBufReader(subOpenTrace.FileName, opts);
            }

            int d = 0;
            
            DsoReader.DsoWaveform refWaveform = traceFile.waveforms[0];

            foreach (DsoReader.DsoWaveform waveform in binFile.waveforms)
            {
                DsoReader.ePBDataType wavet = waveform.getDatasetType(d);

                while (wavet == DsoReader.ePBDataType.PB_DATA_NORMAL)
                {
                    int size = waveform.getCount(d);
                    subWave.setCount(size, d);

                    // Integrate the waveform.
                    for (int i = 0; i < size; i++)
                    {
                        double checkedVal = waveform.getVoltage(i, d);
                        double subVal = refWaveform.getVoltage(i, 0);

                        if (subGreater.Checked)
                        {
                            if (checkedVal > subValue)
                            {
                                subWave.setVoltage((float)subVal, i, d);
                            }
                            else
                            {
                                subWave.setVoltage(Single.NaN, i, d);
                            }
                        }
                        else if (subLess.Checked)
                        {
                            if (checkedVal < subValue)
                            {
                                subWave.setVoltage((float)subVal, i, d);
                            }
                            else
                            {
                                subWave.setVoltage(Single.NaN, i, d);
                            }
                        }
                    }

                    d++;
                    wavet = waveform.getDatasetType(d);
                }
            }

            subFile.writeWaveform(subWave);

            subFile.close();

            (new MainForm(subFilename)).Show();
        }

    }
}
