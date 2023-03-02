using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Controllers
{
    public class ProjectsController : ApiController
    {
        private ProjectDBEntities db = new ProjectDBEntities();

        // GET: api/Projects
        [HttpGet]
        [Route("api/GetAllProjects")]
        public List<Project> GetAllSanpham()
        {
            return db.Projects.ToList();
        }

        // GET: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ProjectID)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Projects
        [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(project);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = project.ProjectID }, project);
        }

        // DELETE: api/Projects/5
        //[ResponseType(typeof(Project))]
        //public IHttpActionResult DeleteProject(int id)
        //{
        //    Project project = db.Projects.Find(id);
        //    if (project == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Projects.Remove(project);
        //    db.SaveChanges();

        //    return Ok(project);
        //}
        [HttpGet]
        [Route("api/Projects/DeleteProject/{id}")]
        public List<Project> DeleteProject(int id)
        {
            db.Projects.Remove(db.Projects.FirstOrDefault(e => e.ProjectID == id));
            db.SaveChanges();
            return db.Projects.ToList();

        }
        [HttpGet]
        [Route("api/Projects/Search")]
        public List<Project> Search(string ProjectName)
        {
            using (ProjectDBEntities db = new ProjectDBEntities())
            {
                try
                {
                    return db.Projects.Where(e => e.ProjectName.Contains(ProjectName)).ToList();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.ProjectID == id) > 0;
        }
    }
}