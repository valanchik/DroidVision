using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YoloDetection
{
    class KalmanFilterSimple1D
    {
        public double X0 { get; private set; } // predicted state
        public double P0 { get; private set; } // predicted covariance

        public double F { get; private set; } // factor of real value to previous real value
        public double Q { get; private set; } // measurement noise
        public double H { get; private set; } // factor of measured value to real value
        public double R { get;  set; } // environment noise

        public double State { get; private set; }
        public double Covariance { get;  set; }

        public KalmanFilterSimple1D(double q, double r, double f = 1, double h = 1)
        {
            Q = q;
            R = r;
            F = f;
            H = h;
        }

        public void SetState(double state, double covariance)
        {
            State = state;
            Covariance = covariance;
        }

        public void Correct(double data)
        {
            //time update - prediction
            X0 = F * State;
            P0 = F * Covariance * F + Q;

            //measurement update - correction
            var K = H * P0 / (H * P0 * H + R);
            State = X0 + K * (data - H * X0);
            Covariance = (1 - K * H) * P0;
        }
    }

    class KalmanFilter2D
    {
        private KalmanFilterSimple1D X;
        private KalmanFilterSimple1D Y;
        private Vector _State = new Vector(0,0);
        public Vector State => _State;
        public double R { set
            {
                X.R = value;
                Y.R = value;
            } }
        public float Covariance
        {
            set
            {
                X.Covariance = value;
                Y.Covariance = value;
            }
        }
        public KalmanFilter2D(double q, double r, double f = 1, double h = 1)
        {
            X = new KalmanFilterSimple1D(q, r, f, h);
            Y = new KalmanFilterSimple1D(q, r, f, h);
        }

        public void SetState(Vector state, double covariance)
        {
            X.SetState((double)state.X, covariance);
            Y.SetState((double)state.Y, covariance);
            _State.X = X.State;
            _State.Y = Y.State;
        }

        public void Correct(Vector data)
        {
            X.Correct((double)data.X);
            Y.Correct((double)data.Y);
            _State.X = X.State;
            _State.Y = Y.State;
        }
    }

    class KalmanFilterRectangle
    {
        private KalmanFilterSimple1D X;
        private KalmanFilterSimple1D Y;
        private KalmanFilterSimple1D W;
        private KalmanFilterSimple1D H;
        private Rectangle _State = new Rectangle(0, 0, 0, 0);
        public Rectangle State => _State;
        public double R
        {
            set
            {
                X.R = value;
                Y.R = value;
                W.R = value;
                H.R = value;
            }
        }
        public float Covariance
        {
            set
            {
                X.Covariance = value;
                Y.Covariance = value;
                W.Covariance = value;
                H.Covariance = value;
            }
        }
        public KalmanFilterRectangle(double q, double r, double f = 1, double h = 1)
        {
            X = new KalmanFilterSimple1D(q, r, f, h);
            Y = new KalmanFilterSimple1D(q, r, f, h);
            W = new KalmanFilterSimple1D(q, r, f, h);
            H = new KalmanFilterSimple1D(q, r, f, h);
        }

        public void SetState(Rectangle state, double covariance)
        {
            X.SetState((double)state.X, covariance);
            Y.SetState((double)state.Y, covariance);
            W.SetState((double)state.Width, covariance);
            H.SetState((double)state.Height, covariance);
            SetState();
        }

        public void Correct(Rectangle data)
        {
            X.Correct((double)data.X);
            Y.Correct((double)data.Y);
            W.Correct((double)data.Width);
            H.Correct((double)data.Height);
            SetState();
        }
        private void SetState ()
        {
            _State.X = (int)X.State;
            _State.Y = (int)Y.State;
            _State.Width = (int)W.State;
            _State.Height = (int)H.State;
        }
    }

}
