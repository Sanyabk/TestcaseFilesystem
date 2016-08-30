using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TestcaseFilesystem.Classes;

namespace TestcaseFilesystem.Mvc.Controllers
{
    public class DirectoriesController : ApiController
    {
		public IHttpActionResult Get(string path) 
		{
			List<DirectoryItem> directories = null;

			//empty path - is call to ROOT directory
			if (String.IsNullOrWhiteSpace(path))
			{
				directories = Directory.GetLogicalDrives()
								.Select(d => new DirectoryItem
								{
									Name = d,
									FullPath = d
								}).ToList();
			}
			else 
			{
				try 
				{
					DirectoryContentGetter getter = new DirectoryContentGetter(path);
					directories = getter.GetDirectories()
									.Select(d => new DirectoryItem
									{
										Name = d.Name,
										FullPath = d.FullName
									}).ToList();

					//adding DirectoryItem that leads to previous directory
					var parentPath = getter.GetParentPath();
					directories.Insert(0, (new DirectoryItem { Name = "..", FullPath = parentPath })); 
				}
				catch (Exception e) 
				{
					return BadRequest(e.Message);
				}
			}			  
			return Json(directories);
		}
    }
}
