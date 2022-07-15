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
            // Assign the icons to the buttons
            customButton0.Text = "\uE800";
            customButton1.Text = "\uE801";
            customButton2.Text = "\uE802";
            customButton3.Text = "\uE803";
            customButton4.Text = "\uE804";
        }
    }
    partial class CustomButton : Button
    {
        public CustomButton()
        {
            UseCompatibleTextRendering = true;
            TextAlign = ContentAlignment.MiddleCenter;
            refCount++;
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode) initFont();
        }
        private void initFont()
        {
            if (privateFontCollection == null)
            {
                privateFontCollection = new PrivateFontCollection();
                var path = Path.Combine(Path.GetDirectoryName(
                    Assembly.GetEntryAssembly().Location),
                    "Fonts",
                    "flashlight-filter-history-favorite-search.ttf");
                privateFontCollection.AddFontFile(path);
                var fontFamily = privateFontCollection.Families[0];
                GlyphFontUp = new Font(fontFamily, 16F);
                GlyphFontDown = new Font(fontFamily, 15F);
            }
            Font = GlyphFontUp;
            ForeColor = GlyphColorUp;
        }
        PrivateFontCollection privateFontCollection = null;
        public static Font GlyphFontUp { get; private set; } = null;
        public static Font GlyphFontDown { get; private set; } = null;
        public static Color GlyphColorUp { get; } = Color.Teal;
        public static Color GlyphColorDown { get; } = Color.DarkCyan;

        private static int refCount = 0;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                refCount--;
                if (refCount == 0)
                {
                    GlyphFontUp?.Dispose();
                    privateFontCollection?.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Image = new Bitmap(Resources.buttonDown, Size);
            Font = GlyphFontDown;
            ForeColor = GlyphColorDown;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Font = GlyphFontUp;
            ForeColor = GlyphColorUp;
            Image = null;
        }
    }
}
