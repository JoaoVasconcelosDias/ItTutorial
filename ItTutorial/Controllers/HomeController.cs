using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItTutorial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace ItTutorial.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize (Roles = "Admin")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Certification()
        {
            ViewData["Message"] = "Your Certification page.";

            return View();
        }

        public IActionResult Consola()
        {
            ViewData["Message"] = "Your Consola page.";

            return View();
        }

        //metodo
        public class CSharpScriptEngine
        {
            private static ScriptState<object> scriptState = null;
            public static object Execute(string code)
            {
                scriptState = scriptState == null ? CSharpScript.RunAsync(code).Result : scriptState.ContinueWithAsync(code).Result;
                if (scriptState.ReturnValue != null && !string.IsNullOrEmpty(scriptState.ReturnValue.ToString()))
                    return scriptState.ReturnValue;
                return null;
            }
        }

        [HttpPost]
        public IActionResult Consola(string FirstCompile)
        {           
            object resultado = CSharpScriptEngine.Execute(FirstCompile);                              
            return View(CSharpScriptEngine.Execute("new MyClass().Result"));                    
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
