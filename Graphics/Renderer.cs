using System;
using System.Collections.Generic;
using System.IO;
using Tao.OpenGl;

namespace Graphics
{
    class Renderer
    {
        Shader sh;
        uint vertexBufferID;
        uint colorBufferID;
        List<uint> curveBufferIDs = new List<uint>();

        private void GenerateAndBufferCurves(List<float[]> points, int segments)
        {
            for (int i = 0; i < points.Count; i += 3)
            {
                if (i + 2 >= points.Count) // Ensure there are enough points  
                    break;

                var curvePoints = GenerateBezierCurve(points[i], points[i + 1], points[i + 2], segments);
                uint bufferID = GPU.GenerateBuffer(curvePoints.ToArray());
                curveBufferIDs.Add(bufferID); // Store the buffer ID  
            }
        }

        public void Initialize()
        {
            // Declare the two shaders files to OpenGL  
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader",
                projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");
            Gl.glClearColor(0.211f, 0.588f, 0.541f, 0); // Background color  

            float[] verts = {
            -0.497f,0.391f,0f, //outer left ear
            -0.4f,0.795f,0f,
            -0.2f,0.577f,0f,

            0.211f,0.571f,0f,//outer right ear
            0.406f,0.801f,0f,
            0.531f,0.379f,0f,

            -0.439f,0.385f,0f,//inner left ear
            -0.389f,0.692f,0f,
            -0.272f,0.526f,0f,

            0.461f,0.385f,0f, //inner right ear
            0.306f,0.513f,0f,
            0.394f,0.667f,0f,


             0.011f, -0.186f,0f,//nose
            -0.075f, -0.124f,0f,
            0.006f, -0.061f,0f,
            0.097f, -0.118f,0f,

            -0.324f,  0.097f, 0.0f, // left eye
            -0.304f, -0.009f, 0.0f,
            -0.225f, -0.044f, 0.0f,
            -0.14f,  0.005f, 0.0f,
            -0.103f,  0.09f,  0.0f,
            -0.128f,  0.189f, 0.0f,
            -0.208f,  0.224f, 0.0f,
            -0.293f,  0.196f, 0.0f,

            0.137f, 0.118f, 0.0f, // right eye
            0.157f, 0.034f, 0.0f,
            0.196f, -0.023f, 0.0f,
            0.259f, -0.03f, 0.0f,
            0.321f, 0.034f, 0.0f,
            0.352f, 0.118f, 0.0f,
            0.304f, 0.203f, 0.0f,
            0.219f, 0.217f, 0.0f,
            0.151f, 0.161f, 0.0f,

            //line undernose
            0.011f, -0.186f,0f,
            0.011f, -0.270f,0f,
            };
            vertexBufferID = GPU.GenerateBuffer(verts);


            // Define control points for Bézier curves
            List<float[]> points = new List<float[]>
            {  
                // Left face curve  
                new float[] { -0.497f, 0.391f },
                new float[] { -0.525f, -0.329f },
                new float[] { -0.389f, -0.564f },  
                
                // Chin curve  
                new float[] { -0.389f, -0.564f },
                new float[] { 0.006f, -0.905f },
                new float[] { 0.417f, -0.571f },  
                
                // Right face curve  
                new float[] { 0.417f, -0.571f },
                new float[] { 0.553f, -0.342f },
                new float[] { 0.531f, 0.379f },  
                
                // Curve between ears  
                new float[] { -0.2f, 0.577f },
                new float[] { 0f, 0.621f },
                new float[] { 0.211f, 0.571f },  
                
                // Left upper mustache  
                new float[] { -0.294f, -0.217f },
                new float[] { -0.639f, -0.18f },
                new float[] { -0.911f, -0.248f },  
                
                // Left middle mustache  
                new float[] { -0.311f, -0.28f },
                new float[] { -0.656f, -0.36f },
                new float[] { -0.953f, -0.553f },  
                
                // Left lower mustache  
                new float[] { -0.281f, -0.329f },
                new float[] { -0.578f, -0.528f },
                new float[] { -0.778f, -0.789f },  
                
                // Right upper mustache  
                new float[] { 0.292f, -0.255f },
                new float[] { 0.617f, -0.161f },
                new float[] { 0.958f, -0.242f },  
                
                // Right middle mustache  
                new float[] { 0.328f, -0.286f },
                new float[] { 0.689f, -0.354f },
                new float[] { 0.933f, -0.547f },  
                
                // Right lower mustache  
                new float[] { 0.311f, -0.354f },
                new float[] { 0.6f, -0.528f },
                new float[] { 0.828f, -0.87f },  
                
                // Left curve body  
                new float[] { -0.389f, -0.564f },
                new float[] { -0.472f, -0.861f },
                new float[] { -0.469f, -1f },  
                
                // Right curve body  
                new float[] { 0.417f, -0.571f },
                new float[] { 0.492f, -0.776f },
                new float[] { 0.486f, -1f },  
                
                // Curve nose left  
                new float[] { 0.011f, -0.270f },
                new float[] { -0.050f, -0.326f },
                new float[] { -0.081f, -0.226f },  
                
                // Curve nose right  
                new float[] { 0.011f, -0.270f },
                new float[] { 0.070f, -0.326f },
                new float[] { 0.111f, -0.226f },
            };
            GenerateAndBufferCurves(points, 50);

            float[] colors = {
                1.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f,
                1.0f, 0.0f, 0.0f,
            };
            colorBufferID = GPU.GenerateBuffer(colors);
        }

        private List<float> GenerateBezierCurve(float[] p0, float[] p1, float[] p2, int segments)
        {
            var points = new List<float>();
            for (int i = 0; i <= segments; i++)
            {
                float t = i / (float)segments;
                float oneMinusT = 1 - t;

                float x = oneMinusT * oneMinusT * p0[0] + 2 * oneMinusT * t * p1[0] + t * t * p2[0];
                float y = oneMinusT * oneMinusT * p0[1] + 2 * oneMinusT * t * p1[1] + t * t * p2[1];

                points.Add(x);
                points.Add(y);
                points.Add(0.0f); // Z-coordinate for 2D  
            }
            return points;
        }


        public void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            sh.UseShader();

            Gl.glEnableVertexAttribArray(0);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, vertexBufferID);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 0, IntPtr.Zero);

            // Enable vertex color attribute
            Gl.glEnableVertexAttribArray(1);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, colorBufferID);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 0, IntPtr.Zero);

            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, 3);//outer left ear
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 3, 3);//outer right ear
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 6, 3); //inner left ear
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 9, 3);//inner right ear
            Gl.glDrawArrays(Gl.GL_TRIANGLE_FAN, 12, 4); // nose
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 16, 8);  // left eye
            Gl.glDrawArrays(Gl.GL_LINE_LOOP, 24, 9);  // rith eye
            Gl.glDrawArrays(Gl.GL_LINES, 33, 2);      // mouse
            Gl.glColor3f(0.9f, 0.0f, 0.0f);
            DrawCircle(-0.2f, 0.1f, 0.02f); // Left pupil eye 
            DrawCircle(0.2f, 0.1f, 0.02f);  // Right pupil eye 
            foreach (var curveBufferID in curveBufferIDs)
            {
                DrawCurve(curveBufferID, 51);
            }

            Gl.glDisableVertexAttribArray(0);
        }

        private void DrawCurve(uint curveBufferID, int pointCount)
        {
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, curveBufferID);
            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 0, IntPtr.Zero);

            // Draw the curve using the line strip mode  
            Gl.glDrawArrays(Gl.GL_LINE_STRIP, 0, pointCount);

            // Clean up  
            Gl.glDisableVertexAttribArray(0);
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, 0); // Unbind the buffer  
        }

        private void DrawCircle(float x, float y, float radius)
        {
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            for (float angle = 0; angle <= 2 * Math.PI; angle += 0.1f)
            {
                float dx = radius * (float)Math.Cos(angle);
                float dy = radius * (float)Math.Sin(angle);
                Gl.glVertex2f(x + dx, y + dy);
            }
            Gl.glEnd();
        }

        public void Update()
        {
            // Update logic (if needed)
        }

        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
