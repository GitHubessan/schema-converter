using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaConverter
{
	public class Class
	{
		public string IriAddress { get; set; }

		public string Name { get; set; }

		public string Language { get; set; } = "ru";

		public int IndentLevel { get; set; }

		public List<Class> ParentClasses { get; } = new List<Class>();

		public Dictionary<AnnotationProperty, string> AnnotationProperties { get; }
			= new Dictionary<AnnotationProperty, string>();

		public override string ToString() => Name;
	}
}
