using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;


/*
 * 
 * This camera class is an extension/ C# version of the camera class 
 * from 
 * Advanced CodeColony Camera
 * Philipp Crocoll, 2003
 * 
 * Used with permission based on source code in Main in the camera project
 * 
 * "This tutorial was written by Philipp Crocoll
  Contact: 
	philipp.crocoll@web.de TODO: Email him and thank him for the guidance.
	www.codecolony.de

  Every comment would be appreciated.

  If you want to use parts of any code of mine:
	let me know and
	use it!" 
*/
namespace OpenTkPractice
{
    class Camera
    {
	
	Vector3d ViewDir;
	Vector3d RightVector;	
	Vector3d UpVector;
	Vector3d Position;
    Vector3d ActualUp;
    bool isgrounded; 

	float RotatedX, RotatedY, RotatedZ;	
	public Camera()
    {
        Position = new Vector3d(0, 0, 0);
        ViewDir = new Vector3d(0,0,-1);
        RightVector = new Vector3d (1.0, 0.0, 0.0);
        UpVector = new Vector3d (0.0, 1.0, 0.0);
        ActualUp = new Vector3d(0.0, 1.0, 0.0);
        RotatedX = RotatedY = RotatedZ = 0.0f;
        isgrounded = true; 

    }
	public void Render ()	//executes some glRotates and a glTranslate command
 	{					//Note: You should call glLoadIdentity before using Render 
	    Vector3d ViewPoint = Position + ViewDir;
        Matrix4 lookat; 
        if (isgrounded)
        {
            lookat = Matrix4.LookAt((float)Position.X, (float)Position.Y, (float)Position.Z,
                            (float)ViewPoint.X, (float)ViewPoint.Y, (float)ViewPoint.Z,
                            (float)ActualUp.X, (float)ActualUp.Y, (float)ActualUp.Z);
        }
        else
        {
            lookat = Matrix4.LookAt((float)Position.X, (float)Position.Y, (float)Position.Z,
                                        (float)ViewPoint.X, (float)ViewPoint.Y, (float)ViewPoint.Z,
                                        (float)UpVector.X, (float)UpVector.Y, (float)UpVector.Z);
        }
        //Console.WriteLine("Lookat:" + (float)Position.X +  (float)Position.Y +  (float)Position.Z + "\n" 
                                       //+ (float)ViewPoint.X + (float)ViewPoint.Y+ (float)ViewPoint.Z + "\n"
                                       //+ (float)UpVector.X+ (float)UpVector.Y+ (float)UpVector.Z + "\n");
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadMatrix(ref lookat);

    }
    public void Move ( Vector3d Direction )
    {
        Position = Position + Direction;
    }
	public void RotateX ( float Angle )
    {
        RotatedX += Angle;
	    Vector3d temp;
	    //Rotate viewdir around the right vector:
        {
            temp = (ViewDir * Math.Cos(Angle * Math.PI / 180) 
                    + UpVector * Math.Sin(Angle * Math.PI / 180));
        }
        ViewDir = temp.Normalized();
	    //now compute the new UpVector (by cross product)
	    UpVector = Vector3d.Cross(ViewDir, RightVector)*-1;
    }
	public void RotateY ( float Angle )
    {
        RotatedY += Angle;
	    //Rotate viewdir around the upvector:
        Vector3d temp = (ViewDir * Math.Cos(Angle * Math.PI / 180) + RightVector * Math.Sin(Angle * Math.PI / 180));
        ViewDir = temp.Normalized();
	    //now compute the new RightVector (by cross product)
	    RightVector = Vector3d.Cross(ViewDir, UpVector);
        //RightVector.Z = Position.Z;

    }
    public void RotateZ ( float  Angle )
    {
        RotatedZ += Angle;
	    //Rotate viewdir around the right vector:
	    Vector3d temp = (RightVector*Math.Cos(Angle*Math.PI/180) 
                         + UpVector*Math.Sin(Angle*Math.PI/180));
        RightVector = temp.Normalized();
        //RightVector.Z = Position.Z;
	    //now compute the new UpVector (by cross product)
	    UpVector = Vector3d.Cross(ViewDir, RightVector)*-1;
    }
    
	public void MoveForward ( float  Distance )
    {
        Position = Position + (ViewDir*-Distance);
    }
	public void MoveUpward ( float Distance )
    {
        Position = Position + (UpVector*Distance);
    }
	public void StrafeRight ( float Distance )
    {
        Position = Position + (RightVector*Distance);
    }

    
    }
}