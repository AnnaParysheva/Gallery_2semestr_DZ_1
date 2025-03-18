using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Gallery
{
    public partial class Gallery : Form
    {
        public Gallery()
        {
            InitializeComponent();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                listBoxPictures.Items.Clear();
                pictureBox.Image = null;
                textBoxTitle.Clear();
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                foreach (string line in lines)
                {
                    listBoxPictures.Items.Add(line);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> lines = new List<string>();
                foreach (var item in listBoxPictures.Items)
                {
                    string line = item.ToString();
                    if (item == listBoxPictures.SelectedItem && !string.IsNullOrEmpty(textBoxTitle.Text))
                    {
                        string[] parts = line.Split(',');
                        line = parts[0] + "," + textBoxTitle.Text;
                    }
                    lines.Add(line);
                }
                File.WriteAllLines(saveFileDialog.FileName, lines);
            }
        }

        private void listBoxPictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPictures.SelectedItem != null)
            {
                string[] parts = listBoxPictures.SelectedItem.ToString().Split(',');
                if (File.Exists(parts[0]))
                {
                    pictureBox.Image = Image.FromFile(parts[0]);
                }
                if (parts.Length > 1)
                {
                    textBoxTitle.Text = parts[1];
                }
                else
                {
                    textBoxTitle.Text = string.Empty;
                }
            }
        } 
    }
}
