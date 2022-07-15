using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace intuitive_buttons
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            customButton0.Text = "\uE800";
            customButton1.Text = "\uE801";
            customButton2.Text = "\uE802";
            customButton3.Text = "\uE803";
            customButton4.Text = "\uE804";
        }
    }
    class CustomButton : Button
    {
        public CustomButton()
        {
            UseCompatibleTextRendering = true;
            TextAlign = ContentAlignment.MiddleCenter;
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
            {
                initFont();
            }
        }

        private void initFont()
        {
            if(_privateFontCollection == null)
            {
                _privateFontCollection = new PrivateFontCollection();
                var path = Path.Combine(Path.GetDirectoryName(
                    Assembly.GetEntryAssembly().Location),
                    "Fonts",
                    "flashlight-filter-history-favorite-search.ttf");
                _privateFontCollection.AddFontFile(path); 
                var fontFamily = _privateFontCollection.Families[0];
                GlyphFont = new Font(fontFamily, 16F);
            }
            Font = GlyphFont;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            BackgroundImage = Resources.buttonDown;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            BackgroundImage = null;
        }

        private static PrivateFontCollection _privateFontCollection = null;

        public static Font GlyphFont { get; private set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _privateFontCollection?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
