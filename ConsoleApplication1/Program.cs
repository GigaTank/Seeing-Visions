using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTkPractice
{

    class Game : GameWindow
    {

        Camera mycamera = new Camera();
        MouseState current, previous;
        #region World information

        World myworld = new World(); 

        #endregion

        Matrix4 matrixProjection, matrixModelview;
        float cameraRotation = 0f;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            VSync = VSyncMode.On;
            Title = "Testing Title";
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.EnableClientState(EnableCap.VertexArray);
            GL.EnableClientState(EnableCap.ColorArray);
            //mycube2.MoveBlock(0,0,1);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            matrixProjection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1f, 100f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref matrixProjection);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //GL.LoadIdentity();
            #region Camera

            
            //cameraRotation = (cameraRotation < 360f) ? (cameraRotation + 1f * (float)e.Time) : 0f;
            //Matrix4.CreateRotationY(cameraRotation, out matrixModelview);
            //matrixModelview *= Matrix4.LookAt(0f, 0f, -5f, 0f, 0f, 0f, 0f, 1f, 0f);
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadMatrix(ref matrixModelview);
            mycamera.Render();
            #endregion

            #region World
            ////GL.Translate(0.0, -0.5, -6.0);

            //GL.Scale(30.0, 10.0, 30.0);
            
            //float size = 2.0f;
            //int LinesX = 30;
            //int LinesZ = 30;
            
            //float halfsize = (float)(size / 2.0);
            ////Draw the "world":

            //GL.Color3(1.0, 1.0, 1.0);
            //GL.PushMatrix();
            //GL.Translate(0.0, -halfsize, 0.0);
            //DrawNet(size, LinesX, LinesZ);
            //GL.Translate(0.0, size, 0.0);
            //DrawNet(size, LinesX, LinesZ);
            //GL.PopMatrix();
            //GL.Color3(0.0, 0.0, 1.0);
            //GL.PushMatrix();
            //GL.Translate(-halfsize, 0.0, 0.0);
            //GL.Rotate(90.0, 0.0, 0.0, halfsize);
            //DrawNet(size, LinesX, LinesZ);
            //GL.Translate(0.0, -size, 0.0);
            //DrawNet(size, LinesX, LinesZ);
            //GL.PopMatrix();
            //GL.Color3(1.0, 0.0, 0.0);
            //GL.PushMatrix();
            //GL.Translate(0.0, 0.0, -halfsize);
            //GL.Rotate(90.0, halfsize, 0.0, 0.0);
            //DrawNet(size, LinesX, LinesZ);
            //GL.Translate(0.0, size, 0.0);
            //DrawNet(size, LinesX, LinesZ);
            //GL.PopMatrix();
            myworld.draw();
            //mycube.draw();
            //mycube2.draw();
            #endregion
            SwapBuffers();
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            current = OpenTK.Input.Mouse.GetState();
                    if (current != previous)
                    {
                        // Mouse state has changed
                        int xdelta = current.X - previous.X;
                        int ydelta = current.Y - previous.Y;
                        int zdelta = current.Wheel - previous.Wheel;
                        //OpenTK.Input.Mouse.SetPosition(960, 540);
                        
                        if (xdelta != 0)
                        {
                            mycamera.RotateY(xdelta/10);
                            //Console.WriteLine("Move X:" + xdelta);
                        }
                        if (ydelta != 0)
                        {
                            mycamera.RotateX(-ydelta/10);
                            //Console.WriteLine("Move Y:" + ydelta);
                        }
                    }
                    current = OpenTK.Input.Mouse.GetState();
                    previous = current;
        }
        [STAThread]
        private static void Main(string[] args)
        {
            
            using (Game game = new Game())
            {

                    game.Run(60);
            }
        }
        void DrawNet(float size, int LinesX, int LinesZ)
        {
            GL.Begin(PrimitiveType.Lines);
            for (int xc = 0; xc < LinesX; xc++)
            {
                GL.Vertex3(-size / 2.0 + xc / (float)(LinesX - 1) * size,
                             0.0,
                             size / 2.0);
                GL.Vertex3(-size / 2.0 + xc / (float)(LinesX - 1) * size,
                            0.0,
                            size / -2.0);
            }
            for (int zc = 0; zc < LinesX; zc++)
            {
                GL.Vertex3(size / 2.0,
                            0.0,
                            -size / 2.0 + zc / (float)(LinesZ - 1) * size);
                GL.Vertex3(size / -2.0,
                            0.0,
                            -size / 2.0 + zc / (float)(LinesZ - 1) * size);
            }
            GL.End();
        }
    }
}
