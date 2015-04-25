using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMeGL
{
	public class ShaderProgram
	{
		int id;
		public int ID { get { return id; } }

		public ShaderProgram(params Shader[] shaders)
		{
			id = GL.CreateProgram();
			foreach (var s in shaders)
			{
				GL.AttachShader(id, s.Id);
			}
			GL.LinkProgram(id);
			Validate();
		}



		void Validate()
		{
			int compileResult;
			GL.GetProgram(id, ProgramParameter.LinkStatus, out compileResult);
			if (compileResult == 0)
			{
				throw new Exception("Unknown shader compile error: " + GL.GetProgramInfoLog(id));
			}
		}
	}
}
