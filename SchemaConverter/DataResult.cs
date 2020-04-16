using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchemaConverter
{
	public class DataResult
	{
		public bool OccurException { get; private set; }

		public string ExceptionMessage { get; private set; }

		public List<Class> Classes { get; } = new List<Class>();

		public List<AnnotationProperty> AnnotationProperties { get; } = new List<AnnotationProperty>();

		public DataResult(bool occurException, string exceptionMessage)
		{
			OccurException = occurException;
			ExceptionMessage = exceptionMessage;
		}

		public DataResult() : this(false, "") { }
	}
}
