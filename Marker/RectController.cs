﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection.Marker
{
    interface IRectController
    {
        PictureBox PictureBox { get; set; }
    }
    class RectController : IRectController
    {
        private PictureBox _pictureBox { get; set; }
        private Image originImage;
        private Rectangle rect = new Rectangle();
        private Size imageSize = new Size();
        private Vector2 startPoint = new Vector2();
        private Vector2 endPoint = new Vector2();
        private bool Drawing = false;
        public PictureBox PictureBox { 
            get {
                return _pictureBox;
            }
            set {
                _pictureBox = value;
                _pictureBox.MouseDown += MouseDown;
                _pictureBox.MouseUp +=  MouseUp;
                _pictureBox.MouseMove +=  Move;
                _pictureBox.Resize += Resize;
                imageSize = _pictureBox.Size;
            } }
        public RectController(PictureBox pictureBox)
        {
            PictureBox = pictureBox;
        }
        private void MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = ConverPointToVector2(e.Location);
            if (originImage != null)
            {
                PictureBox.Image = (Image)originImage.Clone();
            } else
            {
                originImage = (Image)PictureBox.Image.Clone();
            }
            Drawing = true;
        }
        private void MouseUp(object sender, MouseEventArgs e)
        {
            Drawing = false;
            endPoint = ConverPointToVector2(e.Location);
            Draw();
        }
        private void Move(object sender, MouseEventArgs e)
        {
            if (Drawing)
            {
                endPoint = ConverPointToVector2(e.Location);
                Draw();
            }
            
        }
        private void Resize(object sender, EventArgs e)
        {
            
            imageSize = PictureBox.Size;
            
            if (Drawing) Draw();
        }
        private void Draw()
        {
            if (PictureBox.Image == null) return;
            if (originImage != null) PictureBox.Image = (Image)originImage.Clone();

            using (Pen pen = new Pen(Color.Red, 2))
            using (Graphics G = Graphics.FromImage(PictureBox.Image))
            {
                
                Point start = ConverVector2ToPoint(startPoint);
                Point end = ConverVector2ToPoint(endPoint);
                
                G.DrawRectangle(pen, GetRectangleFromPoints(start, end));
                PictureBox.Refresh();
            }
            

        }
        private Vector2 ConverPointToVector2(Point point)
        {
            Vector2 tmp = new Vector2();
            tmp.X = point.X>0? (float)point.X / (float)imageSize.Width: 0;
            tmp.Y = point.Y>0? (float)point.Y / (float)imageSize.Height: 0;
            return tmp;
        }
        private Point ConverVector2ToPoint(Vector2 vector)
        {
            Point tmp = new Point();
            tmp.X = (int)(vector.X * imageSize.Width);
            tmp.Y = (int)(vector.Y * imageSize.Height);
            return tmp;
        }
        private Rectangle GetRectangleFromPoints(Point start, Point end)
        {
            int X = start.X, 
                Y = start.Y,
                Width = Math.Abs(start.X-end.X),
                Height = Math.Abs(start.Y-end.Y);

            if (start.X > end.X) X = end.X;
            if (start.Y > end.Y) Y = end.Y;
            Rectangle rect = new Rectangle(X,Y,Width,Height);
            return rect;
        }
    }
}
