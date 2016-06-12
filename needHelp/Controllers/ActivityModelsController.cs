using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using needHelp.Models;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace needHelp.Controllers
{
    public class ActivityModelsController : Controller
    {
        private GeneralModel db = new GeneralModel();

        // GET: ActivityModels
        public ActionResult Index()
        {
            try
            {
                bool role = User.IsInRole(roleType.admin.ToString());
            }
            catch (Exception ex)
            {

                throw;
            }

            /*
            string user_name = User.Identity.Name;
            if (user_name != String.Empty)
            {
                var result = from s in db.Users
                             where s.UserName.Contains(user_name)
                             select s;
                List<Microsoft.AspNet.Identity.EntityFramework.IdentityUser> users =  result.ToList();
                
                if (users != null && users.Count > 0)
                {
                    string id = users.First().Id;
                    var result = from s in db.
                             where s.UserName.Contains(user_name)
                             select s;
                }
            }
            db.Users
                */
            var activities = db.activities.Include(a => a.city);

            ViewBag.cityId = new SelectList(db.cities, "id", "name");
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name");
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName");
            return View(activities.ToList());
        }

        // GET: ActivityModels/Details/5
        public ActionResult Details(int? id,int? partial)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels activityModels = db.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }

            if (partial == null)
            {
                return View(activityModels);
            }
            else
            {
                return PartialView(activityModels);
            }
        }

        // GET: ActivityModels/Create
        public ActionResult Create()
        {
            ViewBag.cityId = new SelectList(db.cities, "id", "name");
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name");
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName");
            return View();
        }

        // POST: ActivityModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,organizationId,address,cityId,date,typeId,description")] ActivityModels activityModels)
        {
            if (ModelState.IsValid)
            {
                OrganizationModels current_org;
                if (User.IsInRole(roleType.organization.ToString()))
                {
                    current_org = db.organizations.FirstOrDefault(i => i.email.Equals(User.Identity.Name));
                }
                else
                {
                    current_org = db.organizations.Find(activityModels.organizationId);
                }

                activityModels.org = current_org;
                activityModels.city = db.cities.Find(activityModels.cityId);
                activityModels.type = db.help_types.Find(activityModels.typeId);
                db.activities.Add(activityModels);
                db.SaveChanges();

                if (User.IsInRole(roleType.organization.ToString()))
                {
                    return RedirectToAction("Index", "ActivityManagement");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.cityId = new SelectList(db.cities, "id", "name", activityModels.cityId);
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name", activityModels.organizationId);
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName", activityModels.typeId);

            return View(activityModels);
        }

        // GET: ActivityModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels activityModels = db.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.cityId = new SelectList(db.cities, "id", "name", activityModels.cityId);
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name", activityModels.organizationId);
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // POST: ActivityModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,organizationId,address,cityId,date,typeId,description")] ActivityModels activityModels)
        {
            if (User.IsInRole(roleType.organization.ToString()))
            {
                activityModels.org = db.organizations.FirstOrDefault(i => i.email.Equals(User.Identity.Name));
            }
            else
            {
                activityModels.org = db.organizations.Find(activityModels.organizationId);
            }

            if (ModelState.IsValid)
            {
                activityModels.type = db.help_types.Find(activityModels.typeId);
                db.Entry(activityModels).State = EntityState.Modified;
                db.SaveChanges();

                if (User.IsInRole(roleType.organization.ToString()))
                {
                    return RedirectToAction("Index", "ActivityManagement");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.cityId = new SelectList(db.cities, "id", "name", activityModels.cityId);
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name", activityModels.organizationId);
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName", activityModels.typeId);
            return View(activityModels);
        }

        // GET: ActivityModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels activityModels = db.activities.Find(id);
            if (activityModels == null)
            {
                return HttpNotFound();
            }
            return View(activityModels);
        }

        // POST: ActivityModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActivityModels activityModels = db.activities.Find(id);
            db.activities.Remove(activityModels);
            db.SaveChanges();

            if (User.IsInRole(roleType.organization.ToString()))
            {
                return RedirectToAction("Index", "ActivityManagement");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult SearchActivities()
        {
            string activityName = Request["txtActivityName"].ToString();
            int organizationID = 0, typeID = 0, cityID = 0;
            DateTime defaultDate = new DateTime(1777,1,1);
            DateTime startDate = defaultDate;
            DateTime endDate = defaultDate;

            string req = Request["organizationId"];
            if (req != null && req != String.Empty)
            {
               organizationID = Int32.Parse(req.ToString());
            }
            req = Request["typeId"];
            if (req != null && req != String.Empty)
            {
               typeID = Int32.Parse(req.ToString());
            }
            req = Request["cityId"];
            if (req != null && req != String.Empty)
            {
                cityID = Int32.Parse(req.ToString());
            }

            req = Request["txtStartDate"];
            if (req != null && req != String.Empty)
            {
                DateTime temp = new DateTime();
                if (DateTime.TryParse(req.ToString(), out temp))
                {
                    startDate = temp;
                }
            }

            req = Request["txtEndDate"];
            if (req != null && req != String.Empty)
            {
                DateTime temp = new DateTime();
                if (DateTime.TryParse(req.ToString(),out temp))
                {
                    endDate = temp;
                }
            }
           
            var result = from s in db.activities
                         where s.name.Contains(activityName) &&
                         (cityID == 0 || s.cityId == cityID) &&
                         (typeID == 0 || s.typeId == typeID) &&
                         (organizationID == 0 || s.organizationId == organizationID) &&
                         (startDate.Equals(defaultDate) || startDate <= s.date) &&
                         (endDate.Equals(defaultDate) || endDate >= s.date)
                         select s;

            //return RedirectToAction("Index","Series");
            ViewBag.cityId = new SelectList(db.cities, "id", "name");
            ViewBag.organizationId = new SelectList(db.organizations, "id", "name");
            ViewBag.typeId = new SelectList(db.help_types, "id", "typeName");

            //Save the user search data for the learning-algorithem

            if (User.Identity.IsAuthenticated && db.volunteers.Any() && db.volunteers.First(user => user.email.Equals(User.Identity.Name)) != null)
            {
                UserSearchDataModels userSearchData = new UserSearchDataModels();
                userSearchData.typeId = (typeID == 0 ? null : (int?)typeID); // cant case DBNull.Value
                userSearchData.cityId = (cityID == 0 ? null : (int?)cityID);
                userSearchData.startDate = (startDate == defaultDate ? null : (DateTime?)startDate);
                userSearchData.endDate = (endDate == defaultDate ? null : (DateTime?)endDate);
                userSearchData.searchDate = DateTime.Now;
                userSearchData.organizationId = (organizationID == 0 ? null : (int?)organizationID);
                userSearchData.VolunteerId = db.volunteers.First(user => user.email.Equals(User.Identity.Name)).id;
                db.search_data.Add(userSearchData);
                db.SaveChanges();
            }

            return View("Index", result.ToList());
        }

        public ActionResult SignInActivity(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ActivityModels newActivity = db.activities.Find(id);
            if (newActivity == null)
            {
                return HttpNotFound();
            }

            IQueryable<VolunteerModels> volunteers = from vol in db.volunteers
                                        where vol.email == User.Identity.Name
                                        select vol;

            VolunteerModels volunteer = volunteers.First();

            bool isAlreadySignIn = false;
            bool isDateAlert = false;

            bool needToRenewRequest = false;
            int needToRenewReqActId = 0;
            int needToRenewReqVolId = 0;

            foreach (UserRequestModels message in volunteer.messages) {
                if (id == message.activityId)
                {
                    if (message.isDeletedByUser && !message.isDeletedByOrganization)
                    {
                        needToRenewRequest = true;
                        needToRenewReqActId = message.activityId;
                        needToRenewReqVolId = message.volunteerId;
                    }
                    else 
                    {
                       isAlreadySignIn = true;
                    }
                }
                else if (message.activity.date.Date.Equals(newActivity.date.Date))
                {
                    isDateAlert = true;
                }
            }

            if(needToRenewRequest)
            {
                UserRequestModels requestMsg = db.user_requests.Find(needToRenewReqVolId,needToRenewReqActId);
                if (requestMsg == null)
                {
                    return HttpNotFound();
                }

                requestMsg.isDeletedByUser = false;
                db.Entry(requestMsg).State = EntityState.Modified;
                db.SaveChanges();
            }

            // SignIn to new activity
            if (!isAlreadySignIn && !needToRenewRequest)
            {
                // check if volunteer is confirmed by the organization of new activity, if not send a request to organization
                TrustedUserModels trusted = new TrustedUserModels();
                trusted.volunteerId = volunteer.id;
                trusted.organizationId = newActivity.organizationId;

                // check if the volunteer is confirmed
                if (newActivity.org.trustedUsers.Contains(trusted))
                {
                    // automatic confirm request by organization
                    // create userRequest - isAccepted = false, isAnswered = true
                    UserRequestModels volunteerRequest = new UserRequestModels();
                    volunteerRequest.volunteerId = volunteer.id;
                    volunteerRequest.activityId = newActivity.id;
                    volunteerRequest.isAccepted = true;
                    volunteerRequest.isAnswered = false;
                    volunteerRequest.isDeletedByUser = false;
                    volunteerRequest.isDeletedByOrganization = false;

                    db.user_requests.Add(volunteerRequest);

                    volunteer.registered_activities.Add(newActivity);
                }
                else // need to send a request to organization
                {
                    UserRequestModels volunteerRequest = new UserRequestModels();
                    volunteerRequest.volunteerId = volunteer.id;
                    volunteerRequest.activityId = newActivity.id;
                    volunteerRequest.isAccepted = false;
                    volunteerRequest.isAnswered = false;
                    volunteerRequest.isDeletedByUser = false;
                    volunteerRequest.isDeletedByOrganization = false;

                    db.user_requests.Add(volunteerRequest);
                    //volunteer.messages.Add(volunteerRequest);
                }
            }

            TempData["isAlreadySignIn"] = isAlreadySignIn;
            TempData["isDateAlert"] = isDateAlert;

            db.SaveChanges();
            return RedirectToAction("Index","ProfileModels");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public HttpResponseMessage Get()
        {
             var result = db.activities.Include(a => a.city).ToList();
             HttpResponseMessage msg = new HttpResponseMessage();
             msg.Content = new ObjectContent<object>(result, new System.Net.Http.Formatting.JsonMediaTypeFormatter());



             msg.StatusCode = HttpStatusCode.OK;
             return msg;
        }
    }
}
