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
        private Device device = null;
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            device.Clear(ClearFlags.Target, System.Drawing.Color.Beige,
            1.0f, 0);
            CustomVertex.PositionColored[] verts = new CustomVertex.PositionColored[3];
            verts[0].Position=new Vector3(0.0f, 1.0f, 1.0f);
            verts[0].Color = System.Drawing.Color.Aqua.ToArgb();
            verts[1].Position=new Vector3(-1.0f, -1.0f, 1.0f);
            verts[1].Color = System.Drawing.Color.Black.ToArgb();
            verts[2].Position=new Vector3(1.0f, -1.0f, 1.0f);
            verts[2].Color = System.Drawing.Color.Purple.ToArgb();
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
