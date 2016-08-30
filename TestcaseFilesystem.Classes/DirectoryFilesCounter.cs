using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TestcaseFilesystem.Classes
{
	public static class DirectoryFilesCounter
	{
		static private CounterResult CountFilesInDirectory(DirectoryInfo directory)
		{
			var files = directory.GetFiles();
			var counterResult = new CounterResult();
			var megabyte = Megabyte.Value;
			
			counterResult.Small = files.Count(f => f.Length <= 10 * megabyte);
			counterResult.Medium = files.Count(f => f.Length > 10 * megabyte && f.Length <= 50 * megabyte);
			counterResult.Large = files.Count(f => f.Length >= 100 * megabyte);

			return counterResult;
		}

		static public CounterResult CountFiles(string path)
		{
			var directory = new DirectoryInfo(path);
			if (!directory.Exists) 
			{
				throw new DirectoryNotFoundException();
			}

			//there may be exception, if junction point is passed
			CounterResult counterResult;
			try 
			{
				counterResult = CountFilesInDirectory(directory);
			}
			catch 
			{
				//let Controller handle it
				throw;
			}

			var directories = directory.GetDirectories();
			foreach (var dir in directories)
			{
				try 
				{
					counterResult += CountFilesInDirectory(dir);
				}
				catch (UnauthorizedAccessException) 
				{
					//may be out IEnumerable<string> parameter for gathering folders where access is denied
				}
			}

			return counterResult;
		}
	}
}