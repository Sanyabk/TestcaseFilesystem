using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestcaseFilesystem.Classes
{
	public class DirectoryContentGetter
	{
		private readonly DirectoryInfo _directory;

		public DirectoryContentGetter(string path)
		{
			_directory = new DirectoryInfo(path);
			if (!_directory.Exists)
			{
				throw new DirectoryNotFoundException("Directory doesn't exist");
			}
		}

		public IEnumerable<DirectoryInfo> GetDirectories() 
		{
			return _directory.GetDirectories();
		}

		public IEnumerable<FileInfo> GetFiles()
		{
			return _directory.GetFiles();						 
		}

		public string GetParentPath()
		{
			return _directory.Parent == null ? "" : _directory.Parent.FullName;
		}
	}
}
