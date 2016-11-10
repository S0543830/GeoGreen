namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Kinect;
    using Coding4Fun.Kinect.Wpf;

    public partial class MainWindow : Window
    {
        #region Variable
        public bool _continue;
        public System.IO.Ports.SerialPort _serialport = new System.IO.Ports.SerialPort();
        public int ScreenMaxY = 180;
        public int ScreenMaxX = 180;
        public bool continuee
        {
            get { return _continue; } set { _continue = value; }
        }
        public System.IO.Ports.SerialPort serialport
        {
            get { return _serialport; }
            set { _serialport = value; }
        }
        #endregion

        #region Arduino Funktionen
        //Serial wird gesetzt
        private void ArduinoSetSerial()
        {
            string ArduinoCom = ComPort.Text;
            serialport.PortName = "COM6";
            serialport.BaudRate = 9600; ;
            serialport.DataBits = 8;
            serialport.Handshake = 0;
            serialport.ReadTimeout = 500;
            serialport.WriteTimeout = 500;
        }

        //Öffnet das Serial des Arduinos
        private void ArduinoOpenSerial()
        {
            if((!serialport.IsOpen))
            {
                serialport.Open();
            }
            else { MessageBox.Show("Serielle Verbindung kann nicht geöffnet werden!"); }
            continuee = true;
        }
        //Schließt das Serial vom Arduino
        private void ArduinoCloseSerial()
        {
            if(serialport.IsOpen)
            {
                try {serialport.Close(); }
                catch(Exception e)
                { MessageBox.Show(e.ToString()); }
            }
            else { MessageBox.Show("Serielle Verbindung ist geschloßen"); }
        }
        //Koordinaten werden an dem Arduino Board gesendet
        private void ArduinoSendByte( Single kinectX, Single kinectY, Single kinectZ, Single kinectJ)
        {
            var x = 0;
            var y = 0;
            var z = 0;
            if (!Double.IsNaN(kinectX)&&(!Double.IsNaN(kinectY))&&(!Double.IsNaN(kinectZ))) {
                byte j;
            x = Math.Abs(Convert.ToInt32(kinectX));
            if(kinectZ<0)
            { kinectZ = kinectZ * (-1); }
            y = Math.Abs(Convert.ToByte(kinectY));
            z = Math.Abs(Convert.ToByte(kinectZ));
            //z = Convert.ToByte(kinectZ);*/
            j = Convert.ToByte(kinectJ);

            byte[] ArduinoBuffer = new byte[] { Convert.ToByte(x), Convert.ToByte(y), Convert.ToByte(z), j };
            if(serialport.IsOpen)
            {

                serialport.Write(ArduinoBuffer, 0, ArduinoBuffer.Length);
            }  }
                
            
        }

        //Koordinaten werden an dem Arduino Board gesendet
        private void ArduinoSendByteNEw(Single lelle, Single lschulterxy, Single lschulterz, Single kopf, Single rschulterz, Single rschultery, Single relle)
        {
            var a = 0;
            var b = 0;
            var c = 0;
            var d = 0;
            var e = 0;
            var f = 0;
            var g = 0;
            if (!Double.IsNaN(lelle) && !Double.IsNaN(lschulterxy) && !Double.IsNaN(lschulterz) && !Double.IsNaN(kopf) && !Double.IsNaN(rschultery) && !Double.IsNaN(rschulterz) && !Double.IsNaN(relle))
            {
                if (lschulterz < 0)
                { lschulterz = lschulterz * (-1); }
                if (rschulterz < 0)
                { rschulterz = rschulterz * (-1); }

                a = Math.Abs(Convert.ToInt32(lelle));
                b = Math.Abs(Convert.ToInt32(lschulterxy));
                c = Math.Abs(Convert.ToInt32(lschulterz));
                d = Math.Abs(Convert.ToInt32(kopf));
                e = Math.Abs(Convert.ToInt32(rschulterz));
                f = Math.Abs(Convert.ToInt32(rschultery));
                g = Math.Abs(Convert.ToInt32(relle));


                byte[] ArduinoBuffer = new byte[] { Convert.ToByte(a), Convert.ToByte(b), Convert.ToByte(c), Convert.ToByte(d), Convert.ToByte(e), Convert.ToByte(f), Convert.ToByte(g) };
                if (serialport.IsOpen)
                {
                    serialport.Write(ArduinoBuffer, 0, ArduinoBuffer.Length);
                }
            }
        }

        //Koordinaten wird dem Skelet übergeben
        private void SendToArduino(Joint joint, JointType JID)
        {
            var scaledJoint = joint.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            var z = (scaledJoint.Position.Z - 1) * 100;
            ArduinoSendByte(scaledJoint.Position.X, scaledJoint.Position.Y, z, Convert.ToInt32(JID));
        }

        #endregion

        #region Membervariable für die Zeichnung
        // Breite der äußeren Zeichnung
        private const float RenderWidth = 640.0f;

        //Höhe der äußeren Zeichnung
        private const float RenderHeight = 480.0f;
       
        private const double JointThickness = 3;
        
        private const double BodyCenterThickness = 10;
        
        private const double ClipBoundsThickness = 10;
        
        private readonly Brush centerPointBrush = Brushes.Blue;
        
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
       
        private readonly Brush inferredJointBrush = Brushes.Yellow;
        
        private readonly Pen trackedBonePen = new Pen(Brushes.Green, 6);
       
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);
        
        private KinectSensor sensor;
        
        private DrawingGroup drawingGroup;
        
        private DrawingImage imageSource;
        #endregion

        #region Brechnungen
        //Länge zwischen zwei Joints berechnen
        private double laengeberechnung(Joint joint1, Joint joint2)
        {
            double lange;
            var scaledJoint1 = joint1.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            var scaledJoint2 = joint2.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            lange = Convert.ToDouble(Math.Sqrt((Convert.ToInt32(scaledJoint1.Position.X - scaledJoint2.Position.X) ^ 2) + (Convert.ToInt32(scaledJoint1.Position.Y - scaledJoint2.Position.Y) ^ 2) + Convert.ToInt32(scaledJoint1.Position.Z - scaledJoint2.Position.Z) ^ 2));
            return lange;
        }

        //Vektorenberechnung
        private SkeletonPoint VektorAusZweiPunkten(Joint p1, Joint p2, string Richtung)
        {
            var vektorz = 0;
            var vektorx = 0;
            var vektory = 0;
            var scaledJoint1 = p1.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            var scaledJoint2 = p2.ScaleTo(ScreenMaxX, ScreenMaxY, 0.5F, 0.2F);
            switch (Richtung)
            {
                case "xy":

                    vektorx = Convert.ToInt32(scaledJoint1.Position.X - scaledJoint2.Position.X);
                    vektory = Convert.ToInt32(scaledJoint1.Position.Y - scaledJoint2.Position.Y);
                    break;
                case "xz":
                    vektorx = Convert.ToInt32(scaledJoint1.Position.X - scaledJoint2.Position.X);
                    vektorz = Convert.ToInt32(scaledJoint1.Position.Z - scaledJoint2.Position.Z);
                    break;
                case "yz":
                    vektory = Convert.ToInt32(scaledJoint1.Position.Y - scaledJoint2.Position.Y);
                    vektorz = Convert.ToInt32(scaledJoint1.Position.Z - scaledJoint2.Position.Z);
                    break;
                case "xyz":
                    vektorx = Convert.ToInt32(scaledJoint1.Position.X - scaledJoint2.Position.X);
                    vektory = Convert.ToInt32(scaledJoint1.Position.Y - scaledJoint2.Position.Y);
                    vektorz = Convert.ToInt32(scaledJoint1.Position.Z - scaledJoint2.Position.Z);
                    break;
            }

            SkeletonPoint vektorxyz = new SkeletonPoint();
            vektorxyz.X = vektorx;
            vektorxyz.Y = vektory;
            vektorxyz.Z = vektorz;
            return vektorxyz;
        }

        //Winkelberechnung
        private double Winkelberechnung(SkeletonPoint V1, SkeletonPoint V2, string Richtung)
        {
            double gesamt = 0;
            double Zaehler = 0;
            double nenner1 = 0;
            double nenner2 = 0;
            switch (Richtung)
            {
                case "xy":
                    Zaehler = (V1.X * V2.X) + (V1.Y * V2.Y);
                    nenner1 = Math.Sqrt((V1.X * V1.X) + (V1.Y * V1.Y));
                    nenner2 = Math.Sqrt((V2.X * V2.X) + (V2.Y * V2.Y));
                    break;
                case "xz":
                    Zaehler = (V1.X * V2.X) + (V1.Z * V2.Z);
                    nenner1 = Math.Sqrt((V1.X * V1.X) + (V1.Z * V1.Z));
                    nenner2 = Math.Sqrt((V2.X * V2.X) + (V2.Z * V2.Z));
                    break;
                case "yz":
                    Zaehler = (V1.Y * V2.Y) + (V1.Z * V2.Z);
                    nenner1 = Math.Sqrt((V1.Y * V1.Y) + (V1.Z * V1.Z));
                    nenner2 = Math.Sqrt((V2.Y * V2.Y) + (V2.Z * V2.Z));
                    break;
                case "xyz":
                    Zaehler = (V1.X * V2.X) + (V1.Y * V2.Y) + (V1.Z * V2.Z);
                    nenner1 = Math.Sqrt((V1.X * V1.X) + (V1.Y * V1.Y) + (V1.Z * V1.Z));
                    nenner2 = Math.Sqrt((V2.X * V2.X) + (V2.Y * V2.Y) + (V2.Z * V2.Z));
                    break;
            }
            gesamt = (Math.Acos(Zaehler / (nenner1 * nenner2)) * 57.29577951);
            return gesamt;
        }

        private double cosinuwinkelberechnung(double a, double b, double c)
        {
            double winkel = 0;
            if ((!Double.IsNaN(a)) || (!Double.IsNaN(b) || (!Double.IsNaN(b))))
            {
                double zahl = ((a * a) + (b * b) - (c * c)) / (2 * a * b);
                winkel = Math.Acos(zahl);
                winkel = winkel * 57.29577951;
            }
            return winkel;
        }
        #endregion

        //Zeichnet Knochen und Joints //Übergabe ans Arduino
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            #region Zeichnung
            // Bereiche des Körpers defnieren
            this.DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);


            // Linker Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Rechter Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);


            // Linkes Bein
            this.DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Rechtes Bein
            this.DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);
            #endregion

            #region Koordinaten werden an Arduino gesendet

            #region Erste Methode
            #region Linke Arm

            //Linker Ellenbogen
            SkeletonPoint SchulterElle = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.ShoulderLeft], "xyz");
            SkeletonPoint ElleHand = VektorAusZweiPunkten(skeleton.Joints[JointType.WristLeft], skeleton.Joints[JointType.ElbowLeft], "xyz");
            float winkelElleLinks = Convert.ToSingle(Winkelberechnung(SchulterElle, ElleHand, "xyz"));
           // ArduinoSendByte(winkelElleLinks, 0, 0, 100);


            //Schulter links in Z Richtung
            SkeletonPoint SchulterElleZ = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.ShoulderLeft], "yz");
            SkeletonPoint ShoulterSpline = VektorAusZweiPunkten(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine], "yz");
            float winkelShulterLinks = Convert.ToSingle(Winkelberechnung(SchulterElleZ, ShoulterSpline, "yz"));
            //ArduinoSendByte(0, 0, winkelShulterLinks, 99);
            
            //Schulter links in xy Richtung--
            SkeletonPoint SchulterEllez = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.ShoulderLeft], "xy");
            SkeletonPoint ShoulterSplinexy = VektorAusZweiPunkten(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine], "xy");
            float winkelShulterLinksxy = Convert.ToSingle(Winkelberechnung(SchulterEllez, ShoulterSplinexy, "xy"));
            //ArduinoSendByte(0, winkelShulterLinksxy, 0, 98);

            #endregion

            #region Rechter Arm
            //Rechter Ellenbogen
            SkeletonPoint SchulterEllerechts = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight], "xy");
            SkeletonPoint ElleHandrechts = VektorAusZweiPunkten(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.ElbowRight], "xy");
            float winkelElleRechts = Convert.ToSingle(Winkelberechnung(SchulterEllerechts, ElleHandrechts, "xy"));
            //ArduinoSendByte(winkelElleRechts, 0, 0, 97);


            //Schulter rechts in Z Richtung
            SkeletonPoint SchulterElleZRechts = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight], "xyz");
            float winkelShulterRechts = Convert.ToSingle(Winkelberechnung(SchulterElleZRechts, ShoulterSpline, "xyz"));
            //ArduinoSendByte(0, 0, winkelShulterRechts, 95);

            //Schulter rechts in xy Richtung
            float winkelShulterLinksxyRechts = Convert.ToSingle(Winkelberechnung(SchulterEllerechts, ShoulterSplinexy, "xy"));
            //ArduinoSendByte(0, winkelShulterLinksxyRechts, 0, 96);
            #endregion

            #region Kopf
            //SendToArduino(skeleton.Joints[JointType.Head], JointType.Head);
            #endregion

            //Sende alle Winkel aufeinmal
            ArduinoSendByteNEw(winkelElleLinks, winkelShulterLinksxy, winkelShulterLinks, Convert.ToInt32(skeleton.Joints[JointType.Head]), winkelShulterRechts, winkelShulterLinksxyRechts, winkelElleRechts);
            #endregion

            /*
            #region Zweite Methode
            #region Linke Arm

            //Linker Ellenbogen
            SkeletonPoint SchulterElle = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.ShoulderLeft], "xyz");
            SkeletonPoint ElleHand = VektorAusZweiPunkten(skeleton.Joints[JointType.WristLeft], skeleton.Joints[JointType.ElbowLeft], "xyz");
            float winkelElleLinks = Convert.ToSingle(Winkelberechnung(SchulterElle, ElleHand, "xyz"));
            ArduinoSendByte(winkelElleLinks, 0, 0, 100);


            //Schulter links in Z Richtung
            SkeletonPoint SchulterElleZ = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.ShoulderLeft], "yz");
            SkeletonPoint ShoulterSpline = VektorAusZweiPunkten(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine], "yz");
            float winkelShulterLinks = Convert.ToSingle(Winkelberechnung(SchulterElleZ, ShoulterSpline, "yz"));
            ArduinoSendByte(0, 0, winkelShulterLinks, 99);
            
            //SendToArduino(skeleton.Joints[JointType.ElbowLeft], JointType.WristLeft);//6

            //Schulter links in xy Richtung--
            SkeletonPoint SchulterEllez = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowLeft], skeleton.Joints[JointType.ShoulderLeft], "xy");
            SkeletonPoint ShoulterSplinexy = VektorAusZweiPunkten(skeleton.Joints[JointType.ShoulderCenter], skeleton.Joints[JointType.Spine], "xy");
            float winkelShulterLinksxy = Convert.ToSingle(Winkelberechnung(SchulterEllez, ShoulterSplinexy, "xy"));
            ArduinoSendByte(0, winkelShulterLinksxy, 0, 98);
            
            #endregion
            
            #region Rechter Arm
            //Rechter Ellenbogen
            SkeletonPoint SchulterEllerechts = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight], "xy");
            SkeletonPoint ElleHandrechts = VektorAusZweiPunkten(skeleton.Joints[JointType.WristRight], skeleton.Joints[JointType.ElbowRight], "xy");
            float winkelElleRechts = Convert.ToSingle(Winkelberechnung(SchulterEllerechts, ElleHandrechts, "xy"));
            ArduinoSendByte(winkelElleRechts, 0, 0, 97);


            //Schulter rechts in Z Richtung
            SkeletonPoint SchulterElleZRechts = VektorAusZweiPunkten(skeleton.Joints[JointType.ElbowRight], skeleton.Joints[JointType.ShoulderRight], "xyz");
            float winkelShulterRechts = Convert.ToSingle(Winkelberechnung(SchulterElleZRechts, ShoulterSpline, "xyz"));
            ArduinoSendByte(0, 0, winkelShulterRechts, 95);

            //Schulter rechts in xy Richtung
            float winkelShulterLinksxyRechts = Convert.ToSingle(Winkelberechnung(SchulterEllerechts, ShoulterSplinexy, "xy"));
            ArduinoSendByte(0, winkelShulterLinksxyRechts, 0, 96);
            #endregion
            
            #region Kopf
            SendToArduino(skeleton.Joints[JointType.Head], JointType.Head);
            #endregion
            
            #endregion
            */
            #endregion

            #region Kalibrierung der Zeichnung
            // Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
            }
            #endregion
        }

        #region Zusatzfunktionen
        //Hauptfunktion
        public MainWindow()
        {
            InitializeComponent();
        }
       
        //Funktion für die Zeichnung
        private static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, RenderHeight));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight));
            }
        }

        //Starten der Aufgaben
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            this.drawingGroup = new DrawingGroup();

            // Bildquelle wird erzeugt
            this.imageSource = new DrawingImage(this.drawingGroup);

            //  Zeichnung wird angezeigt
            Image.Source = this.imageSource;

            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
                ArduinoSetSerial();
                ArduinoOpenSerial();
            }

           
            
        }

        //Schließt das Fenster
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }
        
        // Event handler für Kinect Sensor's SkeletonFrameReady event
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.AliceBlue,null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        RenderClippedEdges(skel, dc);

                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            this.DrawBonesAndJoints(skel, dc);
                        }
                        else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(
                            this.centerPointBrush,
                            null,
                            this.SkeletonPointToScreen(skel.Position),
                            BodyCenterThickness,
                            BodyCenterThickness);
                        }
                    }
                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
        }
        
        //Funktion ob der ganze Körper dargestellt wird
        private void CheckBoxSeatedModeChanged(object sender, RoutedEventArgs e)
        {/*
            if (null != this.sensor)
            {
                if (this.checkBoxSeatedMode.IsChecked.GetValueOrDefault())
                {
                    this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                }
                else
                {
                    this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                }
            }*/
        }
        
        //Skeletpunkte wird dem Bildschirm übergeben
        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        //Ein Knochen wird zwischen zwei Joints gezeichnet
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }

            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }
        #endregion
    }
}