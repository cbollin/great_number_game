using Nancy;
using System;
using System.Collections.Generic;
namespace GNG1
{
    public class GNGModule : NancyModule
    {
        public GNGModule()
        {
            Get("/", args =>
            {
                ViewBag.showForm = true;
                ViewBag.tooHigh = false;
                ViewBag.tooLow = false;
                ViewBag.correct = false;
                if(Session["num"] == null){
                    int num = new Random().Next(1,101);
                    Session["num"] = num;
                    Console.WriteLine(num);
                }
                return View["GNG.sshtml"];
            });


            Post("/formsubmitted", args =>
            {
                int guess = Request.Form["guess"];
                int num = (int)Session["num"];

                if( guess < num){
                    ViewBag.tooLow = true;
                    ViewBag.showForm = true;
                    Console.WriteLine("Too Low");
                    return View["GNG.sshtml"];
                }
                else if( guess > num){
                    ViewBag.tooHigh = true;
                    ViewBag.showForm = true;
                    Console.WriteLine("Too High");
                    return View["GNG.sshtml"];
                }
                else if( guess == num){
                    ViewBag.correct = true;
                    ViewBag.showForm = false;
                    Console.WriteLine("Correct");
                    return View["GNG.sshtml"];
                }
                return Response.AsRedirect("/");
            });

            Post("/clear", args =>
            {
                Session.DeleteAll();
                return Response.AsRedirect("/");
            });
        }
    }
}