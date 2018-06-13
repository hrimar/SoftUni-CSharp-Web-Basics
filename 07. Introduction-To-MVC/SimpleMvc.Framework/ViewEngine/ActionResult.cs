namespace SimpleMvc.Framework.ViewEngine
{
    using Contracts;
    using System;

    // Returning HTML
    public class ActionResult : IActionResult
    {
        public ActionResult(string viewFullQualifiedName)
        {
            //this.Action = (IRenderable)Activator
            //              .CreateInstance(Type.GetType(viewFullQualifiedName));

            Type viewType = Type.GetType(viewFullQualifiedName);
            this.Action = (IRenderable)Activator.CreateInstance(viewType);
        }

        public IRenderable Action { get; set; }

        public string Invoke() => this.Action.Render();
    }
}
