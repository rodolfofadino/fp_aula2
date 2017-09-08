using System.Collections.Generic;

namespace Fiap2.Core
{
    public interface IPhotoService
    {
        List<Photo> List();
        List<Photo> List(string category);
    }
}