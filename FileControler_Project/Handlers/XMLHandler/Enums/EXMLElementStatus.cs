using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileControler_Project.Handlers.XMLHandler.Classes
{
    public enum EXMLElementStatus : int
    {
        PartialValid,   // Some Child elements missing bit still valid for Auditing
        PartialDump,    // Partial not usable
        Valid,          // All child elements present
        Overflow,       // More child emenets than considered
        Fail            // Total wrong structure or duplicates (e.g. multiple <SAT> tags in one <STAVKA>)
    }
}