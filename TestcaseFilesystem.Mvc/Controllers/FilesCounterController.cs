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
		//call to ROOT
		//if it's root directory, we shouldn't count files
		public IHttpActionResult Get()
		{
			return Ok();
		}

		//call to other directories
		public IHttpActionResult Get(string path) 
		{
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
