using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyJobBoard.DATA.EF;
using Microsoft.AspNet.Identity;
using System.Drawing;
using StoreFrontLab.UI.Utilities;

namespace MyJobBoard.UI.MVC.Controllers
{
    public class UserDetailsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: UserDetails
        
        public ActionResult Index()
        {
            return View(db.UserDetails.ToList());
        }

        public ActionResult UserProfile()
        {
            var currentUser = User.Identity.GetUserId();
            var userProfile = db.UserDetails.Where(x => x.UserId == currentUser);
            return View(userProfile.ToList());
        }

        // GET: UserDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        [HttpGet]
        public PartialViewResult AjaxDetails(int id)
        {
            UserDetail ud = db.UserDetails.Find(id);
            return PartialView(ud);
        }

        // GET: UserDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,ResumeFileName,ProfilePicture")] UserDetail userDetail, HttpPostedFileBase resume, HttpPostedFileBase profilePicture)
        {
            if (ModelState.IsValid)
            {
                string file = " ";
                string picFile = "noImage.png";
                if (resume != null)
                {
                    file = resume.FileName;
                    string ext = file.Substring(file.LastIndexOf("."));
                    string[] goodExts = { ".pdf" };
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        file = Guid.NewGuid() + ext;
                        string savePath = Server.MapPath("~/Content/resumes/");
                    }
                    else
                    {
                        file = null;
                    }
                    userDetail.ResumeFileName = file;
                }
                    if (profilePicture != null)
                    {
                        picFile = profilePicture.FileName;
                        string picExt = file.Substring(file.LastIndexOf("."));
                        string[] goodPicExts = { ".jpg", ".jpeg", ".png", };
                        if (goodPicExts.Contains(picExt.ToLower()) && profilePicture.ContentLength <= 4194304)
                        {
                            picFile = Guid.NewGuid() + picExt;

                            string picSavePath = Server.MapPath("~/Content/profilePictures/");
                            Image convertedImage = Image.FromStream(profilePicture.InputStream);
                            int maxImageSize = 500;
                            int maxThumbSize = 100;
                            ImageServices.ResizeImage(picSavePath, picFile, convertedImage, maxImageSize, maxThumbSize);
                        }
                        else
                        {
                            picFile = "noImage.png";
                        }
                    userDetail.ProfilePicture = picFile;
                    
                    }



                db.UserDetails.Add(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDetail);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,ResumeFileName,ProfilePicture")] UserDetail userDetail, HttpPostedFileBase resume, HttpPostedFileBase profilePicture)
        {
            if (ModelState.IsValid)
            {
                
                string file = " ";
                if (resume != null)
                {
                    file = resume.FileName;
                    string ext = file.Substring(file.LastIndexOf("."));
                    string[] goodExts = { ".pdf" };
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        file = Guid.NewGuid() + ext;
                        string savePath = Server.MapPath("~/Content/resumes/");
                        if (userDetail.ResumeFileName != null && userDetail.ResumeFileName != " ")
                        {
                            resume.SaveAs(Server.MapPath("~/Content/resumes/"+file));
                        }

                    }
                    userDetail.ResumeFileName = file;
                }

                string picFile = "noImage.png";
                if (profilePicture != null)
                {
                    picFile = profilePicture.FileName;
                    string picExt = picFile.Substring(picFile.LastIndexOf("."));
                    string[] goodPicExts = { ".jpg", ".jpeg", ".png" };
                    if (goodPicExts.Contains(picExt.ToLower()) && profilePicture.ContentLength <= 4194304)
                    {
                        picFile = Guid.NewGuid() + picExt;

                        string picSavePath = Server.MapPath("~/Content/profilePictures/");
                        Image convertedImage = Image.FromStream(profilePicture.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 200;
                        ImageServices.ResizeImage(picSavePath, picFile, convertedImage, maxImageSize, maxThumbSize);
                    }


                    userDetail.ProfilePicture = picFile;
                }

                
                
                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetail);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
