using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceMeGL
{
	public class Shader
	{
		private int id = 0;
		public int Id { get { return id; } }
		private ShaderType type;

		public Shader(string source, ShaderType type)
		{
			this.type = type;
			id = GL.CreateShader(type);
			if (id == 0)
				throw new Exception("Could not create shader object.");

			GL.ShaderSource(id, source);

			GL.CompileShader(id);
			Validate();
		}

		void Validate()
		{
			int compileResult;
			GL.GetShader(id, ShaderParameter.CompileStatus, out compileResult);
			if (compileResult == 0) 
			{
				throw new Exception("Unknown shader compile error: " + GL.GetShaderInfoLog(id));
			}
		}
	}
}
