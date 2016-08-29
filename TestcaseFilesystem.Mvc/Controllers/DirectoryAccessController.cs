using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestcaseFilesystem.Mvc.Controllers
{
    public class DirectoryAccessController : ApiController
    {
		public IHttpActionResult Get(string path) 
		{
			if (!String.IsNullOrWhiteSpace(path)) 
			{
				try 
				{
					DirectoryInfo directory = new DirectoryInfo(path);
					directory.GetDirectories();
					directory.GetFiles();
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
