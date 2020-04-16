using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchemaConverter;
using System.Linq;

namespace SchemaConverterTest
{
	[TestClass]
	public class DocxParserTest
	{
		[TestMethod]
		[DataRow("Some class [its-key]", "Some class", "its-key")]
		public void GetClassFrom_ValidString_ReturnsClassInstants(string expression, string name, string iriAddress)
		{
			var item = DocxParser.GetClassFrom(expression);

			Assert.AreEqual(item.IriAddress, iriAddress);
			Assert.AreEqual(item.Name, name);
		}

		[TestMethod]
		[DataRow("Some class +[its-key]")]
		[DataRow("Some class [iasd asd d ts-key]")]
		public void GetClassFrom_InvalidString_ReturnsNull(string expression)
		{
			var item = DocxParser.GetClassFrom(expression);

			Assert.IsNull(item);
		}

		[TestMethod]
		public void GetAnnotationPropertyFrom_ValidExpression_ReturnsAnnotationPropertyInstant()
		{
			var (item, list) = DocxParser.GetAnnotationPropertyFrom("Some property [propaddress] [someclassaddr [its value]]");

			Assert.AreEqual(item.IriAddress, "propaddress");
			Assert.AreEqual(item.Name, "Some property");
			Assert.AreEqual(list[0].Item1, "someclassaddr");
			Assert.AreEqual(list[0].Item2, "its value");
		}

		[TestMethod]
		public void GetAnnotationPropertyFrom_InvalidExpression_ReturnsNulls()
		{
			var (item, list) = DocxParser.GetAnnotationPropertyFrom("Some property [propaddress] [someclassaddr its value]]");

			Assert.IsNull(item);
			Assert.IsNull(list);
		}
	}
}
