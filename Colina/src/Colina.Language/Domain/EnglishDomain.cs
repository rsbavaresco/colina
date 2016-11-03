using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Language.Domain
{
    /// <summary>
    /// Fake Data Class
    /// </summary>
    public static class EnglishDomain
    {
        public static IReadOnlyList<string> AvaiableCommands => new List<string>()
        {
            "Insert", "Remove", "Move"
        };

        public static IReadOnlyList<string> AvaiablePaletteObjects => new List<string>()
        {
            "Table", "Chair", "Bed", "Shelf", "Book", "Desk",
            "Chandelier", "Picture-frame", "Computer", "Lampshade", "Nightstand"
        }.Select(e => e.ToLower()).ToList();

        public static IReadOnlyList<Guid> AvaiablePaletteObjectsIds => new List<Guid>()
        {
            Guid.Parse("338cf86d-1df2-463e-bdfe-9854242167b3"),
            Guid.Parse("6b0747d1-9efc-43d2-9c7f-8db33cd01ab7"),
            Guid.Parse("d138ee6d-2c32-4020-b8af-b7dd8fc5ebba"),
            Guid.Parse("8c323c4c-3944-4d1c-8b50-e73d4585fad7"),
            Guid.Parse("62d7761f-ce42-40f8-ad8b-4d8c5abdb713"),
            Guid.Parse("4b4855db-fd01-4ef9-b359-1358c0f851b4"),
            Guid.Parse("335b1732-fe6c-480e-87df-fe43abdc430e"),
            Guid.Parse("e7c730e9-aa80-4c76-9a9c-51e7c6b01e66"),
            Guid.Parse("e4919660-5fe8-4dd7-b882-48b91855fc85"),
            Guid.Parse("25d56a96-a5ab-4e9e-b27d-297528b842ae"),
            Guid.Parse("851b5507-6638-4b51-967b-9305ad33b936")
        };

        public static IReadOnlyList<string> AvaiableAbsolutePositions => new List<string>()
        {
            "One", "Two", "Three", "Four", "Five", "Six"
        };

        public static IReadOnlyList<string> AvaiableRelativeUnityPositions => new List<string>()
        {
            "Pixel", "Centimeter"
        };
    }
}
