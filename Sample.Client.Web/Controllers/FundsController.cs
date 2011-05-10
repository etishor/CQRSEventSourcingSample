using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StorageAccess;
using NanoMessageBus;
using Sample.ReadModel.Funds;
using Sample.Messages.Commands.Funds;
using Sample.Client.Web.Models;
using Sample.ReadModel;

namespace Sample.Client.Web.Controllers
{
    public class FundsController : Controller
    {
        private readonly IQueryStorage store;
        private readonly ISendMessages bus;

        public FundsController(IQueryStorage store, ISendMessages bus)
        {
            this.store = store;
            this.bus = bus;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Documents()
        {
            return View(store.Items<Document>());
        }

        [HttpGet]
        public ActionResult Shares()
        {
            return View(store.Items<ShareClass>());
        }

        [HttpGet]
        public ActionResult CreateDocument()
        {
            return View( new Document(Guid.NewGuid()));
        }

        [HttpPost]
        public ActionResult CreateDocument(Document document)
        {
            bus.Send(new CreateDocument(Guid.NewGuid(), document.AccessionNumber));

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateShare()
        {
            return View(new ShareClass(Guid.NewGuid()));
        }

        [HttpPost]
        public ActionResult CreateShare(ShareClass share)
        {
            if (share.Type != "Open" && share.Type != "Closed")
            {
                this.ModelState.AddModelError("Type", "Type must be Open or Closed");
                return View(share);
            }

            bus.Send(new CreateShareClass(Guid.NewGuid(), share.Ticker, share.Type));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Document(Guid id)
        {
            DocumentViewModel model = new DocumentViewModel
            {
                Document = store.Load<Document>(id),
                AllShares = store.Items<ShareClass>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AssociateShareClass(Guid share, Guid document)
        {
            ShareClass shareClass = store.Load<ShareClass>(share);
            Document doc = store.Load<Document>(document);

            bus.Send(new AssociateShareClassToDocument(document, share, shareClass.Type));
            return RedirectToAction("Document", new { id = document });   
        }
    }
}
