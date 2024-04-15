using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SimpleImageToMP4
{
	public partial class Form1 : Form
	{

		private BackgroundWorker worker = new BackgroundWorker();
		private bool _startedWithArguments = false;

		public Form1()
		{
			InitializeComponent();
			comboBox1.SelectedIndex = 0; // Assuming this is for resolution selection
			cmbFPS.SelectedIndex = 0; // Assuming this is for frame rate selection
			chkWhatsapp.Checked = true; // Checkbox to optimize video for WhatsApp
										// Initialize and configure the ProgressBar
			progressBar1.Minimum = 0;
			progressBar1.Maximum = 100; // Or another maximum value depending on your logic

			worker.DoWork += worker_DoWork;
			worker.ProgressChanged += worker_ProgressChanged;
			worker.RunWorkerCompleted += worker_RunWorkerCompleted;
			worker.WorkerReportsProgress = true;
			statusLabel.Text = "Press Convert";
			// Handle command-line arguments
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1)
			{
				_startedWithArguments = true;  // Set the flag since we have an argument
				string filePath = args[1];  // The file path is passed as the first argument
											// Optionally set the path in a text box or directly start the conversion
				txtSelectedDirectory.Text = filePath;
				StartConversion(filePath);
			}
		}

		private void StartConversion(string filePath)
		{
			// Your conversion logic here
			btnStartConvert_Click(null, EventArgs.Empty);  // Simulate button click to start conversion
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_startedWithArguments)
			{
				// Prevent any "Are you sure you want to exit?" prompts if the app was started with arguments
				e.Cancel = false;
			}
			else
			{
				// Optionally, handle normal close prompts
			}
		}

		private void worker_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;

			var data = (Tuple<string, string, int, string, int, double>)e.Argument;
			string ffmpegInputPattern = data.Item1;
			string outputPath = data.Item2;
			double totalDuration = data.Item6; // This needs to be total video duration in seconds

			string ffmpegCommand = $"-framerate {data.Item3} -i {ffmpegInputPattern} -c:v libx264 -b:v {data.Item5}k -s {data.Item4} -pix_fmt yuv420p -vf \"fps={data.Item3}\" -y \"{outputPath}\"";
			var startInfo = new ProcessStartInfo("ffmpeg", ffmpegCommand)
			{
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};

			try
			{
				using (var process = Process.Start(startInfo))
				{
					process.BeginErrorReadLine();
					process.ErrorDataReceived += (sendingProcess, errorLine) =>
					{
						if (!string.IsNullOrEmpty(errorLine.Data) && errorLine.Data.Contains("time="))
						{
							string timeString = errorLine.Data.Split(new string[] { "time=" }, StringSplitOptions.None)[1].Split(' ')[0];
							double currentTime = ConvertTimeStringToSeconds(timeString);
							int progressPercentage = (int)((currentTime / totalDuration) * 100);
							worker.ReportProgress(progressPercentage);
						}
					};

					while (!process.HasExited)
					{
						// To keep the loop active until the process exits
					}

					process.WaitForExit();
					if (process.ExitCode == 0)
					{
						e.Result = "Video created at " + outputPath;
					}
					else
					{
						e.Result = "FFmpeg failed to create video.";
					}
				}
			}
			catch (Exception ex)
			{
				e.Result = "Failed to execute FFmpeg: " + ex.Message;
			}

		}

		private double ConvertTimeStringToSeconds(string timeString)
		{
			TimeSpan time = TimeSpan.Parse(timeString);
			return time.TotalSeconds;
		}


		private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			// Update progress bar or status label
			progressBar1.Value = e.ProgressPercentage;
			statusLabel.Text = "Processing: " + e.ProgressPercentage + "%";  // Update status label
		}

		private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			// This code executes after the worker has completed
			// Update UI to reflect completion
			//MessageBox.Show(e.Result.ToString());
			//statusLabel.Text = "Conversion Completed";
			//this.Cursor = Cursors.Default;
			//btnStartConvert.Enabled = true;
			//progressBar1.Value = 0;

			// Check if the application was started with arguments
			if (_startedWithArguments)
			{
				this.Close();  // Closes the form, thus exiting the application if this is the main form
			}
			else
			{
				// Update UI or reset the form for another possible manual operation
				btnStartConvert.Enabled = true;
				statusLabel.Text = "Conversion Completed";
				progressBar1.Value = 0;
			}
		}

		private void btnStartConvert_Click(object sender, EventArgs e)
		{
			btnStartConvert.Enabled = false;  // Disable the button to prevent multiple presses
											  //worker.RunWorkerAsync();  // Start the background worker
			statusLabel.Text = "Started";


			string selectedPath = txtSelectedDirectory.Text;
			string folderPath = Path.GetDirectoryName(selectedPath);
			if (string.IsNullOrEmpty(selectedPath) || !File.Exists(selectedPath))
			{
				MessageBox.Show("Please select a valid file.");
				return;
			}

			// Get file details
			string[] supportedExtensions = { ".png", ".jpg", ".jpeg" };
			var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
								 .Where(f => supportedExtensions.Contains(Path.GetExtension(f).ToLower()))
								 .OrderBy(f => f)
								 .ToArray();

			if (files.Length == 0)
			{
				MessageBox.Show("No supported image files found in the selected directory.");
				return;
			}

			// Extract the filename prefix
			string firstFileName = Path.GetFileName(files[0]);
			int lastDigitIndex = firstFileName.TakeWhile(c => char.IsLetter(c) || c == '.').Count();
			string prefix = firstFileName.Substring(0, lastDigitIndex);
			string extension = Path.GetExtension(firstFileName);
			string inputFileNamePattern = $"{prefix}%04d{extension}";
			string ffmpegInputPattern = $"\"{Path.Combine(folderPath, inputFileNamePattern)}\"";
			string resolution = comboBox1.Text;
			int frameRate = int.Parse(cmbFPS.Text);
			int frameCount = files.Length;
			double durationInSeconds = frameCount / (double)frameRate;
			int targetBitrate = 15000;  // Default to 5000 kbps or another reasonable value

			string outputPath = Path.Combine(folderPath, $"{prefix.Replace(".", "")}_{resolution}.mp4");
			if (chkWhatsapp.Checked)
			{
				int targetFileSizeMB = 59;
				targetBitrate = (int)((targetFileSizeMB * 8 * 1024) / durationInSeconds);
				outputPath = Path.Combine(folderPath, $"{prefix.Replace(".", "")}_{resolution}_whatsapp.mp4");
			}

			// Start the background worker with necessary parameters
			worker.RunWorkerAsync(new Tuple<string, string, int, string, int, double>(ffmpegInputPattern, outputPath, frameRate, resolution, targetBitrate, durationInSeconds));
		}

		private void btnSelectDirectory_Click_1(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				// Include JPEG in the filter and allow selecting PNG and JPG files too
				ofd.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
				ofd.Multiselect = false; // Set to true if you want to allow multiple file selections
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					txtSelectedDirectory.Text = ofd.FileName;
				}
			}
		}
	}
}
