using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchemaConverter
{
	public partial class Form1 : Form
	{
		private static string PathToDoc { get; set; } = "";

		public Form1()
		{
			InitializeComponent();
		}

		private void OpenDocx_Click(object sender, EventArgs e)
		{
			var openFileDialog = new OpenFileDialog {
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				PathToDoc = openFileDialog.FileName;
				StatusTextBox.Text = $"Открыт файл: {PathToDoc}";
			}
			else {
				PathToDoc = "";
				StatusTextBox.Text = $"Программа готова к работе";
			}

			openFileDialog.Dispose();
		}

		private void SaveXml_Click(object sender, EventArgs e)
		{
			if (PathToDoc == "") {
				MessageBox.Show(
					$"Не выбран файл для конвертации. Для начала необходимо выбрать DOCX-файл с данными нажав на кнопку \"{OpenDocx.Text}\"",
					"Ошибка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
				return;
			}

			try {
				var dataResult = new DocxParser(PathToDoc).Parse();

				if (dataResult.OccurException) {
					MessageBox.Show(
						dataResult.ExceptionMessage,
						"Ошибка",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning
					);
					return;
				}

				var saveFileDialog = new SaveFileDialog {
					InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
					DefaultExt = "owl",
					Filter = "OWL-файл (*.owl)|*.owl"
				};

				if (saveFileDialog.ShowDialog() == DialogResult.OK) {
					new XmlCreator(dataResult).Create().Save(saveFileDialog.FileName);
				}
			} catch (IOException ex) {
				MessageBox.Show(
					ex.Message,
					"Ошибка",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
			}
		}
	}
}
