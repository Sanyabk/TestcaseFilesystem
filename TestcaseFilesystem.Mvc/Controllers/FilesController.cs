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
    public class FilesController : ApiController
    {
		//it's request to the ROOT, List of Logical Drives should be the only response,
		//there are no files
		public IHttpActionResult Get()
		{
			return Ok();
		}

		//request to other directories, with path
		public IHttpActionResult Get(string path) 
		{
			try 
			{
				DirectoryContentGetter getter = new DirectoryContentGetter(path);
				double megabyte = Megabyte.Value;

				var files = getter.GetFiles()
								.Select(f => new FileItem
								{
									Name = f.Name,
									FullPath = f.FullName,
									Length = Double.Parse(String.Format("{0:F3}",f.Length / megabyte))
								}).ToList();
				return Json(files);
			}
			catch (Exception e) 
			{
				return BadRequest(e.Message);
			}
		}
    }
}
