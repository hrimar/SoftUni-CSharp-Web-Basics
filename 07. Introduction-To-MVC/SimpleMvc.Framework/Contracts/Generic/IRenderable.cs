namespace SimpleMvc.Framework.Contracts.Generic
{
    public interface IRenderable<T> : IRenderable
        //where TModel : class
    {
        T Model { get; set; }
    }
}
