using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;


namespace OpenTkPractice
{
    class World
    {
        List<Cube> Cubes;
        public World()
        {
            Cubes = new List<Cube>();
          for (float i = -100; i < 100; i++)
          {
              for (float j = -100; j < 100; j++)
              
                  Cubes.Add(new Cube(new Vector3(i, -1.8f, j)));
              }
          }
        public void draw()
        {
            foreach (Cube itercube in Cubes)
            {
                itercube.draw();
            }
        }
    }
}
