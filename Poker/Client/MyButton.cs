using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Client
{
    public class MyButton : Button
    {
        Color topColor;
        public Color TopColor { get => topColor; set { topColor = value; Invalidate(); } }

        Color bottomColor;
        public Color BottomColor { get => bottomColor; set { bottomColor = value; Invalidate(); } }

        bool isHover;


        public MyButton()
        {
            TopColor = Color.FromArgb(165, 53, 53);
            BottomColor = Color.FromArgb(206, 66, 66);
            isHover = false;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            isHover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHover = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (!isHover)
            {
                base.OnPaint(pevent);
                var lgb = new LinearGradientBrush(ClientRectangle, topColor, bottomColor, 90f);
                pevent.Graphics.FillRectangle(lgb, ClientRectangle);
                lgb.Dispose();
                TextRenderer.DrawText(pevent.Graphics, Text, Font, ClientRectangle, Color.White);
            }
            else
            {
                pevent.Graphics.FillRectangle(new SolidBrush(Color.Red), ClientRectangle);
            }
            TextRenderer.DrawText(pevent.Graphics, Text, Font, ClientRectangle, Color.White);
        }
    }
}
