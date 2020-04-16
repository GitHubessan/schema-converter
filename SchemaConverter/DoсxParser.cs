using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xceed.Words.NET;

namespace SchemaConverter
{
	public class DocxParser
	{
		private DocX Doc { get; set; }

		public DocxParser(string pathToDoc)
		{
			Doc = DocX.Load(pathToDoc);
		}

		public DataResult Parse()
		{
			if (Doc.Lists.Count != 2) {
				return new DataResult(true, "Неверное число список в DOCX-файле.");
			}

			var dataResult = new DataResult();
			var lastItems = new Stack<Class>();

			foreach (var i in Doc.Lists[0].Items) {
				var item = GetClassFrom(i.Text);

				if (item == null) {
					return new DataResult(true, $"Возникла ошибка при чтении строки \"{i.Text}\".");
				}

				item.IndentLevel = i.IndentLevel ?? 0;
				dataResult.Classes.Add(item);

				if (lastItems.Count == 0) {
					lastItems.Push(item);
				}
				else if (lastItems.Peek().IndentLevel == item.IndentLevel) {
					lastItems.Pop();

					if (lastItems.Count != 0 && lastItems.Peek().IndentLevel < item.IndentLevel) {
						item.ParentClasses.Add(lastItems.Peek());
					}

					lastItems.Push(item);
				}
				else if (lastItems.Peek().IndentLevel < item.IndentLevel) {
					item.ParentClasses.Add(lastItems.Peek());
					lastItems.Push(item);
				}
				else if (lastItems.Peek().IndentLevel > item.IndentLevel) {
					while (lastItems.Count != 0 && lastItems.Peek().IndentLevel >= item.IndentLevel) {
						lastItems.Pop();
					}

					if (lastItems.Count != 0 && lastItems.Peek().IndentLevel < item.IndentLevel) {
						item.ParentClasses.Add(lastItems.Peek());
					}

					lastItems.Push(item);
				}
			}

			foreach (var i in Doc.Lists[1].Items) {
				var (property, references) = GetAnnotationPropertyFrom(i.Text);

				if (property == null) {
					return new DataResult(true, $"Ошибка при анализе строки \"{i.Text}\"");
				}

				dataResult.AnnotationProperties.Add(property);

				foreach (var j in references) {
					dataResult.Classes.Where(x => x.IriAddress == j.Item1).Single().AnnotationProperties.Add(property, j.Item2);
				}
			}

			return dataResult;
		}

		public static Class GetClassFrom(string expression)
		{
			var pattern = new Regex(@"^[\w\d ]+ \[[\w\d-]+\]$");
			var name = new Regex(@"^[\w\d ]+(?!\[)");
			var iriAddress = new Regex(@"(?<=\[)[\w\d-]+(?=\])");

			if (!pattern.IsMatch(expression))
				return null;

			return new Class {
				IriAddress = iriAddress.Match(expression).Value,
				Name = name.Match(expression).Value
			};
		}

		public static (AnnotationProperty, List<(string, string)>) GetAnnotationPropertyFrom(string expression)
		{
			var pattern = new Regex(@"^[\w\d ]+ \[[\w\d-]+\]( \[[\w\d-]+ \[[\w\d- ]+\]\])*$");
			var name = new Regex(@"^[\w\d ]+(?!\[)");
			var iriAddress = new Regex(@"(?<=\[)[\w\d-]+(?=\])");
			var referenceIriAddress = new Regex(@"(?<=\[)[\w\d-]+(?= \[[\w\d- ]+\]\])");
			var referenceValue = new Regex(@"(?<=\[[\w\d- ]+ \[)[\w\d- ]+(?=\])");

			if (!pattern.IsMatch(expression)) {
				return (null, null);
			}

			var annotationProperty = new AnnotationProperty {
				IriAddress = iriAddress.Match(expression).Value,
				Name = name.Match(expression).Value
			};

			var list = new List<(string, string)>();

			foreach (var i in referenceIriAddress.Matches(expression)) {
				list.Add((i.ToString(), null));
			}

			var n = 0;

			foreach (var i in referenceValue.Matches(expression)) {
				list[n] = (list[n].Item1, i.ToString());
				++n;
			};

			return (annotationProperty, list);
		}
	}
}
