﻿using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class MessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
        // GET: Message

        [Authorize]
        public ActionResult InBox(string p)
        {
            var messagelist = mm.GetListInbox(p);
            return View(messagelist);
        } 
        public ActionResult SendBox(string p)
        {
            var messagelist = mm.GetListSendBox(p);
            return View(messagelist);
        }
        public ActionResult GetInBoxMessageDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("InBox");
            }

            var values = mm.GetByID(id.Value);
            return View(values);
        }
        public ActionResult GetSendBoxMessageDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("SendBox"); 
            }

            var values = mm.GetByID(id.Value);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            ValidationResult results = messagevalidator.Validate(p);
            if (results.IsValid)
            {
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());

                mm.MessageAdd(p);
                return RedirectToAction("SendBox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

    }
}