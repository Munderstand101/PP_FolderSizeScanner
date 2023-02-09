using System;
using System.IO;
using System.Windows.Forms;

namespace FolderSizeScanner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            string path = pathTextBox.Text;

            if (!Directory.Exists(path))
            {
                MessageBox.Show("The specified folder does not exist.");
                return;
            }

            long folderSize = GetFolderSize(new DirectoryInfo(path));
            resultLabel.Text = "Total size of the folder and all its subfolders: " + folderSize + " bytes";
        }

        static long GetFolderSize(DirectoryInfo folder)
        {
            long folderSize = 0;
            try
            {
                FileInfo[] fileInfo = folder.GetFiles();
                foreach (FileInfo file in fileInfo)
                {
                    folderSize += file.Length;
                }

                DirectoryInfo[] subFolders = folder.GetDirectories();
                foreach (DirectoryInfo subFolder in subFolders)
                {
                    folderSize += GetFolderSize(subFolder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }

            return folderSize;
        }
    }
}
