using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace TestDX3
{
    public partial class Form1 : Form
    {
        private float angle = 0;
        class Triangle
        {
            public const int quantity =90;
            public CustomVertex.PositionNormalColored[,] verts = new CustomVertex.PositionNormalColored[quantity,3];
            public const double alpha = Math.PI*2/quantity;
        }

        private Device device = null;
        Triangle treugolnik = new Triangle();
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            device.Clear(ClearFlags.Target, System.Drawing.Color.BurlyWood, 1.0f, 0);
            SetupCamera();
            float Radius = Convert.ToSingle(0.1 / Math.Sqrt(2 * (1 - Math.Cos(Math.PI / 180))));
            float[] coordinatsAB = new float[4];
            coordinatsAB[0] = 0;
            coordinatsAB[1] = -Radius;
            coordinatsAB[2] = Convert.ToSingle(coordinatsAB[0] * Math.Cos(Triangle.alpha) - coordinatsAB[1] * Math.Sin(Triangle.alpha));
            coordinatsAB[3] = Convert.ToSingle(coordinatsAB[1] * Math.Cos(Triangle.alpha) + coordinatsAB[0] * Math.Sin(Triangle.alpha));
            for (int i = 0; i < Triangle.quantity; i++)
            {

                //вычисления неовых двух точек
                coordinatsAB[0] = coordinatsAB[2];
                coordinatsAB[1] = coordinatsAB[3];
                coordinatsAB[2] = Convert.ToSingle(coordinatsAB[0] * Math.Cos(Triangle.alpha) - coordinatsAB[1] * Math.Sin(Triangle.alpha));
                coordinatsAB[3] = Convert.ToSingle(coordinatsAB[1] * Math.Cos(Triangle.alpha) + coordinatsAB[0] * Math.Sin(Triangle.alpha));

                treugolnik.verts[i, 0].Position = new Vector3(coordinatsAB[0], coordinatsAB[1], 0f);
                treugolnik.verts[i,0].Normal = new Vector3(0.0f, 0.0f, -1.0f);
                treugolnik.verts[i,0].Color = System.Drawing.Color.White.ToArgb();
                treugolnik.verts[i,1].Position = new Vector3(coordinatsAB[2], coordinatsAB[3], 0f);
                treugolnik.verts[i,1].Normal = new Vector3(0.0f, 0.0f, -1.0f);
                treugolnik.verts[i,1].Color = System.Drawing.Color.White.ToArgb();
                treugolnik.verts[i,2].Position = new Vector3(0f, 0f, 0f);
                treugolnik.verts[i,2].Normal = new Vector3(0.0f, 0.0f, -1.0f);
                treugolnik.verts[i,2].Color = System.Drawing.Color.White.ToArgb();
            }
            device.Lights[0].Type = LightType.Point;
            device.Lights[0].Position = new Vector3(0, 0, -5);
            device.Lights[0].Diffuse = System.Drawing.Color.Yellow;
            device.Lights[0].Attenuation0 = 1f;
            device.Lights[0].Range = 16f;
            device.Lights[0].Enabled = true;
            device.Lights[0].Update();
            device.BeginScene();
            device.VertexFormat = CustomVertex.PositionNormalColored.Format;
            device.DrawUserPrimitives(PrimitiveType.TriangleList, Triangle.quantity, treugolnik.verts);
            device.EndScene();
            this.Invalidate();
            device.Present();

        }
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }
        private void SetupCamera()
        {
            device.RenderState.CullMode = Cull.None;
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, this.Width / this.Height, 1.0f, 100.0f);
            device.Transform.View = Matrix.LookAtLH(new Vector3(0, 3, 30.0f), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            device.RenderState.Lighting = true;
            device.Transform.World = Matrix.RotationAxis(new Vector3(angle / ((float)Math.PI * 2.0f), angle / ((float)Math.PI * 4.0f), angle / ((float)Math.PI * 6.0f)), angle / (float)Math.PI);           
            angle += 0.05f;
        }

        public void InitializeGraphics()
        {
            // Set our presentation parameters
            PresentParameters presentParams = new PresentParameters();
            presentParams.Windowed = true;
            presentParams.SwapEffect = SwapEffect.Discard;
            // Create our device
            device = new Device(0, DeviceType.Hardware, this, CreateFlags.SoftwareVertexProcessing, presentParams);
        }
    }
}
