using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace TerraScan.Common
{
    public static class ExtendedGraphics
    {

        #region Fills a Rounded Rectangle with integers.

        private static void FillRoundRectangle(System.Drawing.Brush brush,
          int x, int y,
          int width, int height, int radius, Graphics myGraphics)
        {

            float fx = Convert.ToSingle(x);
            float fy = Convert.ToSingle(y);
            float fwidth = Convert.ToSingle(width);
            float fheight = Convert.ToSingle(height);
            float fradius = Convert.ToSingle(radius);
            FillRoundRectangle(brush, fx, fy,
              fwidth, fheight, fradius, myGraphics);

        }
        #endregion


        #region Fills a Rounded Rectangle with continuous numbers.

        private static void FillRoundRectangle(System.Drawing.Brush brush,
         float x, float y,
         float width, float height, float radius,Graphics myGraphics)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = GetRoundedRect(rectangle, radius);
            myGraphics.FillPath(brush, path);
        }
        #endregion


        #region Draws a Rounded Rectangle border with integers.


        private static void DrawRoundRectangle(System.Drawing.Pen pen, int x, int y,
         int width, int height, int radius,Graphics myGraphics)
        {
            float fx = Convert.ToSingle(x);
            float fy = Convert.ToSingle(y);
            float fwidth = Convert.ToSingle(width);
            float fheight = Convert.ToSingle(height);
            float fradius = Convert.ToSingle(radius);
            DrawRoundRectangle(pen, fx, fy, fwidth, fheight, fradius, myGraphics);
        }
        #endregion


        #region Draws a Rounded Rectangle border with continuous numbers.

        private static void DrawRoundRectangle(System.Drawing.Pen pen,
          float x, float y,
          float width, float height, float radius,Graphics myGraphics)
        {
            RectangleF rectangle = new RectangleF(x, y, width, height);
            GraphicsPath path = GetRoundedRect(rectangle, radius);
            myGraphics.DrawPath(pen, path);
        }
        #endregion


        #region Get the desired Rounded Rectangle path.
        private static GraphicsPath GetRoundedRect(RectangleF baseRect,
           float radius)
        {
            // if corner radius is less than or equal to zero, 
            // return the original rectangle 
            if (radius <= 0.0F)
            {
                GraphicsPath mPath = new GraphicsPath();
                mPath.AddRectangle(baseRect);
                mPath.CloseFigure();
                return mPath;
            }

            // if the corner radius is greater than or equal to 
            // half the width, or height (whichever is shorter) 
            // then return a capsule instead of a lozenge 
            if (radius >= (Math.Min(baseRect.Width, baseRect.Height)) / 2.0)
                return GetCapsule(baseRect);

            // create the arc for the rectangle sides and declare 
            // a graphics path object for the drawing 
            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            // top left arc 
            path.AddArc(arc, 180, 90);

            // top right arc 
            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc 
            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc
            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
        #endregion

        #region Gets the desired Capsular path.
        private static GraphicsPath GetCapsule(RectangleF baseRect)
        {
            float diameter;
            RectangleF arc;
            GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            try
            {
                if (baseRect.Width > baseRect.Height)
                {
                    // return horizontal capsule 
                    diameter = baseRect.Height;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 90, 180);
                    arc.X = baseRect.Right - diameter;
                    path.AddArc(arc, 270, 180);
                }
                else if (baseRect.Width < baseRect.Height)
                {
                    // return vertical capsule 
                    diameter = baseRect.Width;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    arc = new RectangleF(baseRect.Location, sizeF);
                    path.AddArc(arc, 180, 180);
                    arc.Y = baseRect.Bottom - diameter;
                    path.AddArc(arc, 0, 180);
                }
                else
                {
                    // return circle 
                    path.AddEllipse(baseRect);
                }
            }
            catch (Exception)
            {
                path.AddEllipse(baseRect);
            }
            finally
            {
                path.CloseFigure();
            }
            return path;
        }
        #endregion

        #region Drawstring

        private static void DrawString(string stringText, Font font, Brush brush, PointF point, Graphics myGraphics)
        {
            StringFormat myStringFormat = new StringFormat();
            myStringFormat.Alignment = StringAlignment.Center;
            myGraphics.DrawString(stringText, font, brush, point, myStringFormat);
        }

        #endregion Drawstring

        #region RoundedRectangle


        public static Bitmap GenerateHorizontalImage(int rectangleWidth, int rectangleHeight, string tabText, int redColor, int greenColor, int blueColor)
        {
            Bitmap myBitMap = new Bitmap(rectangleWidth, rectangleHeight);
            Graphics myGraphics = System.Drawing.Graphics.FromImage(myBitMap);
            Color myColor = Color.FromArgb(redColor, greenColor, blueColor);
            Color penColor = Color.Black;
            SolidBrush myBrush = new SolidBrush(myColor);
            Font myFont = new Font("Arial", 10, FontStyle.Bold);
            Pen rect = new Pen(penColor, 1);
            FillRoundRectangle(myBrush, 0, 1, rectangleWidth - 1, rectangleHeight - 4, 8, myGraphics);
            DrawRoundRectangle(rect, 0, 1, rectangleWidth - 1, rectangleHeight - 4, 8, myGraphics);
            PointF fontPoint = getHorizontalFontPosition(rectangleWidth , rectangleHeight , myFont.Size, myFont.Height, tabText.Length);
            DrawString(tabText, myFont, Brushes.White, new Point(rectangleWidth / 2, Convert.ToInt32 (fontPoint.Y)), myGraphics);
            return myBitMap;

        }
        public static Bitmap GenerateVerticalImage(int rectangleHeight, int rectangleWidth, string tabText, int redColor, int greenColor, int blueColor)
        {
            Bitmap myBitMap = new Bitmap(rectangleHeight, rectangleWidth);
            Graphics myGraphics = System.Drawing.Graphics.FromImage(myBitMap);
            Color myColor = Color.FromArgb(redColor, greenColor, blueColor);
            Color penColor = Color.Black;
            SolidBrush myBrush = new SolidBrush(myColor);
            Font myFont = new Font("Arial", 10, FontStyle.Bold);
            Pen rect = new Pen(penColor, 1);
            FillRoundRectangle(myBrush, 0, 1, rectangleHeight - 1, rectangleWidth-4, 8, myGraphics);
            DrawRoundRectangle(rect, 0, 1, rectangleHeight - 1, rectangleWidth -4 , 8, myGraphics);
            PointF fontPoint = getVerticalFontPosition(rectangleHeight, rectangleWidth, myFont.Size, myFont.Height, tabText.Length);
            
            DrawString(tabText, myFont, Brushes.White, new Point(rectangleHeight / 2, Convert.ToInt32 (fontPoint.Y)), myGraphics);
            myBitMap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return myBitMap;
           
        }

        public static Bitmap GenerateVerticalImageWithSmallFont(int rectangleHeight, int rectangleWidth, string tabText, int redColor, int greenColor, int blueColor)
        {
            Bitmap myBitMap = new Bitmap(rectangleHeight, rectangleWidth);
            Graphics myGraphics = System.Drawing.Graphics.FromImage(myBitMap);
            Color myColor = Color.FromArgb(redColor, greenColor, blueColor);
            Color penColor = Color.Black;
            SolidBrush myBrush = new SolidBrush(myColor);
            Font myFont = new Font("Arial", 7, FontStyle.Bold);
            Pen rect = new Pen(penColor, 1);
            FillRoundRectangle(myBrush, 0, 1, rectangleHeight - 1, rectangleWidth - 4, 8, myGraphics);
            DrawRoundRectangle(rect, 0, 1, rectangleHeight - 1, rectangleWidth - 4, 8, myGraphics);
            PointF fontPoint = getVerticalFontPosition(rectangleHeight, rectangleWidth, myFont.Size, myFont.Height, tabText.Length);

            DrawString(tabText, myFont, Brushes.White, new Point(rectangleHeight / 2, Convert.ToInt32(fontPoint.Y)), myGraphics);
            myBitMap.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return myBitMap;

        }

        public static Bitmap GenerateImage(int rectangleHeight, int rectangleWidth, int redColor, int greenColor, int blueColor)
        {
            Bitmap myBitMap = new Bitmap(rectangleHeight, rectangleWidth);
            Graphics myGraphics = System.Drawing.Graphics.FromImage(myBitMap);
            Color myColor = Color.FromArgb(redColor, greenColor, blueColor);
            SolidBrush myBrush = new SolidBrush(myColor);
            Pen rect = new Pen(myBrush);
            myGraphics.DrawRectangle(rect, 1, 1, 1, 1);
            return myBitMap;

        }

        private static void DrawString(string tabText, Font myFont, Brush brush, PointF fontPoint, Graphics myGraphics, RectangleF myRectangle)
        {
            myGraphics.DrawString(tabText, myFont, brush, myRectangle);
        }
        #endregion RoundedRectangle

        #region FontPosition

        private static PointF getHorizontalFontPosition(int rectaglewidth, int rectangleHeight, float fontSize,int fontHeight, int fontTextLength)
        {
            return new PointF((Convert.ToInt32 ((rectaglewidth / 2)) - (Convert.ToInt32 ((fontSize * fontTextLength) / 2))), Convert.ToInt32 (((Convert.ToInt32(rectangleHeight / 2) - Convert.ToInt32 (fontHeight / 2)))));
        }

        private static PointF getVerticalFontPosition(int rectaglewidth, int rectangleHeight, float fontSize, int fontHeight, int fontTextLength)
        {
            return new PointF((Convert.ToInt32((rectaglewidth / 2)) - (Convert.ToInt32((fontSize * fontTextLength) / 2))), Convert.ToInt32(((Convert.ToInt32(rectangleHeight / 2) - Convert.ToInt32(fontHeight / 2)))));
        }


        #endregion FontPosition

        #region RDL VerticalLabel Text

        /// <summary>
        /// Generates the vertical label text.
        /// </summary>
        /// <param name="rectangleHeight">Height of the rectangle.</param>
        /// <param name="rectangleWidth">Width of the rectangle.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="backGroundColor">Color of the back ground.</param>
        /// <param name="textFont">The text font.</param>
        /// <returns>vertical text</returns>
        public static Bitmap GenerateVerticalLabelText(int rectangleHeight, int rectangleWidth, string tabText, Color backGroundColor, Font textFont)
        {
            Bitmap myBitMap = new Bitmap(rectangleHeight, rectangleWidth);
            Graphics myGraphics = System.Drawing.Graphics.FromImage(myBitMap);
            Color myColor = backGroundColor;
            Color penColor = Color.Black;
            SolidBrush myBrush = new SolidBrush(myColor);
            Font myFont = textFont;
            Pen rect = new Pen(penColor, 1);
            FillRoundRectangle(myBrush, 0, 1, rectangleHeight, rectangleWidth, 0, myGraphics);
            DrawRoundRectangle(rect, 0, 1, rectangleHeight, rectangleWidth, 0, myGraphics);
            PointF fontPoint = getVerticalFontPosition(rectangleHeight, rectangleWidth, myFont.Size, myFont.Height, tabText.Length);

            DrawString(tabText, myFont, Brushes.Black, new Point(rectangleHeight / 2, Convert.ToInt32(fontPoint.Y)), myGraphics);
            myBitMap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            return myBitMap;

        }

        #endregion
    }
}

