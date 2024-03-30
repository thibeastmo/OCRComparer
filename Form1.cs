using OCRComparer.Execution;
using OCRComparer.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OCRComparer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SetHandForControls(this);
            Constants.ApplicationStartTime = DateTime.Now;
        }

        #region Design

        private void SetHandForControls(Control parentControl)
        {
            if (parentControl.Controls.Count == 0) return;
            foreach (Control control in parentControl.Controls)
            {
                if (control.GetType() == typeof(Button) || control.GetType() == typeof(CheckBox))
                {
                    control.Cursor = Cursors.Hand;
                }
                else if (control.Controls.Count > 0)
                {
                    SetHandForControls(control);
                }
            }
        }

        #endregion

        private void pbOriginalPreview_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            // Show the dialog and check if the user selected a file
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Set the selected image to the PictureBox
                pbOriginalPreview.Image = new Bitmap(ofd.FileName);
                pbOriginalPreview.ImageLocation = ofd.FileName;
            }
        }

        private void rtbGT_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(ofd.FileName))
                {
                    rtbGT.Text = reader.ReadToEnd();
                }
            }
        }

        private async void btnTestAllOCRs_Click(object sender, EventArgs e)
        {
            string imageLocation = pbOriginalPreview.ImageLocation;
            var image = pbOriginalPreview.Image;
            string gt = rtbGT.Text;
            await Task.Run(async () =>
            {
                UpdateProgressBar(1);
                for (int i = 0; i < clbAllOCRs.CheckedItems.Count; i++)
                {
                    var item = clbAllOCRs.CheckedItems[i];
                    string checkedItemText = item.ToString().Replace(" ", "_");
                    await ExecutionHandler.Handle(checkedItemText,image,imageLocation,gt);
                    int progress = (int)(((i + 1) / (double)clbAllOCRs.CheckedItems.Count) * 100);
                    UpdateProgressBar(progress);
                }
            });
        }

        private void UpdateProgressBar(int value)
        {
            if (progressBarImagePreprocessing.InvokeRequired)
            {
                progressBarImagePreprocessing.Invoke(new Action<int>(UpdateProgressBar), value);
            }
            else
            {
                progressBarImagePreprocessing.Value = value;
            }
        }


        #region Page behaviour

        private async void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.TabPages[tcMain.SelectedIndex].Name.ToLower().Contains("statistic"))
            {
                await LoadStatisticsPage();
            }
        }

        #region Statistics
        private async Task LoadStatisticsPage()
        {
            await Task.Run(() =>
            {
                this.Invoke(new Action(() => { Cursor = Cursors.WaitCursor; }));
                
                tpStatistics.Invoke(new Action(() => { tpStatistics.Controls.Clear(); }));
                string[] directories = Directory.GetDirectories(LocationUtil.GetFullPath(Constants.TestImagesPath));

                Dictionary<string, List<Point>> pointsPerOCR = StatisticsHandler.GetPointsForOCRs();
                this.Invoke(new Action(() => { Cursor = Cursors.Default; }));

                const int chartWidth = 500;
                const int chartHeight = 400;
                const int margin = -15;
                int lastX = 0;
                int lastY = 0;

                foreach (string directory in directories)
                {
                    string dirName = Path.GetFileName(directory);

                    Chart chart = new Chart();
                    chart.Size = new Size(chartWidth, chartHeight);
                    chart.BorderlineWidth = 2;
                    chart.BorderlineColor = Color.Gray;

                    // Set the chart title
                    Title title = new Title(dirName.Replace("_", " "));
                    title.Font = new Font(title.Font.FontFamily, 24);
                    chart.Titles.Add(title);

                    // Create a new chart area
                    ChartArea chartArea = new ChartArea("MainChartArea");

                    // Set the X-axis and Y-axis titles
                    chartArea.AxisX.Title = "Character error rate";
                    chartArea.AxisX.TitleFont = new Font(chartArea.AxisX.TitleFont.FontFamily, 16); // font size of the title.
                    chartArea.AxisY.Title = "Ground truth length";
                    chartArea.AxisY.TitleFont = new Font(chartArea.AxisY.TitleFont.FontFamily, 16); // font size of the title
                    // Handle Format event to adjust font size of axis interval values
                    chartArea.AxisX.LabelStyle.Font = new Font(chartArea.AxisX.TitleFont.FontFamily, 20);
                    chartArea.AxisY.LabelStyle.Font = new Font(chartArea.AxisY.TitleFont.FontFamily, 20);

                    // Create a new Series with Point chart type
                    Series series = new Series("DataSeries");
                    series.ChartType = SeriesChartType.Point;
                    series.MarkerStyle = MarkerStyle.Circle;
                    series.MarkerSize = 10; // Adjust the size as needed
                                            // Set border color and width for each dot
                    series.MarkerBorderColor = Color.White;
                    series.MarkerBorderWidth = 1; // Adjust the width as needed

                    Series lineSeries = new Series("RedLineSeries");
                    lineSeries.Color = Color.Orange;
                    lineSeries.ChartType = SeriesChartType.Line;
                    lineSeries.MarkerStyle = MarkerStyle.None;
                    lineSeries.BorderWidth = 4; // Adjust the width as needed
                    Series newSeries = new Series("NewDataSeries");
                    newSeries.ChartType = SeriesChartType.Point;
                    newSeries.MarkerStyle = MarkerStyle.Circle;
                    newSeries.MarkerSize = 10; // Adjust the size as needed
                                               // Set border color and width for each dot
                    newSeries.MarkerBorderColor = Color.White;
                    newSeries.MarkerBorderWidth = 1; // Adjust the width as needed
                    newSeries.Color = Color.MediumVioletRed;

                    chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
                    chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
                    chartArea.AxisX.MinorGrid.LineColor = Color.LightGray;
                    chartArea.AxisY.MinorGrid.LineColor = Color.LightGray;

                    // Set specific axis ranges
                    chartArea.AxisX.Minimum = 0;
                    chartArea.AxisX.Maximum = 100;
                    chartArea.AxisY.Minimum = 0;
                    chartArea.AxisY.Maximum = 3000;

                    chartArea.AxisX.Interval = 10;
                    chartArea.AxisY.Interval = 300;


                    // Add some sample data (replace this with your actual data)
                    KeyValuePair<string, List<Point>> pointsForOCR = pointsPerOCR.SingleOrDefault(p => p.Key == dirName);
                    var orderedPointsForOCR = pointsForOCR.Value.OrderBy(p => p.X).Reverse();
                    if (pointsForOCR.Value != null)
                    {
                        int counter = 0;
                        for (int i = 0; i < orderedPointsForOCR.Count(); i++)
                        {
                            Point point = orderedPointsForOCR.ElementAt(i);
                            if (point.X > 100)
                            {
                                point.X = (int)chartArea.AxisX.Maximum;
                            }
                            if (point.Y < 0)
                            {
                                newSeries.Points.AddXY(point.X, Math.Abs(point.Y));
                            }
                            else
                            {
                                series.Points.AddXY(point.X, point.Y);
                            }

                            if (counter == orderedPointsForOCR.Count() / 2)
                            {
                                lineSeries.Points.AddXY(point.X, 0);
                                lineSeries.Points.AddXY(point.X, chartArea.AxisY.Maximum);
                            }
                            counter++;
                        }
                    }

                    // Add the series to the chart
                    chart.Series.Add(lineSeries);
                    chart.Series.Add(series);
                    chart.Series.Add(newSeries);

                    // Add the chart area to the chart
                    chart.ChartAreas.Add(chartArea);

                    // Add the chart to the form
                    tpStatistics.Invoke(new Action(() =>
                    {
                        tpStatistics.Controls.Add(chart);
                        //calculate position
                        lastX += margin;
                        if (lastX + chartWidth > tpStatistics.Width)
                        {
                            lastX = margin;
                            lastY += chartHeight + margin;
                        }

                        chart.Location = new Point(lastX, lastY);
                        lastX += chartWidth;
                    }));
                }
            });
        }



        #endregion

        private async void MainForm_Resize(object sender, EventArgs e)
        {
            if (tcMain.TabPages[tcMain.SelectedIndex].Name.ToLower().Contains("statistic"))
            {
                await LoadStatisticsPage();
            }
        }

        #endregion
    }
}