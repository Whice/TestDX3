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

        private Device device = null;
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            device.Clear(ClearFlags.Target, System.Drawing.Color.BurlyWood,
            1.0f, 0);
            //device.DeviceResizing += new CancelEventHandler(this.CancelResize);
            SetupCamera();
            //Triangle 0
            CustomVertex.PositionNormalColored[] verts = new CustomVertex.PositionNormalColored[3];
            verts[0].Position=new Vector3(0.0f, 0.0f, 0.0f);
            verts[0].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts[0].Color = System.Drawing.Color.White.ToArgb();
            verts[1].Position=new Vector3(1.0f, -2.0f, 0.5f);
            verts[1].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts[1].Color = System.Drawing.Color.White.ToArgb();
            verts[2].Position = new Vector3(-1.0f, -2.0f, 0.5f);
            verts[2].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts[2].Color = System.Drawing.Color.White.ToArgb();
            //Triangle 1
            /*CustomVertex.PositionNormalColored[] verts2 = new CustomVertex.PositionNormalColored[3];
            verts2[0].Position = new Vector3(0.0f, 0.0f, 0.0f);
            verts2[0].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts2[0].Color = System.Drawing.Color.White.ToArgb();
            verts2[1].Position = new Vector3(1.0f, -2.0f, 0.5f);
            verts2[1].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts2[1].Color = System.Drawing.Color.White.ToArgb();
            verts2[2].Position = new Vector3(0.0f, -2.0f, -1.0f);
            verts2[2].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts2[2].Color = System.Drawing.Color.White.ToArgb();
            //Triangle 2
            CustomVertex.PositionNormalColored[] verts3 = new CustomVertex.PositionNormalColored[3];
            verts3[0].Position = new Vector3(0.0f, 0.0f, 0.0f);
            verts3[0].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts3[0].Color = System.Drawing.Color.White.ToArgb();
            verts3[1].Position = new Vector3(-1.0f, -2f, 0.5f);
            verts3[1].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts3[1].Color = System.Drawing.Color.White.ToArgb();
            verts3[2].Position = new Vector3(0.0f, -2.0f, -1.0f);
            verts3[2].Normal = new Vector3(0.0f, -0.0f, -1.0f);
            verts3[2].Color = System.Drawing.Color.White.ToArgb();
            //Triangle 3
            CustomVertex.PositionNormalColored[] verts4 = new CustomVertex.PositionNormalColored[3];
            verts4[0].Position = new Vector3(1.0f, -2.0f, 0.5f);
            verts4[0].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts4[0].Color = System.Drawing.Color.White.ToArgb();
            verts4[1].Position = new Vector3(-1.0f, -2.0f, 0.5f);
            verts4[1].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts4[1].Color = System.Drawing.Color.White.ToArgb();
            verts4[2].Position = new Vector3(0.0f, -2.0f, -1.0f);
            verts4[2].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts4[2].Color = System.Drawing.Color.White.ToArgb();*/

            device.Lights[0].Type = LightType.Point;
            device.Lights[0].Position = new Vector3(5, 5, 5);
            device.Lights[0].Diffuse = System.Drawing.Color.White;
            device.Lights[0].Attenuation0 = 100f;
            device.Lights[0].Range = 10000f;
            device.Lights[0].Enabled = true;
            device.Lights[0].Update();
            /*
            device.Lights[1].Type = LightType.Point;
            device.Lights[1].Position = new Vector3(0, 5, 0);
            device.Lights[1].Diffuse = System.Drawing.Color.LightBlue;
            device.Lights[1].Attenuation0 = 1f;
            device.Lights[1].Range = 10f;
            device.Lights[1].Enabled = true;
            device.Lights[1].Update();

            device.Lights[2].Type = LightType.Point;
            device.Lights[2].Position = new Vector3(0, 0, 5); ;
            device.Lights[2].Diffuse = System.Drawing.Color.LightGreen;
            device.Lights[2].Attenuation0 = 1f;
            device.Lights[2].Range = 10f;
            device.Lights[2].Enabled = true;
            device.Lights[2].Update();*/

            device.BeginScene();
            device.VertexFormat = CustomVertex.PositionNormalColored.Format;
            device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, verts);
            /*device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, verts2);
            device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, verts3);
            device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, verts4);*/
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
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI/4, this.Width / this.Height, 1f, 100.0f);
            device.Transform.View = Matrix.LookAtLH(new Vector3(0, 10, 5.0f), new Vector3(), new Vector3(0,1,0));
            device.RenderState.Lighting = true;
            /*device.Transform.World = Matrix.RotationZ((float)Math.PI / 6.0f);
            device.Transform.World = Matrix.RotationZ(angle / (float)Math.PI);*/
            device.Transform.World = Matrix.RotationAxis(new Vector3(angle / ((float)Math.PI * 2.0f), angle / ((float)Math.PI * 4.0f), angle / ((float)Math.PI * 6.0f)), angle / (float)Math.PI);           
            angle += 0.05f;
            //device.Transform.World = Matrix.RotationZ((System.Environment.TickCount/ 450.0f)/ (float)Math.PI);
            //device.Transform.World = Matrix.RotationX((System.Environment.TickCount / 450.0f) / (float)Math.PI);
        }
        /*private void CancelResize(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }*/
        //private 
        /// <summary>
        /// We will initialize our graphics device here
        /// </summary>
        public void InitializeGraphics()
        {
            // Set our presentation parameters
            PresentParameters presentParams = new PresentParameters();
            presentParams.Windowed = true;
            presentParams.SwapEffect = SwapEffect.Discard;
            // Create our device
            device = new Device(0, DeviceType.Hardware, this,
            CreateFlags.SoftwareVertexProcessing, presentParams);
        }
    }
}
