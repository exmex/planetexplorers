using UnityEngine;
using System;

namespace CameraForge
{
	[Menu("Math/Vector/Cross",3)]
	public class Cross : FunctionNode
	{
		public Cross ()
		{
			A = new Slot ("A");
			B = new Slot ("B");
		}

		public override Var Calculate ()
		{
			A.Calculate();
			B.Calculate();

			Var a = A.value;
			Var b = B.value;
			Var c = Var.Null;
			
			if (!a.isNull && !b.isNull)
			{
				if (a.type == EVarType.Bool && b.type == EVarType.Bool)
				{
					c = (a.value_b && b.value_b);
				}
				else if (a.type == EVarType.Bool && b.type == EVarType.Int)
				{
					c = (a.value_i * b.value_i);
				}
				else if (a.type == EVarType.Int && b.type == EVarType.Bool)
				{
					c = (a.value_i * b.value_i);
				}
				else if (a.type == EVarType.Int && b.type == EVarType.Int)
				{
					c = (a.value_i * b.value_i);
				}
				else if (a.type == EVarType.Int && b.type == EVarType.Float)
				{
					c = (a.value_f * b.value_f);
				}
				else if (a.type == EVarType.Float && b.type == EVarType.Int)
				{
					c = (a.value_f * b.value_f);
				}
				else if (a.type == EVarType.Float && b.type == EVarType.Float)
				{
					c = (a.value_f * b.value_f);
				}

				else if (a.type == EVarType.Vector && b.type == EVarType.Int)
				{
					c = (a.value_v * b.value_f);
				}
				else if (a.type == EVarType.Vector && b.type == EVarType.Float)
				{
					c = (a.value_v * b.value_f);
				}
				else if (a.type == EVarType.Int && b.type == EVarType.Vector)
				{
					c = (a.value_f * b.value_v);
				}
				else if (a.type == EVarType.Float && b.type == EVarType.Vector)
				{
					c = (a.value_f * b.value_v);
				}
				else if (a.type == EVarType.Vector && b.type == EVarType.Vector)
				{
					c = Vector3.Cross(new Vector3(a.value_v.x, a.value_v.y, a.value_v.z), new Vector3(b.value_v.x, b.value_v.y, b.value_v.z));
				}

				else if (a.type == EVarType.Int && b.type == EVarType.Color)
				{
					c = (a.value_f * b.value_c);
				}
				else if (a.type == EVarType.Float && b.type == EVarType.Color)
				{
					c = (a.value_f * b.value_c);
				}
				else if (a.type == EVarType.Color && b.type == EVarType.Int)
				{
					c = (a.value_c * b.value_f);
				}
				else if (a.type == EVarType.Color && b.type == EVarType.Float)
				{
					c = (a.value_c * b.value_f);
				}
				else if (a.type == EVarType.Color && b.type == EVarType.Color)
				{
					c = Vector3.Cross(new Vector3(a.value_v.x, a.value_v.y, a.value_v.z), new Vector3(b.value_v.x, b.value_v.y, b.value_v.z));
				}
			}

			return c;
		}

		public override Slot[] slots
		{
			get { return new Slot[2] {A, B}; }
		}

		public Slot A;
		public Slot B;
	}
}