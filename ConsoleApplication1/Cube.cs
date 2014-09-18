using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;


/***********************************************
 * 
 *This is an extension of the hellocube from :
 *http://www.opentk.com/node/2873
 */
namespace ConsoleApplication1
{
    enum Directions {north, south, east, west, up, down}
    class Cube
    {
        float[] cubeColors;
        byte[] triangles;
        float[] cube;
        public Cube()
        {
            cubeColors = new float[] {
			1.0f, 0.0f, 0.0f, 1.0f,
			0.0f, 1.0f, 0.0f, 1.0f,
			0.0f, 0.0f, 1.0f, 1.0f,
			0.0f, 1.0f, 1.0f, 1.0f,
			1.0f, 0.0f, 0.0f, 1.0f,
			0.0f, 1.0f, 0.0f, 1.0f,
			0.0f, 0.0f, 1.0f, 1.0f,
			0.0f, 1.0f, 1.0f, 1.0f,
		};

            triangles = new byte[]
		{
			1, 0, 2, // front
			3, 2, 0,
			6, 4, 5, // back
			4, 6, 7,
			4, 7, 0, // left
			7, 3, 0,
			1, 2, 5, //right
			2, 6, 5,
			0, 1, 5, // top
			0, 5, 4,
			2, 3, 6, // bottom
			3, 7, 6,
		};

             cube = new float[] {
			-0.5f,  0.5f,  0.5f, // vertex[0]
			 0.5f,  0.5f,  0.5f, // vertex[1]
			 0.5f, -0.5f,  0.5f, // vertex[2]
			-0.5f, -0.5f,  0.5f, // vertex[3]
			-0.5f,  0.5f, -0.5f, // vertex[4]
			 0.5f,  0.5f, -0.5f, // vertex[5]
			 0.5f, -0.5f, -0.5f, // vertex[6]
			-0.5f, -0.5f, -0.5f, // vertex[7]
		};
        }
        public void draw()
        {
            GL.VertexPointer(3, VertexPointerType.Float, 0, cube);
            GL.ColorPointer(4, ColorPointerType.Float, 0, cubeColors);
            GL.DrawElements(BeginMode.Triangles, 36, DrawElementsType.UnsignedByte, triangles);

        }
        public void MoveBlock(float NorthMoveBlocks, float EastMoveBlocks, float UpMoveBlocks)
        {   
            for (int i=0; i < cube.Length; i += 3)
            {
                cube[i] += EastMoveBlocks;
            }
            for (int i = 1; i < cube.Length; i += 3)
            {
                cube[i] += UpMoveBlocks;
            }
            for (int i = 2; i < cube.Length; i += 3)
            {
                cube[i] += NorthMoveBlocks;
            } 
        }   

    }
}
