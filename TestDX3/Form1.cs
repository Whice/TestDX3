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
            CustomVertex.PositionNormalColored[] verts = new CustomVertex.PositionNormalColored[3];
            verts[0].Position=new Vector3(0.0f, 1.0f, 1.0f);
            verts[0].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts[0].Color = System.Drawing.Color.White.ToArgb();
            verts[1].Position=new Vector3(-1.0f, -1.0f, 1.0f);
            verts[1].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts[1].Color = System.Drawing.Color.White.ToArgb();
            verts[2].Position = new Vector3(1.0f, -1.0f, 1.0f);
            verts[2].Normal = new Vector3(0.0f, 0.0f, -1.0f);
            verts[2].Color = System.Drawing.Color.White.ToArgb();
            device.Lights[0].Type = LightType.Point;
            device.Lights[0].Position = new Vector3(0,0,0);
            device.Lights[0].Diffuse = System.Drawing.Color.Coral;
            device.Lights[0].Attenuation0 = 0.5f;
            device.Lights[0].Range = 1.5f;
            device.Lights[0].Enabled = true;
            device.Lights[0].Update();
            device.BeginScene();
            device.VertexFormat = CustomVertex.PositionColored.Format;
            device.DrawUserPrimitives(PrimitiveType.TriangleList, 1, verts);
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
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI/4, this.Width / this.Height, 1.0f, 100.0f);
            device.Transform.View = Matrix.LookAtLH(new Vector3(0,3, 5.0f), new Vector3(), new Vector3(0,1,0));
            device.RenderState.Lighting = true;
            /*device.Transform.World = Matrix.RotationZ((float)Math.PI / 6.0f);
            device.Transform.World = Matrix.RotationZ(angle / (float)Math.PI);*/
            device.Transform.World = Matrix.RotationAxis(new Vector3(angle / ((float)Math.PI * 2.0f), angle / ((float)Math.PI * 4.0f), angle / ((float)Math.PI * 6.0f)), angle / (float)Math.PI);
            angle += 0.05f;
            /*device.Transform.World = Matrix.RotationZ((System.Environment.TickCount/ 450.0f)/ (float)Math.PI);
            device.Transform.World = Matrix.RotationY((System.Environment.TickCount / 450.0f) / (float)Math.PI);*/
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
