using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchemaConverter
{
	public class XmlCreator
	{
		private DataResult DataResult { get; set; }

		public XmlCreator(DataResult dataResult)
		{
			DataResult = dataResult;
		}

		public XDocument Create()
		{
			XNamespace bs = "http://webprotege.stanford.edu/project/B71Enl7pEVBgGstNnbhPEz";
			XNamespace owl = "http://www.w3.org/2002/07/owl#";
			XNamespace rdf = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
			XNamespace xxml = "http://www.w3.org/XML/1998/namespace";
			XNamespace rdfs = "http://www.w3.org/2000/01/rdf-schema#";
			XNamespace xmlns = "http://webprotege.stanford.edu/project/B71Enl7pEVBgGstNnbhPEz#";
			XNamespace webprotege = "http://webprotege.stanford.edu/";

			var xml = new XDocument(
				new XDeclaration("1.0", null, null),
				new XElement(
					rdf + "RDF",
					new XAttribute("xmlns", xmlns),
					new XAttribute(XNamespace.Xml + "base", bs),
					new XAttribute(XNamespace.Xmlns + "owl", owl),
					new XAttribute(XNamespace.Xmlns + "rdf", rdf),
					new XAttribute(XNamespace.Xmlns + "xml", xxml),
					new XAttribute(XNamespace.Xmlns + "rdfs", rdfs),
					new XAttribute(XNamespace.Xmlns + "webprotege", webprotege)
				)
			);

			xml.Root.Add(
				new XElement(
					owl + "Ontology",
					new XAttribute(rdf + "about", "http://webprotege.stanford.edu/project/B71Enl7pEVBgGstNnbhPEz")
				)
			);

			foreach (var i in DataResult.AnnotationProperties) {
				xml.Root.Add(
					new XComment(i.IriAddress)
				);

				xml.Root.Add(
					new XElement(
						owl + "AnnotationProperty",
						new XAttribute(rdf + "about", webprotege.ToString() + i.IriAddress),
						new XElement(
							rdfs + "label",
							new XAttribute(xxml + "lang", i.Language),
							i.Name
						)
					)
				);
			}

			foreach (var i in DataResult.Classes) {
				var item = new XElement(
					owl + "Class",
					new XAttribute(rdf + "about", webprotege.ToString() + i.IriAddress)
				);

				if (i.ParentClasses.Count != 0) {
					item.Add(
						new XElement(
							rdfs + "subClassOf",
							new XAttribute(rdf + "resource", webprotege.ToString() + i.ParentClasses[0].IriAddress)
						)
					);
				}

				foreach (var j in i.AnnotationProperties.Keys) {
					item.Add(
						new XElement(
							webprotege + j.IriAddress,
							new XAttribute(xxml + "lang", i.Language),
							i.AnnotationProperties[j]
						)
					);
				}

				item.Add(
					new XElement(
						rdfs + "label",
						new XAttribute(xxml + "lang", i.Language),
						i.Name
					)
				);

				xml.Root.Add(
					new XComment(i.IriAddress)
				);
				xml.Root.Add(item);
			}

			return xml;
		}
	}
}
