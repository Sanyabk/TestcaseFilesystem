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
		//call to ROOT directory
		public IHttpActionResult Get() 
		{
			var directories = Directory.GetLogicalDrives()
								.Select(d => new DirectoryItem {
									Name = d,
									FullPath = d
								}).ToList();

			return Json(directories);
		}

		//call to other directories, with path
		public IHttpActionResult Get(string path) 
		{
			try 
			{
				DirectoryContentGetter getter = new DirectoryContentGetter(path);
				var directories = getter.GetDirectories()
								.Select(d => new DirectoryItem
								{
									Name = d.Name,
									FullPath = d.FullName
								}).ToList();

				//adding DirectoryItem that leads to previous directory
				var parentPath = getter.GetParentPath();
				directories.Insert(0, (new DirectoryItem { Name = "..", FullPath = parentPath })); 
				return Json(directories);
			}
			catch (Exception e) 
			{
				return BadRequest(e.Message);
			}			  
		}
    }
}
