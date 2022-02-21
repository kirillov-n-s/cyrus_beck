using System;
using System.Drawing;
using System.Windows.Forms;

namespace laba1
{
    public partial class MainForm : Form
    {
        PictureBox _picturebox = new PictureBox();
        Scene _scene;
        bool _clipped = false;

        public MainForm()
        {
            InitializeComponent();
            _picturebox.Width = Width;
            _picturebox.Height = Height;
            _picturebox.MouseClick += PictureBox_MouseClick;
            Controls.Add(_picturebox);
            _scene = new Scene(_picturebox, Color.Yellow, Color.Cyan);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    try
                    {
                        _scene.ApplyCyrusBeck(_clipped ^= true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _clipped ^= true;
                    }
                    break;
                case Keys.Space:
                    _scene.Clear();
                    break;
                case Keys.Back:
                    _scene.RemoveLastPolygonPoint();
                    break;
                default:
                    break;
            }
        }

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    _scene.AddSegmentPoint(e.Location);
                    break;
                case MouseButtons.Right:
                    _scene.AddPolygonPoint(e.Location);
                    break;
                default:
                    break;
            }
        }
    }
}
