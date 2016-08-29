using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestcaseFilesystem.Classes;

namespace TestcaseFilesystem.Mvc.Controllers
{
    public class FilesCounterController : ApiController
    {
		public IHttpActionResult Get(string path) 
		{
			//if it's root directory, we shouldn't count files
			if (String.IsNullOrWhiteSpace(path)) 
			{
				return Ok();
			}

			try 
			{
				CounterResult result = DirectoryFilesCounter.CountFiles(path);
				return Json(result);
			}
			catch (Exception e) 
			{
				return BadRequest(e.Message);
			}
		}
    }
}
