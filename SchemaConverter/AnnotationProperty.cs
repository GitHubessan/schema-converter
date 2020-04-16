using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaConverter
{
	public class AnnotationProperty
	{
		public string IriAddress { get; set; }

		public string Name { get; set; }

		public string Language { get; set; } = "ru";

		public override string ToString() => Name;
	}
}
