using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestcaseFilesystem.Classes;

namespace TestcaseFilesystem.Mvc.Controllers
{
    public class DirectoryAccessController : ApiController
    {
		public IHttpActionResult Get(string path) 
		{
			// if it isn't call to ROOT directory, we should check posibilities 
			// of getting directories and folders
			if (!String.IsNullOrWhiteSpace(path)) 
			{
				try 
				{
					DirectoryContentGetter getter = new DirectoryContentGetter(path);
					getter.GetDirectories();
					getter.GetFiles();
				}
				catch (Exception e)
				{
					return BadRequest(e.Message);
				}
			}
			return Ok();
		}
    }
}
