using System.Collections.Generic;

namespace FDMCats.Services.Contracts
{
    public interface IKittenBusinessService
    {
        bool Add(string name, int age, string breed);

        List<string> All();
    }
}
