using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

namespace Graphics
{
    class Renderer
    {
        Shader sh;
        uint vertexBufferID;
        public void Initialize()
        {

            // Declare the two shaders files to the OpenGl
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader",    // shader of draw positions of geometeric object.
                projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");           // shader of filling colors pf geometeric object.

            Gl.glClearColor(0.211f, 0.588f, 0.541f, 0);

            //initialize vertex to draw the primitives 
            float[] verts = { 
		
                // 1-  Ears			  point index
		        //-----
                0.237f,0.818f, 0.0f,  //left
                0.288f,0.965f, 0.0f,
                0.4f,0.883f, 0.0f,

                0.619f, 0.883f, 0.0f, // right 
                0.718f, 0.972f, 0.0f,  
                0.786f, 0.808f, 0.0f,

                0.266f, 0.815f, 0.0f, // left-inner
                0.302f, 0.923f, 0.0f,
                0.362f, 0.867f, 0.0f,

                0.658f, 0.867f, 0.0f,  //right-inner
                0.706f, 0.925f, 0.0f,
                0.749f, 0.813f, 0.0f,

                //2- Face 
                0.4f, 0.883f, 0.0f,
                0.506f, 0.703f, 0.0f,
                0.621f, 0.886f, 0.0f,

                0.231f, 0.813f, 0.0f,
                0.297f, 0.474f, 0.0f,
                0.718f, 0.477f, 0.0f,
                0.786f, 0.815f, 0.0f,

                //3- Body
                0.298f, 0.47f, 0.0f,
                0.314f, 0.199f, 0.0f,
                0.689f, 0.187f, 0.0f,
                0.719f, 0.47f, 0.0f,

                //4- Legs 
                0.387f, 0.203f, 0.0f,   //left
                0.387f, 0.054f, 0.0f, 
                0.467f, 0.054f, 0.0f, 
                0.467f, 0.203f, 0.0f,

                0.54f, 0.203f, 0.0f,  //right
                0.54f, 0.054f, 0.0f,
                0.63f, 0.054f, 0.0f,
                0.63f, 0.203f, 0.0f,

                //5- Eays 
                0.368f, 0.673f, 0.0f,
                0.664f, 0.673f, 0.0f,

                //6- Nose 
                0.461f, 0.629f, 0.0f,
                0.506f, 0.591f, 0.0f,
                0.548f, 0.631f, 0.0f,

            };

            // ID is refernece to the vertex buffer of points 
            vertexBufferID = GPU.GenerateBuffer(verts);
        }

        

        public void Draw()
        {

            // Determine the background Color of the Buffer.
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            // call the vertex and fragement shader that will be used
            sh.UseShader();
            // enable vertex attributes using pointer(0)
            Gl.glEnableVertexAttribArray(0);
            // convert the array to the vertex points (x,y,z)
            //describe vertex attributes (index, size, arrayType, normalized, stride, offset)
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 0, IntPtr.Zero);


            //// Draw your primitives !
            //// 1-  Ears        
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, 3);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 3, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 6, 3);
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 9, 3);

            //// 2-  Face
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 12, 3);
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 15, 4);

            //// 3- Body
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 19, 4);

            //// 4- Legs(left)
            Gl.glDrawArrays(Gl.GL_POLYGON, 23, 4);

            //// 4- Legs(right)
            Gl.glDrawArrays(Gl.GL_POLYGON, 27, 4);

            //// 5- Eays 
            Gl.glDrawArrays(Gl.GL_POINTS, 31, 2);

            ///// 6- Nose
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 33, 3);

            Gl.glDisableVertexAttribArray(0);
        }

        public void Update()
        {

        }

        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
